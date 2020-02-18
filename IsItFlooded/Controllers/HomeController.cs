using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using IsItFlooded.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using IsItFlooded.Models;

namespace IsItFlooded.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            MainPageViewModel model = new MainPageViewModel();

            foreach(var file in System.IO.Directory.GetFiles("Config", "*.json"))
            {
                model.Locations.Add(System.Text.Json.JsonSerializer.Deserialize<FloodedLocation>(System.IO.File.ReadAllText(file)));
            }

            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
