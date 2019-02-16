using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ecommerce.Controllers
{
    public class DashbordController : Controller
    {
        // GET: Dashbord
        public ActionResult Index()
        {
            return View();
        }

        // GET: Dashbord/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Dashbord/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Dashbord/Create
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

        // GET: Dashbord/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Dashbord/Edit/5
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

        // GET: Dashbord/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Dashbord/Delete/5
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
