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

namespace Exwhyzee.ESS.Areas.Admin.Controllers
{
    public class SchoolSessionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/SchoolSessions
        public async Task<ActionResult> Index()
        {
            return View(await db.SchoolSessions.ToListAsync());
        }

        // GET: Admin/SchoolSessions/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SchoolSession schoolSession = await db.SchoolSessions.FindAsync(id);
            if (schoolSession == null)
            {
                return HttpNotFound();
            }
            return View(schoolSession);
        }

        // GET: Admin/SchoolSessions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/SchoolSessions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Session")] SchoolSession schoolSession)
        {
            if (ModelState.IsValid)
            {
                db.SchoolSessions.Add(schoolSession);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(schoolSession);
        }

        // GET: Admin/SchoolSessions/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SchoolSession schoolSession = await db.SchoolSessions.FindAsync(id);
            if (schoolSession == null)
            {
                return HttpNotFound();
            }
            return View(schoolSession);
        }

        // POST: Admin/SchoolSessions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Session")] SchoolSession schoolSession)
        {
            if (ModelState.IsValid)
            {
                db.Entry(schoolSession).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(schoolSession);
        }

        // GET: Admin/SchoolSessions/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SchoolSession schoolSession = await db.SchoolSessions.FindAsync(id);
            if (schoolSession == null)
            {
                return HttpNotFound();
            }
            return View(schoolSession);
        }

        // POST: Admin/SchoolSessions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            SchoolSession schoolSession = await db.SchoolSessions.FindAsync(id);
            db.SchoolSessions.Remove(schoolSession);
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
