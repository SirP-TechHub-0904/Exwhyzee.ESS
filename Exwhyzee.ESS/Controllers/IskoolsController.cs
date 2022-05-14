using Exwhyzee.ESS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using Exwhyzee.ESS.Models.Entities;
using Exwhyzee.ESS.Models.Entities.Dto;
using System.Threading.Tasks;
using Exwhyzee.ESS.Models.Profile;
using PagedList;

namespace Exwhyzee.ESS.Controllers
{
    public class IskoolsController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index(int? id, string tryagain)
        {
            TempData["idpart"] = id;
            TempData["tryagain"] = tryagain;
            return View();
        }

        public ActionResult About()
        {

            return View();
        }

        public ActionResult Volunteer()
        {

            return View();
        }

        public ActionResult Contact()
        {

            return View();
        }

        [HttpPost]
  
        public async Task<ActionResult> Mail(string Name, string Email, string TextMsg)
        {
            try
            {
                string textt = "Website Contact--- Name: " + Name + " ---Email: " + Email + " ----Message: " + TextMsg;
               await HelperClass.SendMail("onwukaemeka41@gmail.com", textt);
                TempData["msg"] = "Mail Sent Successful. We will contact you soon";
                return RedirectToAction("Contact");
            }
            catch(Exception c)
            {

            }
            TempData["error"] = "Error! Try again";
            return RedirectToAction("Contact");
            }
        public ActionResult Teacher(string searchString, string currentFilter, int? page)
        {
            var profile = db.UserProfileModels.Include(x => x.User).Where(x=>x.ShowTeacher == true).OrderByDescending(x => x.DateRegistered).ToList();
            var topics = db.Topics.ToList();
            var reviewsa = db.UserReviews.ToList();
            var subjectsp = db.SubjectSpecialistModels.ToList();
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;


            var output = profile.Select(x => new UserWithInfo
            {
                Id = x.Id,
                Title = x.Title,
                Email = x.User.Email,
                Phone = x.User.PhoneNumber,
                SurName = x.SurName,
                FirstName = x.FirstName,
                LastName = x.LastName,
                ContactAddress = x.ContactAddress,
                Country = x.Country,
                CV_Views = x.CV_Views,
                Portfolio_Views = x.Portfolio_Views,
                DateOfBirth = x.DateOfBirth,
                LastSeen = TimeAgo(x.User.LastSeen),
                FacebookLink = x.FacebookLink,
                TwitterLink = x.TwitterLink,
                LinkedinLink = x.LinkedinLink,
                PhotoUrl = x.PhotoUrl,
                ComplementryCardKeywords = x.ComplementryCardKeywords,
                IskoolId = x.IskoolId,
                Gender = x.Gender,
                Topics = topics.Where(s => s.UserId == x.UserId).ToList(),
                Reviews = reviewsa.Where(s => s.UserId == x.UserId).ToList(),
                SubjectSpecialistModels = subjectsp.Where(s => s.UserProfileId == x.Id).ToList(),
                SubjectString = string.Join(" * ", subjectsp.Where(s => s.UserProfileId == x.Id).Select(r=>r.Subject)),
                Verified = x.Verified
        });




            if (!String.IsNullOrEmpty(searchString))
            {
                
                output = output.Where(s => (s.FirstName != null) && s.FirstName.ToUpper().Contains(searchString.ToUpper()) 
                || (s.LastName != null) && s.LastName.ToUpper().Contains(searchString.ToUpper())
                || (s.SurName != null) && s.SurName.ToUpper().Contains(searchString.ToUpper())
                || (s.LastSeen != null) && s.LastSeen.ToUpper().Contains(searchString.ToUpper())
                || (s.IskoolId != null) && s.IskoolId.ToUpper().Contains(searchString.ToUpper())
                || (s.Title != null) && s.Title.ToUpper().Contains(searchString.ToUpper())
                || (!String.IsNullOrEmpty(s.SubjectString)) && s.SubjectString.ToUpper().Contains(searchString.ToUpper())
                || s.Email.ToUpper().Contains(searchString.ToUpper())
                || (s.Phone != null) && s.Phone.ToUpper().Contains(searchString.ToUpper())
                                                              );
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            ViewBag.Total = output.Count();
            return View(output.OrderByDescending(x => x.Id).ToPagedList(pageNumber, pageSize));


        }

        public static string TimeAgo(DateTime? dt)
        {
            if (dt != null)
            {
                TimeSpan span = DateTime.Now - dt.Value;
                if (span.Days > 365)
                {
                    int years = (span.Days / 365);
                    if (span.Days % 365 != 0)
                        years += 1;
                    return String.Format("about {0} {1} ago",
                    years, years == 1 ? "year" : "years");
                }
                if (span.Days > 30)
                {
                    int months = (span.Days / 30);
                    if (span.Days % 31 != 0)
                        months += 1;
                    return String.Format("about {0} {1} ago",
                    months, months == 1 ? "month" : "months");
                }
                if (span.Days > 0)
                    return String.Format("about {0} {1} ago",
                    span.Days, span.Days == 1 ? "day" : "days");
                if (span.Hours > 0)
                    return String.Format("about {0} {1} ago",
                    span.Hours, span.Hours == 1 ? "hour" : "hours");
                if (span.Minutes > 0)
                    return String.Format("about {0} {1} ago",
                    span.Minutes, span.Minutes == 1 ? "minute" : "minutes");
                if (span.Seconds > 5)
                    return String.Format("about {0} seconds ago", span.Seconds);
                if (span.Seconds <= 5)
                    return "just now";
            }
            return string.Empty;
        }
        public ActionResult TeacherDetails(string uid)
        {
            var items = db.UserProfileModels.Include(x => x.User).FirstOrDefault(x => x.UserId == uid);
            return View(items);
        }

        public ActionResult _ReviewByTeacher(string uid)
        {
            var review = db.UserReviews.Where(x => x.UserId == uid).ToList();
            return View();
        }

        public ActionResult Features()
        {

            return View();

        }
        public ActionResult Benefits()
        {

            return View();
        }
        public ActionResult Referrals()
        {

            return View();
        }
        public ActionResult Faqs()
        {

            return View();
        }
        public ActionResult Pricing()
        {

            return View();
        }

        public ActionResult Listing()
        {
            var sch = db.SchoolRankings.ToList();

            var data = db.SchoolPortalDatas.ToList();

            var students = data.Select(x => new SchoolRanking
            {
                Position = 0,
                Name = x.SchoolName,
                SchoolType = x.SchoolType,
                State = x.SchoolAddress,
                IT = "True",
                Website = x.WebUrl
            });
            List<SchoolRanking> Rank = new List<SchoolRanking>();
            Rank.AddRange(sch);
            Rank.AddRange(students);
            Rank.Where(x => x.Name != "" || x.Name != null).OrderByDescending(x => x.Name).ToList();
            return View(Rank);
        }

        public ActionResult _TopicsListing()
        {
            var sch = db.Topics.Include(x => x.Subject).Include(x => x.Subject.ClassLevel).ToList();
            var mot = sch.Where(x => x.Subject.ClassLevel.SchoolType == Exwhyzee.ESS.Models.Entities.School.Motivation).Take(3).ToList();
            var sec = sch.Where(x => x.Subject.ClassLevel.SchoolType == Exwhyzee.ESS.Models.Entities.School.Secondary).Take(3).ToList();
            var prim = sch.Where(x => x.Subject.ClassLevel.SchoolType == Exwhyzee.ESS.Models.Entities.School.Primary).Take(3).ToList();

            List<Topic> listing = new List<Topic>();
            listing.AddRange(mot);
            listing.AddRange(sec);
            listing.AddRange(prim);

            return PartialView(listing);
        }
        [Authorize]
        public ActionResult AddReview(string uid)
        {
            ViewBag.id = uid;
            return View();
        }

        // POST: Games/Games/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddReview(UserReview model)
        {
            if (ModelState.IsValid)
            {
                db.UserReviews.Add(model);
                await db.SaveChangesAsync();
                return RedirectToAction("FindATeacher");
            }

            return View(model);
        }

        public JsonResult LgaList(string Id)
        {
            var stateId = db.States.FirstOrDefault(x => x.StateName == Id).Id;
            var local = from s in db.LocalGovs
                        where s.StatesId == stateId
                        select s;

            return Json(new SelectList(local.ToArray(), "LGAName", "LGAName"), JsonRequestBehavior.AllowGet);
        }
        public ActionResult _Slider()
        {
            var slide = db.ImageSliders.Where(x => x.CurrentSlider == true).OrderBy(x=>x.OrderSort).ToList();
            return PartialView(slide);
        }

    }
}