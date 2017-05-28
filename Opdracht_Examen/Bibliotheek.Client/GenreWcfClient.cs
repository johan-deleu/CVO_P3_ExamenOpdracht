using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Threading.Tasks;
using Bibliotheek.DataModel;
using Bibliotheek.Service;

namespace Bibliotheek.Client
{
    class GenreWcfClient:IGenreService
    {
        private IGenreService _genreWcfClient;
        public GenreWcfClient()
        {
            var binding = new BasicHttpBinding();           
            var genreServiceEndpointAddress = new EndpointAddress("http://localhost:54398/GenreService.svc");
            var genreChannelFactory = new ChannelFactory<IGenreService>(binding, genreServiceEndpointAddress);
            _genreWcfClient = genreChannelFactory.CreateChannel();
        }

        public async Task<Genre> OphalenGenreAsync(Genre genre)
        {
            return await _genreWcfClient.OphalenGenreAsync(genre);
        }

        public async Task<int?> ToevoegenGenreAsync(Genre genre)
        {
            return await _genreWcfClient.ToevoegenGenreAsync(genre);
        }

        public async Task<int?> VerwijderenGenreAsync(Genre genre)
        {
            return await _genreWcfClient.VerwijderenGenreAsync(genre);
        }

        public async Task<List<Genre>> VerwijderenGenreLijstAsync(List<Genre> genreLijst)
        {
            return await _genreWcfClient.VerwijderenGenreLijstAsync(genreLijst);
        }

        public async Task<int?> WijzigenGenreAsync(Genre bestaandGenre, Genre bijgewerktGenre)
        {
            return await _genreWcfClient.WijzigenGenreAsync(bestaandGenre,bijgewerktGenre);
        }

        public async Task<List<Genre>> OphalenGenresAsync()
        {
            return await _genreWcfClient.OphalenGenresAsync();
        }       
    }
}
