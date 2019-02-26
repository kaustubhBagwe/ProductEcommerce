using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Ecommerce.DAL;
using Ecommerce.Models;

namespace Ecommerce.Controllers
{
    public class SubsubCategoriesController : Controller
    {
        private EcommerceDBContect db = new EcommerceDBContect();

        // GET: SubsubCategories
        public ActionResult Index()
        {
            return View(db.SubsubCategories.ToList());
        }

        // GET: SubsubCategories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubsubCategory subsubCategory = db.SubsubCategories.Find(id);
            if (subsubCategory == null)
            {
                return HttpNotFound();
            }
            return View(subsubCategory);
        }

        // GET: SubsubCategories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SubsubCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SubsubCategory subsubCategory)
        {
            if (ModelState.IsValid)
            {
                SubsubCategory sc = new SubsubCategory();
                sc.SubsubCategoryName = subsubCategory.SubsubCategoryName;
                sc.SubCategoryId = subsubCategory.SubCategoryId;
                sc.IsActive = true;
                sc.IsDelete = false;
                sc.IsUpdate = false;
                sc.InsertDate = DateTime.Now;
                sc.LMDDate = DateTime.Now;
                db.SubsubCategories.Add(sc);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(subsubCategory);
        }

        // GET: SubsubCategories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubsubCategory subsubCategory = db.SubsubCategories.Find(id);
            if (subsubCategory == null)
            {
                return HttpNotFound();
            }
            return View(subsubCategory);
        }

        // POST: SubsubCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SubsubCategoryID,SubsubCategoryName,SubCategoryId,IsActive,IsDelete,IsUpdate,InsertDate,LMDDate")] SubsubCategory subsubCategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(subsubCategory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(subsubCategory);
        }

        // GET: SubsubCategories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubsubCategory subsubCategory = db.SubsubCategories.Find(id);
            if (subsubCategory == null)
            {
                return HttpNotFound();
            }
            return View(subsubCategory);
        }

        // POST: SubsubCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SubsubCategory subsubCategory = db.SubsubCategories.Find(id);
            db.SubsubCategories.Remove(subsubCategory);
            db.SaveChanges();
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
