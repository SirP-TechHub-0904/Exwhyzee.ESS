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
using Exwhyzee.ESS.Models.UserProfile;

namespace Exwhyzee.ESS.Areas.Edu.Controllers
{
    public class TeacherSubjectsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Edu/TeacherSubjects
        public async Task<ActionResult> Index()
        {
            var teacherLiveSubjectAssignments = db.TeacherLiveSubjectAssignments.Include(t => t.Class).Include(t => t.Subject).Include(t => t.Teacher);
            return View(await teacherLiveSubjectAssignments.ToListAsync());
        }

        // GET: Edu/TeacherSubjects/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TeacherLiveSubjectAssignment teacherLiveSubjectAssignment = await db.TeacherLiveSubjectAssignments.FindAsync(id);
            if (teacherLiveSubjectAssignment == null)
            {
                return HttpNotFound();
            }
            return View(teacherLiveSubjectAssignment);
        }

        // GET: Edu/TeacherSubjects/Create
        public ActionResult Create()
        {
            ViewBag.ClassId = new SelectList(db.LiveClassLevel, "Id", "Name");
            //ViewBag.SubjectId = new SelectList(db.LiveClassSubject, "Id", "Subject");
            ViewBag.TeacherId = new SelectList(db.UserProfileModels, "Id", "FullName");
            return View();
        }

        // POST: Edu/TeacherSubjects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(TeacherLiveSubjectAssignment teacherLiveSubjectAssignment)
        {
            if (ModelState.IsValid)
            {
                db.TeacherLiveSubjectAssignments.Add(teacherLiveSubjectAssignment);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ClassId = new SelectList(db.LiveClassLevel, "Id", "Name", teacherLiveSubjectAssignment.ClassId);
            //ViewBag.SubjectId = new SelectList(db.LiveClassSubject, "Id", "Subject", teacherLiveSubjectAssignment.SubjectId);
            ViewBag.TeacherId = new SelectList(db.UserProfileModels, "Id", "FullName", teacherLiveSubjectAssignment.TeacherId);
            return View(teacherLiveSubjectAssignment);
        }

        // GET: Edu/TeacherSubjects/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TeacherLiveSubjectAssignment teacherLiveSubjectAssignment = await db.TeacherLiveSubjectAssignments.FindAsync(id);
            if (teacherLiveSubjectAssignment == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClassId = new SelectList(db.LiveClassLevel, "Id", "Name", teacherLiveSubjectAssignment.ClassId);
            //ViewBag.SubjectId = new SelectList(db.LiveClassSubject, "Id", "Subject", teacherLiveSubjectAssignment.SubjectId);
            ViewBag.TeacherId = new SelectList(db.UserProfileModels, "Id", "FullName", teacherLiveSubjectAssignment.TeacherId);
            return View(teacherLiveSubjectAssignment);
        }

        // POST: Edu/TeacherSubjects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(TeacherLiveSubjectAssignment teacherLiveSubjectAssignment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(teacherLiveSubjectAssignment).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ClassId = new SelectList(db.LiveClassLevel, "Id", "Name", teacherLiveSubjectAssignment.ClassId);
            //ViewBag.SubjectId = new SelectList(db.LiveClassSubject, "Id", "Subject", teacherLiveSubjectAssignment.SubjectId);
            ViewBag.TeacherId = new SelectList(db.UserProfileModels, "Id", "FullName", teacherLiveSubjectAssignment.TeacherId);
            return View(teacherLiveSubjectAssignment);
        }

        // GET: Edu/TeacherSubjects/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TeacherLiveSubjectAssignment teacherLiveSubjectAssignment = await db.TeacherLiveSubjectAssignments.FindAsync(id);
            if (teacherLiveSubjectAssignment == null)
            {
                return HttpNotFound();
            }
            return View(teacherLiveSubjectAssignment);
        }

        // POST: Edu/TeacherSubjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            TeacherLiveSubjectAssignment teacherLiveSubjectAssignment = await db.TeacherLiveSubjectAssignments.FindAsync(id);
            db.TeacherLiveSubjectAssignments.Remove(teacherLiveSubjectAssignment);
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
