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
    [Authorize(Roles = "SuperAdmin,Admin,Graphics")]

    public class ImageSlidersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/ImageSliders
        public async Task<ActionResult> Index()
        {
            return View(await db.ImageSliders.ToListAsync());
        }

        // GET: Admin/ImageSliders/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ImageSlider imageSlider = await db.ImageSliders.FindAsync(id);
            if (imageSlider == null)
            {
                return HttpNotFound();
            }
            return View(imageSlider);
        }

        // GET: Admin/ImageSliders/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/ImageSliders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ImageSlider imageSlider, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                if (upload != null && upload.ContentLength > 0)
                {


                    // Find its length and convert it to byte array
                    int ContentLength = upload.ContentLength;

                    // Create Byte Array
                    byte[] bytImg = new byte[ContentLength];

                    // Read Uploaded file in Byte Array
                    upload.InputStream.Read(bytImg, 0, ContentLength);

                    imageSlider.Content = bytImg;
                    imageSlider.ContentType = upload.ContentType;
                    imageSlider.FileName = upload.FileName;

                }
                imageSlider.CurrentSlider = true;
                imageSlider.OrderSort = 9999999;

                db.ImageSliders.Add(imageSlider);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(imageSlider);
        }

        // GET: Admin/ImageSliders/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ImageSlider imageSlider = await db.ImageSliders.FindAsync(id);
            if (imageSlider == null)
            {
                return HttpNotFound();
            }
            return View(imageSlider);
        }

        // POST: Admin/ImageSliders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ImageSlider imageSlider)
        {
            if (ModelState.IsValid)
            {
                db.Entry(imageSlider).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(imageSlider);
        }

        // GET: Admin/ImageSliders/Delete/5
        //public async Task<ActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    ImageSlider imageSlider = await db.ImageSliders.FindAsync(id);
        //    if (imageSlider == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(imageSlider);
        //}

        // POST: Admin/ImageSliders/Delete/5
       
        public async Task<ActionResult> Delete(int id)
        {
            ImageSlider imageSlider = await db.ImageSliders.FindAsync(id);
            db.ImageSliders.Remove(imageSlider);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }


        public async Task<ActionResult> AddToSlider(int id)
        {
            var models = await db.ImageSliders.FindAsync(id);
            if (models.CurrentSlider == false)
            {
                models.CurrentSlider = true;
                db.Entry(models).State = EntityState.Modified;
                await db.SaveChangesAsync();
            }
            else
            {
                models.CurrentSlider = false;
                db.Entry(models).State = EntityState.Modified;
                await db.SaveChangesAsync();
            }
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
