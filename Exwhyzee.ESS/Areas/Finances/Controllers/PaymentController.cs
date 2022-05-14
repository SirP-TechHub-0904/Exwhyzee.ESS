using Exwhyzee.ESS.Areas.Data.IServices;
using Exwhyzee.ESS.Areas.Data.Services;
using Exwhyzee.ESS.Models;
using Exwhyzee.ESS.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using System.Net;
using Microsoft.AspNet.Identity;

namespace Exwhyzee.ESS.Areas.Finances.Controllers
{
    [Authorize(Roles = "Teacher,Admin,SuperAdmin,Volunteer,Student")]
    public class PaymentController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private IPaystackTransactionService _paystackTransactionService = new PaystackTransactionService();
        private ISubscriptionService _subscription = new SubscriptionService();

        public PaymentController()
        {

        }

        public PaymentController(
            SubscriptionService subscription,
            PaystackTransactionService paystackTransactionService
          )
        {

            _paystackTransactionService = paystackTransactionService;
            _subscription = subscription;
        }

        // GET: Finances/Payment
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult BankDetail()
        {
            return View();
        }

        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<ActionResult> Subscriptions()
        {
            var item = await _subscription.AllSubscription();
            return View(item);
        }

        public async Task<ActionResult> MySubscription()
        {
            var item = await _subscription.GetUserSubscription();
            return View(item);
        }



        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<ActionResult> AdminSubDetails(int? id)
        {
            var item = await _subscription.Get(id);
            var student = db.StudentModel.Include(x => x.ClassLevel).Where(x => x.UserId == item.UserId).FirstOrDefault();
            var subject = db.LiveClassSubject.Include(x => x.LiveClassLevel).Where(x => x.LiveClassLevelId == student.ClassLevelId).ToList();
            var enrolledSubject = db.LiveClassSubjectEnrollment.
              Include(x => x.LiveClassLevel).Include(x => x.StudentModel)
              .Include(x => x.LiveClassSubject).
              Where(x => x.UserId == student.UserId && x.LiveClassLevelId == student.ClassLevelId).ToList();
            var std = db.StudentModel.Include(x => x.User).FirstOrDefault(x => x.UserId == item.UserId);
            ViewBag.pro = std;
            ViewBag.subject = enrolledSubject;
            var set = db.ZoomSetting.FirstOrDefault();
            ViewBag.subAmount = set;
            return View(item);

        }


        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<ActionResult> AdminSubPrint(int? id)
        {
            var item = await _subscription.Get(id);
            var student = db.StudentModel.Include(x => x.ClassLevel).Where(x => x.UserId == item.UserId).FirstOrDefault();
            var subject = db.LiveClassSubject.Include(x => x.LiveClassLevel).Where(x => x.LiveClassLevelId == student.ClassLevelId).ToList();
            var enrolledSubject = db.LiveClassSubjectEnrollment.
              Include(x => x.LiveClassLevel).Include(x => x.StudentModel)
              .Include(x => x.LiveClassSubject).
              Where(x => x.UserId == student.UserId && x.LiveClassLevelId == student.ClassLevelId).ToList();
            var std = db.StudentModel.Include(x => x.User).FirstOrDefault(x => x.UserId == item.UserId);
            ViewBag.pro = std;
            ViewBag.subject = enrolledSubject;
            var set = db.ZoomSetting.FirstOrDefault();
            ViewBag.subAmount = set;
            return View(item);
        }

        public async Task<ActionResult> SubDetails(int? id)
        {
            var userId = User.Identity.GetUserId();
            var student = db.StudentModel.Include(x => x.ClassLevel).Where(x => x.UserId == userId).FirstOrDefault();
            var subject = db.LiveClassSubject.Include(x => x.LiveClassLevel).Where(x => x.LiveClassLevelId == student.ClassLevelId).ToList();
            var enrolledSubject = db.LiveClassSubjectEnrollment.
              Include(x => x.LiveClassLevel).Include(x => x.StudentModel)
              .Include(x => x.LiveClassSubject).
              Where(x => x.UserId == student.UserId && x.LiveClassLevelId == student.ClassLevelId).ToList();
            var item = await _subscription.Get(id);
            var std = db.StudentModel.Include(x => x.User).FirstOrDefault(x => x.UserId == item.UserId);
            ViewBag.pro = std;
            ViewBag.subject = enrolledSubject;
            var set = db.ZoomSetting.FirstOrDefault();
            ViewBag.subAmount = set;
            return View(item);

        }


