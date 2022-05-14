using Exwhyzee.ESS.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;

namespace Exwhyzee.ESS.Areas.Student.Controllers
{

    [Authorize(Roles = "Student")]
    public class AccountController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Student/Account
       
        public ActionResult Index()
        {
            string user = User.Identity.GetUserId();
            var userinfo = db.Users.FirstOrDefault(x => x.Id == user);
            ViewBag.Email = userinfo.Email;
            ViewBag.Phone = userinfo.PhoneNumber;
            var profile = db.StudentModel.FirstOrDefault(x => x.UserId == user);

            return View(profile);
        }
        public ActionResult Profile()
        {
            string user = User.Identity.GetUserId();
            var userinfo = db.Users.FirstOrDefault(x => x.Id == user);
            var profile = db.StudentModel.Include(x=>x.User).Include(x=>x.ClassLevel).FirstOrDefault(x => x.UserId == user);

            return View(profile);
        }
       

        public ActionResult _DashImage()
        {
            string user = User.Identity.GetUserId();
            var userinfo = db.Users.FirstOrDefault(x => x.Id == user);
            ViewBag.user = userinfo;
            var profile = db.StudentModel.FirstOrDefault(x => x.UserId == user);
            return PartialView(profile);
        }
    }
}