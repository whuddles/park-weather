using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Web.Models
{
    public class Survey
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Favorite National Park is required")]
        public string FavoriteNationalPark { get; set; }

        [Required(ErrorMessage = "State of residence is required")]
        public string State { get; set; }

        [Required(ErrorMessage = "Activity level is required")]
        public string ActivityLevel { get; set; }
        
    }
}
