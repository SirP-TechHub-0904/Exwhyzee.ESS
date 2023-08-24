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
using System.Web.Script.Serialization;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using Exwhyzee.ESS.Models.Entities.Dto;

namespace Exwhyzee.ESS.Areas.Admin.Controllers
{
    [Authorize(Roles = "SuperAdmin,Admin")]



    public class DatasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/SchoolPortalDatas
        public async Task<ActionResult> Index()
        {

            var sch = db.SchoolPortalDatas.AsQueryable();

            ViewBag.schools = await sch.CountAsync();
            ViewBag.Primary = await sch.Where(x => x.SchoolType.ToLower().Contains("prim")).CountAsync();
            ViewBag.AllPrimary = await sch.Where(x => x.SchoolType.ToLower().Contains("prim") && x.AddAsActive == true).CountAsync();
            ViewBag.Secondary = await sch.Where(x => x.SchoolType.ToLower().Contains("second")).CountAsync();
            ViewBag.AllSecondary = await sch.Where(x => x.SchoolType.ToLower().Contains("second") && x.AddAsActive == true).CountAsync();
            ViewBag.Active = await sch.Where(x => x.AddAsActive == true).CountAsync();
            ViewBag.Nonactive = await sch.Where(x => x.AddAsActive == false).CountAsync();

            ViewBag.ActiveEnrolledStudents = sch
        .Where(x => x.AddAsActive == true)
        .Select(x => new
        {
            EnrolStudentsCount = x.EnrolStudentsCount
        })
        .AsEnumerable() // Switch to LINQ to Objects to use the Sum method
        .Sum(x => int.TryParse(x.EnrolStudentsCount, out int count) ? count : 0);

            ViewBag.NonUsedcard = sch
       .Where(x => x.AddAsActive == true)
       .Select(x => new
       {
           NonUsedcard = x.NonUsedcard
       })
       .AsEnumerable() // Switch to LINQ to Objects to use the Sum method
       .Sum(x => int.TryParse(x.NonUsedcard, out int count) ? count : 0);

            ViewBag.Totalcard = sch
     .Where(x => x.AddAsActive == true)
     .Select(x => new
     {
         Totalcard = x.Totalcard
     })
     .AsEnumerable() // Switch to LINQ to Objects to use the Sum method
     .Sum(x => int.TryParse(x.Totalcard, out int count) ? count : 0);

            ViewBag.Usedcard = sch
       .Where(x => x.AddAsActive == true)
       .Select(x => new
       {
           Usedcard = x.Usedcard
       })
       .AsEnumerable() // Switch to LINQ to Objects to use the Sum method
       .Sum(x => int.TryParse(x.Usedcard, out int count) ? count : 0);

            ViewBag.TotalStaff = sch
       .Where(x => x.AddAsActive == true)
       .Select(x => new
       {
           TotalStaff = x.TotalStaff
       })
       .AsEnumerable() // Switch to LINQ to Objects to use the Sum method
       .Sum(x => int.TryParse(x.TotalStaff, out int count) ? count : 0);



            return View();
        }
        public async Task<ActionResult> Schools(int id = 0, bool active = true, string typex = null)
        {

            var sch = db.SchoolPortalDatas.OrderByDescending(x => x.SchoolName).Where(x => x.AddAsActive == active).AsQueryable();
            if (typex != null)
            {
                sch = sch.Where(x => x.SchoolType.ToLower().Contains(typex)).AsQueryable();
            }
            if (id > 0)
            {
                sch = sch.Where(x => x.SchoolCategoryId == id).AsQueryable();
            }

                ViewBag.schools = await sch.CountAsync();
            ViewBag.Primary = await sch.Where(x => x.SchoolType.ToLower().Contains("prim")).CountAsync();
            ViewBag.AllPrimary = await sch.Where(x => x.SchoolType.ToLower().Contains("prim") && x.AddAsActive == true).CountAsync();
            ViewBag.Secondary = await sch.Where(x => x.SchoolType.ToLower().Contains("second")).CountAsync();
            ViewBag.AllSecondary = await sch.Where(x => x.SchoolType.ToLower().Contains("second") && x.AddAsActive == true).CountAsync();
            ViewBag.Active = await sch.Where(x => x.AddAsActive == true).CountAsync();
            ViewBag.Nonactive = await sch.Where(x => x.AddAsActive == false).CountAsync();

            ViewBag.ActiveEnrolledStudents = sch
        .Where(x => x.AddAsActive == true)
        .Select(x => new
        {
            EnrolStudentsCount = x.EnrolStudentsCount
        })
        .AsEnumerable() // Switch to LINQ to Objects to use the Sum method
        .Sum(x => int.TryParse(x.EnrolStudentsCount, out int count) ? count : 0);

            ViewBag.NonUsedcard = sch
       .Where(x => x.AddAsActive == true)
       .Select(x => new
       {
           NonUsedcard = x.NonUsedcard
       })
       .AsEnumerable() // Switch to LINQ to Objects to use the Sum method
       .Sum(x => int.TryParse(x.NonUsedcard, out int count) ? count : 0);

            ViewBag.Totalcard = sch
     .Where(x => x.AddAsActive == true)
     .Select(x => new
     {
         Totalcard = x.Totalcard
     })
     .AsEnumerable() // Switch to LINQ to Objects to use the Sum method
     .Sum(x => int.TryParse(x.Totalcard, out int count) ? count : 0);

            ViewBag.Usedcard = sch
       .Where(x => x.AddAsActive == true)
       .Select(x => new
       {
           Usedcard = x.Usedcard
       })
       .AsEnumerable() // Switch to LINQ to Objects to use the Sum method
       .Sum(x => int.TryParse(x.Usedcard, out int count) ? count : 0);

            ViewBag.TotalStaff = sch
       .Where(x => x.AddAsActive == true)
       .Select(x => new
       {
           TotalStaff = x.TotalStaff
       })
       .AsEnumerable() // Switch to LINQ to Objects to use the Sum method
       .Sum(x => int.TryParse(x.TotalStaff, out int count) ? count : 0);



            return View(await sch.ToListAsync());
        }
        public async Task<ActionResult> ViewSchools(int id)
        {

            var sch = await db.SchoolPortalDatas.Where(x => x.SelectedAsActive == true && x.SchoolCategoryId == id).OrderByDescending(x => x.SchoolName).ToListAsync();
            var xtitle = await db.SchoolCategories.FirstOrDefaultAsync(x => x.Id == id);
            TempData["title"] = xtitle.Name;
            return View(sch);
        }

