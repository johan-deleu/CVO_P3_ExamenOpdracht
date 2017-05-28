using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Bibliotheek.DataModel;
using Bibliotheek.BusinessLogic;

namespace BibliotheekWebApi.Service.Controllers
{
    public class BoekenController : ApiController
    {
        private BoekLogica _boekLogica = new BoekLogica();

        public async Task<IEnumerable<Boek>> Get()
        {
            return await _boekLogica.OphalenBoekenAsync();
            //return await _boekLogica.OphalenBoekenMetGenreAsync();
        }

        public async Task<Boek> Get(Int32 code)
        {
            //return await _boekLogica.OphalenBoekAsync(code);
            var opgehaaldBoek = await _boekLogica.OphalenBoekMetGenreAsync(code);
            return opgehaaldBoek;
        }
        //public async Task<Boek> Get(Int32 code,[FromUri]bool includeGenres)
        //{
        //    //Hier een onderscheid maken of genres al dan niet mee moeten
        //    //met genres
        //    if (includeGenres)
        //    {
        //        var opgehaaldBoek = await _boekLogica.OphalenBoekMetGenreAsync(code);
        //        return opgehaaldBoek;
        //    }
        //    //zonder genres
        //    else
        //    {
        //        return await _boekLogica.OphalenBoekAsync(code);
        //    }          
        //}
        public async Task<Int32> Post([FromBody]Boek boek)
        {
            return await _boekLogica.ToevoegenBoekAsync(boek);
        }

        public async Task<Int32?> Delete(Int32 code)
        {
            return await _boekLogica.VerwijderenBoekAsync(code);
            //De return value lijkt niet te werken
        }

        public async Task<Int32> Put(Int32 code, [FromBody]Boek boek)
        {
            return await _boekLogica.BewerkenBoekAsync(code, boek);
        }
    }
}
