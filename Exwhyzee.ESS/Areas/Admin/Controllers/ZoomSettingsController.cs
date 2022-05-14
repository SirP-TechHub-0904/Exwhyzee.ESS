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
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class ZoomSettingsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/ZoomSettings
        public async Task<ActionResult> Index()
        {
            return View(await db.ZoomSetting.ToListAsync());
        }

        // GET: Admin/ZoomSettings/Details/5
        public async Task<ActionResult> Details()
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            var zoomSetting = await db.ZoomSetting.FirstOrDefaultAsync();
            if (zoomSetting == null)
            {
                return HttpNotFound();
            }
            return View(zoomSetting);
        }

        // GET: Admin/ZoomSettings/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/ZoomSettings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ZoomSetting zoomSetting)
        {
            if (ModelState.IsValid)
            {
                db.ZoomSetting.Add(zoomSetting);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(zoomSetting);
        }

        // GET: Admin/ZoomSettings/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ZoomSetting zoomSetting = await db.ZoomSetting.FindAsync(id);
            if (zoomSetting == null)
            {
                return HttpNotFound();
            }
            return View(zoomSetting);
        }

        // POST: Admin/ZoomSettings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ZoomSetting zoomSetting)
        {
            if (ModelState.IsValid)
            {
                db.Entry(zoomSetting).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Details");
            }
            return View(zoomSetting);
        }

        // GET: Admin/ZoomSettings/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ZoomSetting zoomSetting = await db.ZoomSetting.FindAsync(id);
            if (zoomSetting == null)
            {
                return HttpNotFound();
            }
            return View(zoomSetting);
        }

        // POST: Admin/ZoomSettings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ZoomSetting zoomSetting = await db.ZoomSetting.FindAsync(id);
            db.ZoomSetting.Remove(zoomSetting);
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
