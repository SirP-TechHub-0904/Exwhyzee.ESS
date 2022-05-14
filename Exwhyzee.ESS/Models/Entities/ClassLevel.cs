using Exwhyzee.ESS.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Exwhyzee.ESS.Models.Entities
{
    public class ClassLevel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public School SchoolType { get; set; }
        public ICollection<Subject> Subjects { get; set; }
    }
}