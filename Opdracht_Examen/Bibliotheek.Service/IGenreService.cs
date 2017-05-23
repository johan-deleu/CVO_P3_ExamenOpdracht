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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IGenreService" in both code and config file together.
    [ServiceContract]
    public interface IGenreService
    {

        [OperationContract]
        Task<List<Genre>> OphalenGenresAsync();

        //[OperationContract]
        //Task<Genre[]> OphalenGenres();

        [OperationContract]
        Task<Genre> OphalenGenreAsync(Genre genre);

        [OperationContract]
        Task<Int32?> WijzigenGenreAsync(Genre bestaandGenre, Genre bijgewerktGenre);

        //[OperationContract]
        //Task<Int32?> VerwijderenGenreAsync(Int32 code);

        [OperationContract]
        Task<Int32?> VerwijderenGenreAsync(Genre genre);

        [OperationContract]
        Task<List<Genre>> VerwijderenGenreLijstAsync(List<Genre> genreLijst);

        [OperationContract]
        Task<Int32?> ToevoegenGenreAsync(Genre genre);
        
    }
}
