using Exwhyzee.ESS.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Exwhyzee.ESS.Models.Entities
{
    public class ApiSessionList
    {
        public int Id { get; set; }

        [Display(Name = "Full Session")]

        public string FullSession { get; set; }

        [Display(Name = "Session Status")]

        public SessionStatus SessionStatus { get; set; }

        [Display(Name = "Session")]
        public string Session { get; set; }

        [Display(Name = "Term")]
        public string Term { get; set; }
    }

    public class ApiResultBySessionList
    {
        public string ClassCount { get; set; }

        public string CurrentSession { get; set; }
        public string EnrolStudentsCount { get; set; }
        public string TotalCummulativeResults { get; set; }
        public string TotalResults { get; set; }
        
    }

    public class ApiDMMMResultBySessionList
    {
        public string School { get; set; }

        public string CurrentSession { get; set; }
        public string TotalResult { get; set; }
        public string TotalStudent { get; set; }

    }
}