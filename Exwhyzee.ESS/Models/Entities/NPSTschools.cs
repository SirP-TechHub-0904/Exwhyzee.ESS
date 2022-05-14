using Exwhyzee.ESS.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Exwhyzee.ESS.Models.Entities
{
    public class NPSTschools
    {
        public int Id { get; set; }
        public string SchoolName {get;set;}
        public string ContactAddress {get;set;}
        public string EmailAddress { get;set;}
        public string Phone {get;set; }
        public string WebsiteLink {get;set; }
        public string HeadName {get;set; }
        public int PopulationSize { get;set; }
        public string Zone {get;set; }
        public string State {get;set;}        
        public string LGA {get;set;}
        public SchoolType Type { get; set; }
        public string About { get; set; }
        public DateTime DateAdded { get; set; }

    }
}