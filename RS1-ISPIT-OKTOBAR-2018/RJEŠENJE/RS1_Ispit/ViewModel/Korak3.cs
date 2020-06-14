using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_Ispit_asp.net_core.ViewModel
{
    public class Korak3
    {
        public int NastavnikId { get; set; }
        public string Nastavnik { get; set; }
        public int PredajePredmetId { get; set; }
        public IEnumerable<SelectListItem> OdjeljenjePremeti { get; set; }
        public DateTime Datum { get; set; }
    }
}
