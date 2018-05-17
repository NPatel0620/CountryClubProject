using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using CountryClubProject.Models;
using Microsoft.Extensions.Configuration;

namespace CountryClubProject.Controllers
{
    public class ProductController : Controller
    {
        private readonly CountryClubDbContext _context;
        public ProductController(CountryClubDbContext context)
        {
            _context = context;
        }
        
        public IActionResult Index()
        {
            List<Product> products = _context.Products.ToList();
            return View();
        }
        public IActionResult Details(int? id)
        {
            if (id.HasValue)
            {
                Product p = _context.Products.Find(id.Value);
                return View(p);
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult Details(int id, int quantity = 1)
        {
            Guid cartId;
            CartController cart = null;
            if (Request.Cookies.ContainsKey("cartId"))
            {

                if (Guid.TryParse(Request.Cookies["cartId"], out cartId))
                {
                    cart = _context.Carts
                        .Include(carts => carts.CartItems)
                        .ThenInclude(cartitems => cartitems.Product)
                        .FirstOrDefault(x => x.CookieIdentifier == cartId);
        
                }
            }

            if (cart == null)
            {
                cart = new Cart();
                cartId = Guid.NewGuid();
                cart.CookieIdentifier = cartId;

                _context.Carts.Add(cart);
                Response.Cookies.Append("cartId", cartId.ToString(), new Microsoft.AspNetCore.Http.CookieOptions { Expires = DateTime.UtcNow.AddYears(100) });
            }
            CartItem item = cart.CartItems.FirstorDefault(x => x.Product.ID == id);
            if(item == null)
            {
                item = new CartItem();
                item.Product = _context.Products.Find(id);
                cart.CartItems.Add(item);
            }
            item.Quantity += quantity;

            cart.LastModified = DateTime.Now;

            _context.SaveChanges();
            return RedirectToAction("Index", "Cart");
        }
    }
}




        