        public async Task<ActionResult> SubPrint(int? id)
        {
            var userId = User.Identity.GetUserId();
            var student = db.StudentModel.Include(x => x.ClassLevel).Where(x => x.UserId == userId).FirstOrDefault();
            var subject = db.LiveClassSubject.Include(x => x.LiveClassLevel).Where(x => x.LiveClassLevelId == student.ClassLevelId).ToList();
            var enrolledSubject = db.LiveClassSubjectEnrollment.
              Include(x => x.LiveClassLevel).Include(x => x.StudentModel)
              .Include(x => x.LiveClassSubject).
              Where(x => x.UserId == student.UserId && x.LiveClassLevelId == student.ClassLevelId).ToList();
            var item = await _subscription.Get(id);
            var std = db.StudentModel.Include(x => x.User).FirstOrDefault(x => x.UserId == item.UserId);
            ViewBag.pro = std;
            ViewBag.subject = enrolledSubject;
            var set = db.ZoomSetting.FirstOrDefault();
            ViewBag.subAmount = set;
            return View(item);
        }


        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<ActionResult> ApproveDeposit(Subscription item, int? id)
        {
            await _subscription.BankDeposit(item, id);
            return RedirectToAction("AdminSubDetails", "Payment", new { id = item.Id });
        }


        [Authorize(Roles = "Admin,SuperAdmin,Teacher,Volunteer")]
        public async Task<ActionResult> WithdrawalRequest()
        {
            var user = User.Identity.GetUserId();
            var teacher = await db.UserProfileModels.Include(x => x.User).FirstOrDefaultAsync(x => x.UserId == user);
            var wallet = db.TeacherWallet.Include(x => x.User).Include(x => x.Teacher).Where(x => x.TeacherId == teacher.Id && x.UserId == user);
            ViewBag.Amount = wallet.Sum(x => x.Amount);
            return View();
        }

