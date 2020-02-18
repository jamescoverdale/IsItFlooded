using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace IsItFlooded.Data
{
    public class RiverLevelMeasurement
    {
        public string StationId { get; set; }
        public string StationName { get; set; }

        public string StationRLOIid { get; set; }
        public double Measurement { get; set; }
        public double LastMeasurement { get; set; }
        public DateTime MeasurementDateTime { get; set; }

        public Trend GetTrend()
        {
            if (LastMeasurement < Measurement)
                return Trend.Up;

            if (LastMeasurement > Measurement)
                return Trend.Down;

            return Trend.NoChange;
        }
    }

    public enum Trend
    {
        NoChange,
        Up,
        Down
    }
}
