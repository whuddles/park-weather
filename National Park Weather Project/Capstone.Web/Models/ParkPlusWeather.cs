﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Web.Models
{
    public class ParkPlusWeather
    {
        public Park Park {get;set;}

        public List<Weather> Weathers { get; set; }

       

    }
}
