using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Exwhyzee.ESS.Models;
using PagedList;

namespace Exwhyzee.ESS.Areas.Edu.Controllers
{
    [Authorize(Roles = "SuperAdmin,Admin,Content")]

    public class QuestionMainsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Edu/QuestionMains
     
        public async Task<ActionResult> Index(string searchString, string currentFilter, int? page)
        {
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            var items = db.QuestionMain.Include(q => q.QuestionCategory).Where(x => x.TextQ != "");
            if (!String.IsNullOrEmpty(searchString))
            {

                items = items.Where(s => s.TextQ.ToUpper().Contains(searchString.ToUpper()) ||
                               s.QuestionCategory.Name.ToUpper().Contains(searchString.ToUpper())
                                                              );
            }

            int pageSize = 50;
            int pageNumber = (page ?? 1);
            ViewBag.Total = items.Count();
            return View(items.OrderByDescending(x => x.Id).ToPagedList(pageNumber, pageSize));
        }

            // GET: Edu/QuestionMains/Details/5
            public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuestionMain questionMain = await db.QuestionMain.FindAsync(id);
            if (questionMain == null)
            {
                return HttpNotFound();
            }
            return View(questionMain);
        }

        // GET: Edu/QuestionMains/Create
        public ActionResult Create()
        {
            ViewBag.QuestionCategoryId = new SelectList(db.QuestionCategories, "Id", "Name");
            return View();
        }

        // POST: Edu/QuestionMains/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(QuestionMain questionMain)
        {
            if (ModelState.IsValid)
            {
                db.QuestionMain.Add(questionMain);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.QuestionCategoryId = new SelectList(db.QuestionCategories, "Id", "Name", questionMain.QuestionCategoryId);
            return View(questionMain);
        }

        // GET: Edu/QuestionMains/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuestionMain questionMain = await db.QuestionMain.FindAsync(id);
            if (questionMain == null)
            {
                return HttpNotFound();
            }
            ViewBag.QuestionCategoryId = new SelectList(db.QuestionCategories, "Id", "Name", questionMain.QuestionCategoryId);
            return View(questionMain);
        }

        // POST: Edu/QuestionMains/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(QuestionMain questionMain)
        {
            if (ModelState.IsValid)
            {
                db.Entry(questionMain).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.QuestionCategoryId = new SelectList(db.QuestionCategories, "Id", "Name", questionMain.QuestionCategoryId);
            return View(questionMain);
        }

        // GET: Edu/QuestionMains/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuestionMain questionMain = await db.QuestionMain.FindAsync(id);
            if (questionMain == null)
            {
                return HttpNotFound();
            }
            return View(questionMain);
        }

        // POST: Edu/QuestionMains/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            QuestionMain questionMain = await db.QuestionMain.FindAsync(id);
            db.QuestionMain.Remove(questionMain);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
