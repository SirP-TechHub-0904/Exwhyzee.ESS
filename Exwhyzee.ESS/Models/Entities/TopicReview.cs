using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Exwhyzee.ESS.Models.Entities
{
    public class TopicReview
    {
        public int Id { get; set; }
        public string Review { get; set; }
        public DateTime? DateReview { get; set; }
        public int? Rating { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int? TopicId { get; set; }
        public Topic Topic { get; set; }
    }
}