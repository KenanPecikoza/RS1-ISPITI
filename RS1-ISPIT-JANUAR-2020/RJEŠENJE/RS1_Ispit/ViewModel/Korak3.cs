using Microsoft.AspNetCore.Mvc.Razor.Compilation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_Ispit_asp.net_core.ViewModel
{
    public class Korak3
    {
        public string SkolaDomacin { get; set; }
        public IEnumerable<SelectListItem> Predmet{ get; set; }
        public int PredmetId { get; set; }
        public int Razred{ get; set; }
        public DateTime Datum { get; set; }
        public int SkolaDomacinId { get; set; }
    }
}
