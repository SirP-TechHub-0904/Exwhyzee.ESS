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
using System.IO;

namespace Exwhyzee.ESS.Areas.Edu.Controllers
{
    [Authorize(Roles = "SuperAdmin,Admin,Content")]

    public class QuestionMainsoldController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Edu/QuestionMains
        public async Task<ActionResult> Index()
        {
            return View(await db.QuestionMain.ToListAsync());
        }

        // GET: Edu/QuestionMains/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuestionMain questionMain = await db.QuestionMain.FindAsync(id);
            if (questionMain == null)
            {
                return HttpNotFound();
            }
            return View(questionMain);
        }

        // GET: Edu/QuestionMains/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Edu/QuestionMains/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(QuestionMain questionMain, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    if (upload != null && upload.ContentLength > 0)
                    {


                        string date1 = DateTime.UtcNow.AddHours(1).ToString("ssfff");
                        string name = date1 + "-" + upload.FileName;
                        string fileName = Path.GetFileName(name);
                        questionMain.Img = fileName;
                        fileName = Path.Combine(Server.MapPath("~/Uploads/"), fileName);
                        upload.SaveAs(fileName);

                    }

                }
                catch (Exception c) { }

                db.QuestionMain.Add(questionMain);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(questionMain);
        }

        // GET: Edu/QuestionMains/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuestionMain questionMain = await db.QuestionMain.FindAsync(id);
            if (questionMain == null)
            {
                return HttpNotFound();
            }
            return View(questionMain);
        }

        // POST: Edu/QuestionMains/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(QuestionMain questionMain, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    if (upload != null && upload.ContentLength > 0)
                    {


                        string date1 = DateTime.UtcNow.AddHours(1).ToString("ssfff");
                        string name = date1 + "-" + upload.FileName;
                        string fileName = Path.GetFileName(name);
                        questionMain.Img = fileName;
                        fileName = Path.Combine(Server.MapPath("~/Uploads/"), fileName);
                        upload.SaveAs(fileName);

                    }

                }
                catch (Exception c) { }
                db.Entry(questionMain).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(questionMain);
        }

        // GET: Edu/QuestionMains/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuestionMain questionMain = await db.QuestionMain.FindAsync(id);
            if (questionMain == null)
            {
                return HttpNotFound();
            }
            return View(questionMain);
        }

        // POST: Edu/QuestionMains/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            QuestionMain questionMain = await db.QuestionMain.FindAsync(id);
            db.QuestionMain.Remove(questionMain);
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
