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
using System.IO;
using SautinSoft;

namespace Exwhyzee.ESS.Areas.ELibrary.Controllers
{
    [Authorize(Roles = "SuperAdmin,Admin,Content")]

    public class BooksController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ELibrary/Books
        public async Task<ActionResult> Index()
        {
            var books = db.Books.Include(b => b.BookCategory);
            return View(await books.ToListAsync());
        }

        public async Task<ActionResult> BookByCategory(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var books = db.Books.Include(b => b.BookCategory).Where(x=>x.BookCategoryId == id);
            return View(await books.ToListAsync());
        }

        // GET: ELibrary/Books/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = await db.Books.FindAsync(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // GET: ELibrary/Books/Create
        public ActionResult Create()
        {
            ViewBag.BookCategoryId = new SelectList(db.BookCategories, "Id", "Title");
            return View();
        }

        // POST: ELibrary/Books/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Book model, HttpPostedFileBase upload, HttpPostedFileBase cover)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    if (upload != null && upload.ContentLength > 0)
                    {
                       

                        string date1 = DateTime.UtcNow.AddHours(1).ToString("ssfff");
                        string name = "Iskool-eLirary-" +date1 + "-" + upload.FileName;
                        string fileName = Path.GetFileName(name);
                        model.path = fileName;

                        fileName = Path.Combine(Server.MapPath("~/Library/"), fileName);
                        upload.SaveAs(fileName);
                       

                    }
                    if (cover != null && cover.ContentLength > 0)
                    {
                        if (cover != null && cover.ContentLength > 0)
                        {


                            // Find its length and convert it to byte array
                            int ContentLength = cover.ContentLength;

                            // Create Byte Array
                            byte[] bytImg = new byte[ContentLength];

                            // Read Uploaded file in Byte Array
                            cover.InputStream.Read(bytImg, 0, ContentLength);

                            model.BookImagePath = bytImg;
                          
                        }
                        

                    }

                }
                catch (Exception c)
                {
                    
                }
                model.Date = DateTime.UtcNow;
                db.Books.Add(model);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.BookCategoryId = new SelectList(db.BookCategories, "Id", "Title", model.BookCategoryId);
            return View(model);
        }

        // GET: ELibrary/Books/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = await db.Books.FindAsync(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            ViewBag.BookCategoryId = new SelectList(db.BookCategories, "Id", "Title", book.BookCategoryId);
            return View(book);
        }

        // POST: ELibrary/Books/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Book book, HttpPostedFileBase upload, HttpPostedFileBase cover)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    if (upload != null && upload.ContentLength > 0)
                    {


                        string date1 = DateTime.UtcNow.AddHours(1).ToString("ssfff");
                        string name = "Iskool-eLirary-" + date1 + "-" + upload.FileName;
                        string fileName = Path.GetFileName(name);
                        book.path = fileName;

                        fileName = Path.Combine(Server.MapPath("~/Library/"), fileName);
                        upload.SaveAs(fileName);


                    }
                    if (cover != null && cover.ContentLength > 0)
                    {
                        if (cover != null && cover.ContentLength > 0)
                        {


                            // Find its length and convert it to byte array
                            int ContentLength = cover.ContentLength;

                            // Create Byte Array
                            byte[] bytImg = new byte[ContentLength];

                            // Read Uploaded file in Byte Array
                            cover.InputStream.Read(bytImg, 0, ContentLength);

                            book.BookImagePath = bytImg;

                        }


                    }

                }
                catch (Exception c)
                {

                }

                db.Entry(book).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.BookCategoryId = new SelectList(db.BookCategories, "Id", "Title", book.BookCategoryId);
            return View(book);
        }

        // GET: ELibrary/Books/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = await db.Books.FindAsync(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: ELibrary/Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Book book = await db.Books.FindAsync(id);
            db.Books.Remove(book);
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