        public async Task<ActionResult> UpdateSchool(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SchoolPortalData schoolSession = await db.SchoolPortalDatas.FindAsync(id);
            if (schoolSession == null)
            {
                return HttpNotFound();
            }
            ViewBag.SchoolCategoryId = new SelectList(db.SchoolCategories, "Id", "Name");

            return View(schoolSession);
        }

        // POST: Admin/SchoolSessions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UpdateSchool(SchoolPortalData schoolSession)
        {
            if (ModelState.IsValid)
            {
                SchoolPortalData x = await db.SchoolPortalDatas.FindAsync(schoolSession.Id);
                x.DateAdded = DateTime.UtcNow.AddHours(1);
                x.SchoolCategoryId = schoolSession.SchoolCategoryId;
                x.SelectedAsActive = schoolSession.SelectedAsActive;
                db.Entry(x).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.SchoolCategoryId = new SelectList(db.SchoolCategories, "Id", "Name");

            return View(schoolSession);
        }

        public async Task<ActionResult> Category()
        {

            var sch = await db.SchoolCategories.Include(x => x.SchoolPortalDatas).ToListAsync();
            return View(sch);
        }

        public async Task<ActionResult> ResultDMMM(string session, string term)
        {
            ViewBag.session = session;
            ViewBag.term = term;

            var sch = await db.SchoolPortalDatas.Where(x => x.EnrolStudentsCount != "0").Where(x => x.EnrolStudentsCount != null).OrderByDescending(x => x.SchoolName).ToListAsync();
            return View(sch);
        }
        public async Task<ActionResult> SchoolsWithResult(string Term)
        {

            var sch = await db.SchoolPortalDatas.Where(x => Convert.ToInt32(x.EnrolStudentsCount) > 1).OrderByDescending(x => x.SchoolName).ToListAsync();
            return View(sch);
        }
        public async Task<ActionResult> SchoolsWithoutStudent()
        {

            var sch = await db.SchoolPortalDatas.Where(x => Convert.ToInt32(x.EnrolStudentsCount) < 1).OrderByDescending(x => x.SchoolName).ToListAsync();
            return View(sch);
        }

        [AllowAnonymous]
        public async Task<ActionResult> ComprehensiveList()
        {

            var sch = await db.SchoolPortalDatas.OrderByDescending(x => x.SchoolName).ToListAsync();
            return View(sch);
        }

        [AllowAnonymous]
        public async Task<ActionResult> AddToList(int id)
        {

            var sch = await db.SchoolPortalDatas.FindAsync(id);
            if (sch != null)
            {
                sch.AddAsActive = true;
                db.Entry(sch).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        public JsonResult LgaList(string Id)
        {
            var schools = db.SchoolPortalDatas.OrderByDescending(x => x.SchoolName);

            return Json(schools, JsonRequestBehavior.AllowGet);
        }
        public async Task<ActionResult> RefreshAll()
        {
            try
            {
                var portals = await db.SchoolPortalDatas.Where(x=>x.AddAsActive == true).ToListAsync();
                foreach (var item in portals)
                {
                    try
                    {
                        string endurl = ":80/api/SchoolApi/GetSchool";
                        string starturl = "http://";
                        string apiUrl = String.Format(item.PortalUrl + endurl);

                        WebRequest requestObj = WebRequest.Create(apiUrl);
                        requestObj.Method = "GET";

                        HttpWebResponse responseGet = null;
                        responseGet = (HttpWebResponse)requestObj.GetResponse();
                        string result = null;
                        using (Stream stream = responseGet.GetResponseStream())
                        {
                            StreamReader sr = new StreamReader(stream);

                            result = sr.ReadToEnd();
                            SchoolPortalDataDto school = await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<SchoolPortalDataDto>(result));
                            var model = await db.SchoolPortalDatas.FirstOrDefaultAsync(x => x.PortalUrl == item.PortalUrl);

                            model.ClassCount = school.ClassCount;
                            model.SiteName = school.SiteName;
                            model.SchoolName = school.SchoolName;
                            model.SchoolAddress = school.SchoolAddress;
                            model.SchoolCurrentPrincipal = school.SchoolCurrentPrincipal;
                            model.EnrolStudentsCount = school.EnrolStudentsCount;
                            model.TotalStudentsCount = school.TotalStudentsCount;
                            model.UnEnrolStudentsCount = school.UnEnrolStudentsCount;
                            model.WebUrl = school.Url;
                            model.Usedcard = school.Usedcard;
                            model.NonUsedcard = school.NonUsedcard;
                            model.Totalcard = school.Totalcard;
                            model.TotalStaff = school.TotalStaff;
                            model.CurrentSession = school.CurrentSession;
                            model.LastModifiedDate = DateTime.UtcNow.AddHours(1);
                            model.ClassCount = school.ClassCount;
                            model.SchoolType = school.SchoolType;
                            model.Term = school.Term;
                            model.Session = school.Session;
                            model.BatchResultPrint = school.BatchPrint;
                            db.Entry(model).State = EntityState.Modified;
                            await db.SaveChangesAsync();

                            sr.Close();
                        }
                    }
                    catch (Exception c) { }
                }
            }

            catch (WebException webex)
            {
                WebResponse errResp = webex.Response;
                using (Stream respStream = errResp.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(respStream);
                    string text = reader.ReadToEnd();
                }
            }

            return RedirectToAction("Index");
        }

        public async Task<ActionResult> RefreshSingleSchool(string url)
        {
            try
            {
                var item = await db.SchoolPortalDatas.FirstOrDefaultAsync(x => x.PortalUrl == url);

                string endurl = ":80/api/SchoolApi/GetSchool";
                // string starturl = "http://";

                string apiUrl = String.Format(item.PortalUrl + endurl);

                WebRequest requestObj = WebRequest.Create(apiUrl);
                requestObj.Method = "GET";

                HttpWebResponse responseGet = null;
                responseGet = (HttpWebResponse)requestObj.GetResponse();
                string result = null;
                using (Stream stream = responseGet.GetResponseStream())
                {
                    StreamReader sr = new StreamReader(stream);
                    result = sr.ReadToEnd();
                    SchoolPortalDataDto school = await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<SchoolPortalDataDto>(result));
                    var model = await db.SchoolPortalDatas.FirstOrDefaultAsync(x => x.PortalUrl == item.PortalUrl);

                    model.ClassCount = school.ClassCount;
                    model.SiteName = school.SiteName;
                    model.SchoolName = school.SchoolName;
                    model.SchoolAddress = school.SchoolAddress;
                    model.SchoolCurrentPrincipal = school.SchoolCurrentPrincipal;
                    model.EnrolStudentsCount = school.EnrolStudentsCount;
                    model.TotalStudentsCount = school.TotalStudentsCount;
                    model.UnEnrolStudentsCount = school.UnEnrolStudentsCount;
                    model.WebUrl = school.Url;
                    model.Usedcard = school.Usedcard;
                    model.NonUsedcard = school.NonUsedcard;
                    model.Totalcard = school.Totalcard;
                    model.TotalStaff = school.TotalStaff;
                    model.CurrentSession = school.CurrentSession;
                    model.LastModifiedDate = DateTime.UtcNow.AddHours(1);
                    model.ClassCount = school.ClassCount;
                    model.SchoolType = school.SchoolType;
                    model.Term = school.Term;
                    model.Session = school.Session;
                    model.BatchResultPrint = school.BatchPrint;
                    db.Entry(model).State = EntityState.Modified;
                    await db.SaveChangesAsync();

                    sr.Close();
                }


            }

            catch (WebException webex)
            {
                WebResponse errResp = webex.Response;
                using (Stream respStream = errResp.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(respStream);
                    string text = reader.ReadToEnd();
                }
            }

            return RedirectToAction("Index");
        }


        public async Task<ActionResult> ResultsBySection(string Section)
        {
            ViewBag.ses = Section;
            try
            {
                //var item = await db.SchoolPortalDatas.Where(x => x.AddAsActive == true).ToListAsync();

                IQueryable<SchoolPortalData> itemlist = from s in db.SchoolPortalDatas

                   .Where(x => x.AddAsActive == true)
                                                        select s;


                IQueryable<ResultList> listProduct = Enumerable.Empty<ResultList>().AsQueryable();
                List<ResultList> alist = new List<ResultList>();
                foreach (var item in itemlist)
                {
                    ResultList ill = new ResultList();
                    try
                    {

                        string endurl = "/api/SchoolApi/GetAllResultsBySession";
                        string starturl = "http://";
                        string apiUrl = String.Format(starturl + item.PortalUrl.Replace("http://", "") + endurl);
                        apiUrl = apiUrl.Replace("http://http://", "http://");

                        WebRequest requestObj = WebRequest.Create(apiUrl);
                        requestObj.Method = "GET";

                        HttpWebResponse responseGet = null;
                        responseGet = (HttpWebResponse)requestObj.GetResponse();
                        string result = null;
                        List<ResultGetBySession> schools = new List<ResultGetBySession>();
                        using (Stream stream = responseGet.GetResponseStream())
                        {
                            StreamReader sr = new StreamReader(stream);
                            result = sr.ReadToEnd();
                            schools = await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<List<ResultGetBySession>>(result));
                            var ischools = schools.FirstOrDefault(x => x.Session == Section);
                            sr.Close();
                            ill.School = item.SchoolName;
                            ill.Result = ischools.ResultCount;
                            ill.Session = ischools.Session;
                        }
                    }
                    catch (Exception s)
                    {

                    }

                    alist.Add(ill);
                }




                return View(alist.ToList());
            }

            catch (WebException webex)
            {
                WebResponse errResp = webex.Response;
                using (Stream respStream = errResp.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(respStream);
                    string text = reader.ReadToEnd();
                }
            }

            return RedirectToAction("Index");
        }

