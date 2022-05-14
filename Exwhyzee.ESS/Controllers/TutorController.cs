using Exwhyzee.ESS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using Exwhyzee.ESS.Models.Entities;
using PagedList;
using Exwhyzee.ESS.Models.Entities.Dto;
using Lucene.Net.Support;

namespace Exwhyzee.ESS.Controllers
{
    public class TutorController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Tutor
        public ActionResult _Topics()
        {
            var rnd = new Random();

            var items = db.Topics.Include(x => x.Subject).Include(x=>x.TopicReview).Include(x => x.Subject.ClassLevel).Where(x=>x.Title != null).ToList();
            items = items.ToList();
            var kg = items.Where(x => x.Subject.ClassLevel.SchoolType == School.KindergartenAndNursery).Take(10).ToList();
            var pri = items.Where(x => x.Subject.ClassLevel.SchoolType == School.Primary).Take(10).ToList();
            var sec = items.Where(x => x.Subject.ClassLevel.SchoolType == School.Secondary).Take(10).ToList();
            var motiv = items.Where(x => x.Subject.ClassLevel.SchoolType == School.Motivation).Take(10).ToList();

            ViewBag.kg = kg;
            ViewBag.pri = pri;
            ViewBag.sec = sec;
            ViewBag.motiv = motiv;

            ViewBag.kgc = kg.Count();
            ViewBag.pric = pri.Count();
            ViewBag.secc = sec.Count();
            ViewBag.motivc = motiv.Count();
            return PartialView();
        }

        public ActionResult _TopicsFooter()
        {
            var items = db.Topics.Include(x => x.Subject).Include(x => x.Subject.ClassLevel).Where(x => x.Title != "").OrderByDescending(x => x.Views).Take(3).ToList();

            return PartialView(items);
        }
        public async Task<ActionResult> TopicIndex(string searchString, string currentFilter, int? page, int id)
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
            var items = db.Topics.Include(x => x.Subject).Include(x => x.Subject.ClassLevel).Where(x => x.SubjectId == id);
            if (!String.IsNullOrEmpty(searchString))
            {

                items = items.Where(s => s.Subject.Name.ToUpper().Contains(searchString.ToUpper()) ||
                               s.Title.ToUpper().Contains(searchString.ToUpper())
                                                              );
            }

