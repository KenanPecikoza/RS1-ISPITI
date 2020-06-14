using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_Ispit_asp.net_core.EntityModels
{
    public class MaturskiIspit
    {
        public int Id { get; set; }
        public Skola Skola { get; set; }
        public int SkolaId { get; set; }
        public Nastavnik Nastavnik { get; set; }
        public int NastavnikId { get; set; }
        public SkolskaGodina SkolskaGodina { get; set; }
        public int SkolskaGodinaId { get; set; }
        public DateTime Datum { get; set; }
        public Predmet Predmet { get; set; }
        public int PredmetId { get; set; }
        public string Napomena { get; set; }
    }
}
