using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ispit.Web.ViewModels
{
    public class Korak1
    {
        public IEnumerable<Oznaceni> OznaceniDogadjaji { get; set; }
        public IEnumerable<Neoznaceni> NeoznaceniDogadjaji { get; set; }

        public class Oznaceni
        {
            public int DogadjajId { get; set; }
            public string Datum { get; set; }
            public string NastavnikImeIPrezime { get; set; }
            public string OpisDogadjaja { get; set; }
            public double Realizovano { get; set; }

        }
        public class Neoznaceni
        {
            public int DogadjajId { get; set; }
            public string Datum { get; set; }
            public string NastavnikImeIPrezime { get; set; }
            public string OpisDogadjaja { get; set; }
            public int Obaveza { get; set; }

        }
    }
}
