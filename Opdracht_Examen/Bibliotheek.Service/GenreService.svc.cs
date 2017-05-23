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
        public async Task<List<Genre>> OphalenGenresAsync()
        {
            return await _genreLogica.OphalenGenresAsync();
        }
        
        public Task<Genre> OphalenGenreAsync(Genre genre)
        {
            return _genreLogica.OphalenGenreAsync(genre);
        }

        public Task<Int32?> WijzigenGenreAsync(Genre bestaandGenre, Genre bijgewerktGenre)
        {
            return _genreLogica.WijzigenGenreAsync(bestaandGenre, bijgewerktGenre);
        }

        //public async Task<Int32?> VerwijderenGenreAsync(Int32 code)
        //{
        //    return await _genreLogica.VerwijderenGenreAsync(code);
        //}

        public async Task<Int32?> VerwijderenGenreAsync(Genre genre)
        {
            return await _genreLogica.VerwijderenGenreAsync(genre);
        }

        public async Task<List<Genre>> VerwijderenGenreLijstAsync(List<Genre> genreLijst)
        {
            return await _genreLogica.VerwijderenGenreLijstAsync(genreLijst);
        }

        public async Task<Int32?> ToevoegenGenreAsync(Genre genre)
        {
            return await _genreLogica.ToevoegenGenreAsync(genre);
        }
    }
}
