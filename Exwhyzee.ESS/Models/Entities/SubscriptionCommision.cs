using Exwhyzee.ESS.Models.Profile;
using Exwhyzee.ESS.Models.UserProfile;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Exwhyzee.ESS.Models.Entities
{
    public class SubscriptionCommision
    {
        public int Id { get; set; }
        public decimal? Amount { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public decimal? TeacherCommission { get; set; }

        public decimal? SystemCommission { get; set; }

        [ForeignKey("Subject")]
        public int? SubjectId { get; set; }
        public LiveClassSubject Subject { get; set; }

        [ForeignKey("Teacher")]
        public long? TeacherId { get; set; }
        public UserProfileModel Teacher { get; set; }


        [ForeignKey("StudentModel")]
        public int? StudentModelId { get; set; }
        public StudentModel StudentModel { get; set; }

        [ForeignKey("ClassLevel")]
        public int? ClassLevelId { get; set; }
        public LiveClassLevel ClassLevel { get; set; }


        [ForeignKey("Subscription")]
        public int? SubscriptionId { get; set; }
        public Subscription Subscription { get; set; }
    }
}