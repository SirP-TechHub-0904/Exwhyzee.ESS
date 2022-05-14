using Exwhyzee.ESS.Models.UserProfile;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Exwhyzee.ESS.Models.Entities
{
    public class TeacherWithdrawal
    {
        public int Id { get; set; }
        public decimal? Amount { get; set; }
        public string AccountName { get; set; }
        public string AccountNumber { get; set; }
        public string BankName { get; set; }
        public DateTime? RequestDate { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public WithdrawalStatus Status { get; set; }

        [ForeignKey("Teacher")]
        public long? TeacherId { get; set; }
        public UserProfileModel Teacher { get; set; }

        [ForeignKey("ApprovedBy")]
        public long? ApprovedbyId { get; set; }
        public UserProfileModel ApprovedBy { get; set; }
    }
}