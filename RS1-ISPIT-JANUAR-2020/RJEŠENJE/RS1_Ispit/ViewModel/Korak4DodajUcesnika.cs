using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_Ispit_asp.net_core.ViewModel
{
    public class Korak4DodajUcesnika
    {
        public int OdjeljenjeStavkaId { get; set; }
        public IEnumerable<SelectListItem> OdjeljenjeStavka { get; set; }
        public int TakmicenjeId { get; set; }
        public int Bodovi { get; set; }
    }
}
