using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Bibliotheek.DataModel;

namespace Bibliotheek.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IBoekService" in both code and config file together.
    [ServiceContract]
    public interface IBoekService
    {
        [OperationContract]
        Task<List<Boek>> OphalenBoekenAsync();
        
        [OperationContract]
        Task<Boek> OphalenBoek(Int32 code);

        [OperationContract]
        Task<Boek> OphalenBoekMetGenreAsync(Boek boek);

        [OperationContract]
        Task<Int32?> VerwijderenBoekAsync(Boek boek);

        [OperationContract]
        Task<Int32> ToevoegenBoekAsync(Boek nieuwBoek);

        [OperationContract]
        Task<Int32> BewerkenBoekAsync(Boek teBewerkenBoek, Boek bewerktBoek);
    }
}
