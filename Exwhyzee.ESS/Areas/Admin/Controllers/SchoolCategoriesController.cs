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

namespace Exwhyzee.ESS.Areas.Admin.Controllers
{
    public class SchoolCategoriesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/SchoolCategories
        public async Task<ActionResult> Index()
        {
            return View(await db.SchoolCategories.ToListAsync());
        }

        // GET: Admin/SchoolCategories/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SchoolCategory schoolCategory = await db.SchoolCategories.FindAsync(id);
            if (schoolCategory == null)
            {
                return HttpNotFound();
            }
            return View(schoolCategory);
        }

        // GET: Admin/SchoolCategories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/SchoolCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name")] SchoolCategory schoolCategory)
        {
            if (ModelState.IsValid)
            {
                db.SchoolCategories.Add(schoolCategory);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(schoolCategory);
        }

        // GET: Admin/SchoolCategories/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SchoolCategory schoolCategory = await db.SchoolCategories.FindAsync(id);
            if (schoolCategory == null)
            {
                return HttpNotFound();
            }
            return View(schoolCategory);
        }

        // POST: Admin/SchoolCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name")] SchoolCategory schoolCategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(schoolCategory).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(schoolCategory);
        }

        // GET: Admin/SchoolCategories/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SchoolCategory schoolCategory = await db.SchoolCategories.FindAsync(id);
            if (schoolCategory == null)
            {
                return HttpNotFound();
            }
            return View(schoolCategory);
        }

        // POST: Admin/SchoolCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            SchoolCategory schoolCategory = await db.SchoolCategories.FindAsync(id);
            db.SchoolCategories.Remove(schoolCategory);
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
