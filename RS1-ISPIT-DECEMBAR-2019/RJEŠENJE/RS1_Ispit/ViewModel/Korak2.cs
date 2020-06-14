using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_Ispit_asp.net_core.ViewModel
{
    public class Korak2
    {
        public int SkolaId { get; set; }
        public int PredmetId { get; set; }
        public int SkolskaGodinaId { get; set; }
        public string Skola { get; set; }
        public string Predmet { get; set; }
        public string SkolskaGodina { get; set; }

        public IEnumerable<Row> Rows { get; set; }
        public class Row
        {
            public int NastavnikId { get; set; }
            public string Datum { get; set; }
            public string Nastavnik { get; set; }
            public int BrojUčenika { get; set; }
            public int BrojUcenikaKojiSuPolozili { get; set; }
            public int PopravniIspitId { get; set; }

        }
    }
}
