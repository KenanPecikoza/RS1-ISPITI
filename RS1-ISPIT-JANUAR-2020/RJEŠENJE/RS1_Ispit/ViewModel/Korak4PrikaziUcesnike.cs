using RS1_Ispit_asp.net_core.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RS1_Ispit_asp.net_core.ViewModel
{
    public class Korak4PrikaziUcesnike
    {
        public int TakmicenjeId { get; set; }
        public IEnumerable<Ucesnik> Ucesnici { get; set; }
        public class Ucesnik
        {
            public string Odjeljenje { get; set; }
            public int BrojUDnevniku { get; set; }
            public bool Pristupio { get; set; }
            public double Bodovi{ get; set; }
            public int UcesnikId { get; set; }
        }
    }
}
