using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Exwhyzee.ESS.Models.ProfileExperience
{
  public class ExperienceModel
    {
        public long Id { get; set; }
       
        public string Title { get; set; }

        public string ExperienceType { get; set; }

        public string Address { get; set; }
        [Display(Name = "Start Date")]
        public string StartDate { get; set; }
        [Display(Name = "End Date")]
        public string EndDate { get; set; }

        public string Description { get; set; }

        public bool IsCurrent { get; set; }
        public long UserProfileId { get; set; }

    }
}
