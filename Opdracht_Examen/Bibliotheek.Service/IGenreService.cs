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
        //Task<List<Genre>> OphalenGenres();
        Task<Genre[]> OphalenGenres();

        [OperationContract]
        Task<Genre> OphalenGenre_async(Int32 code);

        [OperationContract]
        Task WijzigenGenre_async(Genre genre);

        [OperationContract]
        Task VerwijderenGenre(Int32 code);

        Task ToevoegenGenre(Genre genre);
        
    }
}
