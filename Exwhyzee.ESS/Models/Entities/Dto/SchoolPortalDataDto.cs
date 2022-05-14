using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Exwhyzee.ESS.Models.Entities.Dto
{
    public class SchoolPortalDataDto
    {
        public int Id { get; set; }
        public string SiteName { get; set; }
        public string SchoolName { get; set; }
        public string SchoolAddress { get; set; }
        public string SchoolCurrentPrincipal { get; set; }
        public string ClassCount { get; set; }
        public string EnrolStudentsCount { get; set; }
        public string UnEnrolStudentsCount { get; set; }
        public string TotalStudentsCount { get; set; }
        public string PortalUrl { get; set; }
        public string Url { get; set; }
        public DateTime DateCeated { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public string SchoolType { get; set; }
        public string Usedcard { get; set; }
        public string NonUsedcard { get; set; }
        public string Totalcard { get; set; }
        public string TotalStaff { get; set; }
        public string CurrentSession { get; set; }
       
        public string Session { get; set; }
        public string Term { get; set; }
        public string BatchPrint { get; set; }
    }
}