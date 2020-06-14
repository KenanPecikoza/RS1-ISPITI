using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_Ispit_asp.net_core.ViewModel
{
    public class Korak2
    {
        public string Predmet { get; set; }
        public string ImeIPrezimeNastavnika{ get; set; }
        public string AkademskaGodina { get; set; }
        public int AngazovanId { get; set; }
        public IEnumerable<Row> Rows { get; set; }
        public class Row
        {
            public string DatumIspita { get; set; }
            public int BrojStudenataKojiNisuPolozili { get; set; }
            public int BrojPrijavljenihStudenata { get; set; }
            public bool EvidentiraniRezultati { get; set; }
            public int IspitId { get; set; }
        }
    }
}
