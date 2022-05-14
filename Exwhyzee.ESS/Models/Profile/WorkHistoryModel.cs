using System;
using System.Collections.Generic;
using System.Text;

namespace Exwhyzee.ESS.Models.ProfileWorkHistory
{
  public class WorkHistoryModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string VideoUrl { get; set; }
        public string Location { get; set; }
        public long UserProfileId { get; set; }

    }
}
