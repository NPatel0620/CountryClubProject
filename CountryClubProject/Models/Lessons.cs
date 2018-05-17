using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CountryClubProject.Models
{
    public class Lessons
    {
        public int ID { get; set; }
        public string Instructor { get; set; }
        public decimal? Price { get; set; }
        public int LessonQty { get; set; }
        public int TimeMin { get; set; }
        public string Image { get; set; }
    }
}
