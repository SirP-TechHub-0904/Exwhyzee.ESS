using Exwhyzee.ESS.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Exwhyzee.ESS.Models.Entities
{
    public class Volunteer
    {
        public int Id { get; set; }
        public School SectionType { get; set; }

        public string CoverPhoto { get; set; }
        public string VideoLink { get; set; }

        public bool Approved { get; set; }

    }
}