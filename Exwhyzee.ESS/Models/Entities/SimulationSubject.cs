using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Exwhyzee.ESS.Models.Entities
{
    public class SimulationSubject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description {get; set;}

        public ICollection<SimulationCategory> SimulationCategorys { get; set; }
    }
}