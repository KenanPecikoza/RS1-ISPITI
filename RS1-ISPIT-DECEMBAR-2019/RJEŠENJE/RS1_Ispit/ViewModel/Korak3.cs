using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_Ispit_asp.net_core.ViewModel
{
    public class Korak3
    {

        public int ClanKomisije1Id { get; set; }
        public int ClanKomisije2Id { get; set; }
        public int ClanKomisije3Id { get; set; }
        public IEnumerable<SelectListItem> ClanoviKomisije { get; set; }
        public int SkolaId { get; set; }
        public string Skola { get; set; }
        public int SkolskaGodinaId { get; set; }
        public string SkolskaGodina { get; set; }
        public int PremdetId { get; set; }
        public string Predmet { get; set; }
        public DateTime Datum { get; set; }
        public int PopravniIspitId { get; set; }
    }
}
