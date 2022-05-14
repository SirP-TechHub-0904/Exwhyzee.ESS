using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Exwhyzee.ESS.Models.Entities.Dto
{
    public class CommentDto
    {
       public string RId { get; set; }
      public string Review { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Rate1 { get; set; }
        public string Rate2 { get; set; }
        public string Rate3 { get; set; }
        public string Rate4 { get; set; }
        public string Rate5 { get; set; }
    }
}