using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IsItFlooded.Data;

namespace IsItFlooded.Models
{
    public class MainPageViewModel
    {
        public List<FloodedLocation> Locations { get; set; }

        public MainPageViewModel()
        {
            Locations = new List<FloodedLocation>();
        }
    }
}
