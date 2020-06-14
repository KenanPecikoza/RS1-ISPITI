using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_Ispit_asp.net_core.ViewModel
{
    public class Korak2
    {
        public int NastavnikId { get; set; }
        public IEnumerable<Row> Red { get; set; }

        public class Row
        {
            public string Datum { get; set; }
            public string Skola { get; set; }
            public int OdrzaniCasId { get; set; }
            public string Predmet { get; set; }
            public string SkolskaGodinaOdjeljenje { get; set; }
            public IEnumerable<string> Odsutni { get; set; }
        }
    }
}
