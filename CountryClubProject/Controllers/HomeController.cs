using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CountryClubProject.Models;
using Microsoft.EntityFrameworkCore;


namespace CountryClubProject.Controllers
{
    public class HomeController : Controller
    {
        CountryClubDbContext _db;

        public HomeController(CountryClubDbContext countryClubDbContext)
        {
            _db = countryClubDbContext;
        }
        public TestEntity[] FooBar()
        {
            return _db.TestEntities.ToArray();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Account()
        {
            return View();
        }

        public IActionResult Course()
        {
            //added a course view in the soulution explorer
            return View();
            
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Overview()
        {
            return View();
        }

        public IActionResult Ttime()
        {
            TtimeViewModel model = new TtimeViewModel();
            
            return View(model);
        }

        [HttpPost]
        public IActionResult Ttime(TtimeViewModel model)
        {

            return View(model);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> CartSummary()
        {
            Guid cartId;
            Cart cart = null;
            if(Request.Cookies.ContainsKey("cartId"))
            {
                if(Guid.TryParse(Request.Cookies["cartId"], out cartId))
                {
                    cart = await _db.Carts
                        .Include(carts => carts.CartItems)
                        .ThenInclude(cartitems => cartitems.Product)
                        .FirstOrDefaultAsync(x => x.CookieIdentifier == cartId);
                }
            }
            if(cart == null)
            {
                cart = new Cart();
            }
            return Json(cart);
        }
    }
    
}
