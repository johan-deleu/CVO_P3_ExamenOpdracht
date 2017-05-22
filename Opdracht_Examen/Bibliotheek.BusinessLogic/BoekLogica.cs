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

        public async Task<Boek> OphalenBoek(Int32 code)
        {
            return await dbAccess.OphalenBoek_async(code);
        }

        public async Task VerwijderenBoek(Int32 code)
        {
            await dbAccess.VerwijderenBoek_async(code);
            return;
        }

        public async Task<Int32> ToevoegenBoek(Boek nieuwBoek)
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
    }
}
