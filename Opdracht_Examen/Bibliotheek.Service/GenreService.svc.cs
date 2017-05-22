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
    
    public class GenreService:IGenreService
    {
        private readonly GenreLogica _genreLogica;

        
        //Deze standaard constructor is nodig om de WCF test client te kunnen opstarten
        public GenreService()
        {
            _genreLogica = new GenreLogica();
        }
        public GenreService(GenreLogica genreLogica)
        {
            _genreLogica = genreLogica;
        }
        //public Task<List<Genre>> OphalenGenres()
        public Task<Genre[]> OphalenGenres()
        {
            return _genreLogica.OphalenGenres();
        }

        public Task<Genre> OphalenGenre_async(Int32 code)
        {
            return _genreLogica.OphalenGenre_async(code);
        }

        public Task WijzigenGenre_async(Genre genre)
        {
            return _genreLogica.WijzigenGenre_async(genre);
        }

        public  Task VerwijderenGenre(Int32 code)
        {    
            return _genreLogica.VerwijderenGenre(code);
        }

        public Task ToevoegenGenre(Genre genre)
        {
            return _genreLogica.ToevoegenGenre(genre);
        }
    }
}
