using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bibliotheek.DataModel;
using Bibliotheek.DataAccess;

namespace Bibliotheek.BusinessLogic
{
    public class GenreLogica
    {
        private readonly IDbAccess dbAccess = new DbAccessEF();
        //public async Task<List<Genre>> OphalenGenres()
        public async Task<Genre[]> OphalenGenres()
        {
            var genreLijst = await dbAccess.OphalenGenres_async();
            //return genreLijst;
            return genreLijst.ToArray() ;
            //return await dbAccess.OphalenGenres_async();
            
        }

        public async Task<Genre> OphalenGenre_async(Int32 code)
        {
            return await dbAccess.OphalenGenre_async(code);
        }

        public async Task WijzigenGenre_async(Genre genre)
        {
            await dbAccess.BijwerkenGenre_async(genre);

        }
        public async Task VerwijderenGenre(Int32 code)
        {
            //Eerst nakijken of er nog boeken naar dit Genre verwijzen
            //Zo ja: Foutmelding
            var genre = await dbAccess.OphalenGenre_async(code);
            var boekenGenre = await dbAccess.OphalenBoeken_async(genre);

            if (boekenGenre.Count() == 0)
            {
                await dbAccess.VerwijderenGenre_async(code);
            }
            //else foutmelding

        }
        public async Task ToevoegenGenre(Genre genre)
        {
            await dbAccess.ToevoegenGenre_async(genre);
        }
    }
}
