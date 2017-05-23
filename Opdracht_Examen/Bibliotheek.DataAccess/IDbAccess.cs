using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bibliotheek.DataModel;

namespace Bibliotheek.DataAccess
{
    public interface IDbAccess
    {

        /// <summary>
        /// Haalt alle genres op uit de database
        /// </summary>
        /// <returns>
        /// List die de genres bevat 
        /// </returns>
        Task<List<Genre>> OphalenGenres_async();

        /// <summary>
        /// Haalt één genre op uit de database op basis van de primary key
        /// </summary>
        /// <returns>
        /// Eén genre  
        /// </returns>
        Task<Genre> OphalenGenre_async(Int32 code);

        /// <summary>
        /// Haalt alle boeken op uit de database
        /// </summary>
        /// <returns>
        /// List die de boeken bevat 
        /// </returns>
        Task<List<Boek>> OphalenBoeken_async();

        /// <summary>
        /// Haalt alle boeken van een specifiek genre op uit de database 
        /// </summary>
        /// <returns>
        /// Lijst die de boeken bevat van dat genre
        /// </returns>
        Task<List<Boek>> OphalenBoeken_async(Genre genre);

        /// <summary>
        /// Haalt één boek op uit de database op basis van de primary key
        /// </summary>
        /// <returns>
        /// Eén boek 
        /// </returns>
        Task<Boek> OphalenBoek_async(Int32 code);

        Task<Boek> OphalenBoekMetGenre_async(Boek boek);

        /// <summary>
        /// Voegt een boek toe aan de database
        /// </summary>
        /// <param name="ToeTeVoegenBoek"></param>
        /// <returns>
        /// Primary key toegekend aan het nieuwe boek
        /// </returns>
        Task<Int32> ToevoegenBoek_async(Boek ToeTeVoegenBoek);

        /// <summary>
        /// Verwijdert één boek uit de database op basis van de primary key
        /// </summary>
        /// <returns> </returns>
        Task<Int32?> VerwijderenBoek_async(Boek boek);

        /// <summary>
        /// Voegt een genre toe aan de database
        /// </summary>
        /// <returns> </returns>
        Task <Int32?> ToevoegenGenre_async(Genre genre);

        /// <summary>
        /// Verwijdert één genre uit de database op basis van de primary key
        /// </summary>
        /// <returns> </returns>
        Task VerwijderenGenre_async(Int32 code);

        /// <summary>
        /// Wijzigt de omschrijving van één genre uit de database op basis van de nieuwe gegevens van het genre
        /// </summary>
        /// <returns> </returns>
        Task<Int32?> WijzigenGenre_async(Genre bestaandGenre,  Genre bijgewerktGenre);
    }
}
