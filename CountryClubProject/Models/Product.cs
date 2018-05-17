using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CountryClubProject.Models
{
    public class Product
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string ClubType { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Description { get; set; }
        public decimal? Price { get; set; }
        public string Image { get; set; }
    }
}
