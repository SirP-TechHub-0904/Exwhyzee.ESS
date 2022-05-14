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

    public class InitializerController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Games/Initializer
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult _CategoryMenu()
        {
            var item = db.CatogoryGames.Include(x=>x.Games).Where(x => x.IsActive == true).ToList();
            return PartialView(item);
        }

        public ActionResult _GameSubMenu()
        {
            var item = db.Gamess.Where(x => x.IsActive == true).ToList();
            return PartialView(item);
        }

        public async Task<ActionResult> Play(int? id, string title)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
           var game = await db.Gamess.FirstOrDefaultAsync(x=>x.Id == id);
            if (game == null)
            {
                return HttpNotFound();
            }
            return View(game);
        }
    }
}