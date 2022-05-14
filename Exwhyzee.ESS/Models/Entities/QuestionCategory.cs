using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Exwhyzee.ESS.Models.Entities
{
    public class QuestionCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<QuestionMain> QuestionMains { get; set; }
    }
}