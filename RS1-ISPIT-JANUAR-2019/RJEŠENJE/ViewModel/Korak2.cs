using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_Ispit_asp.net_core.ViewModel
{
    public class Korak2
    {
        public int NastavnikId { get; set; }
        public IEnumerable<MaturskiIspiti> maturski { get; set; }

        public class MaturskiIspiti
        {
            public string Datum { get; set; }
            public string Skola { get; set; }
            public string Predmet { get; set; }
            public IEnumerable<string> NisuPristupili{ get; set; }
            public int MaturskiId { get; set; }

        }
    }
}
