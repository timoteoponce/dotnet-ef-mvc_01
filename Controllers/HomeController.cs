using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using web_04_ef_sqlite.Models;
using web_04_ef_sqlite.Models.Database;

namespace web_04_ef_sqlite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly AppDbContext dbContext;

        public HomeController(ILogger<HomeController> logger, AppDbContext dbContext)
        {
            this.logger = logger;
            this.dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var students = dbContext.Students;
            var location = GetOrCreateLocation("Heredia, Chuquisaca");

            if (students.Count() == 0)
            {
                dbContext.Add(new Student { Name = "Reina Jimenez", Location = location });
                dbContext.Add(new Student { Name = "Roxana Jimenez", Location = location });
                dbContext.Add(new Student { Name = "Olga Jimenez", Location = location });
                dbContext.SaveChanges();
                logger.LogInformation("New students created");
            }
            else
            {
                foreach (var st in students)
                {
                    dbContext.Remove(st);
                }
                logger.LogInformation($"{students.Count()} Students removed");
                dbContext.SaveChanges();
            }
            foreach(var st in location.Students){
                logger.LogInformation($"Location '{location.Name}' contains student '{st.Name}'");
            }
            return View();
        }

        private Location GetOrCreateLocation(string name)
        {
            var location = dbContext.Locations.Where(l => l.Name.Equals(name)).SingleOrDefault();
            if (location == null)
            {
                location = new Location { Name = name };
                dbContext.Add(location);
                dbContext.SaveChanges();
            }
            return location;
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
