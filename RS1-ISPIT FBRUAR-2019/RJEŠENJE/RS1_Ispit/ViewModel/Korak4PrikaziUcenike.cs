using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_Ispit_asp.net_core.ViewModel
{
    public class Korak4PrikaziUcenike
    {
        public IEnumerable<Row> rows { get; set; }
        public int OdrzaniCasId { get; set; }
        public class Row
        {
            public int UcenikId { get; set; }
            public string ImeIPrezime { get; set; }
            public int? Ocjena { get; set; }
            public bool Prisutan { get; set; }
            public bool? Opravdano { get; set; }
            public int DetaljID { get; set; }
        }
    }
}
