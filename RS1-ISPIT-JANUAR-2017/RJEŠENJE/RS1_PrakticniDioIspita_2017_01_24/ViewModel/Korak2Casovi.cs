using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_PrakticniDioIspita_2017_01_24.ViewModel
{
    public class Korak2Casovi
    {
        public int Id { get; set; }
        public IEnumerable<Row> Casovi { get; set; }
        public class Row
        {

            public int Id { get; set; }
            public DateTime datum { get; set; }
            public string Oznaka { get; set; }
            public string Predmet { get; set; }
        }
    }
}
