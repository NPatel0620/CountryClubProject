using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CountryClubProject.Models
{
    public class SignInViewModel
    {
        [System.ComponentModel.DataAnnotations.Required]
        [System.ComponentModel.DataAnnotations.MinLength(6, ErrorMessage = "Username should be at least 6 characters")]
        [System.ComponentModel.DataAnnotations.MaxLength(15, ErrorMessage = "Password be no more than 15 characters")]
        public string UserName { get; set; }
        //adding the rule keeps you from having to use a ton of if loops
        [System.ComponentModel.DataAnnotations.Required]
        public string Password { get; set; }
    }
}
