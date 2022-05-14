using Exwhyzee.ESS.Models.UserProfile;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Exwhyzee.ESS.Models.Entities
{
    public class OnlineZoom
    {
        [Key]
        public int Id { get; set; }
        public string HostEmail { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public ApplicationUser  User { get; set; }
        public long? MeetingId { get; set; }
        public string MeetingUUId { get; set; }
        public string ClassDate { get; set; }
        public string ClassTime { get; set; }
        public string Duration { get; set; }

        [ForeignKey("LiveClassLevel")]
        public int? LiveClassLevelId { get; set; }
        public LiveClassLevel LiveClassLevel { get; set; }

        [ForeignKey("LiveClassSubject")]
        public int? LiveClassSubjectId { get; set; }
        public LiveClassSubject LiveClassSubject { get; set; }

        public string Description { get; set; }
        public string ClassPassword { get; set; }
        public string MeetingType { get; set; }
        public DateTime DateCreated { get; set; }

    }
}