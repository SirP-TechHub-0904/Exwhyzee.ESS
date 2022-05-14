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
using System.IO;

namespace Exwhyzee.ESS.Areas.Edu.Controllers
{
    [Authorize(Roles = "SuperAdmin,Admin,Content")]

    public class DataImagesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Edu/DataImages
        public async Task<ActionResult> Index()
        {

            ViewBag.slides = Directory.EnumerateFiles(Server.MapPath("~/Uploads"))
                                    .Select(fn => "~/Uploads/" + Path.GetFileName(fn));
            return View(await db.DataImages.ToListAsync());
        }

        public ActionResult _Images()
        {
            return PartialView(db.DataImages.ToList());
        }

        // GET: Edu/DataImages/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DataImages dataImages = await db.DataImages.FindAsync(id);
            if (dataImages == null)
            {
                return HttpNotFound();
            }
            return View(dataImages);
        }

        // GET: Edu/DataImages/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Edu/DataImages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(DataImages dataImages, HttpPostedFileBase upload)
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
                        dataImages.ImageLink = fileName;
                        fileName = Path.Combine(Server.MapPath("~/Uploads/"), fileName);
                        upload.SaveAs(fileName);

                    }

                }
                catch (Exception c) { }

                db.DataImages.Add(dataImages);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(dataImages);
        }

        // GET: Edu/DataImages/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DataImages dataImages = await db.DataImages.FindAsync(id);
            if (dataImages == null)
            {
                return HttpNotFound();
            }
            return View(dataImages);
        }

        // POST: Edu/DataImages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,ImageLink")] DataImages dataImages)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dataImages).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(dataImages);
        }

        // GET: Edu/DataImages/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DataImages dataImages = await db.DataImages.FindAsync(id);
            if (dataImages == null)
            {
                return HttpNotFound();
            }
            return View(dataImages);
        }

        // POST: Edu/DataImages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            DataImages dataImages = await db.DataImages.FindAsync(id);


            
            db.DataImages.Remove(dataImages);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> RemoveFile(string name)
        {
            string completePath = Server.MapPath("~/Uploads/" + name);
            if (System.IO.File.Exists(completePath))
            {

                System.IO.File.Delete(completePath);

            }
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
