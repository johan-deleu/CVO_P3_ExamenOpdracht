using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Bibliotheek.DataModel;
using Bibliotheek.BusinessLogic;

namespace BibliotheekWebApi.Service.Controllers
{
    public class GenresController : ApiController
    {
        private GenreLogica _genreLogica = new GenreLogica();

        public async Task<IEnumerable<Genre>> Get()
        {
            return await _genreLogica.OphalenGenresAsync();
        }

        public async Task<Genre> Get(Int32 code)
        {
            return await _genreLogica.OphalenGenreAsync(code);
        }

        public async Task<Int32?> Post([FromBody]Genre genre)
        {
            return await _genreLogica.ToevoegenGenreAsync(genre);
        }

        public async Task<Int32?> Put(Int32 code,[FromBody]Genre genre)
        {
            return await _genreLogica.WijzigenGenreAsync(code, genre);
        }

        public async Task<Int32?> Delete(Int32 code)
        {
            return await _genreLogica.VerwijderenGenreAsync(code);
            //De return value lijkt niet te werken
        }


    }
}
