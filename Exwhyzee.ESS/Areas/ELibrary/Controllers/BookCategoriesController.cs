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
using Exwhyzee.ESS.Models.Entities;

namespace Exwhyzee.ESS.Areas.ELibrary.Controllers
{
    [Authorize(Roles = "SuperAdmin,Admin,Content")]

    public class BookCategoriesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ELibrary/BookCategories
        public async Task<ActionResult> Index()
        {
            return View(await db.BookCategories.ToListAsync());
        }

        // GET: ELibrary/BookCategories/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookCategory bookCategory = await db.BookCategories.FindAsync(id);
            if (bookCategory == null)
            {
                return HttpNotFound();
            }
            return View(bookCategory);
        }

        // GET: ELibrary/BookCategories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ELibrary/BookCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(BookCategory bookCategory)
        {
            if (ModelState.IsValid)
            {
                bookCategory.Date = DateTime.UtcNow;
                db.BookCategories.Add(bookCategory);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(bookCategory);
        }

        // GET: ELibrary/BookCategories/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookCategory bookCategory = await db.BookCategories.FindAsync(id);
            if (bookCategory == null)
            {
                return HttpNotFound();
            }
            return View(bookCategory);
        }

        // POST: ELibrary/BookCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(BookCategory bookCategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bookCategory).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(bookCategory);
        }

        // GET: ELibrary/BookCategories/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookCategory bookCategory = await db.BookCategories.FindAsync(id);
            if (bookCategory == null)
            {
                return HttpNotFound();
            }
            return View(bookCategory);
        }

        // POST: ELibrary/BookCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            BookCategory bookCategory = await db.BookCategories.FindAsync(id);
            db.BookCategories.Remove(bookCategory);
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
