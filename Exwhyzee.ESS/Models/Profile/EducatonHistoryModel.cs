using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Exwhyzee.ESS.Models.ProfileEducationHistory
{
  public class EducationHistoryModel
    {
        public long Id { get; set; }
        [Display(Name= "School Attended")]
        public string SchoolAttended { get; set; }
       
        public string Course { get; set; }
        [Display(Name = "Start Date")]
        public string StartDate { get; set; }
        [Display(Name = "End Date")]
        public string EndDate { get; set; }
       
        public string Grade { get; set; }

        public bool IsCurrent { get; set; }
        public long UserProfileId { get; set; }

    }
}
