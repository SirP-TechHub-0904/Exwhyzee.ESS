using Exwhyzee.ESS.Models;
using Exwhyzee.ESS.Models.Entities;
using Exwhyzee.ESS.Models.Entities.Dto;
using Exwhyzee.ESS.Models.Profile;
using Exwhyzee.ESS.Models.ProfileEducationHistory;
using Exwhyzee.ESS.Models.ProfileExperience;
using Exwhyzee.ESS.Models.ProfileMembershipOfLearneredSociety;
using Exwhyzee.ESS.Models.ProfileMeritCertificate;
using Exwhyzee.ESS.Models.ProfileSkill;
using Exwhyzee.ESS.Models.UserProfile;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Exwhyzee.ESS.Areas.Dashboard.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Dashboard/Account
        public ActionResult Index()
        {
            string user = User.Identity.GetUserId();
            var userinfo = db.Users.FirstOrDefault(x => x.Id == user);
            ViewBag.Email = userinfo.Email;
            ViewBag.Phone = userinfo.PhoneNumber;
            var profile = db.UserProfileModels.FirstOrDefault(x => x.UserId == user);

            if (String.IsNullOrEmpty(profile.Title) || String.IsNullOrEmpty(profile.State) || String.IsNullOrEmpty(profile.Religion))
            {
                TempData["msg"] = "Kindly update your profile, to enjoy the full features of iskools.";
                return RedirectToAction("Profile", "Account", new { area = "Dashboard" });
            }
            return View(profile);
        }
        public ActionResult Profile()
        {
            string user = User.Identity.GetUserId();
            var userinfo = db.Users.FirstOrDefault(x => x.Id == user);
            ViewBag.Email = userinfo.Email;
            ViewBag.Phone = userinfo.PhoneNumber;
            var profile = db.UserProfileModels.FirstOrDefault(x => x.UserId == user);
            ViewBag.StateOfOrigin = new SelectList(db.States.OrderBy(x => x.StateName), "StateName", "StateName");

            return View(profile);
        }
        public static Image resizeImage(Image image, int new_height, int new_width)
        {
            Bitmap new_image = new Bitmap(new_width, new_height);
            Graphics g = Graphics.FromImage((Image)new_image);
            g.InterpolationMode = InterpolationMode.High;
            g.DrawImage(image, 0, 0, new_width, new_height);
            return new_image;
        }

        public JsonResult LgaList(string Id)
        {
            var stateId = db.States.FirstOrDefault(x => x.StateName == Id).Id;
            var local = from s in db.LocalGovs
                        where s.StatesId == stateId
                        select s;

            return Json(new SelectList(local.ToArray(), "LGAName", "LGAName"), JsonRequestBehavior.AllowGet);
        }
        public static byte[] ImageToByte(Image img)
        {
            ImageConverter converter = new ImageConverter();
            return (byte[])converter.ConvertTo(img, typeof(byte[]));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Profile(UserProfileModel model, string Email, string Phone, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    if (upload != null && upload.ContentLength > 0)
                    {
                        Image image = System.Drawing.Image.FromStream(upload.InputStream);
                        var img = resizeImage(image, 150, 150);
                        //int ContentLength = ImageToByte(img.GetThumbnailImage);
                        byte[] bytes = (byte[])(new ImageConverter()).ConvertTo(img, typeof(byte[]));
                        // Create Byte Array
                        // byte[] bytImg = new byte[ContentLength];

                        // Read Uploaded file in Byte Array
                        // upload.InputStream.Read(bytes, 0, ContentLength);


                        model.PhotoUrl = bytes;

                    }

                }
                catch (Exception c) { }

                var user = db.Users.FirstOrDefault(x => x.Id == model.UserId);
                user.PhoneNumber = Phone;
                user.Email = Email;


                db.Entry(user).State = EntityState.Modified;
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
                TempData["alert"] = "alert(\"Update Successful \");";

                return RedirectToAction("Profile");
            }
            ViewBag.StateOfOrigin = new SelectList(db.States.OrderBy(x => x.StateName), "StateName", "StateName");

            return View(model);
        }

        #region Experience

        public ActionResult Experience(int Profileid)
        {

            var item = db.ExperienceModels.Where(x => x.UserProfileId == Profileid).ToList();
            ViewBag.list = item;
            ViewBag.id = Profileid;
            return PartialView();
        }

        // POST: Edu/DataImages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Experience(ExperienceModel model)
        {
            if (ModelState.IsValid)
            {

                db.ExperienceModels.Add(model);
                db.SaveChanges();
                TempData["alert"] = "alert(\"Update Successful \");";
                return Redirect(Url.Action("Profile", "Account") + "#Experience");
            }

            return Redirect(Url.Action("Profile", "Account") + "#Experience");
        }
        public ActionResult DeleteExperience(int id)
        {
            ExperienceModel model = db.ExperienceModels.Find(id);
            db.ExperienceModels.Remove(model);
            db.SaveChanges();
            TempData["alert"] = "alert(\"Update Successful \");";

            return Redirect(Url.Action("Profile", "Account") + "#Experience");
        }

        #endregion

        #region Skills

        public ActionResult Skill(int Profileid)
        {

            var item = db.SkillModels.Where(x => x.UserProfileId == Profileid).ToList();
            ViewBag.list = item;
            ViewBag.id = Profileid;
            return PartialView();
        }

        // POST: Edu/DataImages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Skill(SkillModel model)
        {
            if (ModelState.IsValid)
            {

                db.SkillModels.Add(model);
                db.SaveChanges();
                TempData["alert"] = "alert(\"Update Successful \");";

                return Redirect(Url.Action("Profile", "Account") + "#Skill");
            }

            return Redirect(Url.Action("Profile", "Account") + "#Skill");
        }
        public ActionResult DeleteSkill(int id)
        {
            SkillModel model = db.SkillModels.Find(id);
            db.SkillModels.Remove(model);
            db.SaveChanges();
            TempData["alert"] = "alert(\"Update Successful \");";

            return Redirect(Url.Action("Profile", "Account") + "#Skill");
        }

        #endregion


        #region Education

        public ActionResult Education(int Profileid)
        {

            var item = db.EducationHistoryModels.Where(x => x.UserProfileId == Profileid).ToList();
            ViewBag.list = item;
            ViewBag.id = Profileid;
            return PartialView();
        }

        // POST: Edu/DataImages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Education(EducationHistoryModel model)
        {
            if (ModelState.IsValid)
            {

                db.EducationHistoryModels.Add(model);
                db.SaveChanges();
                return Redirect(Url.Action("Profile", "Account") + "#Education");
            }

            return Redirect(Url.Action("Profile", "Account") + "#Education");
        }
        public ActionResult DeleteEducation(int id)
        {
            EducationHistoryModel model = db.EducationHistoryModels.Find(id);
            db.EducationHistoryModels.Remove(model);
            db.SaveChanges();
            return Redirect(Url.Action("Profile", "Account") + "#Education");
        }

        #endregion

        #region Language

        public ActionResult Language(int Profileid)
        {

            var item = db.LanguageSpokenModels.Where(x => x.UserProfileId == Profileid).ToList();
            ViewBag.list = item;
            ViewBag.id = Profileid;
            return PartialView();
        }

        // POST: Edu/DataImages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Language(LanguageSpokenModel model)
        {
            if (ModelState.IsValid)
            {

                db.LanguageSpokenModels.Add(model);
                db.SaveChanges();
                return Redirect(Url.Action("Profile", "Account") + "#Language");
            }

            return Redirect(Url.Action("Profile", "Account") + "#Language");
        }
        public ActionResult DeleteLanguage(int id)
        {
            LanguageSpokenModel model = db.LanguageSpokenModels.Find(id);
            db.LanguageSpokenModels.Remove(model);
            db.SaveChanges();
            return Redirect(Url.Action("Profile", "Account") + "#Language");
        }

        #endregion

        #region SubjectSpecialistModel

        public ActionResult SubjectSpecialistModel(int Profileid)
        {

            var item = db.SubjectSpecialistModels.Where(x => x.UserProfileId == Profileid).ToList();
            ViewBag.list = item;
            ViewBag.id = Profileid;
            return PartialView();
        }

        // POST: Edu/DataImages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SubjectSpecialistModel(SubjectSpecialistModel model)
        {
            if (ModelState.IsValid)
            {

                db.SubjectSpecialistModels.Add(model);
                db.SaveChanges();
                return Redirect(Url.Action("Profile", "Account") + "#SubjectSpecialistModel");
            }

            return Redirect(Url.Action("Profile", "Account") + "#SubjectSpecialistModel");
        }
        public ActionResult DeleteSubjectSpecialistModel(int id)
        {
            SubjectSpecialistModel model = db.SubjectSpecialistModels.Find(id);
            db.SubjectSpecialistModels.Remove(model);
            db.SaveChanges();
            return Redirect(Url.Action("Profile", "Account") + "#SubjectSpecialistModel");
        }

        #endregion

        #region Certificate

        public ActionResult Certificate(int Profileid)
        {

            var item = db.MeritCertificateModels.Where(x => x.UserProfileId == Profileid).ToList();
            ViewBag.list = item;
            ViewBag.id = Profileid;
            return PartialView();
        }

        // POST: Edu/DataImages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Certificate(MeritCertificateModel model)
        {
            if (ModelState.IsValid)
            {

                db.MeritCertificateModels.Add(model);
                db.SaveChanges();
                return Redirect(Url.Action("Profile", "Account") + "#Certificate");
            }

            return Redirect(Url.Action("Profile", "Account") + "#Certificate");
        }
        public ActionResult DeleteCertificate(int id)
        {
            MeritCertificateModel model = db.MeritCertificateModels.Find(id);
            db.MeritCertificateModels.Remove(model);
            db.SaveChanges();
            return Redirect(Url.Action("Profile", "Account") + "#Certificate");
        }

        #endregion

        #region MembershipOfLearnered

        public ActionResult MembershipOfLearnered(int Profileid)
        {

            var item = db.MembershipOfLearneredSocietyModels.Where(x => x.UserProfileId == Profileid).ToList();
            ViewBag.list = item;
            ViewBag.id = Profileid;
            return PartialView();
        }

        // POST: Edu/DataImages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MembershipOfLearnered(MembershipOfLearneredSocietyModel model)
        {
            if (ModelState.IsValid)
            {

                db.MembershipOfLearneredSocietyModels.Add(model);
                db.SaveChanges();
                return Redirect(Url.Action("Profile", "Account") + "#MembershipOfLearnered");
            }

            return Redirect(Url.Action("Profile", "Account") + "#MembershipOfLearnered");
        }
        public ActionResult DeleteMembershipOfLearnered(int id)
        {
            MembershipOfLearneredSocietyModel model = db.MembershipOfLearneredSocietyModels.Find(id);
            db.MembershipOfLearneredSocietyModels.Remove(model);
            db.SaveChanges();
            return Redirect(Url.Action("Profile", "Account") + "#MembershipOfLearnered");
        }

        #endregion

        #region TrainingAndWorkShopModels

        public ActionResult TrainingAndWorkShopModels(int Profileid)
        {

            var item = db.TrainingAndWorkShopModels.Where(x => x.UserProfileId == Profileid).ToList();
            ViewBag.list = item;
            ViewBag.id = Profileid;
            return PartialView();
        }

        // POST: Edu/DataImages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TrainingAndWorkShopModels(TrainingAndWorkShopModel model)
        {
            if (ModelState.IsValid)
            {

                db.TrainingAndWorkShopModels.Add(model);
                db.SaveChanges();
                return Redirect(Url.Action("Profile", "Account") + "#TrainingAndWorkShopModel");
            }

            return Redirect(Url.Action("Profile", "Account") + "#TrainingAndWorkShopModel");
        }
        public ActionResult DeleteTrainingAndWorkShopModels(int id)
        {
            TrainingAndWorkShopModel model = db.TrainingAndWorkShopModels.Find(id);
            db.TrainingAndWorkShopModels.Remove(model);
            db.SaveChanges();
            return Redirect(Url.Action("Profile", "Account") + "#TrainingAndWorkShopModel");
        }

        #endregion

        [AllowAnonymous]
        public ActionResult MyCV(string email)
        {

            string user = "";
            if (String.IsNullOrEmpty(email))
            {
                if (!User.Identity.IsAuthenticated)
                {

                    return RedirectToAction("Login", "Auth", new { area = "" });
                }
                else
                {
                    user = User.Identity.Name;
                }

            }
            else
            {
                user = email;
            }

            var userinfo = db.Users.FirstOrDefault(x => x.Email == user);
            var profile = db.UserProfileModels.FirstOrDefault(x => x.UserId == userinfo.Id);
            profile.CV_Views += 1;
            db.Entry(profile).State = EntityState.Modified;
            db.SaveChanges();

            var exp = db.ExperienceModels.Where(x => x.UserProfileId == profile.Id).ToList();
            var skill = db.SkillModels.Where(x => x.UserProfileId == profile.Id).ToList();
            var edu = db.EducationHistoryModels.Where(x => x.UserProfileId == profile.Id).ToList();
            var lang = db.LanguageSpokenModels.Where(x => x.UserProfileId == profile.Id).ToList();
            var cert = db.MeritCertificateModels.Where(x => x.UserProfileId == profile.Id).ToList();
            var mem = db.MembershipOfLearneredSocietyModels.Where(x => x.UserProfileId == profile.Id).ToList();
            var train = db.TrainingAndWorkShopModels.Where(x => x.UserProfileId == profile.Id).ToList();
            var spec = db.SubjectSpecialistModels.Where(x => x.UserProfileId == profile.Id).ToList();
            var output = new CompleteProfileDto
            {
                Id = profile.Id,
                Title = profile.Title,
                Email = userinfo.Email,
                Phone = userinfo.PhoneNumber,
                SurName = profile.SurName,
                FirstName = profile.FirstName,
                LastName = profile.LastName,
                ContactAddress = profile.ContactAddress,
                Country = profile.Country,

                Description = profile.Description,
                DateOfBirth = profile.DateOfBirth,

                FacebookLink = profile.FacebookLink,
                TwitterLink = profile.TwitterLink,
                LinkedinLink = profile.LinkedinLink,
                PhotoUrl = profile.PhotoUrl,
                ComplementryCardKeywords = profile.ComplementryCardKeywords,
                IskoolId = profile.IskoolId,
                Gender = profile.Gender,
                MaritalStatus = profile.MaritalStatus,
                ExperienceModels = exp.ToList(),
                SkillModels = skill.ToList(),
                EducationHistoryModels = edu.ToList(),
                LanguageSpokenModels = lang.ToList(),
                MeritCertificateModels = cert.ToList(),
                MembershipOfLearneredSocietyModels = mem.ToList(),
                TrainingAndWorkShopModels = train.ToList(),
                SubjectSpecialistModels = spec.ToList()



            };



            return View(output);
        }

        public ActionResult NotFoundItem()
        {
            return View();

        }
        #region dashbard
        public ActionResult _DashImage()
        {
            string user = User.Identity.GetUserId();
            var userinfo = db.Users.FirstOrDefault(x => x.Id == user);
            ViewBag.user = userinfo;
            var profile = db.UserProfileModels.FirstOrDefault(x => x.UserId == user);

            ViewBag.viewsCV = profile.CV_Views;
            return PartialView(profile);
        }

        public ActionResult _DashboardWidget()
        {
            string user = User.Identity.GetUserId();
            var userinfo = db.Users.FirstOrDefault(x => x.Id == user);
            ViewBag.user = userinfo;
            var profile = db.UserProfileModels.FirstOrDefault(x => x.UserId == user);

            var videos = db.Topics.Where(x => x.UserId == user).Count();
            ViewBag.videos = videos;

            ViewBag.viewsCV = profile.CV_Views;
            ViewBag.viewsPortfolio = profile.Portfolio_Views;

            var teacher = db.UserProfileModels.Include(x => x.User).Where(x => x.UserId == user).FirstOrDefault();
            var wallet = db.TeacherWallet.Include(x => x.User).Include(x => x.Teacher).Where(x => x.UserId == user && x.TeacherId == teacher.Id);
            var wallet2 = db.TeacherWallet.Include(x => x.User).Include(x => x.Teacher).Where(x => x.UserId == user && x.TeacherId == teacher.Id).FirstOrDefault();
            if (wallet2 != null)
            {
                ViewBag.wallet = wallet.Sum(x => x.Amount);
            }
            else
            {
                ViewBag.wallet = "0.00";
            }


            var withdrawal = db.TeacherWithdrawal.Include(x => x.Teacher).Where(x => x.TeacherId == teacher.Id);
            var withdrawal2 = db.TeacherWithdrawal.Include(x => x.Teacher).Where(x => x.TeacherId == teacher.Id && x.Status == WithdrawalStatus.Approved).FirstOrDefault();
            if (withdrawal2 != null)
            {
                ViewBag.withdrawal = withdrawal.Sum(x => x.Amount);
            }
            else
            {
                ViewBag.withdrawal = "0.00";
            }


            var subscribers = db.SubscriptionCommision.Include(x => x.Teacher)
                .Include(x => x.Subject).Include(x => x.StudentModel).Where(x => x.TeacherId == teacher.Id).Count();
            ViewBag.subscribers = subscribers;

            return PartialView(profile);
        }
        #endregion

        public ActionResult NewVideo()
        {
            var students = db.Subjects.Include(x => x.ClassLevel).Select(x => new SubjectsDto
            {

                Id = x.Id,
                ListItem = x.ClassLevel.Name + " - " + x.Name

            });

            ViewBag.SubjectId = new SelectList(students, "Id", "ListItem");
            string user = User.Identity.GetUserId();
            var profile = db.UserProfileModels.FirstOrDefault(x => x.UserId == user);

            if (String.IsNullOrEmpty(profile.Title) || String.IsNullOrEmpty(profile.State) || String.IsNullOrEmpty(profile.Religion))
            {
                TempData["msg"] = "Kindly update your profile, to enjoy the full features of iskools.";
                return RedirectToAction("Profile", "Account", new { area = "Dashboard" });
            }
            return View();
        }

        // POST: Edu/Topics/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> NewVideo(Topic topic, HttpPostedFileBase upload)
        {
            var user = User.Identity.GetUserId();
            var profile = db.UserProfileModels.FirstOrDefault(x => x.UserId == user);
            if (ModelState.IsValid)
            {
                try
                {

                    if (upload != null && upload.ContentLength > 0)
                    {

                        FileInfo fi = new FileInfo(upload.FileName);
                        string tits = topic.Title.Replace(" ", "-");
                        string date1 = DateTime.UtcNow.AddHours(1).ToString("dd/MM/yyyy");
                        string name = tits + "-" + date1 + "-By-" + profile.SurName + "-" + profile.FirstName + fi.Extension;
                        string fileName = Path.GetFileName(name);
                        topic.VideoFile = fileName;
                        fileName = Path.Combine(Server.MapPath("~/Videos/"), fileName);
                        upload.SaveAs(fileName);

                    }

                }
                catch (Exception c) { }
                topic.UserId = user;
                topic.Date = DateTime.UtcNow.AddHours(1);
                db.Topics.Add(topic);
                await db.SaveChangesAsync();

                var vtopic = db.Topics.FirstOrDefault(x => x.Id == topic.Id);
                vtopic.VideoId = "iskools/Video/" + vtopic.Id;
                db.Entry(vtopic).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Success");
            }

            var students = db.Subjects.Include(x => x.ClassLevel).Select(x => new SubjectsDto
            {

                Id = x.Id,
                ListItem = x.ClassLevel.Name + " - " + x.Name

            });

            ViewBag.SubjectId = new SelectList(students, "Id", "ListItem");
            return View(topic);
        }

        public ActionResult Success()
        {
            return View();
        }

        public async Task<ActionResult> AllTopics()
        {
            string user = User.Identity.GetUserId();
            var profile = db.UserProfileModels.FirstOrDefault(x => x.UserId == user);

            if (String.IsNullOrEmpty(profile.Title) || String.IsNullOrEmpty(profile.State) || String.IsNullOrEmpty(profile.Religion))
            {
                TempData["msg"] = "Kindly update your profile, to enjoy the full features of iskools.";
                return RedirectToAction("Profile", "Account", new { area = "Dashboard" });
            }

            var items = await db.Topics.Include(x => x.Subject).Include(x => x.Subject.ClassLevel).Where(x => x.Title != "" && x.Publish == true).OrderBy(x => x.Subject.ClassLevel.Name).ToListAsync(); ;

            return View(items);
        }
        public async Task<ActionResult> MyVideos()
        {
            var user = User.Identity.GetUserId();
            var profile = db.UserProfileModels.FirstOrDefault(x => x.UserId == user);

            if (String.IsNullOrEmpty(profile.Title) || String.IsNullOrEmpty(profile.State) || String.IsNullOrEmpty(profile.Religion))
            {
                TempData["msg"] = "Kindly update your profile, to enjoy the full features of iskools.";
                return RedirectToAction("Profile", "Account", new { area = "Dashboard" });
            }
            var items = await db.Topics.Include(x => x.Subject).Include(x => x.Subject.ClassLevel).Where(x => x.UserId == user).OrderBy(x => x.Subject.ClassLevel.Name).ToListAsync(); ;

            return View(items);
        }

        public async Task<ActionResult> VideoDetails(int? id)
        {
            string user = User.Identity.GetUserId();
            var profile = db.UserProfileModels.FirstOrDefault(x => x.UserId == user);

            if (String.IsNullOrEmpty(profile.Title) || String.IsNullOrEmpty(profile.State) || String.IsNullOrEmpty(profile.Religion))
            {
                TempData["msg"] = "Kindly update your profile, to enjoy the full features of iskools.";
                return RedirectToAction("Profile", "Account", new { area = "Dashboard" });
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var items = await db.Topics.Include(x => x.Subject).Include(x => x.Subject.ClassLevel).FirstOrDefaultAsync(x => x.Id == id);
            if (items == null)
            {
                return HttpNotFound();
            }

            return View(items);
        }

        public ActionResult Reviews(string id)
        {
            var revi = db.UserReviews.Where(x => x.UserId == id).ToList();
            return View(revi);
        }


        public ActionResult _Covid19()
        {
            var doc = new HtmlAgilityPack.HtmlDocument();
            HtmlAgilityPack.HtmlNode.ElementsFlags["br"] = HtmlAgilityPack.HtmlElementFlag.Empty;
            doc.OptionWriteEmptyNodes = true;

            try
            {
                var webRequest = HttpWebRequest.Create("https://covid19.ncdc.gov.ng/");
                Stream stream = webRequest.GetResponse().GetResponseStream();
                doc.Load(stream);
                stream.Close();
            }
            catch (System.UriFormatException uex)
            {

            }
            catch (System.Net.WebException wex)
            {

            }

            //get the div by id and then get the inner text id=""
            string testDivSelector = "//table[@id='custom1']";
            var divString = doc.DocumentNode.SelectSingleNode(testDivSelector).InnerHtml.ToString();

            string testDivSelector2 = "//table[@id='custom3']";
            var divString2 = doc.DocumentNode.SelectSingleNode(testDivSelector2).InnerHtml.ToString();

            string testDivSelector3 = "//h3[b]";
            var divString3 = doc.DocumentNode.SelectNodes(testDivSelector3).ToList();
            var output = "";
            foreach (var i in divString3)
            {
                if (i.InnerHtml.ToString().Contains("CASE SUMMARY IN"))
                {
                    output = i.InnerHtml.ToString();
                    break;
                }
            }

            ViewBag.status1 = divString;
            ViewBag.status2 = divString2;
            ViewBag.status3 = output;
            return PartialView();
        }

    }
}