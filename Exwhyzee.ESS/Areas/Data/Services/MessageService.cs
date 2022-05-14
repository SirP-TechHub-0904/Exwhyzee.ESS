using Exwhyzee.ESS.Areas.Data.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Data.Entity;
using Exwhyzee.ESS.Models;
using Exwhyzee.ESS.Models.Entities;
using Exwhyzee.ESS.Areas.Service;
using System.Net;
using System.IO;

namespace Exwhyzee.ESS.Areas.Data.Services
{
    public class MessageService : IMessageService
    {

        private ApplicationDbContext db = new ApplicationDbContext();
        private ServiceSmsComponent smsService = new ServiceSmsComponent();


        #region Old components
        public async Task<string> AccountBalance()
        {
            string balance = "";
            var setting = db.SMSSettings.FirstOrDefault();
            //var dataString = "username=" + "onwuka1" + "&password=" + "nation" + "&balance=true";
            var dataString = "username=" + setting.SmsUsername + "&password=" + setting.SmsPassword + "&balance=true";
            var url = "http://www.xyzsms.com/components/com_spc/smsapi.php?" + dataString;

            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.Method = "GET";
            httpWebRequest.ContentType = "application/json";

            //getting the response from the request
            HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            Stream responseStream = httpWebResponse.GetResponseStream();
            StreamReader streamReader = new StreamReader(responseStream);
            string response = streamReader.ReadToEnd();
            balance = response.Trim();
            if (balance == "2905")
            {
                balance = "Wrong Credentials";
            }
            return balance;
        }

        public async Task<string> AllParentInClassContact(int[] classLevelId)
        {
            //var session = await db.Sessions.FirstOrDefaultAsync(x => x.Status == SessionStatus.Current);
            string numbers = "";
            //if (session != null)
            //{
                foreach (var item in classLevelId)
                {
                    var enrolledStudents = db.StudentModel.Include(x => x.ClassLevel).Include(x => x.User).Where(c=>c.ClassLevelId == item && c.ParentPhoneNumber != null).Select(x => x.ParentPhoneNumber);

                    string[] studentNumbers = enrolledStudents.ToArray();
                    numbers = string.Join(",", studentNumbers.ToArray());

                }

            //}
            return numbers;
        }

        public async Task<string> AllParentInContact()
        {
            //var session = await db.Sessions.FirstOrDefaultAsync(x => x.Status == SessionStatus.Current);
            string numbers = "";
            //if (session != null)
            //{
                // var parent = db.StudentProfiles.Include(c => c.user).Where(c => c..SessionYear == session.SessionYear && c.ParentGuardianPhoneNumber != null).Select(x => x.ParentGuardianPhoneNumber);
                var enrolledStudentsParent = db.StudentModel.Include(x => x.ClassLevel).Include(x => x.User).Where(c => c.ParentPhoneNumber != null).Select(x => x.ParentPhoneNumber);

                string[] studentNumbers = enrolledStudentsParent.ToArray();
                numbers = string.Join(",", studentNumbers.ToArray());
                return numbers;
            //}


            //return "";
        }

        public async Task<string> AllStaffContact()
        {
            string numbers = "";
            var staff = db.UserProfileModels.Include(c => c.User).Where(c => c.User.PhoneNumber != null).Select(x => x.User.PhoneNumber);
            string[] studentNumbers = staff.ToArray();
            numbers = string.Join(",", studentNumbers.ToArray());
            return numbers;

        }

        public async Task<string> AllStudentInClassContact(int[] classLevelId)
        {
            //var session = await db.Sessions.FirstOrDefaultAsync(x => x.Status == SessionStatus.Current);
            string numbers = "";
            //if (session != null)
            //{
                foreach (var item in classLevelId)
                {
                    var enrolledStudents = db.StudentModel.Include(x => x.ClassLevel).Include(x => x.User).Where(c => c.ClassLevelId == item && c.PhoneNumber != null).Select(x => x.PhoneNumber);

                    string[] studentNumbers = enrolledStudents.ToArray();
                    numbers = string.Join(",", studentNumbers.ToArray());

                }
                numbers += numbers;
            //}
            return numbers;
        }

