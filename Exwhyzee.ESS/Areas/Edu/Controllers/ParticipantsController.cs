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

    public class ParticipantsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Edu/Participants
        public async Task<ActionResult> Index()
        {
            return View(await db.Participants.Include(x=>x.ParticipantResults).ToListAsync());
        }
       

        // GET: Edu/Participants/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Participant participant = await db.Participants.FindAsync(id);
            if (participant == null)
            {
                return HttpNotFound();
            }
            return View(participant);
        }

        // GET: Edu/Participants/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Edu/Participants/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,School,Email,Phone,Date")] Participant participant)
        {
            if (ModelState.IsValid)
            {
                db.Participants.Add(participant);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(participant);
        }

        // GET: Edu/Participants/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Participant participant = await db.Participants.FindAsync(id);
            if (participant == null)
            {
                return HttpNotFound();
            }
            return View(participant);
        }

        // POST: Edu/Participants/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,School,Email,Phone,Date")] Participant participant)
        {
            if (ModelState.IsValid)
            {
                db.Entry(participant).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(participant);
        }

        // GET: Edu/Participants/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Participant participant = await db.Participants.FindAsync(id);
            if (participant == null)
            {
                return HttpNotFound();
            }
            return View(participant);
        }

        // POST: Edu/Participants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Participant participant = await db.Participants.FindAsync(id);
            db.Participants.Remove(participant);
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
