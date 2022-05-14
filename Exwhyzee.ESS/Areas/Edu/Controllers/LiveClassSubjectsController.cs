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
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class LiveClassSubjectsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Edu/LiveClassSubjects
        public async Task<ActionResult> Index()
        {
            var liveClassSubject = db.LiveClassSubject.Include(l => l.LiveClassLevel);
            return View(await liveClassSubject.ToListAsync());
        }

        // GET: Edu/LiveClassSubjects/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LiveClassSubject liveClassSubject = await db.LiveClassSubject.FindAsync(id);
            if (liveClassSubject == null)
            {
                return HttpNotFound();
            }
            return View(liveClassSubject);
        }

        // GET: Edu/LiveClassSubjects/Create
        public ActionResult Create()
        {
            ViewBag.LiveClassLevelId = new SelectList(db.LiveClassLevel, "Id", "Name");
            return View();
        }

        // POST: Edu/LiveClassSubjects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(LiveClassSubject liveClassSubject)
        {
            if (ModelState.IsValid)
            {
                db.LiveClassSubject.Add(liveClassSubject);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.LiveClassLevelId = new SelectList(db.LiveClassLevel, "Id", "Name", liveClassSubject.LiveClassLevelId);
            return View(liveClassSubject);
        }

        // GET: Edu/LiveClassSubjects/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LiveClassSubject liveClassSubject = await db.LiveClassSubject.FindAsync(id);
            if (liveClassSubject == null)
            {
                return HttpNotFound();
            }
            ViewBag.LiveClassLevelId = new SelectList(db.LiveClassLevel, "Id", "Name", liveClassSubject.LiveClassLevelId);
            return View(liveClassSubject);
        }

        // POST: Edu/LiveClassSubjects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(LiveClassSubject liveClassSubject)
        {
            if (ModelState.IsValid)
            {
                db.Entry(liveClassSubject).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.LiveClassLevelId = new SelectList(db.LiveClassLevel, "Id", "Name", liveClassSubject.LiveClassLevelId);
            return View(liveClassSubject);
        }

        // GET: Edu/LiveClassSubjects/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LiveClassSubject liveClassSubject = await db.LiveClassSubject.FindAsync(id);
            if (liveClassSubject == null)
            {
                return HttpNotFound();
            }
            return View(liveClassSubject);
        }

        // POST: Edu/LiveClassSubjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            LiveClassSubject liveClassSubject = await db.LiveClassSubject.FindAsync(id);
            db.LiveClassSubject.Remove(liveClassSubject);
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
