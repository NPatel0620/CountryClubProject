using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CountryClubProject.Models
{
    public class RegisterViewModel
    {
        [Required]
        [MinLength(6, ErrorMessage ="Username should be at least 6 characters")]
        [MaxLength(15, ErrorMessage = "Password be no more than 15 characters")]
        public string UserName { get; set; }
        //adding the rule keeps you from having to use a ton of if loops
        [Required]
        public string Password { get; set; }

        [Required]
        //[EmailAddress]
        public string Email { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
    }
}
