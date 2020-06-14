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
        public string NastavnikImeIPrezime { get; set; }
        public DateTime Datum { get; set; }
        public string SadrzajCasa { get; set; }
        public List<SelectListItem> SkolaOdjeljenjePredmet { get; set; }
        public int PredajePredmetId { get; set; }
    }
}
