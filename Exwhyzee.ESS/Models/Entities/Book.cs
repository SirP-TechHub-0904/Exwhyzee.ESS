using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Exwhyzee.ESS.Models.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        [AllowHtml]
        public string Description { get; set; }
        public DateTime Date { get; set; }
        [AllowHtml]
        public string path { get; set; } 
        [Display(Name = "Book Cover Image")]
        public byte[] BookImagePath { get; set; }
        [AllowHtml]
        [Display(Name = "Author Information")]

        public string AuthorInformation { get; set; }
        [Display(Name = "Is Active")]

        public bool IsActive { get; set; }
        [Display(Name = "Book Category")]

        public int BookCategoryId { get; set; }

        public BookCategory BookCategory { get; set; }
    }
}