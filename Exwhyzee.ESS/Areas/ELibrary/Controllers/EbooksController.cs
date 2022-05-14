using Exwhyzee.ESS.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Exwhyzee.ESS.Areas.ELibrary.Controllers
{

    public class EbooksController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ELibrary/Ebooks
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult IFrameView()
        {
            return View();
        }
        public async Task<ActionResult> Details(int? id, string title)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var book = await db.Books.Include(x=>x.BookCategory).FirstOrDefaultAsync(x=>x.Id == id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        public ActionResult _CategoryMenu()
        {
            var booksmenu = db.BookCategories.Include(x=>x.Books).Where(x => x.IsActive == true).ToList();
            return PartialView(booksmenu);
        }

    }
}