        public async Task<string> AllStudentsContact()
        {
            //var session = await db.Sessions.FirstOrDefaultAsync(x => x.Status == SessionStatus.Current);
            string numbers = "";
            //if (session != null)
            //{
                // var students = System.Web.Security.Roles.GetUsersInRole("student");
                var enrolledStudents = db.StudentModel.Include(x => x.ClassLevel).Include(x => x.User).Where(c => c.PhoneNumber != null).Select(x => x.PhoneNumber);

                string[] studentNumbers = enrolledStudents.ToArray();
                numbers = string.Join(",", studentNumbers.ToArray());
                return numbers;
            //}
            //return "";
        }

        public async Task<SmsReport> GetMessage(int id)
        {
            var report = await db.SmsReports.FirstOrDefaultAsync(x => x.Id == id);
            return report;
        }


        public async Task<List<SmsReport>> MessageHistory()
        {
            var msg = db.SmsReports.OrderByDescending(x => x.DateSent);
            return await msg.ToListAsync();
        }

       

        public async Task<Property> SmsProperty()
        {
            var item = await db.Properties.FirstOrDefaultAsync();
            return item;
        }

        #endregion

        #region new services

        public async Task<string> SendSms(string SenderId, string Recipients, string Message, string SendOption, string ScheduleDate)
        {
            var response = "";
            var setting = db.SMSSettings.FirstOrDefault();
            //var settings = await db.Settings.FirstOrDefaultAsync();
            try
            {
                 response = smsService.SendSMS(setting.SmsUsername, setting.SmsPassword, SenderId, Recipients, Message, SendOption, ScheduleDate);
            }catch(Exception c)
            {

            }
            return response;

        }
        public async Task<string> GetPhoneNumbers(string category, string ClassSend)
        {
            string numbers = "";
            try
            {


                //var session = await db.Sessions.FirstOrDefaultAsync(x => x.Status == SessionStatus.Current);
               
                if (category == "SendToAllStudent")
                {
                    var studentsnumber = await db.StudentModel.Include(x => x.User).Select(x => x.PhoneNumber).ToListAsync();

                    string[] studentNumbers = studentsnumber.ToArray();
                    numbers = string.Join(",", studentNumbers.ToArray());
                }
                else if (ClassSend == "Sendtostudent")
                {
                    int classid = Convert.ToInt32(category);
                    var studentsnumber = await db.StudentModel.Include(x => x.User).Include(x => x.ClassLevel).Where(x => x.ClassLevelId == classid).Select(x => x.PhoneNumber).ToListAsync();

                    string[] studentNumbers = studentsnumber.ToArray();
                    numbers = string.Join(",", studentNumbers.ToArray());
                }
                else if (category == "SendToStaff")
                {
                    var staff = await db.UserProfileModels.Include(x => x.User).Select(x => x.User.PhoneNumber).ToListAsync();
                    string[] staffNumbers = staff.ToArray();
                    numbers = string.Join(",", staffNumbers.ToArray());
                }
                else if (category == "SendToAllParent")
                {
                    var studentsParentnumber = await db.StudentModel.Include(x => x.User).Select(x => x.ParentPhoneNumber).ToListAsync();

                    string[] parentsNumbers = studentsParentnumber.ToArray();
                    numbers = string.Join(",", parentsNumbers.ToArray());
                }
                else if (ClassSend == "SendtoParent")
                {
                    int classid = Convert.ToInt32(category);
                    var studentsnumber = await db.StudentModel.Include(x => x.User).Include(x => x.ClassLevel).Where(x => x.ClassLevelId == classid).Select(x => x.ParentPhoneNumber).ToListAsync();

                    string[] studentNumbers = studentsnumber.ToArray();
                    numbers = string.Join(",", studentNumbers.ToArray());
                }
                else if (category == "")
                {
                    numbers = "No contact found";
                }
            }
            catch (Exception f)
            {
                numbers = "No contact found";
            }
            return numbers;
        }


        #endregion
    }
}