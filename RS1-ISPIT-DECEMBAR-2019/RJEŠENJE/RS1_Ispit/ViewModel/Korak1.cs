using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_Ispit_asp.net_core.ViewModel
{
    public class Korak1
    {
        public int SkolaId { get; set; }
        public IEnumerable<SelectListItem> Skole { get; set; }
        public int SkolskaGodinaId { get; set; }
        public IEnumerable<SelectListItem> SkolskeGodine { get; set; }
        public int PredmetId { get; set; }
        public IEnumerable<SelectListItem> Predmeti { get; set; }
    }
}
