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
using Exwhyzee.ESS.Models.Entities;

namespace Exwhyzee.ESS.Areas.Student.Controllers
{
    [Authorize(Roles = "Student")]
    public class OnlineClassController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Student/OnlineClass
        #region online class

        public async Task<ActionResult> LiveList()
        {
            var user = User.Identity.GetUserId();

            //Get Student
            var student = db.StudentModel.Include(x => x.User).Include(x => x.ClassLevel).Where(x => x.UserId == user).FirstOrDefault();

            //Enrolled Live Class Subject
            var enr = db.LiveClassSubjectEnrollment.
                Include(x => x.LiveClassLevel).
                Include(x => x.LiveClassSubject).
                Include(x => x.StudentModel).
                Include(x => x.User).Where(x => x.StudentModelId == student.Id && x.LiveClassLevelId == student.ClassLevelId).FirstOrDefault();
            
            var model = await db.OnlineZooms.Include(x => x.LiveClassLevel).Include(x=>x.LiveClassSubject).Include(x => x.User).ToListAsync();
            string uid = User.Identity.GetUserId();
            var iprofile = await db.StudentModel.FirstOrDefaultAsync(x => x.UserId == uid);
            //Get Online Zooms with Enrolled Live Class
            model = model.Where(x => x.LiveClassLevelId == enr.LiveClassLevelId).ToList();
            var class1 = db.LiveClassLevel.FirstOrDefault(x => x.Id == enr.LiveClassLevelId);
            ViewBag.data = iprofile;
            ViewBag.class1 = class1;
            var sub = db.Subscriptions.Include(x => x.User).
                Where(x => x.UserId == uid && x.ClassLevelId == enr.LiveClassLevelId).FirstOrDefault();
            ViewBag.sub = sub;
            return View(model);
        }

        [AllowAnonymous]
        public async Task<ActionResult> DetailsLive(long id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<User> userdata = new List<User>();
            var content = TokenManager.GetAMeeting(id);

            RootobjectDetails data = JsonConvert.DeserializeObject<RootobjectDetails>(content);
            var prof = await db.OnlineZooms.Include(x => x.LiveClassLevel).Include(x=>x.LiveClassSubject).Include(x => x.User).FirstOrDefaultAsync(x => x.MeetingId == data.id);
            
            //Student Meeting Joining Records
            var user = User.Identity.GetUserId();
            var std = db.StudentModel.Include(x => x.User).
                Include(x => x.ClassLevel).FirstOrDefault(x => x.UserId == user);
            ZoomStudentJoinedRecord join = new ZoomStudentJoinedRecord();
            join.Date = DateTime.UtcNow.AddHours(1);
            join.StudentModelId = std.Id;
            join.UserId = std.UserId;
            db.ZoomStudentJoinedRecord.Add(join);
            await db.SaveChangesAsync();

            ViewBag.prof = prof;
            return View(data);
        }

        public async Task<ActionResult> LiveClassRecord(string id)
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
                var prof = await db.OnlineZooms.Include(x => x.LiveClassLevel).Include(x=>x.LiveClassSubject).Include(x => x.User).FirstOrDefaultAsync(x => x.MeetingId == data.id);
                ViewBag.prof = prof;
                return View(data.recording_files);
            }
            catch (Exception c)
            {

            }
            return View();
        }

        #endregion
    }
}