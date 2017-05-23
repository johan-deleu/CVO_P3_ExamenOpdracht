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
        public async Task<List<Boek>> OphalenBoekenAsync()
        {
            return await _boekLogica.OphalenBoekenAsync();
        }

        public async Task<Boek> OphalenBoek(Int32 code)
        {
            return await _boekLogica.OphalenBoek(code);
        }
        public async Task<Boek> OphalenBoekMetGenreAsync(Boek boek)
        {
            return await _boekLogica.OphalenBoekMetGenreAsync(boek);
        }

        public async Task<Int32?> VerwijderenBoekAsync(Boek boek)
        {
            return await _boekLogica.VerwijderenBoekAsync(boek);
        }

        public async Task<Int32> ToevoegenBoekAsync(Boek nieuwBoek)
        {
            return await _boekLogica.ToevoegenBoekAsync(nieuwBoek);
        }

        public async Task<Int32> BewerkenBoekAsync(Boek teBewerkenBoek, Boek bewerktBoek)
        {
            return await _boekLogica.BewerkenBoekAsync(teBewerkenBoek, bewerktBoek);
        }
    }
}
