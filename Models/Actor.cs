using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieBookingApp.Models
{
    public class Actor
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Profile Picture URL")]
        [Required(ErrorMessage = "Prfile Picture Url required")]
        public string ProfilePictureURL { get; set; }
        [Display(Name = "Full Name")]
        [Required(ErrorMessage = "Full Name required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name length should be min 3 and max 50 charectors")]
        public string FullName { get; set; }
        [Display(Name = "Biography")]
        [Required(ErrorMessage = "Biography required")]
        public string Bio { get; set; }

        //Relation
        public List<Actor_Movie> Actor_Movies { get; set; }
    }
}
