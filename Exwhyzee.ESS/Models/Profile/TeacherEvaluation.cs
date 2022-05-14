using Exwhyzee.ESS.Models.UserProfile;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Linq;
using System.Web;

namespace Exwhyzee.ESS.Models.Profile
{
    public class TeacherEvaluation
    {
        public int Id { get; set; }

        [Display(Name = "Subject of Interest")]
        public string Subject1 { get; set; }

        [Display(Name = "Subject of Interest")]
        public string Subject2 { get; set; }

        [Display(Name = "Subject of Interest")]
        public string Subject3 { get; set; }

        [Display(Name = "Qualification")]
        public string Qualification { get; set; }

        [Display(Name = "Qualification Front")]
        public string QualificationFront { get; set; }

        [Display(Name = "Qualification Back")]
        public string QualificationBack { get; set; }

        [Display(Name = "Youtube Sample Video")]
        public string YouTubeVideo { get; set; }

        [ForeignKey("UserProfile")]
        public long? UserProfileId { get; set; }
        public UserProfileModel UserProfile { get; set; }
    }
}