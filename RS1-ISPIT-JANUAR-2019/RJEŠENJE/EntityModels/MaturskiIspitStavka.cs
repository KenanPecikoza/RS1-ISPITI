using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_Ispit_asp.net_core.EntityModels
{
    public class MaturskiIspitStavka
    {
        public int Id { get; set; }
        public MaturskiIspit MaturskiIspit{ get; set; }
        public int MaturskiIspitId { get; set; }
        public OdjeljenjeStavka OdjeljenjeStavka { get; set; }
        public int OdjeljenjeStavkaId { get; set; }
        public bool Pristupio { get; set; }
        public double RezultatiMaturskog { get; set; }
        public double ProsjekOcjena { get; set; }

    }
}
