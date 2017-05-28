using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bibliotheek.DataModel;
using Bibliotheek.DataAccess;

namespace Bibliotheek.BusinessLogic
{
    public class BoekLogica
    {
        private readonly IDbAccess dbAccess = new DbAccessEF();
        public async Task<List<Boek>> OphalenBoekenAsync()
        {
            return await dbAccess.OphalenBoeken_async();
        }
        public async Task<List<Boek>> OphalenBoekenMetGenreAsync()
        {
            return await dbAccess.OphalenBoekenMetGenre_async();
        }
        public async Task<Boek> OphalenBoekAsync(Int32 code)
        {
            return await dbAccess.OphalenBoek_async(code);
        }

        public async Task<Boek> OphalenBoekMetGenreAsync(Boek boek)
        {
            return await dbAccess.OphalenBoekMetGenre_async(boek.Code);
        }

        public async Task<Boek> OphalenBoekMetGenreAsync(Int32 code)
        {
            return await dbAccess.OphalenBoekMetGenre_async(code);
        }

        public async Task<List<Boek>> OphalenBoekenAsync(Genre genre)
        {
            return await dbAccess.OphalenBoeken_async(genre);
        }

        public async Task<Int32?> VerwijderenBoekAsync(Boek boek)
        {
            return await dbAccess.VerwijderenBoek_async(boek.Code);
        }

        public async Task<Int32?> VerwijderenBoekAsync(Int32 code)
        {
            return await dbAccess.VerwijderenBoek_async(code);
        }

        public async Task<Int32> ToevoegenBoekAsync(Boek nieuwBoek)
        {
            //Genres moeten eerst uit de database opgehaald worden om correct aan het nieuwe boek gelinkt te worden
            //Indien 'nieuwBoek' rechtstreeks wordt toegevoegd duikt een exeption op 
            //'An entity object cannot be referenced by multiple instances of IEntityChangeTracker'

            var toeTeVoegenBoek = new Boek();
            toeTeVoegenBoek.Titel = nieuwBoek.Titel;
            toeTeVoegenBoek.Auteur = nieuwBoek.Auteur;
            toeTeVoegenBoek.Paginas = nieuwBoek.Paginas;
            ICollection<Genre> genreLijst = new List<Genre>();

            foreach (Genre genre in nieuwBoek.Genres)
            {
                genreLijst.Add(await dbAccess.OphalenGenre_async(genre.Code));
            }
            toeTeVoegenBoek.Genres = genreLijst;
            return await dbAccess.ToevoegenBoek_async(toeTeVoegenBoek);
        }

        public async Task<Int32> BewerkenBoekAsync(Int32 code, Boek bewerktBoek)
        {
            //Snelle implementatie
            await VerwijderenBoekAsync(code);
            return await ToevoegenBoekAsync(bewerktBoek);

            //betere implementatie door het boek effectief te wijzigen en de PK te behouden
        }
    }
}
