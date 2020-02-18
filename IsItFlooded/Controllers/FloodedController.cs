using IsItFlooded.Data;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using IsItFlooded.Models;
using Microsoft.EntityFrameworkCore.Internal;

namespace IsItFlooded.Controllers
{
    public class FloodedController : Controller
    {
        private IRiverLevelService _Service;

        [ResponseCache(Duration = 60 * 15, VaryByQueryKeys = new[] {"name"})]
        public IActionResult Index(string name = "Leeds")
        {
            // look for config
            if (System.IO.File.Exists($"config/{name}.json"))
            {
                IRiverLevelService riverLevelService = new EARiverLevelService();

                var floodedLocation =
                    System.Text.Json.JsonSerializer.Deserialize<FloodedLocation>(
                        System.IO.File.ReadAllText($"config/{name}.json"));

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

        [ResponseCache(Duration = 60 * 15, VaryByQueryKeys = new[] {"name", "footpath"})]
        public IActionResult Details(string name, string footpath)
        {
            _Service = new EARiverLevelService();

            var floodedLocation =
                System.Text.Json.JsonSerializer.Deserialize<FloodedLocation>(
                    System.IO.File.ReadAllText($"config/{name}.json"));

            // find the footpath
            var path = floodedLocation.Footpaths.Single(t => t.Name.Replace(" ", "") == footpath);
            path.LocationName = name;

            // get the last x measurements
            var levels = _Service.GetRiverLevels(path.StationId, 10);
            path.ActualLevel = levels.First().Measurement;

            var model = new DetailsViewModel()
            {
                Footpath = path,
                Levels = levels
            };

            return View(model);
        }
    }
}