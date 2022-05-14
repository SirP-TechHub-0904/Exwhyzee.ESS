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
using PagedList;
using Exwhyzee.ESS.Models.Entities.Dto;

namespace Exwhyzee.ESS.Areas.Edu.Controllers
{
    [Authorize(Roles = "SuperAdmin,Admin,Content")]

    public class TopicsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Edu/Topics
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
            var items = db.Topics.Include(x => x.Subject).Include(x => x.Subject.ClassLevel).Where(x => x.Title != "");
            if (!String.IsNullOrEmpty(searchString))
            {

                items = items.Where(s => s.Subject.Name.ToUpper().Contains(searchString.ToUpper()) ||
                               s.Title.ToUpper().Contains(searchString.ToUpper()) ||
                               s.Subject.ClassLevel.Name.ToUpper().Contains(searchString.ToUpper())
                                                              );
            }

            int pageSize = 50;
            int pageNumber = (page ?? 1);
            ViewBag.Total = items.Count();
            return View(items.OrderByDescending(x => x.Id).ToPagedList(pageNumber, pageSize));
        }




        // GET: Edu/Topics/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Topic topic = await db.Topics.FindAsync(id);
            if (topic == null)
            {
                return HttpNotFound();
            }
            return View(topic);
        }

        // GET: Edu/Topics/Create
        public ActionResult Create()
        {
            var students = db.Subjects.Include(x => x.ClassLevel).Select(x => new SubjectsDto
            {

                Id = x.Id,
                ListItem = x.ClassLevel.Name + " - " + x.Name

            });

            ViewBag.SubjectId = new SelectList(students, "Id", "ListItem");
            return View();
        }

        // POST: Edu/Topics/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Topic topic, HttpPostedFileBase upload)
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
                        topic.VideoCover = fileName;
                        fileName = Path.Combine(Server.MapPath("~/Uploads/"), fileName);
                        upload.SaveAs(fileName);

                    }

                }
                catch (Exception c) { }


                topic.Date = DateTime.UtcNow.AddHours(1);
                db.Topics.Add(topic);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            var students = db.Subjects.Include(x => x.ClassLevel).Select(x => new SubjectsDto
            {

                Id = x.Id,
                ListItem = x.ClassLevel.Name + " - " + x.Name

            });

            ViewBag.SubjectId = new SelectList(students, "Id", "ListItem"); return View(topic);
        }

        // GET: Edu/Topics/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Topic topic = await db.Topics.FindAsync(id);
            if (topic == null)
            {
                return HttpNotFound();
            }
            ViewBag.SubjectId = new SelectList(db.Subjects, "Id", "Name", topic.SubjectId);
            return View(topic);
        }

        // POST: Edu/Topics/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Topic topic, HttpPostedFileBase upload)
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
                        topic.VideoCover = fileName;
                        fileName = Path.Combine(Server.MapPath("~/Uploads/"), fileName);
                        upload.SaveAs(fileName);

                    }

                }
                catch (Exception c) { }

                db.Entry(topic).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.SubjectId = new SelectList(db.Subjects, "Id", "Name", topic.SubjectId);
            return View(topic);
        }

        // GET: Edu/Topics/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Topic topic = await db.Topics.FindAsync(id);
            if (topic == null)
            {
                return HttpNotFound();
            }
            return View(topic);
        }

        // POST: Edu/Topics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Topic topic = await db.Topics.FindAsync(id);
            var reviews = await db.TopicReview.Where(x => x.TopicId == topic.Id).ToListAsync();
            foreach(var i in reviews)
            {
                db.TopicReview.Remove(i);
            }
            db.Topics.Remove(topic);
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
