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
        public async Task<List<Genre>> OphalenGenresAsync()
        {
            return await dbAccess.OphalenGenres_async();
        }
        public async Task<Genre[]> OphalenGenres()
        {
            var genreLijst = await dbAccess.OphalenGenres_async();
            //return genreLijst;
            return genreLijst.ToArray() ;
            //return await dbAccess.OphalenGenres_async();
            
        }

        public async Task<Genre> OphalenGenreAsync(Genre genre)
        {
            return await dbAccess.OphalenGenre_async(genre.Code);
        }

        public async Task<Int32?> WijzigenGenreAsync(Genre bestaandGenre, Genre bijgewerktGenre)
        {
            return await dbAccess.WijzigenGenre_async(bestaandGenre, bijgewerktGenre);

        }
        public async Task<Int32?> VerwijderenGenreAsync(Int32 code)
        {
            //Eerst nakijken of er nog boeken naar dit Genre verwijzen
            //Zo ja: Foutmelding
            var opgehaaldGenre = await dbAccess.OphalenGenre_async(code);
            var boekenVanGenre = await dbAccess.OphalenBoeken_async(opgehaaldGenre);

            if (boekenVanGenre.Count() == 0)
            {
                //Er zijn geen boeken meer van dit Genre, het genre mag gewoon verwijderd worden
                await dbAccess.VerwijderenGenre_async(opgehaaldGenre.Code);
                return opgehaaldGenre.Code;
            }
            else
            {
                return null;
            }
        }

        public async Task<Int32?> VerwijderenGenreAsync(Genre genre)
        {
            //Eerst nakijken of er nog boeken naar dit Genre verwijzen          
            //var opgehaaldGenre = await dbAccess.OphalenGenre_async(genre.Code);
            var boekenVanGenre = await dbAccess.OphalenBoeken_async(genre);

            if (boekenVanGenre.Count() == 0)
            {
                //Er zijn geen boeken meer van dit Genre, het genre mag gewoon verwijderd worden
                await dbAccess.VerwijderenGenre_async(genre.Code);
                return genre.Code;
            }
            else
            {
                return null;
            }    
        }

        public async Task<List<Genre>> VerwijderenGenreLijstAsync(List<Genre> genreLijst)
        {
            List<Genre> verwijderdeGenres = new List<Genre>();
            foreach (Genre genre in genreLijst)
            {
                //Eerst nakijken of er nog boeken naar dit Genre verwijzen          
                var opgehaaldGenre = await dbAccess.OphalenGenre_async(genre.Code);
                var boekenVanGenre = await dbAccess.OphalenBoeken_async(opgehaaldGenre);

                if (boekenVanGenre.Count() == 0)
                {
                    //Er zijn geen boeken meer van dit Genre, het genre mag gewoon verwijderd worden
                    await dbAccess.VerwijderenGenre_async(opgehaaldGenre.Code);
                    verwijderdeGenres.Add(opgehaaldGenre);
                }             
            }
            return verwijderdeGenres;
        }

        public async Task<Int32?> ToevoegenGenreAsync(Genre genre)
        {
            return await dbAccess.ToevoegenGenre_async(genre);
        }
    }
}
