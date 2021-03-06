﻿using System;
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
            var mathCourse = GetOrCreateCourse("Math");
            var bioCourse = GetOrCreateCourse("Biology");

            if (students.Count() == 0)
            {
                createStudent("Reina Jimenez", location, mathCourse);
                createStudent("Olga Jimenez", location, mathCourse);
                createStudent("Roxana Jimenez", location, bioCourse);
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
            return View();
        }

        private Student createStudent(string name, Location location, Course course)
        {
            var student = dbContext.Students.Where(s => s.Name.Equals(name)).SingleOrDefault();
            if (student == null)
            {
                student = new Student { Name = name, Location = location };
                student.Courses.Add(course);
                dbContext.Add(student);
                dbContext.SaveChanges();
            }
            return student;
        }

        private Course GetOrCreateCourse(string name)
        {
            var course = dbContext.Courses.Where(c => c.Name.Equals(name)).SingleOrDefault();
            if (course == null)
            {
                course = new Course { Name = name };
                dbContext.Add(course);
                dbContext.SaveChanges();
            }
            return course;
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
