using Microsoft.AspNetCore.Mvc.Rendering;
using RS1_Ispit_asp.net_core.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_Ispit_asp.net_core.ViewModel
{
    public class Korak3
    {
        public int SkolaId { get; set; }
        public IEnumerable<SelectListItem> Skole{ get; set; }
        public int NastavnikId { get; set; }
        public string Nastavnik { get; set; }
        public int SkolskaGodinaId { get; set; }
        public string SkolskaGodina{ get; set; }
        public DateTime Datum { get; set; }
        public int PredmetId { get; set; }
        public IEnumerable<SelectListItem> Predmeti { get; set; }
    }
}
