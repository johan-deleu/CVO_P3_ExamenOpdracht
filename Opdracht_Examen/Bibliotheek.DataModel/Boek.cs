using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bibliotheek.DataModel
{
    public class Boek
    {
        public Int32 Code { get; set; }
        public string Titel { get; set; }
        public string Auteur { get; set; }
        public Int32 Paginas { get; set; }
        //virtual keyword bij ICollection verwijderd      
        public ICollection<Genre> Genres { get; set; }
        public Boek()
        {

        }
        public Boek(string titel)
        {
            Titel = titel;
        }
        public Boek(Int32 code, string titel, string auteur, Int32 paginas)
        {
            Code = code;
            Titel = titel;
            Auteur = auteur;
            Paginas = paginas;
        }

        public override string ToString()
        {
            return String.Format($"[{Code}] {Titel}, {Auteur} ({Paginas} pagina's)");
        }
    }
}
