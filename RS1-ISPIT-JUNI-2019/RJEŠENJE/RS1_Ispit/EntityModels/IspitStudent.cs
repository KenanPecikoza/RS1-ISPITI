using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_Ispit_asp.net_core.EntityModels
{
    public class IspitStudent
    {
        public int Id { get; set; }
        public SlusaPredmet SlusaPredmet { get; set; }
        public int SlusaPredmetId { get; set; }
        public int Ocjena { get; set; }
        public bool Prijava { get; set; }
        public bool Pristupio { get; set; }
        public bool Polozio { get; set; }
        public Ispit Ispit { get; set; }
        public int IspitId { get; set; }
    }
}
