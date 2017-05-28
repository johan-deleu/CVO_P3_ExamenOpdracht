using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using Bibliotheek.DataModel;
using Bibliotheek.Service;

namespace Bibliotheek.Client
{
    class GenreWebApiClient : IGenreService
    {
        private RestClient _webApiClient = new RestClient("http://localhost:9481/");

        public async Task<Genre> OphalenGenreAsync(Genre genre)
        {
            var webApiRequest = new RestRequest("api/Genres/"+genre.Code, Method.GET);
            var opgehaaldGenre =  await _webApiClient.GetTaskAsync<Genre>(webApiRequest);
            return opgehaaldGenre;
        }

        public async Task<List<Genre>> OphalenGenresAsync()
        {
            var webApiRequest = new RestRequest("api/Genres", Method.GET);
            var genreLijst = await _webApiClient.GetTaskAsync<List<Genre>>(webApiRequest);
            return genreLijst;
        }

        public async Task<int?> ToevoegenGenreAsync(Genre genre)
        {
            var webApiRequest = new RestRequest("api/Genres", Method.POST);
            webApiRequest.RequestFormat = DataFormat.Json;
            webApiRequest.AddBody(genre);
            var response = await _webApiClient.ExecuteTaskAsync<int?>(webApiRequest);
            return (int?) response.Data;

        }

        public async Task<int?> VerwijderenGenreAsync(Genre genre)
        {
            var webApiRequest = new RestRequest("api/Genres/"+genre.Code, Method.DELETE);           
            var response = await _webApiClient.ExecuteTaskAsync<int?>(webApiRequest);
            return (int?)response.Data;
        }

        public Task<List<Genre>> VerwijderenGenreLijstAsync(List<Genre> genreLijst)
        {
            throw new NotImplementedException();
        }

        public async Task<int?> WijzigenGenreAsync(Genre bestaandGenre, Genre bijgewerktGenre)
        {
            var webApiRequest = new RestRequest("api/Genres/"+bestaandGenre.Code, Method.PUT);
            webApiRequest.RequestFormat = DataFormat.Json;
            webApiRequest.AddBody(bijgewerktGenre);
            var response = await _webApiClient.ExecuteTaskAsync<int?>(webApiRequest);
            return (int?)response.Data;
        }
    }
}
