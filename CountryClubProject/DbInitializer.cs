using System;
using System.Linq;
using CountryClubProject.Models;
using Microsoft.EntityFrameworkCore;

namespace CountryClubProject
{
    internal static class DbInitializer
    {
        internal static void Initialize(this CountryClubDbContext db)
        {
            //db.Database.Migrate();

            if (db.Products.Count() == 0)
            {
                db.Products.Add(new Product
                {
                    Description = "Callaway Driver",
                    Image = "/images/epic.jpg",
                    Name = "Callaway Epic",
                    Price = 350.00m
                });
                db.Products.Add(new Product
                {
                    Description = "Titelist Driver",
                    Image = "/images/epic.jpg",
                    Name = "Titelist d420",
                    Price = 300.00m
                });
                db.Products.Add(new Product
                {
                    Description = "Taylormade Driver",
                    Image = "/images/epic.jpg",
                    Name = "Taylormade M2",
                    Price = 399.99m
                });
                db.Products.Add(new Product
                {
                    Description = "Ping Driver",
                    Image = "/images/epic.jpg",
                    Name = "Ping G400",
                    Price = 375.00m
                });

                db.SaveChanges();
            }
        }
    }
}