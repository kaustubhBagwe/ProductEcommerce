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
    public class ProductReviewsController : Controller
    {
        private EcommerceDBContect db = new EcommerceDBContect();

        // GET: ProductReviews
        public ActionResult Index()
        {
            return View(db.ProductReviews.ToList());
        }

        // GET: ProductReviews/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductReviews productReviews = db.ProductReviews.Find(id);
           
            if (productReviews == null)
            {
                return HttpNotFound();
            }
            return View(productReviews);
        }

        // GET: ProductReviews/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductReviews/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Rid,ReviewMsg,ProducctID,Custid,StarCount,IsActive,IsDelete,IsUpdate,InsertDate,LMDDate")] ProductReviews productReviews)
        {
            if (ModelState.IsValid)
            {
                db.ProductReviews.Add(productReviews);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(productReviews);
        }

        // GET: ProductReviews/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductReviews productReviews = db.ProductReviews.Find(id);
            if (productReviews == null)
            {
                return HttpNotFound();
            }
            return View(productReviews);
        }

        // POST: ProductReviews/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Rid,ReviewMsg,ProducctID,Custid,StarCount,IsActive,IsDelete,IsUpdate,InsertDate,LMDDate")] ProductReviews productReviews)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productReviews).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(productReviews);
        }

        // GET: ProductReviews/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductReviews productReviews = db.ProductReviews.Find(id);
            if (productReviews == null)
            {
                return HttpNotFound();
            }
            return View(productReviews);
        }

        // POST: ProductReviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductReviews productReviews = db.ProductReviews.Find(id);
            db.ProductReviews.Remove(productReviews);
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
