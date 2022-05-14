using Exwhyzee.ESS.Models.Profile;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Exwhyzee.ESS.Models.Entities
{
    public class LiveClassSubjectEnrollment
    {
        public int Id { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        [ForeignKey("StudentModel")]
        public int? StudentModelId { get; set; }
        public StudentModel StudentModel { get; set; }

        [ForeignKey("LiveClassSubject")]
        public int? LiveClassSubjectId { get; set; }
        public LiveClassSubject LiveClassSubject { get; set; }

        [ForeignKey("LiveClassLevel")]
        public int? LiveClassLevelId { get; set; }
        public LiveClassLevel LiveClassLevel { get; set; }

        public bool Status { get; set; }
    }
}