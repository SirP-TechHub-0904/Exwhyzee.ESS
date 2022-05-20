using Exwhyzee.ESS.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Exwhyzee.ESS.Models
{
    public class SchoolCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<SchoolPortalData> SchoolPortalDatas { get; set; }
    }
}