        public async Task<ActionResult> SchoolSessions(string url)
        {
            try
            {
                var item = await db.SchoolPortalDatas.FirstOrDefaultAsync(x => x.PortalUrl == url);
                ViewBag.schoolname = item.SchoolName;
                string endurl = "/api/SchoolApi/GetSchoolSessions";
                string starturl = "http://";
                string apiUrl = String.Format(starturl + item.PortalUrl + endurl);

                apiUrl = apiUrl.Replace("http://http://", "http://");

                WebRequest requestObj = WebRequest.Create(apiUrl);
                requestObj.Method = "GET";

                HttpWebResponse responseGet = null;
                responseGet = (HttpWebResponse)requestObj.GetResponse();
                string result = null;
                List<ApiSessionList> schools = new List<ApiSessionList>();
                using (Stream stream = responseGet.GetResponseStream())
                {
                    StreamReader sr = new StreamReader(stream);
                    result = sr.ReadToEnd();
                    schools = await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<List<ApiSessionList>>(result));

                    sr.Close();
                }
                ViewBag.url = url;
                return View(schools);
            }

            catch (WebException webex)
            {
                WebResponse errResp = webex.Response;
                using (Stream respStream = errResp.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(respStream);
                    string text = reader.ReadToEnd();
                }
            }

