using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IsItFlooded.Data;

namespace IsItFlooded.Models
{
    public class DetailsViewModel
    {
        public List<RiverLevelMeasurement> Levels { get; set; }
        public Footpath Footpath { get; set; }
    }
}
