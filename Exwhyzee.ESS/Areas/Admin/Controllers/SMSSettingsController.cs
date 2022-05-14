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

namespace Exwhyzee.ESS.Areas.Admin.Controllers
{
    public class SMSSettingsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/SMSSettings
        public async Task<ActionResult> Index()
        {
            return View(await db.SMSSettings.ToListAsync());
        }

        // GET: Admin/SMSSettings/Details/5
        public async Task<ActionResult> Details()
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //SMSSettings sMSSettings = await db.SMSSettings.FindAsync(id);
            var sMSSettings = await db.SMSSettings.FirstOrDefaultAsync();
            if (sMSSettings == null)
            {
                return HttpNotFound();
            }
            return View(sMSSettings);
        }

        // GET: Admin/SMSSettings/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/SMSSettings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(SMSSettings sMSSettings)
        {
            if (ModelState.IsValid)
            {
                db.SMSSettings.Add(sMSSettings);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(sMSSettings);
        }

        // GET: Admin/SMSSettings/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SMSSettings sMSSettings = await db.SMSSettings.FindAsync(id);
            if (sMSSettings == null)
            {
                return HttpNotFound();
            }
            return View(sMSSettings);
        }

        // POST: Admin/SMSSettings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(SMSSettings sMSSettings)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sMSSettings).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(sMSSettings);
        }

        // GET: Admin/SMSSettings/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SMSSettings sMSSettings = await db.SMSSettings.FindAsync(id);
            if (sMSSettings == null)
            {
                return HttpNotFound();
            }
            return View(sMSSettings);
        }

        // POST: Admin/SMSSettings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            SMSSettings sMSSettings = await db.SMSSettings.FindAsync(id);
            db.SMSSettings.Remove(sMSSettings);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
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
}
