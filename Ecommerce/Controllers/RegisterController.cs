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
using Ecommerce.Models.Admin;

namespace Ecommerce.Controllers
{
    public class RegisterController : Controller
    {
        private EcommerceDBContect db = new EcommerceDBContect();

        // GET: Register
        public ActionResult Index()
        {
            ViewBag.citylist = db.State.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(CustomerDetails customerDetails)
        {
            //if (ModelState.IsValid)
            //{

            CustomerDetails vcd = new CustomerDetails();
            vcd.Password = customerDetails.Password; 
            vcd.CustName = customerDetails.CustName;
            vcd.CustFName = customerDetails.CustFName;
            vcd.CustLName = customerDetails.CustLName; 
            vcd.CusttTitle = customerDetails.CusttTitle;
            vcd.CustAddress1 = customerDetails.CustAddress1;
            vcd.CustAddress2 = customerDetails.CustAddress2;
            vcd.CustCity = customerDetails.CustCity;
            vcd.CustState = customerDetails.CustState; 
            vcd.CustPostalCode = customerDetails.CustPostalCode;
            vcd.CustCountry = customerDetails.CustCountry; 
            vcd.CustPhone = customerDetails.CustPhone; 
            vcd.CustFax = customerDetails.CustFax; 
            vcd.CustEmail = customerDetails.CustEmail;
            vcd.CustWebSite = customerDetails.CustWebSite; 
            vcd.CreaditCardNumber = "";
            vcd.CardType =""; 
            vcd.CardExpMonth = customerDetails.CardExpMonth; 
            vcd.CardExpYear = customerDetails.CardExpYear; 
            vcd.WalletMoney = customerDetails.WalletMoney;
            vcd.InsertDate = DateTime.Now;
            vcd.LMDDate= DateTime.Now;
            vcd.IsActive = true; vcd.IsDelete = true; vcd.IsUpdate = true;
            db.CustomerDetails.Add(vcd);
            db.SaveChanges();
            return RedirectToAction("Index");
            //}
            //else {
            //    ModelState.AddModelError("error", "Enter Proper Details");

            //}
            return View(customerDetails);
        }



        // GET: Register/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerDetails customerDetails = db.CustomerDetails.Find(id);
            if (customerDetails == null)
            {
                return HttpNotFound();
            }
            return View(customerDetails);
        }

        // GET: Register/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Register/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CustID,Password,CustName,CustFName,CustLName,CusttTitle,CustAddress1,CustAddress2,CustCity,CustState,CustPostalCode,CustCountry,CustPhone,CustFax,CustEmail,CustWebSite,CreaditCardNumber,CardType,CardExpMonth,CardExpYear,WalletMoney,IsActive,IsDelete,IsUpdate,InsertDate,LMDDate,roll,IsSubScribe")] CustomerDetails customerDetails)
        {
            if (ModelState.IsValid)
            {
                db.CustomerDetails.Add(customerDetails);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(customerDetails);
        }

        // GET: Register/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerDetails customerDetails = db.CustomerDetails.Find(id);
            if (customerDetails == null)
            {
                return HttpNotFound();
            }
            return View(customerDetails);
        }

        // POST: Register/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CustID,Password,CustName,CustFName,CustLName,CusttTitle,CustAddress1,CustAddress2,CustCity,CustState,CustPostalCode,CustCountry,CustPhone,CustFax,CustEmail,CustWebSite,CreaditCardNumber,CardType,CardExpMonth,CardExpYear,WalletMoney,IsActive,IsDelete,IsUpdate,InsertDate,LMDDate,roll,IsSubScribe")] CustomerDetails customerDetails)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customerDetails).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customerDetails);
        }

        // GET: Register/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerDetails customerDetails = db.CustomerDetails.Find(id);
            if (customerDetails == null)
            {
                return HttpNotFound();
            }
            return View(customerDetails);
        }

        // POST: Register/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CustomerDetails customerDetails = db.CustomerDetails.Find(id);
            db.CustomerDetails.Remove(customerDetails);
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
