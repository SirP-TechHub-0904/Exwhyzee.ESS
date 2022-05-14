using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Exwhyzee.ESS.Models.Entities.Dto
{
    public class SchoolListingDto
    {
        public int Position { get; set; }
        public string Name { get; set; }
        public string SchoolType { get; set; }
        public string State { get; set; }
        public string IT { get; set; }
        public string Website { get; set; }
    }
}