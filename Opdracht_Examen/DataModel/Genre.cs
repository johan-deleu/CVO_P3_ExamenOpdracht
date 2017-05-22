using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class Genre
    {
        public Int32 Code { get; set; }
        public string Omschrijving { get; set; }
        public virtual ICollection<Boek> Boeken { get; set; }

        public Genre()
        {

        }
        public Genre(String omschrijving)
        {
            Omschrijving = omschrijving;
        }

        public Genre(Int32 code, String omschrijving)
        {
            Code = code;
            Omschrijving = omschrijving;
        }
        public override string ToString()
        {
            return Omschrijving;
        }
    }
}
