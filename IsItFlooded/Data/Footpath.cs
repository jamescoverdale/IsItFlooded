using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;

namespace IsItFlooded.Data
{
    public class Footpath
    {
        public string LocationName { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public LongLat Location { get; set; }
        public string StationId { get; set; }
        public double WarningLevel { get; set; }
        public double DangerLevel { get; set; }
        public double ActualLevel { get; set; }

        public FootpathStatus GetFootpathStatus()
        {
            if (ActualLevel >= DangerLevel)
                return FootpathStatus.Danger;
            if (ActualLevel >= WarningLevel)
                return FootpathStatus.Warn;

            return FootpathStatus.Safe;
        }

        
    }

    public class LongLat
    {
        public double Long { get; set; }
        public double Lat { get; set; }
    }

    public enum FootpathStatus
    {
        Safe,
        Warn,
        Danger
    }
}
