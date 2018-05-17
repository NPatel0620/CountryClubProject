using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using CountryClubProject.Models;

namespace CountryClubProject.Controllers
{
    public class LessonsController : Controller
    {
        private List<Lessons> _plans;

        public LessonsController()
        {

            _plans = new List<Lessons>();
            _plans.Add(new Lessons
            {
                ID = 1,
                Instructor = "Chris Gumbach",
                TimeMin = 30,
                LessonQty = 5,
                Image = "/images/augusta.jpg"
            });
            _plans.Add(new Lessons
            {
                ID = 2,
                Instructor = "Pete Dye",
                TimeMin = 45,
                LessonQty = 3,
                Price = 150.00m
            });
            _plans.Add(new Lessons
            {
                ID = 3,
                Instructor = "Neil Patel",
                TimeMin = 35,
                LessonQty = 4,
                Price = 175.00m
            });
        }

            public IActionResult Index()
        {
            return View(_plans);
        }
        public IActionResult Details(int? id)
        {
            if (id.HasValue)
            {
                Lessons p = _plans.Single(x => x.ID == id.Value);
                return View(p);
            }
            return NotFound();
        }
    }
}