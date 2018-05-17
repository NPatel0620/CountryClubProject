using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static CountryClubProject.Models.CountryClubUser;

namespace CountryClubProject.Models
{
    public class CountryClubDbContext : IdentityDbContext<CountryClubUser>
    {
        public CountryClubDbContext() : base()
        {

        }

        public CountryClubDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<TestEntity> TestEntities { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
    }

    public class CountryClubUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        
        public CountryClubUser() : base()
        {

        }

        public CountryClubUser(string userName): base(userName)
        {
        }

        public class TestEntity
        {
            public int ID { get; set; }
            public string Name { get; set; }
        }



        public class Cart
        {
            public Cart()
            {
                this.CartItems = new HashSet<CartItem>();
            }

            public int ID { get; set; }
            public Guid CookieIdentifier { get; set; }
            public DateTime LastModified { get; set; }
            public ICollection<CartItem> CartItems { get; set; }
        }

        public class CartItem
        {
            public int ID { get; set; }
            public Cart Cart { get; set; }
            public Product Product { get; set; }
            public int Quantity { get; set; }
        }
    }
}
