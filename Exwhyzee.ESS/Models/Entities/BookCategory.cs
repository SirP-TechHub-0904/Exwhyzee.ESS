using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Exwhyzee.ESS.Models.Entities
{
    public class BookCategory
    {
        public int Id { get; set; }
        public string Title { get; set; }
        [AllowHtml]
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public bool IsActive { get; set; }

        public ICollection<Book> Books { get; set; }
    }
}