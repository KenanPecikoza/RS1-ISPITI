using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_Ispit_asp.net_core.ViewModel
{
    public class Korak4Uredi
    {
        public int DetaljiID { get; set; }
        public Prisutan UcenikPrisutan { get; set; }
        public Odsutan UcenikOdsutan { get; set; }
        public bool PrisutanNaCasu { get; set; }
        public int  OdrzaniCasId{ get; set; }
        public string Ucenik { get; set; }


        public class Prisutan
        {
            public int? Ocjena { get; set; }
        }
        public class Odsutan
        {
            public string Napomena { get; set; }
            public bool Opravdano { get; set; }
        }

    }
}
