﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_Ispit_asp.net_core.ViewModel
{
    public class Korak1
    {
        public int SkolaID { get; set; }
        public List<SelectListItem> Skole  { get; set; }
        public int Razred { get; set; }
    }
}
