using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_PrakticniDioIspita_2017_01_24.ViewModel
{
    public class Detalji
    {
        public string Datum { get; set; }
        public string Odjeljenje { get; set; }
        public int Ukupno { get; set; }
        public int Prisutno { get; set; }
        public string Predmet { get; set; }
        public string NajboljiUcenik { get; set; }
        public int Id { get; set; }
    }
}
