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
    [Authorize(Roles = "SuperAdmin,Admin,Content")]

    public class AdminManagerController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        #region Simulation subject 
        // GET: Simulations/SimulationSubject
        public async Task<ActionResult> SubjectIndex()
        {
            return View(await db.SimulationSubjects.ToListAsync());
        }

        // GET: Simulations/SimulationSubject/Details/5
        public async Task<ActionResult> SubjectDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SimulationSubject simulationSubject = await db.SimulationSubjects.FindAsync(id);
            if (simulationSubject == null)
            {
                return HttpNotFound();
            }
            return View(simulationSubject);
        }

        // GET: Simulations/SimulationSubject/Create
        public ActionResult SubjectCreate()
        {
            return View();
        }

        // POST: Simulations/SimulationSubject/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SubjectCreate([Bind(Include = "Id,Name,Description")] SimulationSubject simulationSubject)
        {
            if (ModelState.IsValid)
            {
                db.SimulationSubjects.Add(simulationSubject);
                await db.SaveChangesAsync();
                return RedirectToAction("SubjectIndex");
            }

            return View(simulationSubject);
        }

        // GET: Simulations/SimulationSubject/Edit/5
        public async Task<ActionResult> SubjectEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SimulationSubject simulationSubject = await db.SimulationSubjects.FindAsync(id);
            if (simulationSubject == null)
            {
                return HttpNotFound();
            }
            return View(simulationSubject);
        }

        // POST: Simulations/SimulationSubject/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SubjectEdit([Bind(Include = "Id,Name,Description")] SimulationSubject simulationSubject)
        {
            if (ModelState.IsValid)
            {
                db.Entry(simulationSubject).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("SubjectIndex");
            }
            return View(simulationSubject);
        }

        // GET: Simulations/SimulationSubject/Delete/5
        public async Task<ActionResult> SubjectDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SimulationSubject simulationSubject = await db.SimulationSubjects.FindAsync(id);
            if (simulationSubject == null)
            {
                return HttpNotFound();
            }
            return View(simulationSubject);
        }

        // POST: Simulations/SimulationSubject/Delete/5
        [HttpPost, ActionName("SubjectDelete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SubjectDeleteConfirmed(int id)
        {
            SimulationSubject simulationSubject = await db.SimulationSubjects.FindAsync(id);
            db.SimulationSubjects.Remove(simulationSubject);
            await db.SaveChangesAsync();
            return RedirectToAction("SubjectIndex");
        }
#endregion

        #region Simulation category
        // GET: Simulations/AdminManager/Index
        public async Task<ActionResult> Index()
        {
            return View(await db.SimulationCategories.Include(x=>x.SimulationSubjects).ToListAsync());
        }

        // GET: Simulations/AdminManager/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //SimulationCategory Simulationcatogory = await db.SimulationCategories.FindAsync(id);
            var Simulationcatogory = await db.SimulationCategories.Include(x => x.SimulationSubjects).FirstOrDefaultAsync(x => x.Id == id);
            if (Simulationcatogory == null)
            {
                return HttpNotFound();
            }
            return View(Simulationcatogory);
        }

        // GET: Simulations/AdminManager/Create
        public ActionResult Create()
        {
            var subject = db.SimulationSubjects.ToList();
            ViewBag.SimulationSubjectId = new SelectList(subject, "Id", "Name");
            return View();
        }

        // POST: Simulations/AdminManager/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(SimulationCategory SimulationCategory)
        {
            if (ModelState.IsValid)
            {
                SimulationCategory.Date = DateTime.UtcNow;
                db.SimulationCategories.Add(SimulationCategory);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            var subject = db.SimulationSubjects.ToList();
            ViewBag.SimulationSubjectId = new SelectList(subject, "Id", "Name", SimulationCategory.SimulationSubjectId);
            return View(SimulationCategory);
        }

        // GET: Simulations/AdminManager/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SimulationCategory SimulationCategory = await db.SimulationCategories.FindAsync(id);
            if (SimulationCategory == null)
            {
                return HttpNotFound();
            }
            var subject = db.SimulationSubjects.ToList();
            ViewBag.SimulationSubjectId = new SelectList(subject, "Id", "Name", SimulationCategory.SimulationSubjectId);
            return View(SimulationCategory);
        }

        // POST: Simulations/AdminManager/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(SimulationCategory SimulationCategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(SimulationCategory).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
              var subject = db.SimulationSubjects.ToList();
            ViewBag.SimulationSubjectId = new SelectList(subject, "Id", "Name", SimulationCategory.SimulationSubjectId);
            return View(SimulationCategory);
        }

        // GET: Simulations/AdminManager/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //SimulationCategory SimulationCategory = await db.SimulationCategories.FindAsync(id);
            var Simulationcatogory = await db.SimulationCategories.Include(x => x.SimulationSubjects).FirstOrDefaultAsync(x => x.Id == id);

            if (Simulationcatogory == null)
            {
                return HttpNotFound();
            }
            return View(Simulationcatogory);
        }

        // POST: Games/CatogoryGames/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            SimulationCategory SimulationCategory = await db.SimulationCategories.FindAsync(id);
            db.SimulationCategories.Remove(SimulationCategory);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        #endregion

        #region Simulation manager

        // GET: Simulation/Simulation
        public async Task<ActionResult> SimulationIndex()
        {
            return View(await db.Simulations.Include(x=>x.SimulationCatogory).Include(x=>x.SimulationCatogory.SimulationSubjects).ToListAsync());
        }

        // GET: Simulation/Simulation/Details/5
        public async Task<ActionResult> SimulationDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Simulation Simulation = await db.Simulations.FindAsync(id);
            if (Simulation == null)
            {
                return HttpNotFound();
            }
            return View(Simulation);
        }

        // GET: Simulation/Simulation/Create
        public ActionResult SimulationCreate()
        {
            var subject = db.SimulationSubjects.ToList();
            ViewBag.SimulationSubject = new SelectList(subject, "Id", "Name");
            return View();
        }

        public JsonResult CatList(int Id)
        {
            var stateId = db.SimulationSubjects.FirstOrDefault(x => x.Id == Id).Id;
            var cat = from s in db.SimulationCategories
                        where s.SimulationSubjectId == stateId
                        select s;

            return Json(new SelectList(cat.ToArray(), "Id", "Title"), JsonRequestBehavior.AllowGet);
        }
        // POST: Simulation/Simulation/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SimulationCreate(Simulation Simulation)
        {
            if (ModelState.IsValid)
            {
                Simulation.Date = DateTime.UtcNow;
                db.Simulations.Add(Simulation);
                await db.SaveChangesAsync();
                return RedirectToAction("SimulationIndex");
            }
            var Simulations = db.SimulationCategories.Where(x => x.IsActive == true);
            ViewBag.SimulationCategoryId = new SelectList(Simulations, "Id", "Title", Simulation.SimulationCategoryId);
            return View(Simulation);
        }

        // GET: Simulation/Simulation/Edit/5
        public async Task<ActionResult> SimulationEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Simulation Simulation = await db.Simulations.FindAsync(id);
            if (Simulation == null)
            {
                return HttpNotFound();
            }

            var Simulations = db.SimulationCategories.Where(x => x.IsActive == true);
            ViewBag.SimulationCategoryId = new SelectList(Simulations, "Id", "Title", Simulation.SimulationCategoryId);
            return View(Simulation);
        }

        // POST: Simulations/Simulations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SimulationEdit(Simulation Simulation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(Simulation).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("SimulationIndex");
            }
            var Simulations = db.SimulationCategories.Where(x => x.IsActive == true);
            ViewBag.SimulationCategoryId = new SelectList(Simulations, "Id", "Title", Simulation.SimulationCategoryId);
            return View(Simulation);
        }

        // GET: Simulations/Simulation/Delete/5
        public async Task<ActionResult> SimulationDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Simulation Simulation = await db.Simulations.FindAsync(id);
            if (Simulation == null)
            {
                return HttpNotFound();
            }
            return View(Simulation);
        }

        // POST: Simulations/Simulations/Delete/5
        [HttpPost, ActionName("SimulationDelete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SimulationDeleteConfirmed(int id)
        {
            Simulation Simulation = await db.Simulations.FindAsync(id);
            db.Simulations.Remove(Simulation);
            await db.SaveChangesAsync();
            return RedirectToAction("SimulationIndex");
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















       
       