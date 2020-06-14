using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_Ispit_asp.net_core.ViewModel
{
    public class Korak2
    {

        public int? Razred { get; set; }
        public string SkolaDomacin { get; set; }
        public int SkolaDomacinId { get; set; }

        public IEnumerable<Takmicenja> takmicenja;

         public class Takmicenja
        {
            public int TakmicenjeID { get; set; }
            public string Predmet { get; set; }
            public string Datum { get; set; }
            public int Razred { get; set; }
            public int BrojUcenikaKojiNisuPrisupili { get; set; }
            public string NajboljiUcesnik { get; set; }
        }


    }
}
