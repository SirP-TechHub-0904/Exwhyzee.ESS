using Exwhyzee.ESS.Models.Profile;
using Exwhyzee.ESS.Models.UserProfile;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace Exwhyzee.ESS.Models.Entities
{
    public class Subscription
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Email { get; set; }

        public decimal? Amount { get; set; }

        [ForeignKey("ApprovedBy")]
        public long? ApprovedbyId { get; set; }
        public UserProfileModel ApprovedBy { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public SubscriptionStatus Status { get; set; }

        public SubscriptionSource Source { get; set; }

        [Display(Name = "Reference Id")]
        public string ReferenceId { get; set; }

        //[ForeignKey("LiveClassSubject")]
        //public int? LiveClassSubjectId { get; set; }
        //public LiveClassSubject LiveClassSubject { get; set; }

        [ForeignKey("StudentModel")]
        public int? StudentModelId { get; set; }
        public StudentModel StudentModel { get; set; }

        [ForeignKey("ClassLevel")]
        public int? ClassLevelId { get; set; }
        public LiveClassLevel ClassLevel { get; set; }
    }
}