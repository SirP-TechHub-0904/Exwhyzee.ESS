using Exwhyzee.ESS.Data.PostServices;
using Exwhyzee.ESS.Models;
using Exwhyzee.ESS.Models.Entities;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Exwhyzee.ESS.Areas.Admin.Controllers
{
    [Authorize(Roles = "SuperAdmin,Admin,Content")]

    public class PostsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private IPostService _postService = new PostService();


        public PostsController()
        {

        }
        public PostsController(
            PostService postService
            )
        {
            _postService = postService;
        }
        // GET: Content/Posts
        public async Task<ActionResult> Index(string searchString, string currentFilter, int? page)
        {
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            var items = await _postService.List(searchString, currentFilter, page);

            int pageSize = 50;
            int pageNumber = (page ?? 1);
            ViewBag.Total = items.Count();
            return View(items.ToPagedList(pageNumber, pageSize));
        }

        // GET: Content/Posts/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var post = await _postService.Get(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

       

        // GET: Content/Posts/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: Content/Posts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Post model, HttpPostedFileBase upload)
        {
            string response = "";
            if (ModelState.IsValid)
            {
                model.DatePosted = DateTime.UtcNow.AddHours(1);

                var status = await _postService.Create(model, upload);
                response = status;
                if (status == "OK")
                {
                    TempData["Success"] = "Success";
                    return RedirectToAction("Index");
                }

               
            }
            TempData["fail"] = "Failed" + response;
            return View(model);
        }

        // GET: Content/Posts/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var post = await _postService.Get(id);
            if (post == null)
            {
                return HttpNotFound();
            }

            return View(post);
        }

        // POST: Content/Posts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Post model, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {

                var status = await _postService.Edit(model, upload);

                if (status == "OK")
                {
                    TempData["Success"] = "Success";
                    return RedirectToAction("Index");
                }


            }
            TempData["fail"] = "Failed";
            return View(model);
        }

        // GET: Content/Posts/Delete/5
        public async Task<ActionResult> Delete(int? id, string ReturnUrl)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var post = await _postService.Get(id);

            if (post == null)
            {
                return HttpNotFound();
            }
            ViewBag.ReturnUrl = ReturnUrl;
            return View(post);
        }

        // POST: Content/Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id, string ReturnUrl)
        {
           
            await _postService.Delete(id);
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