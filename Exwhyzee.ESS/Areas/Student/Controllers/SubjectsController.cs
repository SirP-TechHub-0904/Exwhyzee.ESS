using Exwhyzee.ESS.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using Exwhyzee.ESS.Models.Entities;
using System.Threading.Tasks;

namespace Exwhyzee.ESS.Areas.Student.Controllers
{
    [Authorize(Roles = "Student")]
    public class SubjectsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Student/Subjects
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var student = db.StudentModel.Include(x => x.User).Where(x => x.UserId == userId).FirstOrDefault();
            var enrolledSubject = db.LiveClassSubjectEnrollment.
                Include(x => x.LiveClassLevel).Include(x => x.StudentModel)
                .Include(x => x.LiveClassSubject).
                Where(x => x.UserId == student.UserId && x.LiveClassLevelId == student.ClassLevelId).ToList();
            return View(enrolledSubject);
        }


        public ActionResult AddSubject()
        {
            var userId = User.Identity.GetUserId();
            var student = db.StudentModel.Include(x => x.ClassLevel).Where(x => x.UserId == userId).FirstOrDefault();
            var subject = db.LiveClassSubject.Include(x => x.LiveClassLevel).Where(x => x.LiveClassLevelId == student.ClassLevelId).ToList();
            var enrolledSubject = db.LiveClassSubjectEnrollment.
              Include(x => x.LiveClassLevel).Include(x => x.StudentModel)
              .Include(x => x.LiveClassSubject).
              Where(x => x.UserId == student.UserId && x.LiveClassLevelId == student.ClassLevelId).FirstOrDefault();
            ViewBag.subject = subject;
            ViewBag.userId = userId;
            ViewBag.esubject = enrolledSubject;
            return View(subject);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddSubject(LiveClassSubjectEnrollment model, int subjectId)
        {
            var userId = User.Identity.GetUserId();
            var student = db.StudentModel.Include(x => x.ClassLevel).Include(x => x.User).FirstOrDefault(x => x.UserId == userId);
            if (ModelState.IsValid)
            {
                if (userId != null)
                {
                    var check = db.LiveClassSubjectEnrollment.Include(x => x.User)
                         .Include(x => x.LiveClassSubject).Include(x => x.LiveClassSubject)
                         .Where(z => z.LiveClassSubjectId == subjectId && z.UserId == userId && z.StudentModelId == student.Id).FirstOrDefault();

                    if(check != null)
                    {
                        check.UserId = check.UserId;
                        check.LiveClassSubjectId = check.LiveClassSubjectId;
                        check.Status = check.Status;
                        check.StudentModelId = check.StudentModelId;
                        check.LiveClassLevelId = check.LiveClassLevelId;
                        db.Entry(check).State = EntityState.Modified;
                        await db.SaveChangesAsync();
                    }
                    else
                    {
                        model.UserId = userId;
                        model.LiveClassSubjectId = subjectId;
                        model.Status = true;
                        model.StudentModelId = student.Id;
                        model.LiveClassLevelId = student.ClassLevelId;
                        db.LiveClassSubjectEnrollment.Add(model);
                        await db.SaveChangesAsync();

                    }

                }
            }
            return RedirectToAction("Index", "Subjects", new { area = "Student" });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Deactivate(int id)
        {
            if (ModelState.IsValid)
            {
               
                    var check = db.LiveClassSubjectEnrollment.Include(x => x.User)
                         .Include(x => x.LiveClassSubject).Include(x => x.LiveClassSubject)
                         .Where(x=>x.Id == id).FirstOrDefault();

                    if (check != null)
                    {
                        check.Status = false;
                        db.Entry(check).State = EntityState.Modified;
                        await db.SaveChangesAsync();
                    }
                   
                
            }
            return RedirectToAction("Index", "Subjects", new { area = "Student" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Activate(int id)
        {
            if (ModelState.IsValid)
            {

                var check = db.LiveClassSubjectEnrollment.Include(x => x.User)
                     .Include(x => x.LiveClassSubject).Include(x => x.LiveClassSubject)
                     .Where(x => x.Id == id).FirstOrDefault();

                if (check != null)
                {
                    check.Status = true;
                    db.Entry(check).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                }


            }
            return RedirectToAction("Index", "Subjects", new { area = "Student" });
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            if (ModelState.IsValid)
            {

                var check = db.LiveClassSubjectEnrollment.Include(x => x.User)
                     .Include(x => x.LiveClassSubject).Include(x => x.LiveClassSubject)
                     .Where(x => x.Id == id).FirstOrDefault();

                if (check != null)
                {
                    db.LiveClassSubjectEnrollment.Remove(check);
                    await db.SaveChangesAsync();
                }


            }
            return RedirectToAction("Index", "Subjects", new { area = "Student" });
        }
       
    }
}
