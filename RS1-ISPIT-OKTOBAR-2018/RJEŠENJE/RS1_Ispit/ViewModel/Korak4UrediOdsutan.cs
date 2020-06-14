using Microsoft.AspNetCore.Mvc.Razor.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_Ispit_asp.net_core.ViewModel
{
    public class Korak4UrediOdsutan
    {
        public string Ucenik { get; set; }
        public int OdrzaniCasStavkaId { get; set; }
        public string Napomena { get; set; }
        public bool Opravdano { get; set; }
    }
}
