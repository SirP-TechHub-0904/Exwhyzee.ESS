using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Exwhyzee.ESS.Models.Entities
{
    public class SessionAnalysis
    {
        public int Id { get; set; }

        [Display(Name="Enrollment Count")]
        public int EnrollmentCount { get; set; }

        [Display(Name = "Staff Count")]
        public int StaffCount { get; set; }

        [Display(Name = "Card Used Count")]
        public int CardUsedCount { get; set; }

        [Display(Name = "Results Count")]
        public int ResultsCount { get; set; }

        [Display(Name = "Cum. Results Count")]
        public int CumulativeResultsCount { get; set; }

        [Display(Name = "Portal Url")]
        public string PortalUrl { get; set; }


        [Display(Name = "Session Id")]
        public string SessionId { get; set; }

        [Display(Name = "Last Date Analysed")]
        public DateTime LastDateAnalysed { get; set; }

        [Display(Name = "Session")]
        public string Session { get; set; }

        [Display(Name = "Term")]
        public string Term { get; set; }
    }
}