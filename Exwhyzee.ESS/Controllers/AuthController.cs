using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Exwhyzee.ESS.Models;
using Exwhyzee.ESS.Models.UserProfile;
using Exwhyzee.ESS.Controllers;
using System.Collections.Generic;
using System.Data.Entity;
using Exwhyzee.ESS.Models.Entities.Dto;
using Exwhyzee.ESS.Models.Profile;
using System.IO;
using Exwhyzee.ESS.Models.Entities;

namespace Exwhyzee.ESS.Controllers
{
    [Authorize]
    public class AuthController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private ApplicationRoleManager _roleManager;

        public AuthController()
        {
        }

        public AuthController(ApplicationUserManager userManager, ApplicationSignInManager signInManager, ApplicationRoleManager roleManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            RoleManager = roleManager;

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
        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
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

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);

            var userinfo = await UserManager.FindByEmailAsync(model.Email);
            if (userinfo != null)
            {
                userinfo.LastSeen = DateTime.UtcNow.AddHours(1);
                await UserManager.UpdateAsync(userinfo);

            }
            var profile = await db.UserProfileModels.FirstOrDefaultAsync(x => x.UserId == userinfo.Id);
            switch (result)
            {
                case SignInStatus.Success:
                    if (await UserManager.IsInRoleAsync(userinfo.Id, "SuperAdmin") || await UserManager.IsInRoleAsync(userinfo.Id, "Admin"))
                    {
                        if (!String.IsNullOrEmpty(profile.Title) || !String.IsNullOrEmpty(profile.State) || !String.IsNullOrEmpty(profile.Religion))
                        {
                            var userinfo2 = await UserManager.FindByEmailAsync(model.Email);
                            if (userinfo2.SurName != null)
                            {
                                userinfo2.SurName = profile.SurName;
                                userinfo2.FirstName = profile.FirstName;
                                userinfo2.LastName = profile.LastName;
                                await UserManager.UpdateAsync(userinfo2);

                            }
                            TempData["msg"] = "Kindly update your profile, to enjoy the full features of iskools.";
                            return RedirectToAction("Index", "Datas", new { area = "Admin" });
                        }

                        if (String.IsNullOrEmpty(profile.Title) || String.IsNullOrEmpty(profile.State) || String.IsNullOrEmpty(profile.Religion))
                        {

                            var userinfo2 = await UserManager.FindByEmailAsync(model.Email);
                            userinfo2.SurName = profile.SurName;
                            userinfo2.FirstName = profile.FirstName;
                            userinfo2.LastName = profile.LastName;
                            await UserManager.UpdateAsync(userinfo2);

                            TempData["msg"] = "Kindly update your profile, to enjoy the full features of iskools.";
                            return RedirectToAction("Index", "Datas", new { area = "Admin" });
                        }
                    }

                    else if (await UserManager.IsInRoleAsync(userinfo.Id, "Teacher") || await UserManager.IsInRoleAsync(userinfo.Id, "Volunteer"))
                    {
                        if (!String.IsNullOrEmpty(profile.Title) || !String.IsNullOrEmpty(profile.State) || !String.IsNullOrEmpty(profile.Religion))
                        {
                            var userinfo2 = await UserManager.FindByEmailAsync(model.Email);
                            if (userinfo2.SurName != null)
                            {
                                userinfo2.SurName = profile.SurName;
                                userinfo2.FirstName = profile.FirstName;
                                userinfo2.LastName = profile.LastName;
                                await UserManager.UpdateAsync(userinfo2);

                            }
                            TempData["msg"] = "Kindly update your profile, to enjoy the full features of iskools.";
                            return RedirectToAction("Profile", "Account", new { area = "Dashboard" });
                        }

                        if (String.IsNullOrEmpty(profile.Title) || String.IsNullOrEmpty(profile.State) || String.IsNullOrEmpty(profile.Religion))
                        {

                            var userinfo2 = await UserManager.FindByEmailAsync(model.Email);
                            userinfo2.SurName = profile.SurName;
                            userinfo2.FirstName = profile.FirstName;
                            userinfo2.LastName = profile.LastName;
                            await UserManager.UpdateAsync(userinfo2);

                            TempData["msg"] = "Kindly update your profile, to enjoy the full features of iskools.";
                            return RedirectToAction("Profile", "Account", new { area = "Dashboard" });
                        }

                    }

                    else if (await UserManager.IsInRoleAsync(userinfo.Id, "Student"))
                    {
                        var checksubject = db.LiveClassSubjectEnrollment.Include(x => x.User).Where(x => x.UserId == userinfo.Id).FirstOrDefault();
                        if (checksubject == null)
                        {
                            return RedirectToAction("SubjectEnrollment", "Auth", new { userId = userinfo.Id });
                        }
                        else
                        {
                            return RedirectToAction("LiveList", "OnlineClass", new { area = "Student" });
                        }

                    }

                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }
        }

        //
        // GET: /Account/VerifyCode
        [AllowAnonymous]
        public async Task<ActionResult> VerifyCode(string provider, string returnUrl, bool rememberMe)
        {
            // Require that the user has already logged in via username/password or external login
            if (!await SignInManager.HasBeenVerifiedAsync())
            {
                return View("Error");
            }
            return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        // POST: /Account/VerifyCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // The following code protects for brute force attacks against the two factor codes. 
            // If a user enters incorrect codes for a specified amount of time then the user account 
            // will be locked out for a specified amount of time. 
            // You can configure the account lockout settings in IdentityConfig
            var result = await SignInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent: model.RememberMe, rememberBrowser: model.RememberBrowser);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(model.ReturnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid code.");
                    return View(model);
            }
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult CreateAccount()
        {
            var role = RoleManager.Roles.Where(x => x.Name.ToLower() != "readonly").ToList();
            role = role.Where(x => x.Name.ToLower() != "content").ToList();
            role = role.Where(x => x.Name.ToLower() != "superadmin").ToList();
            role = role.Where(x => x.Name.ToLower() != "admin").ToList();
            role = role.Where(x => x.Name.ToLower() != "graphics").ToList();
            role = role.Where(x => x.Name.ToLower() != "student").ToList();
            ViewBag.Id = new SelectList(role, "Name", "Name");
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAccount(RegisterViewModel model, string FullName, string RoleId)
        {
            if (ModelState.IsValid)
            {
                List<string> names = FullName.Split(' ').ToList();
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email, PhoneNumber = model.PhoneNumber };
                var result = await UserManager.CreateAsync(user, model.Password);
                var userinfo = await UserManager.FindByEmailAsync(model.Email);
                userinfo.LastSeen = DateTime.UtcNow.AddHours(1);
                await UserManager.UpdateAsync(userinfo);
                if (result.Succeeded)
                {
                    await UserManager.AddToRoleAsync(user.Id, RoleId);

                    UserProfileModel profile = new UserProfileModel();
                    profile.UserId = user.Id;
                    profile.SurName = names[0];
                    if (names.Count() > 1)
                    {
                        profile.FirstName = names[1];
                    }

                    if (names.Count() > 2)
                    {
                        profile.LastName = names[2];
                    }
                    profile.DateRegistered = DateTime.UtcNow.AddHours(1);
                    profile.Verified = true;
                    profile.ShowTeacher = true;
                    db.UserProfileModels.Add(profile);


                    db.SaveChanges();
                    var ouser = db.UserProfileModels.FirstOrDefault(x => x.Id == profile.Id);
                    ouser.IskoolId = "ISKOOL/ID/" + ouser.Id.ToString("0000");
                    db.Entry(ouser).State = EntityState.Modified;
                    db.SaveChanges();



                    var userinfo2 = await UserManager.FindByEmailAsync(model.Email);
                    userinfo2.SurName = names[0];
                    if (names.Count() > 1)
                    {
                        userinfo2.FirstName = names[1];
                    }

                    if (names.Count() > 2)
                    {
                        userinfo2.LastName = names[2];
                    }
                    await UserManager.UpdateAsync(userinfo2);

                    await HelperClass.MainMail(user.Email, "welcome mail");
                    await HelperClass.MainMailToAdmin("welcome mail http://iskools.com/Dashboard/Account/MyCV?email=" + model.Email);

                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    return RedirectToAction("Qualification", "Auth", new { userId = user.Id });

                    //return RedirectToAction("Index", "Account", new { area = "Dashboard" });
                }
                var role = RoleManager.Roles.Where(x => x.Name.ToLower() != "readonly").ToList();
                role = role.Where(x => x.Name.ToLower() != "content").ToList();
                role = role.Where(x => x.Name.ToLower() != "superadmin").ToList();
                role = role.Where(x => x.Name.ToLower() != "admin").ToList();
                role = role.Where(x => x.Name.ToLower() != "graphics").ToList();
                role = role.Where(x => x.Name.ToLower() != "student").ToList();
                ViewBag.Id = new SelectList(role, "Name", "Name");
                AddErrors(result);
              
            }

            // If we got this far, something failed, redisplay form
            return View(model);

        }

        [AllowAnonymous]
        public ActionResult StudentAccount()
        {
            var classss = db.LiveClassLevel.ToList();
            ViewBag.classId = new SelectList(classss, "Id", "Name");
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> StudentAccount(StudentDto model, int? ClassId, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email, PhoneNumber = model.PhoneNumber };
                var result = await UserManager.CreateAsync(user, model.Password);
                var userinfo = await UserManager.FindByEmailAsync(model.Email);
                userinfo.LastSeen = DateTime.UtcNow.AddHours(1);
                await UserManager.UpdateAsync(userinfo);
                if (result.Succeeded)
                {
                    await UserManager.AddToRoleAsync(user.Id, "Student");

                    StudentModel profile = new StudentModel();
                    profile.UserId = user.Id;
                    profile.SurName = model.SurName;
                    profile.FirstName = model.FirstName;
                    profile.OtherName = model.OtherName;
                    profile.Email = model.Email;
                    profile.PhoneNumber = model.PhoneNumber;
                    profile.ClassLevelId = ClassId;
                    profile.ParentPhoneNumber = model.ParentPhoneNumber;
                    profile.DateRegistered = DateTime.UtcNow.AddHours(1);
                    if (upload != null && upload.ContentLength > 0)

                    {
                        System.Random randomInteger = new System.Random();
                        int genNumber = randomInteger.Next(1000);
                        string fileName = Path.GetFileName(model.SurName + "_" + genNumber + "_" + upload.FileName);
                        profile.Picture = "~/Uploads/" + fileName;
                        fileName = Path.Combine(Server.MapPath("~/Uploads/"), fileName);
                        upload.SaveAs(fileName);
                    }

                    db.StudentModel.Add(profile);
                    await db.SaveChangesAsync();

                    var ouser = db.StudentModel.FirstOrDefault(x => x.Id == profile.Id);
                    ouser.IskoolId = "ISKOOL/ID/" + ouser.Id.ToString("0000");
                    db.Entry(ouser).State = EntityState.Modified;
                    await db.SaveChangesAsync();


                    var userinfo2 = await UserManager.FindByEmailAsync(model.Email);
                    userinfo2.SurName = model.SurName;
                    userinfo2.FirstName = model.FirstName;
                    userinfo2.LastName = model.OtherName;
                    await UserManager.UpdateAsync(userinfo2);

                    await HelperClass.MainMail(user.Email, "welcome mail");
                    await HelperClass.MainMailToAdmin("welcome mail http://iskools.com/Student/Account/MyAccount?email=" + model.Email);

                    //await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);


                    //return RedirectToAction("LiveList", "OnlineClass", new { area = "Student" });
                    return RedirectToAction("SubjectEnrollment", "Auth", new { userId = user.Id });
                }
                var classss = db.LiveClassLevel.ToList();
                ViewBag.classId = new SelectList(classss, "Id", "Name");
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }



        [AllowAnonymous]
        public ActionResult SubjectEnrollment(string userId)
        {
            var student = db.StudentModel.Include(x => x.ClassLevel).Where(x => x.UserId == userId).FirstOrDefault();
            var subject = db.LiveClassSubject.Include(x => x.LiveClassLevel).Where(x => x.LiveClassLevelId == student.ClassLevelId).ToList();
            ViewBag.subject = subject;
            ViewBag.userId = userId;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SubjectEnrollment(LiveClassSubjectEnrollment model, string userId, int[] subjectId)
        {
            var student = db.StudentModel.Include(x => x.ClassLevel).Include(x => x.User).FirstOrDefault(x => x.UserId == userId);
            if (ModelState.IsValid)
            {
                if (userId != null)
                {
                    foreach (var subject in subjectId)
                    {
                        model.UserId = userId;
                        model.LiveClassSubjectId = subject;
                        model.Status = true;
                        model.StudentModelId = student.Id;
                        model.LiveClassLevelId = student.ClassLevelId;
                        db.LiveClassSubjectEnrollment.Add(model);
                        await db.SaveChangesAsync();

                    }
                }
            }
            var user = await UserManager.FindByEmailAsync(student.Email);
            await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            return RedirectToAction("LiveList", "OnlineClass", new { area = "Student" });
        }


        [AllowAnonymous]
        public ActionResult Qualification(string userId)
        {
            var teacher = db.UserProfileModels.Include(x => x.User).Where(x => x.UserId == userId).FirstOrDefault();
            ViewBag.Id = teacher.Id;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Qualification(TeacherEvaluation model, long Id, HttpPostedFileBase upload, HttpPostedFileBase upload1)
        {
            var teacher = db.UserProfileModels.Include(x => x.User).Where(x => x.Id == Id).FirstOrDefault();
            if (ModelState.IsValid)
            {


                if (Id != null)
                {

                    if (upload != null && upload.ContentLength > 0)

                    {
                        System.Random randomInteger = new System.Random();
                        int genNumber = randomInteger.Next(1000);
                        string fileName = Path.GetFileName(teacher.SurName + "_" + genNumber + "_" + upload.FileName);
                        model.QualificationFront = "~/Uploads/" + fileName;
                        fileName = Path.Combine(Server.MapPath("~/Uploads/"), fileName);
                        upload.SaveAs(fileName);
                    }

                    if (upload1 != null && upload1.ContentLength > 0)

                    {
                        System.Random randomInteger = new System.Random();
                        int genNumber = randomInteger.Next(1000);
                        string fileName = Path.GetFileName(teacher.SurName + "_" + genNumber + "_" + upload1.FileName);
                        model.QualificationBack = "~/Uploads/" + fileName;
                        fileName = Path.Combine(Server.MapPath("~/Uploads/"), fileName);
                        upload1.SaveAs(fileName);
                    }
                    model.UserProfileId = Id;
                    db.TeacherEvaluation.Add(model);
                    await db.SaveChangesAsync();

                }
            }
            var user = await UserManager.FindByEmailAsync(teacher.User.Email);
            await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            return RedirectToAction("Index", "Account", new { area = "Dashboard" });
        }
        public JsonResult SubjectList(int Id)
        {
            var c = db.LiveClassLevel.FirstOrDefault(x => x.Id == Id).Id;
            var subject = from s in db.LiveClassSubject
                          where s.LiveClassLevelId == c
                          select s;

            return Json(new SelectList(subject.OrderBy(x => x.Subject).ToArray(), "Id", "Subject"), JsonRequestBehavior.AllowGet);
        }


        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    return RedirectToAction("Index", "Iskools");
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var result = await UserManager.ConfirmEmailAsync(userId, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.Email);
                if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return View("ForgotPasswordConfirmation");
                }

                // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                // string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                // var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);		
                // await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
                // return RedirectToAction("ForgotPasswordConfirmation", "Account");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult ChangeResetPassword()
        {
            return View();
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangeResetPassword(ResetPasswordViewModel model)
        {
           
            var user = await UserManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            var result = await UserManager.RemovePasswordAsync(user.Id);
            if (result.Succeeded)
            {
                var resultx = await UserManager.AddPasswordAsync(user.Id, model.Password);
                if (resultx.Succeeded)
                {
                    return RedirectToAction("ResetPasswordConfirmation", "Account");
                }
            }
            AddErrors(result);
            return View();
        }
        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await UserManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            AddErrors(result);
            return View();
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/SendCode
        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
        {
            var userId = await SignInManager.GetVerifiedUserIdAsync();
            if (userId == null)
            {
                return View("Error");
            }
            var userFactors = await UserManager.GetValidTwoFactorProvidersAsync(userId);
            var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
            return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/SendCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendCode(SendCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // Generate the token and send it
            if (!await SignInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
            {
                return View("Error");
            }
            return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
                case SignInStatus.Failure:
                default:
                    // If the user does not have an account, then prompt the user to create an account
                    ViewBag.ReturnUrl = returnUrl;
                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                    return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
            }
        }

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Manage");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Iskools");
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        [AllowAnonymous]


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Iskools");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}