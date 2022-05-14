using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Exwhyzee.ESS.Models.Profile
{
  public class TrainingAndWorkShopModel
    {
        public long Id { get; set; }
        public string Title { get; set; }
       
        public string Abbr { get; set; }
        public string Location { get; set; }
        public string Date { get; set; }
       
        public long UserProfileId { get; set; }

    }
}
