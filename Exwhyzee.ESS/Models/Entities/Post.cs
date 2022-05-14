using Exwhyzee.ESS.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Exwhyzee.ESS.Models.Entities
{
    public class Post
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        [AllowHtml]
        public string Content { get; set; }

        [Required]
        [Display(Name = "Preview Content")]
        public string PreviewContent { get; set; }

        [Required]
        [Display(Name = "Date Posted")]
        public DateTime DatePosted { get; set; }

        [UIHint("Enum")]
        public PostStatus Status { get; set; }

        [UIHint("Enum")]
        [Display(Name = "Who Can See This Post")]
        public WhoSeePost WhoCanSeePost { get; set; }

        [Display(Name = "Posted By")]
        public string PostedBy { get; set; }

        [Display(Name = "Sort Order")]
        public int? SortOrder { get; set; }

        [Display(Name = "Post Image")]
        public string PostImage { get; set; }

    }
}