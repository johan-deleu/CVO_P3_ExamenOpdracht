using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Bibliotheek.DataModel;
using Bibliotheek.BusinessLogic;

namespace Bibliotheek.Service
{
    public class BoekService : IBoekService
    {
        private readonly BoekLogica _boekLogica;

        //Deze standaard constructor is nodig om de WCF test client te kunnen opstarten
        public BoekService()
        {
            _boekLogica = new BoekLogica();
        }
        
        public BoekService(BoekLogica boekLogica)
        {
            _boekLogica = boekLogica;
        }
        //public async Task<List<Boek>> OphalenBoekenAsync()
        //{
        //    return await _boekLogica.OphalenBoekenAsync();
        //}

        public async Task<Boek[]> OphalenBoekenAsync()
        {
            List<Boek> boekenLijst = await _boekLogica.OphalenBoekenAsync();
            Boek[] boekenArray = boekenLijst.ToArray();
            return boekenArray;
        }

        public async Task<Boek> OphalenBoek(Int32 code)
        {
            return await OphalenBoek(code);
        }

        public async Task VerwijderenBoek(Int32 code)
        {
            await VerwijderenBoek(code);
            return;
        }

        public async Task<Int32> ToevoegenBoek(Boek nieuwBoek)
        {
            return await ToevoegenBoek(nieuwBoek);
        }
    }
}
