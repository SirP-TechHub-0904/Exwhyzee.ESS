using Exwhyzee.ESS.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using System.Net;
using Exwhyzee.ESS.Models.Entities.Dto.Zoom;
using Exwhyzee.ESS.Controllers;
using Newtonsoft.Json;
using Exwhyzee.ESS.Models.Entities.Dto;
using Exwhyzee.ESS.Models.Entities;
using System.Net.Mail;
using System.IO;

namespace Exwhyzee.ESS.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class OnlineClassController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/OnlineClass
        public async Task<ActionResult> Index()
        {
            try
            {
                var prof = await db.OnlineZooms.Include(x => x.LiveClassLevel).Include(x => x.User).ToListAsync();

                return View(prof);
            }
            catch (Exception c)
            {

            }
            return View();
        }

        public async Task<ActionResult> LiveClassList()
        {
            // var setting = await
            var model = await db.OnlineZooms.Include(x => x.LiveClassLevel).Include(x => x.User).ToListAsync();
            string uid = User.Identity.GetUserId();
            if (User.IsInRole("SuperAdmin"))
            {

                return View(model);
            }
            if (User.IsInRole("Admin"))
            {

                return View(model);
            }
            model = model.Where(x => x.UserId == uid).ToList();

            return View(model);
        }

        public async Task<ActionResult> DetailsLiveClass(long id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            try
            {

                List<User> userdata = new List<User>();
                var content = TokenManager.GetAMeeting(id);

                //userdata = JsonConvert.DeserializeObject<List<ZoomUserDto>>(response.Content);
                RootobjectDetails data = JsonConvert.DeserializeObject<RootobjectDetails>(content);
                var prof = await db.OnlineZooms.Include(x => x.LiveClassLevel).Include(x => x.User).FirstOrDefaultAsync(x => x.MeetingId == data.id);
                ViewBag.prof = prof;
                return View(data);
            }
            catch (Exception c)
            {

            }
            return View();
        }

        public async Task<ActionResult> LiveClassRecording(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            try
            {

                List<User> userdata = new List<User>();
                var content = TokenManager.GetAMeetingRecording(id);

                //userdata = JsonConvert.DeserializeObject<List<ZoomUserDto>>(response.Content);
                RootobjectMeetingRecord data = JsonConvert.DeserializeObject<RootobjectMeetingRecord>(content);
                var prof = await db.OnlineZooms.Include(x => x.LiveClassLevel).Include(x => x.User).FirstOrDefaultAsync(x => x.MeetingId == data.id);
                ViewBag.prof = prof;
                return View(data.recording_files);
            }
            catch (Exception c)
            {

            }
            return View();
        }

        public async Task<ActionResult> LiveClassParticipant(long id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            try
            {

                List<User> userdata = new List<User>();
                var content = TokenManager.GetAMeetingParticipant(id);

                //userdata = JsonConvert.DeserializeObject<List<ZoomUserDto>>(response.Content);
                RootobjectParticipant data = JsonConvert.DeserializeObject<RootobjectParticipant>(content);
                var prof = await db.OnlineZooms.Include(x => x.LiveClassLevel).Include(x => x.User).FirstOrDefaultAsync(x => x.MeetingId == id);
                ViewBag.prof = prof;
                return View(data.participants.ToList());
            }
            catch (Exception c)
            {

            }
            return View();
        }

        public async Task<ActionResult> NewLiveClass()
        {
            if (User.IsInRole("SuperAdmin") || User.IsInRole("Admin"))
            {
                var item = db.UserProfileModels.Include(x => x.User);
                var output = item.Select(x => new TeacherDropdownListDto
                {
                    StaffId = x.Id,
                    FullName = x.SurName + " " + x.FirstName + " " + x.LastName,
                    UserId = x.UserId
                });
                await output.OrderBy(x => x.FullName).ToListAsync();
                ViewBag.User = new SelectList(output, "UserId", "FullName");
            }
            ViewBag.Classlist = new SelectList(db.LiveClassLevel, "Id", "Name");

            return View();
        }

        // POST: Content/Assignments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> NewLiveClass(OnlineZoom model)
        {
            if (ModelState.IsValid)
            {
                var zoomset = db.ZoomSetting.FirstOrDefault();

                model.DateCreated = DateTime.UtcNow.AddHours(1);
                db.OnlineZooms.Add(model);
                await db.SaveChangesAsync();
              
                RootobjectNewMeeting datamodel = new RootobjectNewMeeting();
                datamodel.topic = "Iskools" + " Live Class Schedule";

                datamodel.type = model.MeetingType;
                datamodel.start_time = model.ClassDate + " " + model.ClassTime;
                datamodel.duration = model.Duration;

                datamodel.password = model.ClassPassword;
                datamodel.agenda = model.Description;
                var content = TokenManager.NewMeeting(datamodel, zoomset.ZoomHostEmail);
                RootobjectDetails data = JsonConvert.DeserializeObject<RootobjectDetails>(content);

                //
                var classmodel = await db.OnlineZooms.FirstOrDefaultAsync(x => x.Id == model.Id);
                classmodel.MeetingId = data.id;
                classmodel.MeetingUUId = data.uuid;
                db.Entry(classmodel).State = EntityState.Modified;
                await db.SaveChangesAsync();
                TempData["success"] = "Successfully created.";
                string mass = "";
                try
                {

                    MailMessage mail = new MailMessage();

                    //set the addresses 
                    mail.From = new MailAddress("learnonline@iskools.com.ng"); //IMPORTANT: This must be same as your smtp authentication address.
                    mail.To.Add("espErrorMail@exwhyzee.ng");
                    mail.To.Add("iskoolsportal@gmail.com");
                    mail.To.Add("onwukaemeka41@gmail.com");
                    mail.To.Add("bernardamaeme@gmail.com");

                    //set the content 

                    mail.Subject = " Live Class Request from " + "Iskools";

                    mass = "Iskools" + " - " + "www.iskools.com.ng" + "/Admin/OnlineClass/DetailsLiveClass/" + data.id + " - visit for more info";

                    mail.Body = mass;
                    //send the message 
                    SmtpClient smtp = new SmtpClient("mail.iskools.com.ng");

                    //IMPORANT:  Your smtp login email MUST be same as your FROM address. 
                    NetworkCredential Credentials = new NetworkCredential("learnonline@iskools.com.ng", "Exwhyzee@123");
                    smtp.Credentials = Credentials;
                    smtp.Send(mail);

                }
                catch (Exception ex)
                {


                }

                try
                {
                    string urlString = "http://xyzsms.com/api/ApiXyzSms/ComposeMessage?username=onwuka1&password=nation&recipients=08165680904,08136662653,07087894399&senderId=ISKOOLS&smsmessage=" + mass + "&smssendoption=SendNow";
                    //  string urlString = "http://www.xyzsms.com/components/com_spc/smsapi.php?username=" + senderUserName + "&password=" + senderPassword + "&sender=" + senderId + "&recipient=" + recipient + "&message=" + message;
                    string response = "";
                    try
                    {
                        HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(urlString);
                        httpWebRequest.Method = "GET";
                        httpWebRequest.ContentType = "application/json";

                        //getting the respounce from the request
                        HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                        Stream responseStream = httpWebResponse.GetResponseStream();
                        StreamReader streamReader = new StreamReader(responseStream);
                        response = streamReader.ReadToEnd();
                    }
                    catch (Exception d)
                    {

                    }
                }
                catch (Exception c) { }

                return RedirectToAction("DetailsLiveClass", new { id = data.id });
            }
            if (User.IsInRole("SuperAdmin") || User.IsInRole("Admin"))
            {
                var item = db.UserProfileModels.Include(x => x.User);
                var output = item.Select(x => new TeacherDropdownListDto
                {
                    StaffId = x.Id,
                    FullName = x.SurName + " " + x.FirstName + " " + x.LastName,
                    UserId = x.UserId
                });
                await output.OrderBy(x => x.FullName).ToListAsync();
                ViewBag.User = new SelectList(output, "UserId", "FullName");
            }
            ViewBag.Classlist = new SelectList(db.LiveClassLevel, "Id", "Name");

            return View(model);
        }

        // GET: Content/Assignments/Edit/5
        public async Task<ActionResult> EditLiveClass(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var model = await db.LiveClassOnlines.Include(x => x.ClassLevel).Include(x => x.User).FirstOrDefaultAsync(x => x.Id == id);
            if (model == null)
            {
                return HttpNotFound();
            }
            if (User.IsInRole("SuperAdmin") || User.IsInRole("Admin"))
            {
                var item = db.UserProfileModels.Include(x => x.User);
                var output = item.Select(x => new TeacherDropdownListDto
                {
                    StaffId = x.Id,
                    FullName = x.SurName + " " + x.FirstName + " " + x.LastName,
                    UserId = x.UserId
                });
                await output.OrderBy(x => x.FullName).ToListAsync();
                ViewBag.User = new SelectList(output, "UserId", "FullName");
            }
            ViewBag.Classlist = new SelectList(db.LiveClassLevel, "Id", "Name");

            return View(model);
        }

        // POST: Content/Assignments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> EditLiveClass(LiveClassOnline data, string LiveStatusString)
        {
            if (ModelState.IsValid)
            {
                var model = await db.LiveClassOnlines.FirstOrDefaultAsync(x => x.Id == data.Id);
                //if (LiveStatusString == "active")
                //{
                //    model.LiveStatus = LiveStatus.Active;
                //}
                //if (LiveStatusString == "waiting")
                //{
                //    model.LiveStatus = LiveStatus.Waiting;
                //}
                //if (LiveStatusString == "ended")
                //{
                //    model.LiveStatus = LiveStatus.Ended;
                //}
                model.LiveClassLevelId = data.LiveClassLevelId;
                model.UrlLive = data.UrlLive;
                model.ClassDate = data.ClassDate;
                model.ClassTime = data.ClassTime;
                model.Duration = data.Duration;

                db.Entry(model).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("LiveClassList");
            }
            if (User.IsInRole("SuperAdmin") || User.IsInRole("Admin"))
            {
                var item = db.UserProfileModels.Include(x => x.User);
                var output = item.Select(x => new TeacherDropdownListDto
                {
                    StaffId = x.Id,
                    FullName = x.SurName + " " + x.FirstName + " " + x.LastName,
                    UserId = x.UserId
                });
                await output.OrderBy(x => x.FullName).ToListAsync();
                ViewBag.User = new SelectList(output, "UserId", "FullName");
            }
            ViewBag.Classlist = new SelectList(db.LiveClassLevel, "Id", "Name");

            return View(data);
        }


    }
}