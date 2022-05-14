using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Exwhyzee.ESS.Models.Profile
{
    public class SubjectSpecialistModel
    {
        public int Id { get; set; }
        public string Subject { get; set; }

        public long UserProfileId { get; set; }
    }
}