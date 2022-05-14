using Exwhyzee.ESS.Models;
using Exwhyzee.ESS.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Exwhyzee.ESS.Areas.Games.Controllers
{
    [Authorize(Roles = "SuperAdmin,Admin,Content")]

    public class AdminManagerController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        #region Game category
        // GET: Games/CatogoryGames
        public async Task<ActionResult> Index()
        {
            return View(await db.CatogoryGames.ToListAsync());
        }

        // GET: Games/CatogoryGames/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CatogoryGame catogoryGame = await db.CatogoryGames.FindAsync(id);
            if (catogoryGame == null)
            {
                return HttpNotFound();
            }
            return View(catogoryGame);
        }

        // GET: Games/CatogoryGames/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Games/CatogoryGames/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CatogoryGame catogoryGame)
        {
            if (ModelState.IsValid)
            {
                catogoryGame.Date = DateTime.UtcNow;
                db.CatogoryGames.Add(catogoryGame);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(catogoryGame);
        }

        // GET: Games/CatogoryGames/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CatogoryGame catogoryGame = await db.CatogoryGames.FindAsync(id);
            if (catogoryGame == null)
            {
                return HttpNotFound();
            }
            return View(catogoryGame);
        }

        // POST: Games/CatogoryGames/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(CatogoryGame catogoryGame)
        {
            if (ModelState.IsValid)
            {
                db.Entry(catogoryGame).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(catogoryGame);
        }

        // GET: Games/CatogoryGames/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CatogoryGame catogoryGame = await db.CatogoryGames.FindAsync(id);
            if (catogoryGame == null)
            {
                return HttpNotFound();
            }
            return View(catogoryGame);
        }

        // POST: Games/CatogoryGames/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            CatogoryGame catogoryGame = await db.CatogoryGames.FindAsync(id);
            db.CatogoryGames.Remove(catogoryGame);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        #endregion

        #region Games manager

        // GET: Games/Games
        public async Task<ActionResult> GameIndex()
        {
            return View(await db.Gamess.Include(x=>x.CatogoryGame).ToListAsync());
        }

        // GET: Games/Games/Details/5
        public async Task<ActionResult> GameDetails(int? id)
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
        public ActionResult GameCreate()
        {
            var games = db.CatogoryGames.Where(x => x.IsActive == true);
            ViewBag.CategoryGameId = new SelectList(games, "Id", "Title");
            return View();
        }

        // POST: Games/Games/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> GameCreate(Game game)
        {
            if (ModelState.IsValid)
            {
                game.Date = DateTime.UtcNow;
                db.Gamess.Add(game);
                await db.SaveChangesAsync();
                return RedirectToAction("GameIndex");
            }
            var games = db.CatogoryGames.Where(x => x.IsActive == true);
            ViewBag.CategoryGameId = new SelectList(games, "Id", "Title", game.CategoryGameId);
            return View(game);
        }

        // GET: Games/Games/Edit/5
        public async Task<ActionResult> GameEdit(int? id)
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

            var games = db.CatogoryGames.Where(x => x.IsActive == true);
            ViewBag.CategoryGameId = new SelectList(games, "Id", "Title", game.CategoryGameId);
            return View(game);
        }

        // POST: Games/Games/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> GameEdit(Game game)
        {
            if (ModelState.IsValid)
            {
                db.Entry(game).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("GameIndex");
            }
            var games = db.CatogoryGames.Where(x => x.IsActive == true);
            ViewBag.CategoryGameId = new SelectList(games, "Id", "Title", game.CategoryGameId);
            return View(game);
        }

        // GET: Games/Games/Delete/5
        public async Task<ActionResult> GameDelete(int? id)
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
        [HttpPost, ActionName("GameDelete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> GameDeleteConfirmed(int id)
        {
            Game game = await db.Gamess.FindAsync(id);
            db.Gamess.Remove(game);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        #endregion

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