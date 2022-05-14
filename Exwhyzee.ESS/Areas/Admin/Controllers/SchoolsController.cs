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
    [Authorize(Roles = "SuperAdmin,Admin")]

    public class SchoolsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/Schools
        public async Task<ActionResult> Index()
        {
            return View(await db.NPSTschools.ToListAsync());
        }

        // GET: Admin/Schools/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NPSTschools nPSTschools = await db.NPSTschools.FindAsync(id);
            if (nPSTschools == null)
            {
                return HttpNotFound();
            }
            return View(nPSTschools);
        }

        // GET: Admin/Schools/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Schools/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(NPSTschools nPSTschools)
        {
            if (ModelState.IsValid)
            {
                nPSTschools.DateAdded = DateTime.UtcNow.AddHours(1);
                db.NPSTschools.Add(nPSTschools);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(nPSTschools);
        }

        // GET: Admin/Schools/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NPSTschools nPSTschools = await db.NPSTschools.FindAsync(id);
            if (nPSTschools == null)
            {
                return HttpNotFound();
            }
            return View(nPSTschools);
        }

        // POST: Admin/Schools/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,SchoolName,ContactAddress,EmailAddress,Phone,WebsiteLink,HeadName,PopulationSize,Zone,State,LGA,Type,About")] NPSTschools nPSTschools)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nPSTschools).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(nPSTschools);
        }

        // GET: Admin/Schools/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NPSTschools nPSTschools = await db.NPSTschools.FindAsync(id);
            if (nPSTschools == null)
            {
                return HttpNotFound();
            }
            return View(nPSTschools);
        }

        // POST: Admin/Schools/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            NPSTschools nPSTschools = await db.NPSTschools.FindAsync(id);
            db.NPSTschools.Remove(nPSTschools);
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
