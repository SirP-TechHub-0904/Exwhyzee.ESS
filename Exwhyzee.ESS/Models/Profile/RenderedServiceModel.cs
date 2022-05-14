using System;
using System.Collections.Generic;
using System.Text;

namespace Exwhyzee.ESS.Models.ProfileRenderedService
{
  public class RenderedServiceModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public long UserProfileId { get; set; }

    }
}
