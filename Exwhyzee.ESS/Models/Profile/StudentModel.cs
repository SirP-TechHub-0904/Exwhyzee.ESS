using Exwhyzee.ESS.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Exwhyzee.ESS.Models.Profile
{
    public class StudentModel
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Surname")]
        public string SurName { get; set; }

        [Display(Name = "First name")]
        [Required]
        public string FirstName { get; set; }


        [Display(Name = "Other name")]
        public string OtherName { get; set; }

        public string FullName
        {
            get
            {
                return SurName + " " + FirstName + " " + OtherName;
            }
        }

        [Required]
        [Display(Name = "Mobile Number")]
        public string PhoneNumber { get; set; }

        [Required]
        [Display(Name = "Parent Mobile Number")]
        public string ParentPhoneNumber { get; set; }


        [ForeignKey("ClassLevel")]
        public int? ClassLevelId { get; set; }
        public LiveClassLevel ClassLevel { get; set; }

        public DateTime? DateRegistered { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public string IskoolId { get; set; }
        public string Picture { get; set; }
    }
}