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

namespace Exwhyzee.ESS.Areas.Games.Controllers
{
    [Authorize(Roles = "SuperAdmin,Admin,Content")]

    public class GamesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Games/Games
        public async Task<ActionResult> Index()
        {
            return View(await db.Gamess.ToListAsync());
        }

        // GET: Games/Games/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = await db.Gamess.FindAsync(id);
            if (game == null)
            {
                return HttpNotFound();
            }
            return View(game);
        }

        // GET: Games/Games/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Games/Games/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,IframLink,Title,Description,Instruction,IsActive,Date,CategoryGameId")] Game game)
        {
            if (ModelState.IsValid)
            {
                db.Gamess.Add(game);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(game);
        }

        // GET: Games/Games/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = await db.Gamess.FindAsync(id);
            if (game == null)
            {
                return HttpNotFound();
            }
            return View(game);
        }

        // POST: Games/Games/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,IframLink,Title,Description,Instruction,IsActive,Date,CategoryGameId")] Game game)
        {
            if (ModelState.IsValid)
            {
                db.Entry(game).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(game);
        }

        // GET: Games/Games/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = await db.Gamess.FindAsync(id);
            if (game == null)
            {
                return HttpNotFound();
            }
            return View(game);
        }

        // POST: Games/Games/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Game game = await db.Gamess.FindAsync(id);
            db.Gamess.Remove(game);
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
