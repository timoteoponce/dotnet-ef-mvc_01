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

        public HomeController(ILogger<HomeController> logger,AppDbContext dbContext)
        {
            this.logger = logger;
            this.dbContext = dbContext;
        }

        public IActionResult Index()
        {
            if(dbContext.Students.Count() == 0){
                dbContext.Add(new Student{Id = 1, Name = "Reina Jimenez"});
                dbContext.Add(new Student{Id = 2, Name = "Roxana Jimenez"});
                dbContext.Add(new Student{Id = 3, Name = "Olga Jimenez"});
                dbContext.SaveChanges();
                logger.LogInformation("New students created");
            }else{
                logger.LogInformation("Students not created");
            }
            return View();
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
