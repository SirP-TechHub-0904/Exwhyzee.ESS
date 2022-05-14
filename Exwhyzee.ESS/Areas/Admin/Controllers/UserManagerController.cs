using Exwhyzee.ESS.Models;
using Exwhyzee.ESS.Models.Entities.Dto;
using Exwhyzee.ESS.Models.UserProfile;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Exwhyzee.ESS.Areas.Admin.Controllers
{
    [Authorize(Roles = "SuperAdmin, Admin")]
    //[Authorize]

    public class UserManagerController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private ApplicationRoleManager _roleManager;
        private ApplicationUserManager _userManager;

        public UserManagerController()
        {
        }

        public UserManagerController(ApplicationRoleManager roleManager, ApplicationUserManager userManager)
        {
           RoleManager = roleManager;
            UserManager = userManager;

        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }
        // GET: Admin/UserManager
        public async Task<ActionResult> Index()
        {
            var users = await db.Users.ToListAsync();
            var profile = await db.UserProfileModels.Include(x => x.User).ToListAsync();
            ViewBag.Roles = RoleManager.Roles.ToList();

            //var data = users.Select(x => new UsersInfoDto
            //{
            //    FullName = profile.FirstOrDefault(s => s.UserId == x.Id).SurName + " " + profile.FirstOrDefault(s => s.UserId == x.Id).FirstName + " " + profile.FirstOrDefault(s => s.UserId == x.Id).LastName,
            //    Id = x.Id,
            //    ID = profile.FirstOrDefault(s => s.UserId == x.Id).IskoolId,
            //    Email = x.Email,
            //    Phone = x.PhoneNumber
            //});
            return View(users);
        }

        [HttpPost]
        public async Task<ActionResult> UserToRole(string rolename, string userId, bool? ischecked)
        {
            if (ischecked.HasValue && ischecked.Value)
            {
                await UserManager.AddToRoleAsync(userId, rolename);
            }
            else
            {
                await UserManager.AddToRoleAsync(userId, rolename);
            }

            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = await db.UserProfileModels.Include(x=>x.User).FirstOrDefaultAsync(x => x.Id == id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }


        public ActionResult AdminReview(string id)
        {
            var profile = db.UserProfileModels.Include(u => u.User).FirstOrDefault(x => x.UserId == id);
           
            return View(profile);
        }

        // POST: Games/Games/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AdminReview(UserProfileModel model)
        {
            try
            {
                var user = db.UserProfileModels.Include(u => u.User).FirstOrDefault(x => x.UserId == model.UserId);
                if (user != null)
                {
                    user.AdminReview = model.AdminReview;
                    user.AdminApproval = model.AdminApproval;
                    user.ShowTeacher = model.ShowTeacher;
                    db.Entry(user).State = EntityState.Modified;

                    await db.SaveChangesAsync();
                    TempData["success"] = "Success";
                }
                else
                {
                    TempData["error"] = "fail. try again";
                }
                    return RedirectToAction("AdminReview", new { id = user.UserId });
                
            }catch(Exception h) { }
         
            return View(model);
        }

        public async Task<ActionResult> Delete(string id)
        {
            var user = await db.Users.FirstOrDefaultAsync(x=>x.Id == id);
            if (user != null)
            {
                var profile = await db.UserProfileModels.FirstOrDefaultAsync(x => x.UserId == user.Id);
                if (profile != null)
                {
                    try
                    {
                        var exc = await db.ExperienceModels.Where(x => x.UserProfileId == profile.Id).ToListAsync();
                        var ed = await db.EducationHistoryModels.Where(x => x.UserProfileId == profile.Id).ToListAsync();
                        var tra = await db.TrainingAndWorkShopModels.Where(x => x.UserProfileId == profile.Id).ToListAsync();
                        var sk = await db.SkillModels.Where(x => x.UserProfileId == profile.Id).ToListAsync();
                        var lan = await db.LanguageSpokenModels.Where(x => x.UserProfileId == profile.Id).ToListAsync();
                        var cer = await db.MeritCertificateModels.Where(x => x.UserProfileId == profile.Id).ToListAsync();
                        var mem = await db.MembershipOfLearneredSocietyModels.Where(x => x.UserProfileId == profile.Id).ToListAsync();
                        var su = await db.SubjectSpecialistModels.Where(x => x.UserProfileId == profile.Id).ToListAsync();

                  
                        foreach (var i in exc)
                        {
                            db.ExperienceModels.Remove(i);
                        }
                        foreach (var i in ed)
                        {
                            db.EducationHistoryModels.Remove(i);
                        }
                        foreach (var i in tra)
                        {
                            db.TrainingAndWorkShopModels.Remove(i);
                        }
                        foreach (var i in sk)
                        {
                            db.SkillModels.Remove(i);
                        }
                        foreach (var i in lan)
                        {
                            db.LanguageSpokenModels.Remove(i);
                        }
                        foreach (var i in cer)
                        {
                            db.MeritCertificateModels.Remove(i);
                        }
                        foreach (var i in mem)
                        {
                            db.MembershipOfLearneredSocietyModels.Remove(i);
                        }
                        foreach (var i in su)
                        {
                            db.SubjectSpecialistModels.Remove(i);
                        }
                    }catch(Exception c) { }
                    db.UserProfileModels.Remove(profile);
                }
                var roles = RoleManager.Roles.ToList();
                foreach (var i in roles)
                {
                    try
                    {
                        bool check = await UserManager.IsInRoleAsync(user.Id, i.Name);
                        if (check == true)
                        {
                            await UserManager.RemoveFromRoleAsync(user.Id, i.Name);
                        }
                    }
                    catch (Exception c) { }
                }
                try
                {
                    db.Users.Remove(user);
                    //await UserManager.DeleteAsync(user);

                }
                catch (Exception c) { }

                await db.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }

    }
}