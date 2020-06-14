using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_Ispit_asp.net_core.ViewModel
{
    public class Korak4PrikaziUcenike
    {
        public  int PopravniIspitStavkaId { get; set; }
        public string Ucenik { get; set; }
        public string Odjeljenje { get; set; }
        public int BrojUDnevniku { get; set; }
        public bool Prisutan { get; set; }
        public double Bodovi { get; set; }
        public bool Izlazak { get; set; }
    }
}
