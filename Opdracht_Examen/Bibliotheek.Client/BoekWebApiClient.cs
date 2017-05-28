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
    class BoekWebApiClient:IBoekService
    {
        private RestClient _webApiClient = new RestClient("http://localhost:9481/");

        public Task<int> BewerkenBoekAsync(Boek teBewerkenBoek, Boek bewerktBoek)
        {
            throw new NotImplementedException();
        }

        public async Task<Boek> OphalenBoekAsync (int code)
        {
            var webApiRequest = new RestRequest("api/Boeken/"+code, Method.GET);
            //var opgehaaldBoek = await _webApiClient.GetTaskAsync<Boek>(webApiRequest);
            //return opgehaaldBoek;
            var opgehaaldBoek = await _webApiClient.ExecuteTaskAsync<Boek>(webApiRequest);
            return opgehaaldBoek.Data;

            //Opgehaaldboek (of de data) is altijd null???
            //Geneste datastructuur lijkt probleem op te leveren
        }

        public async Task<List<Boek>> OphalenBoekenAsync()
        {
            var webApiRequest = new RestRequest("api/Boeken", Method.GET);
            var boekenLijst = await _webApiClient.GetTaskAsync<List<Boek>> (webApiRequest);
            return boekenLijst;         
        }

        //public async Task<Boek> OphalenBoekMetGenreAsync(Boek boek)
        //{
        //    var webApiRequest = new RestRequest("api/Boeken/" + boek.Code, Method.GET);
        //    //Hoe met deze request meegeven dat ook de genres moeten opgehaald worden?
            
        //    webApiRequest.AddQueryParameter("includeGenres","true");
            
        //    var opgehaaldBoek = await _webApiClient.GetTaskAsync<Boek>(webApiRequest);
        //    return opgehaaldBoek;
        //}

        public async Task<int> ToevoegenBoekAsync(Boek nieuwBoek)
        {
            var webApiRequest = new RestRequest("api/Boeken", Method.POST);
            webApiRequest.RequestFormat = DataFormat.Json;
            webApiRequest.AddBody(nieuwBoek);
            var response = await _webApiClient.ExecuteTaskAsync<int>(webApiRequest);
            return (int)response.Data;
        }

        public async Task<int?> VerwijderenBoekAsync(Boek boek)
        {
            var webApiRequest = new RestRequest("api/Boeken/" + boek.Code, Method.DELETE);
            var response = await _webApiClient.ExecuteTaskAsync<int?>(webApiRequest);
            return (int?)response.Data;
        }

        public async Task<int> BewerkenBoekAsync(Int32 code, Boek bijgewerktBoek)
        {
            var webApiRequest = new RestRequest("api/Boeken/" + code, Method.PUT);
            webApiRequest.RequestFormat = DataFormat.Json;
            webApiRequest.AddBody(bijgewerktBoek);
            var response = await _webApiClient.ExecuteTaskAsync<int?>(webApiRequest);
            return (int)response.Data;
        }
    }
}
