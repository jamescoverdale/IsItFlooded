using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IsItFlooded.Models;

namespace IsItFlooded.Data
{
    public class FloodedLocation
    {
        public string Name { get; set; }
        public string RiverName { get; set; }
        public List<Footpath> Footpaths { get; set; }
        public List<RiverLevelMeasurement> RiverLevelMeasurements { get; set; }

        public FloodedLocation()
        {
            RiverLevelMeasurements = new List<RiverLevelMeasurement>();
        }
    }
}
