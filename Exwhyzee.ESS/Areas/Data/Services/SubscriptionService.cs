using Exwhyzee.ESS.Areas.Data.IServices;
using Exwhyzee.ESS.Models;
using Exwhyzee.ESS.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Data;
using System.Data.Entity;
using Microsoft.AspNet.Identity;

namespace Exwhyzee.ESS.Areas.Data.Services
{
    public class SubscriptionService : ISubscriptionService
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<List<Subscription>> AllSubscription()
        {
            var sub = await db.Subscriptions.Include(x => x.User).Include(x => x.ClassLevel).OrderByDescending(x => x.EndDate).ToListAsync();
            return sub;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task Create(Subscription item)
        {
            var set = db.ZoomSetting.FirstOrDefault();
            var uId = HttpContext.Current.User.Identity.GetUserId();
            var student = db.StudentModel.
                Include(x => x.User).
                Include(x => x.ClassLevel).FirstOrDefault(x => x.UserId == uId);

            //Get Student Enrolled Subject
            var enrolledSubject = db.LiveClassSubjectEnrollment.
                Include(x => x.LiveClassLevel).Include(x => x.StudentModel)
                .Include(x => x.LiveClassSubject).
                Where(x => x.UserId == student.UserId && x.LiveClassLevelId == student.ClassLevelId && x.Status == true);
            int enrolledSubjectCount = enrolledSubject.Count();
            decimal? amountInKobo = set.SubscriptionAmount * enrolledSubjectCount;

            item.UserId = student.UserId;
            item.StartDate = DateTime.UtcNow.AddHours(1);
            item.EndDate = DateTime.UtcNow.AddMonths(+1);
            item.Status = SubscriptionStatus.Pending;
            item.StudentModelId = student.Id;
            item.ClassLevelId = student.ClassLevelId;
            //item.Source = SubscriptionSource.Online;
            item.Email = student.Email;
            item.Amount = amountInKobo;
            item.Name = "Subscription";
            db.Subscriptions.Add(item);
            await db.SaveChangesAsync();

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        //public async Task DepositCreate(Subscription item)
        //{
        //    var set = db.ZoomSetting.FirstOrDefault();
        //    var uId = HttpContext.Current.User.Identity.GetUserId();
        //    var student = db.StudentModel.
        //        Include(x => x.User).
        //        Include(x => x.ClassLevel).FirstOrDefault(x => x.UserId == uId);

        //    //Get Student Enrolled Subject
        //    var enrolledSubject = db.LiveClassSubjectEnrollment.
        //        Include(x => x.LiveClassLevel).Include(x => x.StudentModel)
        //        .Include(x => x.LiveClassSubject).
        //        Where(x => x.UserId == student.UserId && x.LiveClassLevelId == student.ClassLevelId && x.Status == true);
        //    int enrolledSubjectCount = enrolledSubject.Count();
        //    decimal? amountInKobo = set.SubscriptionAmount * enrolledSubjectCount;

        //    item.UserId = student.UserId;
        //    item.StartDate = DateTime.UtcNow.AddHours(1);
        //    item.EndDate = DateTime.UtcNow.AddMonths(+1);
        //    item.Status = SubscriptionStatus.Pending;
        //    item.ClassLevelId = student.ClassLevelId;
        //    item.Source = SubscriptionSource.Online;
        //    item.Amount = amountInKobo;
        //    item.Name = "Subscription";
        //    item.Email = student.Email;
        //    db.Subscriptions.Add(item);
        //    await db.SaveChangesAsync();

        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task Delete(int? id)
        {
            var sub = await db.Subscriptions.
                Include(x => x.User).
                Include(x => x.ClassLevel)
                .FirstOrDefaultAsync(x => x.Id == id);
            db.Subscriptions.Remove(sub);
            await db.SaveChangesAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Subscription> Get(int? id)
        {
            var sub = await db.Subscriptions.
                Include(x => x.User).
                Include(x => x.ClassLevel).FirstOrDefaultAsync(x => x.Id == id);
            return sub;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task OnlinePay(Subscription item, int? id)
        {
            var pay = await db.Subscriptions.
                Include(x => x.User).Include(x => x.ClassLevel).FirstOrDefaultAsync(x => x.Id == id);
            pay.Status = SubscriptionStatus.Paid;
            pay.Source = SubscriptionSource.Online;
            db.Entry(pay).State = EntityState.Modified;
            await db.SaveChangesAsync();

            if (pay.Status == SubscriptionStatus.Paid)
            {
                var set = db.ZoomSetting.FirstOrDefault();
                var student = db.StudentModel.
                    Include(x => x.User).
                    Include(x => x.ClassLevel).FirstOrDefault(x => x.UserId == pay.UserId);

                //Get Student Enrolled Subject
                var enrolledSubject = db.LiveClassSubjectEnrollment.
                    Include(x => x.LiveClassLevel).Include(x => x.StudentModel)
                    .Include(x => x.LiveClassSubject).
                    Where(x => x.UserId == student.UserId && x.LiveClassLevelId == student.ClassLevelId && x.Status == true).ToList();

                //Subscription Breakdown according to student subjects
                foreach (var subject in enrolledSubject)
                {
                    var teacher = db.TeacherLiveSubjectAssignments.Include(x => x.Subject).Include(x => x.Class).Where(x => x.SubjectId == subject.LiveClassSubjectId && x.ClassId == subject.LiveClassLevelId).FirstOrDefault();
                    SubscriptionCommision commission = new SubscriptionCommision();
                    commission.Amount = set.SubscriptionAmount;
                    commission.ClassLevelId = subject.LiveClassLevelId;
                    commission.StudentModelId = subject.StudentModelId;
                    commission.SubjectId = subject.LiveClassSubjectId;
                    commission.SubscriptionId = pay.Id;
                    commission.TeacherId = teacher.TeacherId;
                    commission.StartDate = pay.StartDate;
                    commission.EndDate = pay.EndDate;
                    commission.SystemCommission = (decimal)(75.0 / 100.00) * set.SubscriptionAmount;
                    commission.TeacherCommission = (decimal)(25.0 / 100.00) * set.SubscriptionAmount;
                    db.SubscriptionCommision.Add(commission);
                    await db.SaveChangesAsync();

                }

                //Teacher Wallet Payment According to student that subscribe to their subject
                foreach (var subject in enrolledSubject)
                {
                    var techersub = db.TeacherLiveSubjectAssignments.Include(x => x.Subject).Include(x => x.Class).Where(x => x.SubjectId == subject.LiveClassSubjectId && x.ClassId == subject.LiveClassLevelId).FirstOrDefault();
                    var teacher = db.TeacherLiveSubjectAssignments.Include(x => x.Subject).Include(x => x.Class).Where(x => x.SubjectId == subject.LiveClassSubjectId && x.ClassId == subject.LiveClassLevelId && x.TeacherId == techersub.TeacherId).FirstOrDefault();
                    var user = db.UserProfileModels.Include(x => x.User).Where(x => x.Id == techersub.TeacherId).FirstOrDefault();
                    var walletCheck = db.TeacherWallet.Include(x => x.User).Include(x => x.Teacher).FirstOrDefault(x => x.TeacherId == teacher.Id);
                   if(walletCheck != null)
                    {
                        walletCheck.Amount = walletCheck.Amount + (decimal)(25.0 / 100.00) * set.SubscriptionAmount;
                        db.Entry(walletCheck).State = EntityState.Modified;
                        await db.SaveChangesAsync();
                    }
                    else
                    {
                        TeacherWallet wallet = new TeacherWallet();
                        wallet.Amount = wallet.Amount + (decimal)(25.0 / 100.00) * set.SubscriptionAmount;
                        wallet.UserId = user.UserId;
                        wallet.TeacherId = teacher.TeacherId;
                        db.TeacherWallet.Add(wallet);
                        await db.SaveChangesAsync();
                    }
                   
                }

            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task BankDeposit(Subscription item, int? id)
        {
            var uId = HttpContext.Current.User.Identity.GetUserId();
            var approve = db.UserProfileModels.Include(x => x.User).FirstOrDefault(x => x.UserId == uId);
            var pay = await db.Subscriptions.
               Include(x => x.User).Include(x => x.ClassLevel).FirstOrDefaultAsync(x => x.Id == id);
            pay.Status = SubscriptionStatus.Paid;
            pay.ApprovedbyId = approve.Id;
            pay.Source = SubscriptionSource.Bank;
            db.Entry(pay).State = EntityState.Modified;
            await db.SaveChangesAsync();

            if (pay.Status == SubscriptionStatus.Paid)
            {
                var set = db.ZoomSetting.FirstOrDefault();
                var student = db.StudentModel.
                    Include(x => x.User).
                    Include(x => x.ClassLevel).FirstOrDefault(x => x.UserId == pay.UserId);

                //Get Student Enrolled Subject
                var enrolledSubject = db.LiveClassSubjectEnrollment.
                    Include(x => x.LiveClassLevel).Include(x => x.StudentModel)
                    .Include(x => x.LiveClassSubject).
                    Where(x => x.UserId == student.UserId && x.LiveClassLevelId == student.ClassLevelId && x.Status == true).ToList();

                //Subscription Breakdown according to student subjects
                foreach (var subject in enrolledSubject)
                {
                    var teacher = db.TeacherLiveSubjectAssignments.Include(x => x.Subject).Include(x => x.Class).FirstOrDefault(x => x.SubjectId == subject.LiveClassSubjectId && x.ClassId == subject.LiveClassLevelId);
                    SubscriptionCommision commission = new SubscriptionCommision();
                    commission.Amount = set.SubscriptionAmount;
                    commission.ClassLevelId = subject.LiveClassLevelId;
                    commission.StudentModelId = subject.StudentModelId;
                    commission.SubjectId = subject.LiveClassSubjectId;
                    commission.SubscriptionId = pay.Id;
                    commission.TeacherId = teacher.TeacherId;
                    commission.StartDate = pay.StartDate;
                    commission.EndDate = pay.EndDate;
                    commission.SystemCommission = (decimal)(75.0 / 100.00) * set.SubscriptionAmount;
                    commission.TeacherCommission = (decimal)(25.0 / 100.00) * set.SubscriptionAmount;
                    db.SubscriptionCommision.Add(commission);
                    await db.SaveChangesAsync();

                }

                //Teacher Wallet Payment According to student that subscribe to their subject
                foreach (var subject in enrolledSubject)
                {
                    var techersub = db.TeacherLiveSubjectAssignments.Include(x => x.Subject).Include(x => x.Class).FirstOrDefault(x => x.SubjectId == subject.LiveClassSubjectId && x.ClassId == subject.LiveClassLevelId);
                    var teacher = db.TeacherLiveSubjectAssignments.Include(x => x.Subject).Include(x => x.Class).FirstOrDefault(x => x.SubjectId == subject.LiveClassSubjectId && x.ClassId == subject.LiveClassLevelId && x.TeacherId == techersub.TeacherId);
                    var user = db.UserProfileModels.Include(x => x.User).FirstOrDefault(x => x.Id == techersub.TeacherId);
                    var walletCheck = db.TeacherWallet.Include(x => x.User).Include(x => x.Teacher).FirstOrDefault(x => x.TeacherId == teacher.Id);
                   
                    //Teacher Wallet Credit base on the commisssion set in the system
                    if (walletCheck != null)
                    {
                        walletCheck.Amount = walletCheck.Amount + (decimal)(25.0 / 100.00) * set.SubscriptionAmount;
                        db.Entry(walletCheck).State = EntityState.Modified;
                        await db.SaveChangesAsync();
                    }
                    else
                    {
                        TeacherWallet wallet = new TeacherWallet();
                        wallet.Amount = wallet.Amount + (decimal)(25.0 / 100.00) * set.SubscriptionAmount;
                        wallet.UserId = user.UserId;
                        wallet.TeacherId = teacher.TeacherId;
                        db.TeacherWallet.Add(wallet);
                        await db.SaveChangesAsync();
                    }
                }

            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task Edit(Subscription item)
        {
            db.Entry(item).State = EntityState.Modified;
            await db.SaveChangesAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        //public async Task ApproveOnline(Subscription item)
        //{
        //    db.Entry(item).State = EntityState.Modified;
        //    await db.SaveChangesAsync();
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        //public async Task ApproveDeposit(Subscription item)
        //{
        //    db.Entry(item).State = EntityState.Modified;
        //    await db.SaveChangesAsync();
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<List<Subscription>> GetUserSubscription()
        {
            var uId = HttpContext.Current.User.Identity.GetUserId();
            var item = await db.Subscriptions.Include(x => x.ClassLevel).Include(x => x.User).Where(x => x.UserId == uId).OrderByDescending(x => x.EndDate).ToListAsync();
            return item;
        }
    }
}