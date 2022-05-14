using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Exwhyzee.ESS.Areas.Student.Controllers
{
    public class SubscriptionsController : Controller
    {
        // GET: Student/Subscriptions 
        public ActionResult Index()
        {
            return View();
        }

        // GET: Student/Subscriptions/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Student/Subscriptions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Student/Subscriptions/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Student/Subscriptions/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Student/Subscriptions/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Student/Subscriptions/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Student/Subscriptions/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
