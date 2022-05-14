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

    public class SchoolRankingsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Edu/SchoolRankings
        public async Task<ActionResult> Index()
        {
            return View(await db.SchoolRankings.ToListAsync());
        }

        // GET: Edu/SchoolRankings/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SchoolRanking schoolRanking = await db.SchoolRankings.FindAsync(id);
            if (schoolRanking == null)
            {
                return HttpNotFound();
            }
            return View(schoolRanking);
        }

        // GET: Edu/SchoolRankings/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Edu/SchoolRankings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(SchoolRanking schoolRanking)
        {
            if (ModelState.IsValid)
            {
                db.SchoolRankings.Add(schoolRanking);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(schoolRanking);
        }

        // GET: Edu/SchoolRankings/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SchoolRanking schoolRanking = await db.SchoolRankings.FindAsync(id);
            if (schoolRanking == null)
            {
                return HttpNotFound();
            }
            return View(schoolRanking);
        }

        // POST: Edu/SchoolRankings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(SchoolRanking schoolRanking)
        {
            if (ModelState.IsValid)
            {
                db.Entry(schoolRanking).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(schoolRanking);
        }

        // GET: Edu/SchoolRankings/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SchoolRanking schoolRanking = await db.SchoolRankings.FindAsync(id);
            if (schoolRanking == null)
            {
                return HttpNotFound();
            }
            return View(schoolRanking);
        }

        // POST: Edu/SchoolRankings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            SchoolRanking schoolRanking = await db.SchoolRankings.FindAsync(id);
            db.SchoolRankings.Remove(schoolRanking);
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
