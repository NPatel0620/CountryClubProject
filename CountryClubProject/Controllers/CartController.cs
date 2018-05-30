using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CountryClubProject.Models;


namespace CountryClubProject.Controllers
{
    public class CartController : Controller
    {
        private readonly CountryClubDbContext _context;

        public CartController(CountryClubDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            Guid cartId;
            Cart cart = null;
            if(Request.Cookies.ContainsKey("cartId"))
            {
                if (Guid.TryParse(Request.Cookies["cartId"], out cartId))
                {
                    cart = await _context.Carts
                        .Include(carts => carts.CartItems)
                        .ThenInclude(cartItems => cartItems.Product)
                        .FirstOrDefaultAsync(x => x.CookieIdentifier == cartId);
                }
            }
            if(cart == null)
            {
                cart = new Cart();
            }
            return View(cart);
        }

        public async Task<IActionResult> Remove(int id)
        {
            Guid cartId;
            Cart cart = null;
            if (Request.Cookies.ContainsKey("cartId"))
            {
                if (Guid.TryParse(Request.Cookies["cartId"], out cartId))
                {
                    cart = await _context.Carts
                        .Include(carts => carts.CartItems)
                        .ThenInclude(cartItems => cartItems.Product)
                        .FirstOrDefaultAsync(x => x.CookieIdentifier == cartId);
                }
            }
            CartItem item = cart.CartItems.FirstOrDefault(x => x.CartId == id);

            cart.LastModified = DateTime.Now;

            _context.CartItems.Remove(item);

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}