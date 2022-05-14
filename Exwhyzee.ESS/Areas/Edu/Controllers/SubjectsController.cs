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
using PagedList;

namespace Exwhyzee.ESS.Areas.Edu.Controllers
{
    [Authorize(Roles = "SuperAdmin,Admin,Content")]

    public class SubjectsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Edu/Subjects
        public async Task<ActionResult> Index(string searchString, string currentFilter, int? page)
        {
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            var items = db.Subjects.Include(x=>x.ClassLevel).Where(x => x.Name != "");
            if (!String.IsNullOrEmpty(searchString))
            {

                items = items.Where(s => s.ClassLevel.Name.ToUpper().Contains(searchString.ToUpper()) ||
                s.Name.ToUpper().Contains(searchString.ToUpper()) 
                );

            }

            int pageSize = 50;
            int pageNumber = (page ?? 1);
            ViewBag.Total = items.Count();
            return View(items.OrderByDescending(x => x.Id).ToPagedList(pageNumber, pageSize));
        }


        // GET: Edu/Subjects/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subject subject = await db.Subjects.FindAsync(id);
            if (subject == null)
            {
                return HttpNotFound();
            }
            return View(subject);
        }

        // GET: Edu/Subjects/Create
        public ActionResult Create()
        {
            ViewBag.ClassLevelId = new SelectList(db.ClassLevels, "Id", "Name");
            return View();
        }

        // POST: Edu/Subjects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Subject subject, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                if (upload != null && upload.ContentLength > 0)
                {


                    // Find its length and convert it to byte array
                    int ContentLength = upload.ContentLength;

                    // Create Byte Array
                    byte[] bytImg = new byte[ContentLength];

                    // Read Uploaded file in Byte Array
                    upload.InputStream.Read(bytImg, 0, ContentLength);

                    subject.CoverImage = bytImg;
                   

                }
                db.Subjects.Add(subject);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ClassLevelId = new SelectList(db.ClassLevels, "Id", "Name", subject.ClassLevelId);
            return View(subject);
        }

        // GET: Edu/Subjects/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subject subject = await db.Subjects.FindAsync(id);
            if (subject == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClassLevelId = new SelectList(db.ClassLevels, "Id", "Name", subject.ClassLevelId);
            return View(subject);
        }

        // POST: Edu/Subjects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Subject subject, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                if (upload != null && upload.ContentLength > 0)
                {


                    // Find its length and convert it to byte array
                    int ContentLength = upload.ContentLength;

                    // Create Byte Array
                    byte[] bytImg = new byte[ContentLength];

                    // Read Uploaded file in Byte Array
                    upload.InputStream.Read(bytImg, 0, ContentLength);

                    subject.CoverImage = bytImg;


                }
                db.Entry(subject).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ClassLevelId = new SelectList(db.ClassLevels, "Id", "Name", subject.ClassLevelId);
            return View(subject);
        }

        // GET: Edu/Subjects/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subject subject = await db.Subjects.FindAsync(id);
            if (subject == null)
            {
                return HttpNotFound();
            }
            return View(subject);
        }

        // POST: Edu/Subjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Subject subject = await db.Subjects.FindAsync(id);
            db.Subjects.Remove(subject);
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
