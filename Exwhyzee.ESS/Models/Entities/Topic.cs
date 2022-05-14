using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Exwhyzee.ESS.Models.Entities
{
    public class Topic
    {
       
        public int Id { get; set; }
        public string VideoId { get; set; }
        public string Title { get; set; }
        public string VideoUrl { get; set; }
        public string VideoFile { get; set; }
        public string VideoCover { get; set; }
        public DateTime Date { get; set; }
        public bool Publish { get; set; }
        public int SubjectId { get; set; }
        public Subject Subject { get; set; }
        public int SortOrder { get; set; }
        public int Views { get; set; }
        public string Duration { get; set; }
        public string Description { get; set; }
        public string DescriptionTitle { get; set; }
        public string UserId { get; set; }
        public bool Approved { get; set; }

        public ICollection<TopicReview> TopicReview { get; set; }
    }
}