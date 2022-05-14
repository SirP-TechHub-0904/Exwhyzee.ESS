using Exwhyzee.ESS.Models.UserProfile;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Exwhyzee.ESS.Models.Entities
{
    public class TeacherLiveSubjectAssignment
    {
        public int Id { get; set; }

        [ForeignKey("Subject")]
        public int? SubjectId { get; set; }
        public LiveClassSubject Subject { get; set; }

        [ForeignKey("Teacher")]
        public long? TeacherId { get; set; }
        public UserProfileModel Teacher { get; set; }

        //[ForeignKey("User")]
        //public string UserId { get; set; }
        //public ApplicationUser User { get; set; }


        [ForeignKey("Class")]
        public int? ClassId { get; set; }
        public LiveClassLevel Class { get; set; }
    }
}