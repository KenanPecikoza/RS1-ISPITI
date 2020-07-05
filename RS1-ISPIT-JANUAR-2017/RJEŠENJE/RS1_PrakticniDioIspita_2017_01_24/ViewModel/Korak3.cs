using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_PrakticniDioIspita_2017_01_24.ViewModel
{
    public class Korak3
    {
        public int Id { get; set; }
        public string Nastavnik { get; set; }
        public DateTime Datum { get; set; }
        public IEnumerable<SelectListItem> OdjeljenjePredmet { get; set; }
        public int AngazovanId { get; set; }
    }
}
