using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace RS1_Ispit_asp.net_core.EntityModels
{
    public class PopravniIspit
    {
        public int Id { get; set; }
        public DateTime Datum { get; set; }
        public Predmet Predmet { get; set; }
        public int PredmetId { get; set; }
        public SkolskaGodina SkolskaGodina { get; set; }
        public int SkolskaGodinaId { get; set; }
        public Skola Skola { get; set; }
        public int SkolaId { get; set; }
        public Nastavnik ClanKomisije1 { get; set; }
        public int ClanKomisije1Id { get; set; }
        public Nastavnik ClanKomisije2 { get; set; }
        public int ClanKomisije2Id { get; set; }
        public Nastavnik ClanKomisije3 { get; set; }
        public int ClanKomisije3Id { get; set; }
    }
}
