using Exwhyzee.ESS.Models.Profile;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Exwhyzee.ESS.Models.Entities
{
    public class ZoomStudentJoinedRecord
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        [ForeignKey("StudentModel")]
        public int? StudentModelId { get; set; }
        public StudentModel StudentModel { get; set; }
    }
}