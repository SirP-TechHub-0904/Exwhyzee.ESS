using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Exwhyzee.ESS.Models.Entities
{
    public class SchoolPortalData
    {
        public int Id { get; set; }

        [Display(Name = "Site Name")]
        public string SiteName { get; set; }

        [Display(Name = "School Name")]
        public string SchoolName { get; set; }

        [Display(Name = "School Address")]
        public string SchoolAddress { get; set; }

        [Display(Name = "School Current Principal")]
        public string SchoolCurrentPrincipal { get; set; }

        [Display(Name = "Class Count")]
        public string ClassCount { get; set; }

        [Display(Name = "Enrol Students Count")]
        public string EnrolStudentsCount { get; set; }

        [Display(Name = "UnEnrol Students Count")]
        public string UnEnrolStudentsCount { get; set; }

        [Display(Name = "Total Students Count")]
        public string TotalStudentsCount { get; set; }

        [Display(Name = "Portal Url")]
        public string PortalUrl { get; set; }

        [Display(Name = "Web Url")]
        public string WebUrl { get; set; }

        [Display(Name = "Date Ceated")]
        public DateTime DateCeated { get; set; }

        [Display(Name = "Last Modified Date")]
        public DateTime? LastModifiedDate { get; set; }

        [Display(Name = "School Type")]
        public string SchoolType { get; set; }

        [Display(Name = "Used card")]
        public string Usedcard { get; set; }

        [Display(Name = "Non Used card")]
        public string NonUsedcard { get; set; }

        [Display(Name = "Total card")]
        public string Totalcard { get; set; }

        [Display(Name = "Total Staff")]
        public string TotalStaff { get; set; }

        [Display(Name = "Current Session")]
        public string CurrentSession { get; set; }

        [Display(Name = "Session")]
        public string Session { get; set; }

        [Display(Name = "Term")]
        public string Term { get; set; }

        [Display(Name = "Batch Result Printing")]
        public string BatchResultPrint { get; set; }

        [Display(Name = "Contact Phone")]
        public string ContactPhoneNumber { get; set; }
        public bool AddAsActive { get; set; }
        public bool SelectedAsActive { get; set; }
        public DateTime DateAdded { get; set; }

        public int? SchoolCategoryId { get; set; }
        public SchoolCategory SchoolCategory { get; set; }
    }
}