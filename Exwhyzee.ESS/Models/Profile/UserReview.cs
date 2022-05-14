using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Exwhyzee.ESS.Models.Profile
{
    public class UserReview
    {
        public int Id { get; set; }
        public string Review { get; set; }
        public string UserId { get; set; }
        public string ReviewerId { get; set; }
        public int Rating { get; set; }
        public DateTime? DateReviewed { get; set; }
    }
}