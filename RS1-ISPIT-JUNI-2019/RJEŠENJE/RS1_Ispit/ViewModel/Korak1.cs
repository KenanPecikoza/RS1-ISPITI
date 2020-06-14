using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_Ispit_asp.net_core.ViewModel
{
    public class Korak1
    {
        public string NazivPredmeta { get; set; }
        public IEnumerable<Row> Rows { get; set; }

        public class Row
        {
            public int AngazovanId { get; set; }
            public string AkademskaGodina { get; set; }
            public int BrojOdrzanihCasova { get; set; }
            public int BrojStudenataNaPredmetu{get; set;}
            public string ImeIPrezimeNastavnika { get; set; }
            public int Brojac { get; set; }
        }
    }
}
