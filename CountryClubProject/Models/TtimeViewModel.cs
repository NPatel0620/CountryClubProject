using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CountryClubProject.Models
{
    public class TtimeViewModel
    {
        public TtimeViewModel()
        {
            var slot = 8;
            DateTime today = DateTime.Now.Date.AddMinutes(20);
            Groups = new GroupViewModel[10];
            for (int i = 0; i < 10; i++)
            {
                Groups[i] = new GroupViewModel { Time = today.AddHours(slot++) };

            }
        }

        public GroupViewModel[] Groups { get; set; }
    }
}
