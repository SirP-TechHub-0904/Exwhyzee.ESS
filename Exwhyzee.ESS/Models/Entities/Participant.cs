using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Exwhyzee.ESS.Models.Entities
{
    public class Participant
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string School { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime Date { get; set; }

      public ICollection<ParticipantResult> ParticipantResults { get; set; }
        
    }
}