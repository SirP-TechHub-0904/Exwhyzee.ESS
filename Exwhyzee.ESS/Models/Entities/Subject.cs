using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Exwhyzee.ESS.Models.Entities
{
    public class Subject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public byte[] CoverImage { get; set; }
        public int ClassLevelId { get; set; }
        public ClassLevel ClassLevel { get; set; }
        public ICollection<Topic> Topics { get; set; }

       
    }
}