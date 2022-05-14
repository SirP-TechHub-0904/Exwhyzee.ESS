using Exwhyzee.ESS.Models.UserProfile;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Exwhyzee.ESS.Models.Entities
{
    public class TeacherWallet
    {

        public TeacherWallet()
        {
            this.Amount = 0;
        }

        public int Id { get; set; }

        public decimal? Amount { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        [ForeignKey("Teacher")]
        public long? TeacherId { get; set; }
        public UserProfileModel Teacher { get; set; }
    }
}