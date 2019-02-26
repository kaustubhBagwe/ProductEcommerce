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
    public class OrderLogsController : Controller
    {
        private EcommerceDBContect db = new EcommerceDBContect();

        // GET: OrderLogs
        public ActionResult Index()
        {
            return View(db.OrderLogs.ToList());
        }

        // GET: OrderLogs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderLog orderLog = db.OrderLogs.Find(id);
            if (orderLog == null)
            {
                return HttpNotFound();
            }
            return View(orderLog);
        }

        // GET: OrderLogs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OrderLogs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OLogID,OID,CustId,Status,InsertDate,Owner")] OrderLog orderLog)
        {
            if (ModelState.IsValid)
            {
                db.OrderLogs.Add(orderLog);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(orderLog);
        }

        // GET: OrderLogs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderLog orderLog = db.OrderLogs.Find(id);
            if (orderLog == null)
            {
                return HttpNotFound();
            }
            return View(orderLog);
        }

        // POST: OrderLogs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OLogID,OID,CustId,Status,InsertDate,Owner")] OrderLog orderLog)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orderLog).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(orderLog);
        }

        // GET: OrderLogs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderLog orderLog = db.OrderLogs.Find(id);
            if (orderLog == null)
            {
                return HttpNotFound();
            }
            return View(orderLog);
        }

        // POST: OrderLogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OrderLog orderLog = db.OrderLogs.Find(id);
            db.OrderLogs.Remove(orderLog);
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
