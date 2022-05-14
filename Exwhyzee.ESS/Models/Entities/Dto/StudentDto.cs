using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Exwhyzee.ESS.Models.Entities.Dto
{
    public class StudentDto
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Surname")]
        public string SurName { get; set; }

        [Required]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Display(Name = "Other name")]
        public string OtherName { get; set; }

      
        [Required]
        [Display(Name = "Mobile Number")]
        public string PhoneNumber { get; set; }

        [Required]
        [Display(Name = "Parent Mobile Number")]
        public string ParentPhoneNumber { get; set; }

        public string ClassLevelId { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public DateTime? DateRegistered { get; set; }

        public string Picture { get; set; }
    }
}