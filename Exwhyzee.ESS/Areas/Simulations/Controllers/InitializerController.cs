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

namespace Exwhyzee.ESS.Areas.Simulations.Controllers
{

    public class InitializerController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Games/Initializer
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult IFrameView()
        {
            return View();
        }

        public ActionResult _SubjectMenu()
        {
            var item = db.SimulationSubjects.Include(x => x.SimulationCategorys).ToList();
            return PartialView(item);
        }

        public ActionResult _CategoryMenu(int id)
        {
            var sub = db.SimulationSubjects.FirstOrDefault(x=>x.Id == id);
            var item = db.SimulationCategories.Include(x=>x.Simulations).Where(x => x.IsActive == true && x.SimulationSubjectId == id ).ToList();
            return PartialView(item);
        }

        public ActionResult _SimulationSubMenu(int id)
        {
            var item = db.Simulations.Where(x => x.IsActive == true && x.SimulationCategoryId == id).ToList();
            return PartialView(item);
        }

        public async Task<ActionResult> Play(int? id, string title)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
           var Simulation = await db.Simulations.FirstOrDefaultAsync(x=>x.Id == id);
            if (Simulation == null)
            {
                return HttpNotFound();
            }
            return View(Simulation);
        }
    }
}