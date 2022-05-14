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

    public class ParticipantResultsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Edu/ParticipantResults
        public async Task<ActionResult> Index()
        {
            var participantResults = db.ParticipantResults.Include(p => p.Participant);
            return View(await participantResults.ToListAsync());
        }

        public async Task<ActionResult> IndexList(int id)
        {
            var participantResults = db.ParticipantResults.Include(p => p.Participant).Where(x=>x.ParticipantId == id);
            return View(await participantResults.ToListAsync());
        }

        // GET: Edu/ParticipantResults/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ParticipantResult participantResult = await db.ParticipantResults.FindAsync(id);
            if (participantResult == null)
            {
                return HttpNotFound();
            }
            return View(participantResult);
        }

        // GET: Edu/ParticipantResults/Create
        public ActionResult Create()
        {
            ViewBag.ParticipantId = new SelectList(db.Participants, "Id", "Name");
            return View();
        }

        // POST: Edu/ParticipantResults/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Won,Q1,Q2,Q3,Q4,Q5,ParticipantId")] ParticipantResult participantResult)
        {
            if (ModelState.IsValid)
            {
                db.ParticipantResults.Add(participantResult);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ParticipantId = new SelectList(db.Participants, "Id", "Name", participantResult.ParticipantId);
            return View(participantResult);
        }

        // GET: Edu/ParticipantResults/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ParticipantResult participantResult = await db.ParticipantResults.FindAsync(id);
            if (participantResult == null)
            {
                return HttpNotFound();
            }
            ViewBag.ParticipantId = new SelectList(db.Participants, "Id", "Name", participantResult.ParticipantId);
            return View(participantResult);
        }

        // POST: Edu/ParticipantResults/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Won,Q1,Q2,Q3,Q4,Q5,ParticipantId")] ParticipantResult participantResult)
        {
            if (ModelState.IsValid)
            {
                db.Entry(participantResult).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ParticipantId = new SelectList(db.Participants, "Id", "Name", participantResult.ParticipantId);
            return View(participantResult);
        }

        // GET: Edu/ParticipantResults/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ParticipantResult participantResult = await db.ParticipantResults.FindAsync(id);
            if (participantResult == null)
            {
                return HttpNotFound();
            }
            return View(participantResult);
        }

        // POST: Edu/ParticipantResults/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ParticipantResult participantResult = await db.ParticipantResults.FindAsync(id);
            db.ParticipantResults.Remove(participantResult);
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
