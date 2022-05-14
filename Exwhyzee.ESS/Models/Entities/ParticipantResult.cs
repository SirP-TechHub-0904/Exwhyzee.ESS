using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Exwhyzee.ESS.Models.Entities
{
    public class ParticipantResult
    {
        public int Id { get; set; }
        public bool Won { get; set; }

        public bool Q1 { get; set; }
        public bool Q2 { get; set; }
        public bool Q3 { get; set; }
        public bool Q4 { get; set; }
        public bool Q5 { get; set; }
        public bool Recharged { get; set; }

        public Participant Participant { get; set; }
        public int ParticipantId { get; set; }
    }
}