            int pageSize = 50;
            int pageNumber = (page ?? 1);
            ViewBag.Total = items.Count();
            return View(items.OrderByDescending(x => x.Id).ToPagedList(pageNumber, pageSize));
        }

        public async Task<ActionResult> AdvanceSearch(string searchString, string currentFilter, string Querysearch, int? page)
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
            var items = db.Topics.Include(x => x.Subject).Where(x => x.Title != "");

            string[] querry = Querysearch.Split(',').Select(str => str.Trim()).ToArray();

            if (!String.IsNullOrEmpty(searchString))
            {

                items = items.Where(s => s.Subject.Name.ToUpper().Contains(searchString.ToUpper()) ||
                               s.Title.ToUpper().Contains(searchString.ToUpper())
                                                              );
            }

            var students = db.Topics.Include(x => x.Subject.ClassLevel).Include(x => x.Subject).Select(x => new TopicDto
            {
                subject = x.Subject.Name,
                classlevel = x.Subject.ClassLevel.Name,
                topic = x.Title,
                Querysearch = x.Subject.ClassLevel.Name + " - " + x.Subject.Name + " - " + x.Title

            });

            ViewBag.Querysearch = new SelectList(students, "Querysearch", "Querysearch");


            int pageSize = 50;
            int pageNumber = (page ?? 1);
            ViewBag.Total = items.Count();
            return View(items.OrderByDescending(x => x.Id).ToPagedList(pageNumber, pageSize));
        }

        public async Task<ActionResult> ClassIndex(string searchString, string currentFilter, int? page, int? enumm)
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
            var items = db.ClassLevels.Include(x => x.Subjects).ToList();


            if (!String.IsNullOrEmpty(searchString))
            {

                items = items.Where(s => s.Name.ToUpper().Contains(searchString.ToUpper())).ToList();

            }

            int pageSize = 50;
            int pageNumber = (page ?? 1);
            ViewBag.Total = items.Count();
            return View(items.OrderByDescending(x => x.Id).ToPagedList(pageNumber, pageSize));
        }

        public async Task<ActionResult> SubjectIndex(string searchString, string currentFilter, int? page, int? enumm, string fgc)
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
            var items = db.Subjects.Include(x => x.ClassLevel).Include(x => x.Topics).Where(x => x.Name != "").ToList();
            if (enumm == 1)
            {
                items = items.Where(x => x.ClassLevel.SchoolType == Exwhyzee.ESS.Models.Entities.School.KindergartenAndNursery).ToList();

            }

            else if (enumm == 3)
            {
                items = items.Where(x => x.ClassLevel.SchoolType == Exwhyzee.ESS.Models.Entities.School.Primary).ToList();

            }
            else if (enumm == 4)
            {
                items = items.Where(x => x.ClassLevel.SchoolType == Exwhyzee.ESS.Models.Entities.School.Secondary).ToList();

            }
            else if (enumm == 5)
            {
                items = items.Where(x => x.ClassLevel.SchoolType == Exwhyzee.ESS.Models.Entities.School.Motivation).ToList();

            }

            if (!String.IsNullOrEmpty(searchString))
            {

                items = items.Where(s => s.ClassLevel.Name.ToUpper().Contains(searchString.ToUpper()) ||
                s.Description.ToUpper().Contains(searchString.ToUpper()) ||
                s.Name.ToUpper().Contains(searchString.ToUpper()) 
                ).ToList();

            }

            int pageSize = 50;
            int pageNumber = (page ?? 1);
            ViewBag.Total = items.Count();


            ViewBag.name = fgc;
            return View(items.OrderByDescending(x => x.Id).ToPagedList(pageNumber, pageSize));
        }


        public ActionResult _Events()
        {
            var items = db.Posts.Where(x => x.Title != "").OrderByDescending(x => x.SortOrder).Take(6).ToList();

            return PartialView(items);
        }

        public ActionResult _EventsFooter()
        {
            var items = db.Posts.Where(x => x.Title != "").OrderByDescending(x => x.SortOrder).Skip(3).Take(3).ToList();

            return PartialView(items);
        }
        public ActionResult Events()
        {
            var items = db.Posts.Where(x => x.Title != "").OrderByDescending(x => x.SortOrder).Take(6).ToList();

            return View(items);
        }
        public async Task<ActionResult> Details(int? id, string title)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var topic = await db.Topics.Include(x => x.Subject).Include(x => x.Subject.ClassLevel).Include(x => x.TopicReview).FirstOrDefaultAsync(x => x.Id == id);
            if (topic == null)
            {
                return HttpNotFound();
            }
            var review = db.TopicReview.Include(x => x.Topic).Where(x => x.TopicId == id);
            ViewBag.review = review.Count();

            var category = await db.Topics.Include(x => x.Subject).Include(x => x.Subject.ClassLevel).Include(x => x.TopicReview).Where(x => x.SubjectId == topic.SubjectId && x.Subject.ClassLevelId == topic.Subject.ClassLevelId).ToListAsync();
            ViewBag.category = category;

            var intro = await db.Topics.Include(x => x.Subject).Include(x => x.Subject.ClassLevel).Include(x => x.TopicReview).FirstOrDefaultAsync(x => x.SubjectId == topic.SubjectId && x.Subject.ClassLevelId == topic.Subject.ClassLevelId);
            ViewBag.intro = intro;

            var popular = await db.Topics.Include(x => x.Subject).Include(x => x.Subject.ClassLevel).Include(x => x.TopicReview).Where(x => x.Subject.ClassLevelId == topic.Subject.ClassLevelId && x.Id != id).OrderByDescending(x => x.Views).Take(8).ToListAsync();
            ViewBag.popular = popular;

            var tag = await db.Subjects.Include(x => x.ClassLevel).Include(x => x.Topics).Where(x => x.ClassLevelId == topic.Subject.ClassLevelId).ToListAsync();
            ViewBag.tag = tag;

            int viewnuber = topic.Views + 1;
            topic.Views = viewnuber;
            db.Entry(topic).State = EntityState.Modified;
            await db.SaveChangesAsync();

            return View(topic);
        }

        public async Task<ActionResult> UpdateDetails(int? id, string title)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var topic = await db.Posts.FirstOrDefaultAsync(x => x.Id == id);
            if (topic == null)
            {
                return HttpNotFound();
            }
            return View(topic);
        }
        public async Task<ActionResult> Name(string name, string email, string school, string phone)
        {
            int id = 0;
            var check = db.Participants.FirstOrDefault(x => x.Phone == phone || x.Email == email || x.Name == name);
            if (check == null)
            {
                Participant part = new Participant();
                part.Name = name;
                part.Phone = phone;
                part.Email = email;
                part.School = school;
                part.Date = DateTime.UtcNow.AddHours(1);
                db.Participants.Add(part);
                db.SaveChanges();

                TempData["NameId"] = part.Id;
                TempData["NameName"] = part.Name;
                TempData["e"] = part.Email;
                TempData["p"] = part.Phone;
                id = part.Id;
            }
            else
            {
                TempData["NameId"] = check.Id;
                TempData["NameName"] = check.Name;
                TempData["Welcome"] = "Welcome Back";
                TempData["idpart"] = check.Id;
                TempData["tryagain"] = "tryagain";
                TempData["e"] = check.Email;
                TempData["p"] = check.Phone;
                id = check.Id;

            }

            string msg = "Welcome to ISKOOL" + TempData["NameName"] +" with phone and email: "+ TempData["e"] +" and "+ TempData["p"]+ ". Wants to attempt 5 questions with correct answers.";
            try
            {

                await HelperClass.SendSMS(msg, "08165680904,07087894399,08145718441");
                await HelperClass.MainMailToAdmin(msg);
               

            }
            catch (Exception e)
            {
                await HelperClass.SendSMS(msg, "08165680904,07087894399,08145718441");
                await HelperClass.MainMailToAdmin(msg);

            }
            return RedirectToAction("Question", "Tutor", new { id = id });
        }

        public async Task<ActionResult> UpdateQuestion(QuestDto questDto)
        {
            var participant = db.Participants.FirstOrDefault(x => x.Id == questDto.Id6);
            ParticipantResult partresult = new ParticipantResult();
            int resultid = 0;
            if (participant != null)
            {
                var question = db.QuestionMain.ToList();
                // checked first one
                var first = question.FirstOrDefault(x => x.Id == Convert.ToInt32(questDto.A1id));
                if (first != null)
                {
                    if (questDto.A1 != null)
                    {
                        if (first.Answer.ToLower() == questDto.A1.ToLower())
                        {
                            partresult.Q1 = true;
                        }
                    }
                }
                // checked second one
                var second = question.FirstOrDefault(x => x.Id == Convert.ToInt32(questDto.A2id));
                if (second != null)
                {
                    if (questDto.A2 != null)
                    {

                        if (second.Answer.ToLower() == questDto.A2.ToLower())
                        {
                            partresult.Q2 = true;
                        }
                    }
                }
                // checked third one
                var third = question.FirstOrDefault(x => x.Id == Convert.ToInt32(questDto.A3id));
                if (third != null)
                {
                    if (questDto.A3 != null)
                    {

                        if (third.Answer.ToLower() == questDto.A3.ToLower())
                        {
                            partresult.Q3 = true;
                        }
                    }
                }
                // checked fourt one
                var fourt = question.FirstOrDefault(x => x.Id == Convert.ToInt32(questDto.A4id));
                if (fourt != null)
                {
                    if (questDto.A4 != null)
                    {

                        if (fourt.Answer.ToLower() == questDto.A4.ToLower())
                        {
                            partresult.Q4 = true;
                        }
                    }
                }
                // checked fifth one
                var fifth = question.FirstOrDefault(x => x.Id == Convert.ToInt32(questDto.A5id));
                if (fifth != null)
                {
                    if (questDto.A5 != null)
                    {

                        if (fifth.Answer.ToLower() == questDto.A5.ToLower())
                        {
                            partresult.Q5 = true;
                        }
                    }
                }

                partresult.ParticipantId = participant.Id;

                db.ParticipantResults.Add(partresult);
                db.SaveChanges();
                resultid = partresult.Id;
            }

            TempData["newId"] = resultid;
            int active = resultid;
            string a1 = "";
            string a2 = "";
            string a3 = "";
            string a4 = "";
            string a5 = "";
            if (partresult.Q1 == true)
            {
                a1 = "G";
            }
            else
            {
                a1 = "F";
            }
            if (partresult.Q2 == true)
            {
                a2 = "G";
            }
            else
            {
                a2 = "F";
            }
            if (partresult.Q3 == true)
            {
                a3 = "G";
            }
            else
            {
                a3 = "F";
            }
            if (partresult.Q4 == true)
            {
                a4 = "G";
            }
            else
            {
                a4 = "F";
            }
            if (partresult.Q5 == true)
            {
                a5 = "G";
            }
            else
            {
                a5 = "F";
            }

            string msg = "result of " + partresult.Participant.Name + "is " + a1 +" | " + a2+" | " + a3 + " | " + a4 + " | " + a5;
            try
            {

                await HelperClass.SendSMS(msg, "08165680904,07087894399,08145718441");
                await HelperClass.MainMailToAdmin(msg);

            
            }
            catch (Exception e)
            {
                await HelperClass.SendSMS(msg, "08165680904,07087894399,08145718441");
                await HelperClass.MainMailToAdmin(msg);

            }
            return Json(active, JsonRequestBehavior.AllowGet);

        }



        public ActionResult UserInfoResult(int id)
        {
            var u = db.ParticipantResults.Include(x=>x.Participant).FirstOrDefault(x => x.Id == id);
            return View(u);
        }

            public ActionResult Question(int id)
        {
            var namespart = db.Participants.FirstOrDefault(x => x.Id == id);
            
            var rnd = new Random();
            var questions = db.QuestionMain.Include(x => x.QuestionCategory).ToList();
            var question1 = db.QuestionMain.Include(x => x.QuestionCategory).Where(x => x.QuestionCategoryId == 1).ToList();

            var Q11 = question1.OrderBy(x => rnd.Next()).Take(1).ToList();
            var Q1 = Q11.Take(1).ToList();

            var question2 = db.QuestionMain.Include(x => x.QuestionCategory).Where(x => x.QuestionCategoryId == 2).ToList();

            var Q22 = question2.OrderBy(x => rnd.Next()).Take(1).ToList();
            var Q2 = Q22.Take(1).ToList();

            var question3 = db.QuestionMain.Include(x => x.QuestionCategory).Where(x => x.QuestionCategoryId == 3).ToList();

            var Q33 = question3.OrderBy(x => rnd.Next()).Take(1).ToList();
            var Q3 = Q33.Take(1).ToList();

            var question4 = db.QuestionMain.Include(x => x.QuestionCategory).Where(x => x.QuestionCategoryId == 4).ToList();

            var Q44 = question4.OrderBy(x => rnd.Next()).Take(2).ToList();

            var Q4 = Q44.Take(2).ToList();

            List<QuestionMain> Qall = new List<QuestionMain>();
            List<QuestionMain> ReQall = new List<QuestionMain>();
            //Qall = Q1.AddRange(Q2);
            Qall.AddRange(Q1);
            Qall.AddRange(Q2);
            Qall.AddRange(Q3);
            Qall.AddRange(Q4);
            Qall.Take(5).ToList();

            ReQall = Qall.OrderBy(x => rnd.Next()).ToList();

          //  Qall = Q4.ToList();
            ViewBag.names = namespart.Name;
            ViewBag.id = namespart.Id;

            TempData["timmer"] = "truetimmer";
            return View(ReQall.Take(5).ToList());
        }

        [HttpPost]
        public async Task<ActionResult> TopicReview(CommentDto commenting)
        {
            TopicReview comment = new TopicReview();
            comment.Name = commenting.Name;
            comment.Email = commenting.Email;
            comment.Review = commenting.Review;
            comment.TopicId = Convert.ToInt32(commenting.RId);
            //comment.Rating = rating;
            comment.DateReview = DateTime.UtcNow.AddHours(1);
            db.TopicReview.Add(comment);
            await db.SaveChangesAsync();

            //  string resultt = "<div class=\"card-body\" style=\"padding-left:0px;border-bottom:2px solid #808080;padding-bottom:4px;padding-top:0px;\"><h5 class=\"card-title\" style=\"margin:0px;\">"++"</h5><h6 class=\"card-title\" style=\"margin:0px;\">jdkk@krfkf.fkfk</h6><p class=\"card-text\">With supporting text below as a natural lead-in to additional content.</p></div>";
            string resultt = "<div class=\"card-body\" style=\"padding-left:0px;border-bottom:1px solid #868282;padding-bottom:4px;padding-top:0px;color:#868282;\"><h5 class=\"card-title\" style=\"margin:0px;color:#868282;\">" + commenting.Name+ "</h5><h6 class=\"card-title\" style=\"margin:0px;color:#868282;\">" + commenting.Email+"<span style=\"margin-left:4px;\">("+comment.DateReview.Value.ToString("dd MMM, yyyy  HH:mm:tt") +")</span></h6><p class=\"card-text\">"+comment.Review+"</p></div>";
            return Json(resultt, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AskQuestion()
        {
            return View();
        }

        // POST: Edu/Questions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AskQuestion(Question question)
        {
            if (ModelState.IsValid)
            {
                question.Date = DateTime.UtcNow.AddHours(1);
                db.Questions.Add(question);
                await db.SaveChangesAsync();
                return RedirectToAction("Comfirm", new { email = question.Email });
            }

            return View(question);
        }

        public ActionResult Comfirm(string email)
        {
            var user = db.Users.FirstOrDefault(x => x.Email == email);
            if (user == null)
            {
                return RedirectToAction("CreateAccount", "Auth", new { email = email });
            }
            else
            {
                return RedirectToAction("Response", "Tutor");

            }
        }

        public ActionResult Response()
        {

            return View();
        }

        public ActionResult GetStarted()
        {

            return View();
        }



        public ActionResult _ItemCount()
        {
            return PartialView();
        }

    }
}