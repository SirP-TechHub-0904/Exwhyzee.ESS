using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Exwhyzee.ESS.Models.Entities
{
    public class Testimony
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Message { get; set; }
        public DateTime Date { get; set; }
    }
}