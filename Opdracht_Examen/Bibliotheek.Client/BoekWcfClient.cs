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
    class BoekWcfClient : IBoekService
    {
        private IBoekService _boekWcfClient;
        public BoekWcfClient()
        {
            var binding = new BasicHttpBinding();
            var boekServiceEndpointAddress = new EndpointAddress("http://localhost:54398/BoekService.svc");
            var boekChannelFactory = new ChannelFactory<IBoekService>(binding, boekServiceEndpointAddress);
            _boekWcfClient = boekChannelFactory.CreateChannel();           
        }
        public async Task<int> BewerkenBoekAsync(Int32 code, Boek bewerktBoek)
        {
            return await _boekWcfClient.BewerkenBoekAsync(code, bewerktBoek);
        }

        public async Task<Boek> OphalenBoekAsync(Int32 code)
        {
            return await _boekWcfClient.OphalenBoekAsync(code);
        }

        public async Task<List<Boek>> OphalenBoekenAsync()
        {
            return await _boekWcfClient.OphalenBoekenAsync();
        }

        //public async Task<Boek> OphalenBoekMetGenreAsync(Boek boek)
        //{
        //    return await _boekWcfClient.OphalenBoekMetGenreAsync(boek);
        //}

        public async Task<int> ToevoegenBoekAsync(Boek nieuwBoek)
        {
            return await _boekWcfClient.ToevoegenBoekAsync(nieuwBoek);
        }

        public async Task<int?> VerwijderenBoekAsync(Boek boek)
        {
            return await _boekWcfClient.VerwijderenBoekAsync(boek);
        }
    }
}
