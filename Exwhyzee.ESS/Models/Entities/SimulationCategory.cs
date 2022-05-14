using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Exwhyzee.ESS.Models.Entities
{
    public class SimulationCategory
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public DateTime Date { get; set; }

        public int? SimulationSubjectId { get; set; }
        public SimulationSubject SimulationSubjects { get; set; }

        public ICollection<Simulation> Simulations { get; set; }
    }
}