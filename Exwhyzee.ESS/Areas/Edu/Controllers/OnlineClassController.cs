using Exwhyzee.ESS.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Exwhyzee.ESS.Models.Entities.Dto;
using Exwhyzee.ESS.Models.Entities;
using Exwhyzee.ESS.Models.Entities.Dto.Zoom;
using Exwhyzee.ESS.Controllers;
using Newtonsoft.Json;
using System.Net.Mail;
using System.Net;
using System.IO;
using Exwhyzee.ESS.Areas.Data.IServices;
using Exwhyzee.ESS.Areas.Data.Services;

namespace Exwhyzee.ESS.Areas.Edu.Controllers
{
    [Authorize(Roles = "Teacher,Admin,SuperAdmin,Volunteer")]
    public class OnlineClassController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private IMessageService _message = new MessageService();

        public OnlineClassController()
        {

        }

        public OnlineClassController(
            MessageService message
          )
        {

            _message = message;
        }

        #region live video


        public async Task<ActionResult> SendSMSLiveClass(long id, string Target, string ClassSend)
        {
            //var sett = await db.Settings.FirstOrDefaultAsync();
            var liveclass = await db.OnlineZooms.Include(x => x.LiveClassLevel).Include(x => x.LiveClassSubject).Include(x => x.User).FirstOrDefaultAsync(x => x.MeetingId == id);
            var numbers = "08165680904,";
            try
            {
                numbers = await _message.GetPhoneNumbers(Target, ClassSend);


            }
            catch (Exception c)
            {

            }
            string messages = "Log onto your dashboard to join the live class on " + liveclass.LiveClassLevel.Name + "(" + liveclass.LiveClassSubject.Subject + ") on " + liveclass.ClassDate + " by " + liveclass.ClassTime + " Follow the link http://" + "http://www.iskools.com" + " to start";
            try
            {
                string response = await _message.SendSms("ISKOOLS", numbers, messages, "SendNow", "");

            }
            catch (Exception c) { }
            return RedirectToAction("LiveClassList", "OnlineClass", new { area = "Edu" });
        }



