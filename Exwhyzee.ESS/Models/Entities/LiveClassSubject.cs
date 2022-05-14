using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Exwhyzee.ESS.Models.Entities
{
    public class LiveClassSubject
    {
        public int Id { get; set; }
        public string Subject { get; set; }

        [ForeignKey("LiveClassLevel")]
        public int? LiveClassLevelId { get; set; }
        public LiveClassLevel LiveClassLevel { get; set; }
    }
}