using IsItFlooded.Data;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore.Internal;

namespace IsItFlooded.Controllers
{
    public class FloodedController : Controller
    {

        [ResponseCache(Duration = 60 * 15, VaryByHeader = "name")]
        public IActionResult Index(string name = "Leeds")
        {
            // look for config
            if (System.IO.File.Exists($"config/{name}.json"))
            {
                IRiverLevelService riverLevelService = new EARiverLevelService();

                var floodedLocation = System.Text.Json.JsonSerializer.Deserialize<FloodedLocation>(System.IO.File.ReadAllText($"config/{name}.json"));

                foreach (var footpath in floodedLocation.Footpaths)
                {
                    if (floodedLocation.RiverLevelMeasurements.Any(t => t.StationId == footpath.StationId) == false)
                    {
                        var level = riverLevelService.GetRiverLevel(footpath.StationId);
                        footpath.ActualLevel = level.Measurement;
                        floodedLocation.RiverLevelMeasurements.Add(level);
                    }
                    else
                    {
                        footpath.ActualLevel = floodedLocation.RiverLevelMeasurements
                            .Single(t => t.StationId == footpath.StationId).Measurement;
                    }
                }

                return View(floodedLocation);
            }

            return View("NoLocationFound");
        }
    }
}