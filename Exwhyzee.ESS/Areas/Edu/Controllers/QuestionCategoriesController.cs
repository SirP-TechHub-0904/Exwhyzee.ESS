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

namespace Exwhyzee.ESS.Areas.Edu.Controllers
{
    [Authorize(Roles = "SuperAdmin,Admin,Content")]

    public class QuestionCategoriesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Edu/QuestionCategories
        public async Task<ActionResult> Index()
        {
            return View(await db.QuestionCategories.ToListAsync());
        }

        // GET: Edu/QuestionCategories/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuestionCategory questionCategory = await db.QuestionCategories.FindAsync(id);
            if (questionCategory == null)
            {
                return HttpNotFound();
            }
            return View(questionCategory);
        }

        // GET: Edu/QuestionCategories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Edu/QuestionCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name")] QuestionCategory questionCategory)
        {
            if (ModelState.IsValid)
            {
                db.QuestionCategories.Add(questionCategory);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(questionCategory);
        }

        // GET: Edu/QuestionCategories/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuestionCategory questionCategory = await db.QuestionCategories.FindAsync(id);
            if (questionCategory == null)
            {
                return HttpNotFound();
            }
            return View(questionCategory);
        }

        // POST: Edu/QuestionCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name")] QuestionCategory questionCategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(questionCategory).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(questionCategory);
        }

        // GET: Edu/QuestionCategories/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuestionCategory questionCategory = await db.QuestionCategories.FindAsync(id);
            if (questionCategory == null)
            {
                return HttpNotFound();
            }
            return View(questionCategory);
        }

        // POST: Edu/QuestionCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            QuestionCategory questionCategory = await db.QuestionCategories.FindAsync(id);
            db.QuestionCategories.Remove(questionCategory);
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
