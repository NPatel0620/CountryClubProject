using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CountryClubProject.Models
{
    public class GroupViewModel
    {

        public GroupViewModel()
        {
            this.Players = new string[]{ "", "", "", ""};
        }

        public DateTime Time { get; set; }
        public string[] Players { get; set; }
        public string Caddy { get; set; }
    }
}
