using Exwhyzee.ESS.Models;
using Exwhyzee.ESS.Models.Entities.Dto.Zoom;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Entity;
using Exwhyzee.ESS.Models.Entities;

namespace Exwhyzee.ESS.Areas.Service
{
    public class GeneralService
    {

        public static bool ZoomEnable()
        {
            bool data = false;
            using (var db = new ApplicationDbContext())
            {
                data = db.ZoomSetting.FirstOrDefault().EnableZoom;

            }
            return data;

        }

        public static bool MultipleZoomEnable()
        {
            bool data = false;
            using (var db = new ApplicationDbContext())
            {
                data = db.ZoomSetting.FirstOrDefault().EnableMultipleZoom;

            }
            return data;

        }

        public static ZoomStudentJoinedRecord SubscriptionCheck1()
        {
            ZoomStudentJoinedRecord join;
            using (var db = new ApplicationDbContext())
            {
                var userid = HttpContext.Current.User.Identity.GetUserId();
                join = db.ZoomStudentJoinedRecord.Include(x => x.User).FirstOrDefault(x => x.UserId == userid);
            }
            return join;
        }

        public static Subscription SubscriptionCheck()
        {
            Subscription sub;
            using (var db = new ApplicationDbContext())
            {
                var userid = HttpContext.Current.User.Identity.GetUserId();
                sub = db.Subscriptions.Include(x => x.User).FirstOrDefault(x => x.UserId == userid && x.Status == SubscriptionStatus.Paid);
            }
            return sub;

        }

        public static Subscription SubscriptionCheck2()
        {
            Subscription sub;
            using (var db = new ApplicationDbContext())
            {
                var userid = HttpContext.Current.User.Identity.GetUserId();
                sub = db.Subscriptions.Include(x => x.User).Where(x => x.UserId == userid && (x.EndDate > DateTime.UtcNow.AddHours(1)) && x.Status == SubscriptionStatus.Paid).FirstOrDefault();
            }
            return sub;

        }

        public static Subscription SubscriptionCheck3()
        {
            Subscription sub;
            using (var db = new ApplicationDbContext())
            {
                var userid = HttpContext.Current.User.Identity.GetUserId();
                sub = db.Subscriptions.Include(x => x.User).Where(x => x.UserId == userid && (x.EndDate > DateTime.UtcNow.AddHours(1)) && x.Status == SubscriptionStatus.Paid).FirstOrDefault();
            }
            return sub;

        }

        public static Subscription SubscriptionCheck4()
        {
            Subscription sub;
            using (var db = new ApplicationDbContext())
            {
                var userid = HttpContext.Current.User.Identity.GetUserId();
                sub = db.Subscriptions.Include(x => x.User).FirstOrDefault(x => x.UserId == userid && (x.EndDate < DateTime.UtcNow.AddHours(1)) && x.Status == SubscriptionStatus.Paid);
            }
            return sub;

        }

    }
}