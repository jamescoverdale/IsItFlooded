using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IsItFlooded.Data;

namespace IsItFlooded
{
    public interface IRiverLevelService
    {
        RiverLevelMeasurement GetRiverLevel(string stationId);

        List<RiverLevelMeasurement> GetRiverLevels(string stationId, int count = 10);
    }
}
