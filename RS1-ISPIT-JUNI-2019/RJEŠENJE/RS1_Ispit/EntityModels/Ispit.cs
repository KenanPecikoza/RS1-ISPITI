using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_Ispit_asp.net_core.EntityModels
{
    public class Ispit
    {
        public int Id { get; set; }
        public Angazovan Angazovan { get; set; }
        public int AngazovanId { get; set; }
        public DateTime Datum { get; set; }
        public string Napomena { get; set; }
        public bool Zaključano { get; set; }

    }
}