            return RedirectToAction("Index");
        }

        public async Task<ActionResult> SessionsDetails(string url, int sessionid)
        {
            try
            {
                var item = await db.SchoolPortalDatas.FirstOrDefaultAsync(x => x.PortalUrl == url);
                ViewBag.schoolname = item.SchoolName;

                string endurl = "/api/SchoolApi/GetSessionDetails?sessionId=" + sessionid;
                //:80/api/SchoolApi/GetSchoolSessions
                string starturl = "http://";
                string apiUrl = String.Format(starturl + item.PortalUrl + endurl);
                apiUrl = apiUrl.Replace("http://http://", "http://");

                WebRequest requestObj = WebRequest.Create(apiUrl);
                requestObj.Method = "GET";

                HttpWebResponse responseGet = null;
                responseGet = (HttpWebResponse)requestObj.GetResponse();
                string result = null;
                ApiSessionDetails school = null;
                using (Stream stream = responseGet.GetResponseStream())
                {
                    StreamReader sr = new StreamReader(stream);
                    result = sr.ReadToEnd();
                    school = await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<ApiSessionDetails>(result));

                    sr.Close();
                }
                ViewBag.session = school.CurrentSession;
                return View(school);
            }

            catch (WebException webex)
            {
                WebResponse errResp = webex.Response;
                using (Stream respStream = errResp.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(respStream);
                    string text = reader.ReadToEnd();
                }
            }

            return RedirectToAction("Index");
        }

        // GET: Admin/SchoolPortalDatas/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SchoolPortalData schoolPortalData = await db.SchoolPortalDatas.FindAsync(id);
            if (schoolPortalData == null)
            {
                return HttpNotFound();
            }
            return View(schoolPortalData);
        }

        // GET: Admin/SchoolPortalDatas/Create
        [AllowAnonymous]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/SchoolPortalDatas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<ActionResult> Create(SchoolPortalData schoolPortalData)
        {
            if (ModelState.IsValid)
            {
                var check = db.SchoolPortalDatas.FirstOrDefault(x => x.PortalUrl.ToLower() == schoolPortalData.PortalUrl.ToLower());
                if (check == null)
                {
                    schoolPortalData.DateAdded = DateTime.UtcNow.AddHours(1);
                    schoolPortalData.LastModifiedDate = DateTime.UtcNow.AddHours(1);
                    schoolPortalData.DateCeated = DateTime.UtcNow.AddHours(1);
                    db.SchoolPortalDatas.Add(schoolPortalData);
                    await db.SaveChangesAsync();

                    return RedirectToAction("Index");
                }
            }
            TempData["error"] = "exist";
            return View(schoolPortalData);
        }

        // GET: Admin/SchoolPortalDatas/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SchoolPortalData schoolPortalData = await db.SchoolPortalDatas.FindAsync(id);
            if (schoolPortalData == null)
            {
                return HttpNotFound();
            }
            return View(schoolPortalData);
        }

        // POST: Admin/SchoolPortalDatas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(SchoolPortalData schoolPortalData)
        {
            if (ModelState.IsValid)
            {
                db.Entry(schoolPortalData).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(schoolPortalData);
        }

        // GET: Admin/SchoolPortalDatas/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SchoolPortalData schoolPortalData = await db.SchoolPortalDatas.FindAsync(id);
            if (schoolPortalData == null)
            {

                return HttpNotFound();
            }
            return View(schoolPortalData);
        }

        // POST: Admin/SchoolPortalDatas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            SchoolPortalData schoolPortalData = await db.SchoolPortalDatas.FindAsync(id);
            db.SchoolPortalDatas.Remove(schoolPortalData);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        //
        public async Task<ActionResult> SchoolAnalysis(string url)
        {
            var schoolsession = await db.SessionAnalyses.Where(x => x.PortalUrl == url).ToListAsync();
            return View(schoolsession);
        }
        //
        public async Task<ActionResult> RefreshSchoolAnalysis(string url)
        {
            try
            {
                var item = await db.SchoolPortalDatas.FirstOrDefaultAsync(x => x.PortalUrl == url);
                ViewBag.schoolname = item.SchoolName;
                string endurl = "/api/SchoolApi/GetSchoolSessions";
                string starturl = "http://";
                string apiUrl = String.Format(starturl + item.PortalUrl + endurl);
                apiUrl = apiUrl.Replace("http://http://", "http://");

                WebRequest requestObj = WebRequest.Create(apiUrl);
                requestObj.Method = "GET";

                HttpWebResponse responseGet = null;
                responseGet = (HttpWebResponse)requestObj.GetResponse();
                string result = null;
                List<ApiSessionList> schools = new List<ApiSessionList>();
                using (Stream stream = responseGet.GetResponseStream())
                {
                    StreamReader sr = new StreamReader(stream);
                    result = sr.ReadToEnd();
                    schools = await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<List<ApiSessionList>>(result));
                    foreach (var sch in schools)
                    {
                        //                        var checksession = await db.SessionAnalyses.FirstOrDefaultAsync(x => x.PortalUrl == url && x.Session == sch.Session && x.Term == sch.Term);
                        //                        if(checksession == null)
                        //                        {
                        //SessionAnalysis sessionAnalysis = new SessionAnalysis();
                        //                            sessionAnalysis.Term = sch.Term;
                        //                            sessionAnalysis.StaffCount = sch
                        //                        }

                    }


                    sr.Close();
                }
                ViewBag.url = url;
                return View();
            }

            catch (WebException webex)
            {

            }

            return RedirectToAction("Index");
        }

        public async Task<ActionResult> AddNumber(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SchoolPortalData schoolPortalData = await db.SchoolPortalDatas.FindAsync(id);
            if (schoolPortalData == null)
            {
                return HttpNotFound();
            }
            ViewBag.sId = schoolPortalData.Id;
            ViewBag.info = schoolPortalData.SchoolName;
            return View();
        }

        // POST: Admin/SchoolPortalDatas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddNumber(int SchoolId, string Phone)
        {
            if (String.IsNullOrEmpty(Phone))
            {
                var sch = db.SchoolPortalDatas.FirstOrDefault(x => x.Id == SchoolId);
                sch.ContactPhoneNumber = Phone;
                db.Entry(sch).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.sId = SchoolId;
            return View();
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

    public class ResultList
    {
        public string Session { get; set; }
        public string School { get; set; }
        public string Result { get; set; }
    }
    public class ResultGetBySession
    {
        public string ResultCount { get; set; }
        public string Session { get; set; }
    }
}
