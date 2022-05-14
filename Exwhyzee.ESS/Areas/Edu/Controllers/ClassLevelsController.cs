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
using PagedList;

namespace Exwhyzee.ESS.Areas.Edu.Controllers
{
    [Authorize(Roles = "SuperAdmin,Admin,Content")]

    public class ClassLevelsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Edu/ClassLevels
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
            var items = db.ClassLevels.Where(x => x.Name != "");
            if (!String.IsNullOrEmpty(searchString))
            {

                items = items.Where(s => s.Name.ToUpper().Contains(searchString.ToUpper()));

            }

            int pageSize = 50;
            int pageNumber = (page ?? 1);
            ViewBag.Total = items.Count();
            return View(items.OrderByDescending(x=>x.Id).ToPagedList(pageNumber, pageSize));
        }
        // GET: Edu/ClassLevels/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClassLevel classLevel = await db.ClassLevels.FindAsync(id);
            if (classLevel == null)
            {
                return HttpNotFound();
            }
            return View(classLevel);
        }

        // GET: Edu/ClassLevels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Edu/ClassLevels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ClassLevel classLevel)
        {
            if (ModelState.IsValid)
            {
                db.ClassLevels.Add(classLevel);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(classLevel);
        }

        // GET: Edu/ClassLevels/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClassLevel classLevel = await db.ClassLevels.FindAsync(id);
            if (classLevel == null)
            {
                return HttpNotFound();
            }
            return View(classLevel);
        }

        // POST: Edu/ClassLevels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ClassLevel classLevel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(classLevel).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(classLevel);
        }

        // GET: Edu/ClassLevels/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClassLevel classLevel = await db.ClassLevels.FindAsync(id);
            if (classLevel == null)
            {
                return HttpNotFound();
            }
            return View(classLevel);
        }

        // POST: Edu/ClassLevels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ClassLevel classLevel = await db.ClassLevels.FindAsync(id);
            db.ClassLevels.Remove(classLevel);
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
