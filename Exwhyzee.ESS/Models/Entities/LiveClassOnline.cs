using Exwhyzee.ESS.Models.UserProfile;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Exwhyzee.ESS.Models.Entities
{
    public class LiveClassOnline
    {
        public int Id { get; set; }
        public int LiveClassLevelId { get; set; }
        public LiveClassLevel ClassLevel { get; set; }
        public string Subject { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public string UrlLive { get; set; }

        public DateTime DateCreated { get; set; }
        public string ClassDate { get; set; }
        public string ClassTime { get; set; }
        public string Duration { get; set; }
        public string TeacherName { get; set; }
        public string Description { get; set; }

        public LiveStatus LiveStatus { get; set; }
    }
}