        public async Task<ActionResult> LiveClassList()
        {
            var setting = await db.ZoomSetting.FirstOrDefaultAsync();

            var model = await db.OnlineZooms.Include(x => x.LiveClassLevel).Include(x=>x.LiveClassSubject).Include(x => x.User).ToListAsync();
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


        public ActionResult LiveClassType()
        {
            return View();
        }
        public async Task<ActionResult> SingleLiveClass()
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

        public async Task<ActionResult> MultipleLiveClass()
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

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> NewLiveClass(OnlineZoomDto zmodel, string multy)
        {
            long iddetail = 0;
            string result1 = "";
            string result2 = "";
            string result3 = "";
            string eresult1 = "";
            string eresult2 = "";
            string eresult3 = "";
            if (ModelState.IsValid)
            {

                if (multy == "multy")
                {
                    //for class 1

                    try
                    {
                        OnlineZoom model = new OnlineZoom();
                        var sett = db.ZoomSetting.FirstOrDefault();
                        model.DateCreated = DateTime.UtcNow.AddHours(1);
                        model.HostEmail = sett.ZoomHostOne;
                        model.UserId = zmodel.UserId1;
                        model.ClassDate = zmodel.ClassDate;
                        model.ClassTime = zmodel.ClassTime;
                        model.Duration = zmodel.Duration1;
                        model.LiveClassLevelId = zmodel.ClassLevelId1;
                        model.LiveClassSubjectId = zmodel.SubjectId1;
                        model.Description = zmodel.Description1;
                        model.ClassPassword = zmodel.ClassPassword1;

                        db.OnlineZooms.Add(model);
                        await db.SaveChangesAsync();

                        //end create on local

                        //create zoom online
                        var subupdate = await db.LiveClassSubject.FirstOrDefaultAsync(x => x.Id == model.LiveClassSubjectId);
                        RootobjectNewMeeting datamodel = new RootobjectNewMeeting();
                        datamodel.topic = "Iskools" + " Live Class on " + subupdate.Subject;
                        datamodel.type = model.MeetingType;
                        DateTime oDate = Convert.ToDateTime(model.ClassDate);
                        DateTime oTime = Convert.ToDateTime(model.ClassTime);
                        // DateTime DateValueConvert = DateTime.ParseExact(oDate.ToString(), "dd-MM-yyyy", CultureInfo.InvariantCulture);

                        datamodel.start_time = oDate.ToString("yyyy-MM-dd") + "T" + oTime.ToString("HH:mm:ss") + "Z";
                        datamodel.duration = model.Duration;
                        datamodel.password = model.ClassPassword;
                        datamodel.agenda = model.Description;
                        var content = TokenManager.NewMeeting(datamodel, sett.ZoomHostOne);
                        RootobjectDetails data = JsonConvert.DeserializeObject<RootobjectDetails>(content);
                        //end
                        iddetail = data.id;
                        //UPDAte local
                        var classmodel = await db.OnlineZooms.Include(x => x.LiveClassLevel).Include(x=>x.LiveClassSubject).FirstOrDefaultAsync(x => x.Id == model.Id);
                        classmodel.MeetingId = data.id;
                        classmodel.MeetingUUId = data.uuid;
                        db.Entry(classmodel).State = EntityState.Modified;
                        await db.SaveChangesAsync();
                        result1 = "Successfully created live class on " + classmodel.LiveClassSubject.Subject + " for " + classmodel.LiveClassLevel.Name;
                        string mass = "";
                        try
                        {

                            MailMessage mail = new MailMessage();

                            //set the addresses 
                            mail.From = new MailAddress("learnonline@iskools.com"); //IMPORTANT: This must be same as your smtp authentication address.
                            mail.To.Add("espErrorMail@exwhyzee.ng");
                            mail.To.Add("iskoolsportal@gmail.com");
                            mail.To.Add("onwukaemeka41@gmail.com");
                            mail.To.Add("bernardamaeme@gmail.com");

                            //set the content 

                            mail.Subject = " Live Class Request from " + "Iskools";

                            mass = "Iskools" + " - " + "http://wwww.iskools.com" + "/Edu/OnlineClass/DetailsLiveClass/" + data.id + " - visit for more info";

                            mail.Body = mass;
                            //send the message 
                            SmtpClient smtp = new SmtpClient("mail.iskools.com");

                            //IMPORANT:  Your smtp login email MUST be same as your FROM address. 
                            NetworkCredential Credentials = new NetworkCredential("learnonline@iskools.com", "Exwhyzee@123");
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
                    }
                    catch (Exception c)
                    {
                        eresult1 = "first class creation fail. try using single method to recreate a new one";
                    }

                    //for class 2

                    try
                    {
                        OnlineZoom model = new OnlineZoom();
                        var sett = db.ZoomSetting.FirstOrDefault();
                        model.DateCreated = DateTime.UtcNow.AddHours(1);
                        model.HostEmail = sett.ZoomHostTwo;
                        model.UserId = zmodel.UserId2;
                        model.ClassDate = zmodel.ClassDate;
                        model.ClassTime = zmodel.ClassTime;
                        model.Duration = zmodel.Duration2;
                        model.LiveClassLevelId = zmodel.ClassLevelId2;
                        model.LiveClassSubjectId = zmodel.SubjectId2;
                        model.Description = zmodel.Description2;
                        model.ClassPassword = zmodel.ClassPassword2;

                        db.OnlineZooms.Add(model);
                        await db.SaveChangesAsync();

                        //end create on local

                        //create zoom online
                        var subupdate = await db.LiveClassSubject.FirstOrDefaultAsync(x => x.Id == model.LiveClassSubjectId);
                        RootobjectNewMeeting datamodel = new RootobjectNewMeeting();
                        datamodel.topic = "Iskools" + " Live Class on " + subupdate.Subject;
                        datamodel.type = model.MeetingType;
                        DateTime oDate = Convert.ToDateTime(model.ClassDate);
                        DateTime oTime = Convert.ToDateTime(model.ClassTime);
                        // DateTime DateValueConvert = DateTime.ParseExact(oDate.ToString(), "dd-MM-yyyy", CultureInfo.InvariantCulture);

                        datamodel.start_time = oDate.ToString("yyyy-MM-dd") + "T" + oTime.ToString("HH:mm:ss") + "Z";
                        datamodel.duration = model.Duration;
                        datamodel.password = model.ClassPassword;
                        datamodel.agenda = model.Description;
                        var content = TokenManager.NewMeeting(datamodel, sett.ZoomHostTwo);
                        RootobjectDetails data = JsonConvert.DeserializeObject<RootobjectDetails>(content);
                        //end
                        iddetail = data.id;
                        //UPDAte local
                        var classmodel = await db.OnlineZooms.Include(x => x.LiveClassLevel).Include(x=>x.LiveClassSubject).FirstOrDefaultAsync(x => x.Id == model.Id);
                        classmodel.MeetingId = data.id;
                        classmodel.MeetingUUId = data.uuid;
                        db.Entry(classmodel).State = EntityState.Modified;
                        await db.SaveChangesAsync();
                        result2 = "Successfully created live class on " + classmodel.LiveClassSubject.Subject + " for " + classmodel.LiveClassLevel.Name;
                        string mass = "";
                        try
                        {

                            MailMessage mail = new MailMessage();

                            //set the addresses 
                            mail.From = new MailAddress("learnonline@iskools.com"); //IMPORTANT: This must be same as your smtp authentication address.
                            mail.To.Add("espErrorMail@exwhyzee.ng");
                            mail.To.Add("iskoolsportal@gmail.com");
                            mail.To.Add("onwukaemeka41@gmail.com");
                            mail.To.Add("bernardamaeme@gmail.com");

                            //set the content 

                            mail.Subject = " Live Class Request from " + "Iskools";

                            mass = "Iskools" + " - " + "http://www.iskools.com" + "/Edu/DetailsLiveClass/" + data.id + " - visit for more info";

                            mail.Body = mass;
                            //send the message 
                            SmtpClient smtp = new SmtpClient("mail.iskools.com");

                            //IMPORANT:  Your smtp login email MUST be same as your FROM address. 
                            NetworkCredential Credentials = new NetworkCredential("learnonline@iskools.com", "Exwhyzee@123");
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
                    }
                    catch (Exception c)
                    {
                        eresult2 = "second class creation fail. try using single method to recreate a new one";

                    }

                    //for class 3

                    try
                    {
                        OnlineZoom model = new OnlineZoom();
                        var sett = db.ZoomSetting.FirstOrDefault();
                        model.DateCreated = DateTime.UtcNow.AddHours(1);
                        model.HostEmail = sett.ZoomHostThree;
                        model.UserId = zmodel.UserId3;
                        model.ClassDate = zmodel.ClassDate;
                        model.ClassTime = zmodel.ClassTime;
                        model.Duration = zmodel.Duration3;
                        model.LiveClassLevelId = zmodel.ClassLevelId3;
                        model.LiveClassSubjectId = zmodel.SubjectId3;
                        model.Description = zmodel.Description3;
                        model.ClassPassword = zmodel.ClassPassword3;

                        db.OnlineZooms.Add(model);
                        await db.SaveChangesAsync();

                        //end create on local

                        //create zoom online
                        var subupdate = await db.LiveClassSubject.FirstOrDefaultAsync(x => x.Id == model.LiveClassSubjectId);
                        RootobjectNewMeeting datamodel = new RootobjectNewMeeting();
                        datamodel.topic = "Iskools" + " Live Class on " + subupdate.Subject;
                        datamodel.type = model.MeetingType;
                        DateTime oDate = Convert.ToDateTime(model.ClassDate);
                        DateTime oTime = Convert.ToDateTime(model.ClassTime);
                        // DateTime DateValueConvert = DateTime.ParseExact(oDate.ToString(), "dd-MM-yyyy", CultureInfo.InvariantCulture);

                        datamodel.start_time = oDate.ToString("yyyy-MM-dd") + "T" + oTime.ToString("HH:mm:ss") + "Z";
                        datamodel.duration = model.Duration;
                        datamodel.password = model.ClassPassword;
                        datamodel.agenda = model.Description;
                        var content = TokenManager.NewMeeting(datamodel, sett.ZoomHostThree);
                        RootobjectDetails data = JsonConvert.DeserializeObject<RootobjectDetails>(content);
                        //end
                        iddetail = data.id;
                        //UPDAte local
                        var classmodel = await db.OnlineZooms.Include(x => x.LiveClassLevel).Include(x=>x.LiveClassSubject).FirstOrDefaultAsync(x => x.Id == model.Id);
                        classmodel.MeetingId = data.id;
                        classmodel.MeetingUUId = data.uuid;
                        db.Entry(classmodel).State = EntityState.Modified;
                        await db.SaveChangesAsync();
                        result3 = "Successfully created live class on " + classmodel.LiveClassSubject.Subject + " for " + classmodel.LiveClassLevel.Name;
                        string mass = "";
                        try
                        {

                            MailMessage mail = new MailMessage();

                            //set the addresses 
                            mail.From = new MailAddress("learnonline@iskools.com"); //IMPORTANT: This must be same as your smtp authentication address.
                            mail.To.Add("espErrorMail@exwhyzee.ng");
                            mail.To.Add("iskoolsportal@gmail.com");
                            mail.To.Add("onwukaemeka41@gmail.com");
                            mail.To.Add("bernardamaeme@gmail.com");

                            //set the content 

                            mail.Subject = " Live Class Request from " + "Iskools";

                            mass = "Iskools" + " - " + "http://www.iskools.com" + "/Edu/OnlineClass/DetailsLiveClass/" + data.id + " - visit for more info";

                            mail.Body = mass;
                            //send the message 
                            SmtpClient smtp = new SmtpClient("mail.iskools.com");

                            //IMPORANT:  Your smtp login email MUST be same as your FROM address. 
                            NetworkCredential Credentials = new NetworkCredential("learnonline@iskools.com", "Exwhyzee@123");
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
                    }
                    catch (Exception c)
                    {
                        eresult3 = "third class creation fail. try using single method to recreate a new one";

                    }
                }
                else
                {


                    //create zoom on local
                    //
                    try
                    {
                        OnlineZoom model = new OnlineZoom();
                        var sett = db.ZoomSetting.FirstOrDefault();
                        model.DateCreated = DateTime.UtcNow.AddHours(1);
                        model.HostEmail = sett.ZoomHostOne;
                        model.UserId = zmodel.UserId1;
                        model.ClassDate = zmodel.ClassDate;
                        model.ClassTime = zmodel.ClassTime;
                        model.Duration = zmodel.Duration1;
                        model.LiveClassLevelId = zmodel.ClassLevelId1;
                        model.LiveClassSubjectId = zmodel.SubjectId1;
                        model.Description = zmodel.Description1;
                        model.ClassPassword = zmodel.ClassPassword1;

                        db.OnlineZooms.Add(model);
                        await db.SaveChangesAsync();

                        //end create on local

                        //create zoom online
                        var subupdate = await db.LiveClassSubject.FirstOrDefaultAsync(x => x.Id == model.LiveClassSubjectId);
                        RootobjectNewMeeting datamodel = new RootobjectNewMeeting();
                        datamodel.topic = "Iskools" + " Live Class on " + subupdate.Subject;
                        datamodel.type = model.MeetingType;
                        DateTime oDate = Convert.ToDateTime(model.ClassDate);
                        DateTime oTime = Convert.ToDateTime(model.ClassTime);
                        // DateTime DateValueConvert = DateTime.ParseExact(oDate.ToString(), "dd-MM-yyyy", CultureInfo.InvariantCulture);

                        datamodel.start_time = oDate.ToString("yyyy-MM-dd") + "T" + oTime.ToString("HH:mm:ss") + "Z";
                        datamodel.duration = model.Duration;
                        datamodel.password = model.ClassPassword;
                        datamodel.agenda = model.Description;
                        var content = TokenManager.NewMeeting(datamodel, sett.ZoomHostOne);
                        RootobjectDetails data = JsonConvert.DeserializeObject<RootobjectDetails>(content);
                        //end
                        iddetail = data.id;
                        //UPDAte local
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
                            mail.From = new MailAddress("learnonline@iskools.com"); //IMPORTANT: This must be same as your smtp authentication address.
                            mail.To.Add("espErrorMail@exwhyzee.ng");
                            mail.To.Add("iskoolsportal@gmail.com");
                            mail.To.Add("onwukaemeka41@gmail.com");
                            mail.To.Add("bernardamaeme@gmail.com");

                            //set the content 

                            mail.Subject = " Live Class Request from " + "Iskools";

                            mass = "Iskools" + " - " + "http://www.iskools.com" + "/Edu/OnlineClass/DetailsLiveClass/" + data.id + " - visit for more info";

                            mail.Body = mass;
                            //send the message 
                            SmtpClient smtp = new SmtpClient("mail.iskools.com");

                            //IMPORANT:  Your smtp login email MUST be same as your FROM address. 
                            NetworkCredential Credentials = new NetworkCredential("learnonline@iskools.com", "Exwhyzee@123");
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
                    }
                    catch (Exception c)
                    {

                    }

                    //




                }

                if (multy == "multy")
                {
                    TempData["result1"] = result1;
                    TempData["result2"] = result2;
                    TempData["result3"] = result3;

                    return RedirectToAction("LiveClassList");
                }
                else
                {
                    return RedirectToAction("DetailsLiveClass", new { id = iddetail });
                }
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
            if (multy == "multy")
            {

                TempData["eresult1"] = eresult1;
                TempData["eresult2"] = eresult2;
                TempData["eresult3"] = eresult3;
                return RedirectToAction("LiveClassList");
            }
            else
            {
                return View(zmodel);
            }

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
                if (data.Subject != null)
                {
                    model.Subject = data.Subject;
                }
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
                var prof = await db.OnlineZooms.Include(x => x.LiveClassLevel).Include(x=>x.LiveClassSubject).Include(x => x.User).FirstOrDefaultAsync(x => x.MeetingId == id);
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
                var prof = await db.OnlineZooms.Include(x => x.LiveClassLevel).Include(x=>x.LiveClassSubject).Include(x => x.LiveClassSubject).Include(x => x.User).FirstOrDefaultAsync(x => x.MeetingId == data.id);
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
                var prof = await db.OnlineZooms.Include(x => x.LiveClassLevel).Include(x => x.LiveClassSubject).Include(x => x.User).FirstOrDefaultAsync(x => x.MeetingId == id);
                ViewBag.prof = prof;
                return View(data.participants.ToList());
            }
            catch (Exception c)
            {

            }
            return View();
        }

        public JsonResult SubjectList(int Id)
        {
            var c = db.LiveClassLevel.FirstOrDefault(x => x.Id == Id).Id;
            var subject = from s in db.LiveClassSubject
                          where s.LiveClassLevelId == c
                          select s;

            return Json(new SelectList(subject.OrderBy(x => x.Subject).ToArray(), "Id", "Subject"), JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}