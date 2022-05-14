using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Exwhyzee.ESS.Models.Entities
{
    public class Question
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public DateTime Date { get; set; }
        public string PhoneNumber { get; set; }
        public string SchoolName { get; set; }
        public string State { get; set; }
        public string QuestionContent { get; set; }
    }
}