        [Authorize(Roles = "Admin,SuperAdmin,Teacher,Volunteer")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> WithdrawalRequest(TeacherWithdrawal item)
        {
            var user = User.Identity.GetUserId();
            var teacher = db.UserProfileModels.Include(x => x.User).FirstOrDefault(x => x.UserId == user);
            var wallet = db.TeacherWallet.Include(x => x.User).Include(x => x.Teacher).Where(x => x.TeacherId == teacher.Id && x.UserId == user);
            item.Amount = wallet.Sum(x => x.Amount);
            item.TeacherId = teacher.Id;
            item.RequestDate = DateTime.UtcNow.AddHours(1);
            item.Status = WithdrawalStatus.Pending;

            db.TeacherWithdrawal.Add(item);
            await db.SaveChangesAsync();
            //ViewBag.Amount = wallet.Sum(x => x.Amount);
            return RedirectToAction("Withdrawals", "Payment");
        }

        [Authorize(Roles = "Admin,SuperAdmin,Teacher,Volunteer")]
        public async Task<ActionResult> Withdrawals()
        {
            var user = User.Identity.GetUserId();
            var teacher = db.UserProfileModels.Include(x => x.User).FirstOrDefault(x => x.UserId == user);
            var withdrawal = await db.TeacherWithdrawal.Include(x => x.ApprovedBy).Include(x => x.Teacher).Where(x => x.TeacherId == teacher.Id).ToListAsync();
            return View(withdrawal);
        }
       

        [Authorize(Roles = "Admin,SuperAdmin,Teacher,Volunteer")]
        public async Task<ActionResult> UserWithdrawals()
        {
            var withdrawal = await db.TeacherWithdrawal.Include(x => x.ApprovedBy).Include(x => x.Teacher).OrderByDescending(x=>x.RequestDate).ToListAsync();
            return View(withdrawal);
        }


        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<ActionResult> ApproveRequest(int? id)
        {
            var user = User.Identity.GetUserId();
            var admin = db.UserProfileModels.Include(x => x.User).FirstOrDefault(x => x.UserId == user);
            var withdrawal = db.TeacherWithdrawal.Include(x => x.ApprovedBy).Include(x => x.Teacher).FirstOrDefault(x=>x.Id == id);
            withdrawal.ApprovedbyId = admin.Id;
            withdrawal.Status = WithdrawalStatus.Approved;
            withdrawal.ApprovedDate = DateTime.UtcNow.AddHours(1);
            db.Entry(withdrawal).State = EntityState.Modified;
            await db.SaveChangesAsync();
            if(withdrawal.Status == WithdrawalStatus.Approved)
            {
                var wallet = db.TeacherWallet.Include(x => x.Teacher).FirstOrDefault(x => x.TeacherId == withdrawal.TeacherId);
                var amount = withdrawal.Amount - withdrawal.Amount;
                wallet.Amount = amount;
                db.Entry(wallet).State = EntityState.Modified;
                await db.SaveChangesAsync();
            }

            return View(withdrawal);
        }

        public async Task<ActionResult> Subscribe(Subscription item)
        {
            var user = User.Identity.GetUserId();
            var student = db.StudentModel.Include(x => x.User).Where(x => x.UserId == user).FirstOrDefault();
            var check = db.Subscriptions.Include(x => x.User).Include(x => x.StudentModel).Where(x => x.UserId == user && x.StudentModelId == student.Id).FirstOrDefault();
            if (check == null)
            {
                var sub = _subscription.Create(item);
                return RedirectToAction("SubDetails", "Payment", new { id = item.Id });
            }
            else
            {
                return RedirectToAction("SubDetails", "Payment", new { id = check.Id });
            }


        }

        public async Task<ActionResult> PayNow(Subscription item, int? id)
        {

            var sub = _subscription.Get(id);
            //var getsub = await _subscription.Get(item.Id);
            //
            var secretKey = "sk_live_f2dc574ed0268ffc9d26eae14c61bf1c22a321ea";
            var userId = User.Identity.GetUserId();
            var student = db.StudentModel.Include(x => x.User).Where(x => x.UserId == userId).FirstOrDefault();
            //var enrolledSubject = db.LiveClassSubjectEnrollment.
            //    Include(x => x.LiveClassLevel).Include(x => x.StudentModel)
            //    .Include(x => x.LiveClassSubject).
            //    Where(x => x.UserId == student.UserId && x.LiveClassLevelId == student.ClassLevelId && x.Status == true);
            //int enrolledSubjectCount = enrolledSubject.Count();


            int amountInKobo = (int)item.Amount * 100;

            var response = await _paystackTransactionService.
                InitializeTransaction(secretKey, item.User.Email,
                amountInKobo, item.Id, item.User.FirstName,
                item.User.SurName);

            if (response.status == true)
            {
                return Redirect(response.data.authorization_url);
            }

            return RedirectToAction("SubDetails", "Payment", new { id = item.Id });

        }

        public async Task<ActionResult> Complete(Subscription item)
        {
            //
            //var secretKey = _config["SecretKey"];
            var secretKey = "sk_live_f2dc574ed0268ffc9d26eae14c61bf1c22a321ea";
            var tranxRef = HttpContext.Request["reference"].ToString();
            if (tranxRef != null)
            {
                var response = await _paystackTransactionService.VerifyTransaction(tranxRef, secretKey);

                var id = int.Parse(response.data.metadata.CustomFields.FirstOrDefault(x => x.DisplayName == "Transaction Id").Value);
                var sub = await _subscription.Get(id);

                if (response.status)
                {


                    //clientedit.Units = transaction.Amount;

                    if (sub == null)
                    {
                        TempData["warning"] = $"Subscription Payment with Reference {tranxRef} was successful. But Status was not updated. Please contact Help Desk.";
                    }
                    else if (!string.IsNullOrEmpty(sub.ReferenceId))
                    {
                        TempData["warning"] = $"Subscription Payment with Reference {tranxRef} was successful.";
                    }
                    else
                    {
                        await _subscription.OnlinePay(item, id);

                        var refid = await db.Subscriptions.FirstOrDefaultAsync(x => x.Id == id);
                        refid.ReferenceId = tranxRef;
                        db.Entry(refid).State = EntityState.Modified;
                        await db.SaveChangesAsync();
                        TempData["success"] = $"Subscription Payment with Reference {tranxRef} was successful.";

                    }

                    string txt = $"Your Payment with Reference {tranxRef} was Successful";
                    string msgAdmin = sub.User.Email + " Made a successful Payment";

                    await HelperClass.SendSMS(txt, sub.User.PhoneNumber);
                    await HelperClass.SendMail(sub.User.Email, txt);
                    await HelperClass.SendMail("onwukaemeka41@gmail.com", msgAdmin);
                    await HelperClass.SendMail("judengama@gmail.com", msgAdmin);

                    return RedirectToAction("SubDetails", "Payment", new { id = sub.Id });
                }
                else
                {

                    sub.Status = SubscriptionStatus.Cancel;
                    await _subscription.Edit(sub);
                    TempData["error"] = $"Transaction with Reference {tranxRef} failed.";

                    return RedirectToAction("SubDetails", "Payment", new { id = sub.Id });

                }

            }

            TempData["error"] = $"Invoice Payment with Reference {tranxRef} failed.";

            return RedirectToAction("MySubscription");
        }

    }
}