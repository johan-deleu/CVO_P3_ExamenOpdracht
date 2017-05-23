using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bibliotheek.DataModel;
using System.Data.Entity;

namespace Bibliotheek.DataAccess
{
    public class DbAccessEF : Database, IDbAccess
    {

        public DbAccessEF() : base()
        {
        }

        public async Task<List<Genre>> OphalenGenres_async()
        {
            return await Genres.ToListAsync();
        }

        public async Task<Genre> OphalenGenre_async(Int32 code)
        {
            //Haal één specifiek genre op uit de database
            return await (Genres.SingleOrDefaultAsync(x => x.Code == code));
        }

        public async Task<List<Boek>> OphalenBoeken_async()
        {
            return await Boeken.ToListAsync();
        }
 
        public async Task<List<Boek>> OphalenBoekenMetGenre_async()
        {
            return await Boeken.Include(x => x.Genres).ToListAsync();
        }

        public async Task<Boek> OphalenBoekMetGenre_async(Boek boek)
        {
            var opgehaaldBoek= await Boeken.Include(x => x.Genres).SingleOrDefaultAsync(x => x.Code == boek.Code);
            //oneindig recursief onderbreken boeken van onderliggende genres op null te zetten.
            foreach (Genre genre in opgehaaldBoek.Genres)
            {
                genre.Boeken = null;
            }
            return opgehaaldBoek;
        }

        public async Task<List<Boek>> OphalenBoeken_async(Genre genre)
        {
            var boekGenre = new Genre();
            boekGenre = await Genres.SingleOrDefaultAsync(x => x.Code == genre.Code);

            //Haal de lijst met boeken van één specifiek genre op uit de database, NULL indien er geen boeken van dat genre zijn
            List<Boek> genreBoekenLijst = new List<Boek>();
            List<Boek> boekenLijst = await OphalenBoekenMetGenre_async();

            foreach (var boek in boekenLijst)
            {
                foreach (var huidigBoekGenre in boek.Genres)
                {
                    if (huidigBoekGenre.Code==genre.Code)
                    {
                        genreBoekenLijst.Add(boek);
                        break;
                    }
                }
            }
            return genreBoekenLijst;
        }
        public async Task<Boek> OphalenBoek_async(Int32 code)
        {
            //Haal één specifiek boek op uit de database
            return await (Boeken.SingleAsync(x => x.Code == code));
        }

        public async Task<Int32> ToevoegenBoek_async(Boek boek)
        {
            //Voeg één boek toe aan de database
            var toegevoegdBoek = Boeken.Add(boek);
            await SaveChangesAsync();

            //Geef de code (primary key) van het boek terug    
            return (toegevoegdBoek.Code);
        }

        public async Task<Int32?> VerwijderenBoek_async(Boek boek)
        {
            //Verwijder één specifiek boek uit de database
            var teVerwijderenBoek = await Boeken.SingleOrDefaultAsync(x => x.Code == boek.Code);
            if (teVerwijderenBoek != null)
            {
                Boeken.Remove(teVerwijderenBoek);
            }
            await SaveChangesAsync();
            return teVerwijderenBoek.Code;
        }

        public async Task<Int32?> ToevoegenGenre_async(Genre genre)
        {
            Genres.Add(genre);
            await SaveChangesAsync();
            return genre.Code;
        }

        public async Task VerwijderenGenre_async(Int32 code)
        {
            //Geen controle of er nog boeken van dit genre in de database zitten
            var teVerwijderenGenre = Genres.SingleOrDefault(x => x.Code == code);
            Genres.Remove(teVerwijderenGenre);
            await SaveChangesAsync();
            return;
        }

        public async Task<Int32?> WijzigenGenre_async(Genre bestaandGenre, Genre bijgewerktGenre)
        {
            var teWijzigenGenre = Genres.SingleOrDefault(x => x.Code == bestaandGenre.Code);
            teWijzigenGenre.Omschrijving = bijgewerktGenre.Omschrijving;
            await SaveChangesAsync();
            return teWijzigenGenre.Code;
        }
    }
}
