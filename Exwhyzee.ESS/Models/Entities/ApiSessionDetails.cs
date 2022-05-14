using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Exwhyzee.ESS.Models.Entities
{
    public class ApiSessionDetails
    {
        [Display(Name = "School Current Principal")]
        public string SchoolCurrentPrincipal { get; set; }

        [Display(Name = "Class Count")]
        public string ClassCount { get; set; }
        [Display(Name = "Enrol Students Count")]
        public string EnrolStudentsCount { get; set; }

        [Display(Name = "UnEnrol Students Count")]
        public string UnEnrolStudentsCount { get; set; }

        [Display(Name = "Used card")]
        public string Usedcard { get; set; }

        [Display(Name = "Total Staff")]
        public string TotalStaff { get; set; }

        [Display(Name = "Current Session")]
        public string CurrentSession { get; set; }

        [Display(Name = "Total Results")]
        public string TotalResults { get; set; }

        [Display(Name = "Total Cummulative Results")]
        public string TotalCummulativeResults { get; set; }

        [Display(Name = "Session")]
        public string Session { get; set; }

        [Display(Name = "Term")]
        public string Term { get; set; }
    }
}