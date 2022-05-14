using Exwhyzee.ESS.Models;
using Exwhyzee.ESS.Models.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;

namespace Exwhyzee.ESS.Helper
{
    public static class IskoolHelper
    {
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> list, int size)
        {
            var r = new Random();
            var shuffledList =
                list.
                    Select(x => new { Number = r.Next(), Item = x }).
                    OrderBy(x => x.Number).
                    Select(x => x.Item).
                    Take(size); // Assume first @size items is fine

            return shuffledList.ToList();
        }

        public static int SectionAndTerm(string session, string term, string url)
        {
            try
            {
                using (var db = new ApplicationDbContext())
                {
                    try
                    {
                        var item = db.SchoolPortalDatas.FirstOrDefault(x => x.PortalUrl == url);
                        string endurl = "/api/SchoolApi/GetSchoolSessions";
                        string starturl = "http://";
                        string apiUrl = String.Format(item.PortalUrl + endurl);

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
                            schools = JsonConvert.DeserializeObject<List<ApiSessionList>>(result);

                            sr.Close();
                        }
                        var sch = schools.FirstOrDefault(x => x.Session == session && x.Term.ToLower() == term.ToLower());

                        return sch.Id;
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
                }
            }
            catch (Exception c)
            {

            }
            return 0;

        }



        public static ApiResultBySessionList SectionResult(string session, string term, int id)
        {
            try
            {
                using (var db = new ApplicationDbContext())
                {
                    var user = db.SchoolPortalDatas.FirstOrDefault(x => x.Id == id);
                    var sessionId = SectionAndTerm(session, term, user.PortalUrl);

                    try
                    {
                        var item = db.SchoolPortalDatas.FirstOrDefault(x => x.PortalUrl == user.PortalUrl);
                        string endurl = "/api/SchoolApi/GetSessionDetails?sessionId="+sessionId;
                        string starturl = "http://";
                        string apiUrl = String.Format(item.PortalUrl + endurl);

                        WebRequest requestObj = WebRequest.Create(apiUrl);
                        requestObj.Method = "GET";

                        HttpWebResponse responseGet = null;
                        responseGet = (HttpWebResponse)requestObj.GetResponse();
                        string result = null;
                        ApiResultBySessionList schools = new ApiResultBySessionList();
                        using (Stream stream = responseGet.GetResponseStream())
                        {
                            StreamReader sr = new StreamReader(stream);
                            result = sr.ReadToEnd();
                            schools = JsonConvert.DeserializeObject<ApiResultBySessionList>(result);

                            sr.Close();
                        }
                        
                        return schools;
                    }

                    catch (WebException webex)
                    {
                        WebResponse errResp = webex.Response;
                        using (Stream respStream = errResp.GetResponseStream())
                        {
                            StreamReader reader = new StreamReader(respStream);
                            string text = reader.ReadToEnd();
                        }
                        return null;
                    }

                }
            }
            catch (Exception c)
            {

            }
            return null;

        }

        public static string NameyId(string id)
        {
            string subc = "";
            try
            {
                using (var db = new ApplicationDbContext())
                {
                    var user = db.Users.FirstOrDefault(x => x.Id == id);
                    var profil = db.UserProfileModels.FirstOrDefault(x => x.UserId == user.Id);
                    if (profil == null)
                    {
                        return subc = "Admin";
                    }
                    else
                    {
                        subc = profil.SurName + " " + profil.FirstName + " " + profil.LastName;
                    }
                }
            }catch(Exception c)
            {

            }
            return subc;

        }

        public static bool IsUserInRole(string userId, string role)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

            if (manager.IsInRole(userId, role))
            {
                return true;
            }

            return false;
        }
    }
}