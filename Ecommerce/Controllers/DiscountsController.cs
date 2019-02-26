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
using PagedList;
using Ecommerce.Models.Admin;

namespace Ecommerce.Controllers
{
    public class DiscountsController : Controller
    {
        private EcommerceDBContect db = new EcommerceDBContect();

        // GET: Discounts
        public ActionResult Index(int? page)
        {
            int CustID = Convert.ToInt32(Session["CustId"]);

            return View(db.Discounts.Where(c => c.DiscountVendorID == CustID).OrderByDescending(x => x.DicountID).ToPagedList(page ?? 1, 10));
        }

        // GET: Discounts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Discount discount = db.Discounts.Find(id);
            if (discount == null)
            {
                return HttpNotFound();
            }
            return View(discount);
        }

        // GET: Discounts/Create
        public ActionResult Create()
        {
            int CustID = Convert.ToInt32(Session["CustId"]);
            List<ProductDetails> plist = db.ProductDetails.Where(x => x.CID == CustID).ToList();
            if (plist.Count == 0)
            {
                //return RedirectToAction("Index", "Discounts");
                ViewBag.AllVendorProduct = plist;
                ViewBag.AllCategories = db.Category.ToList();
                ViewBag.CountProd = plist.Count;
            }
            else
            {
                ViewBag.AllVendorProduct = plist;
                ViewBag.AllCategories = db.Category.ToList();
                ViewBag.CountProd = plist.Count;
            }
            return View();
        }

        // POST: Discounts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Discount discount, FormCollection form)
        {
            //if (ModelState.IsValid)
            //{
            Discount dcnt = new Discount();
            dcnt.DiscountCode = discount.DiscountCode;
            dcnt.StoreID = Session["StoreID"].ToString();
            dcnt.DiscountTitle = discount.DiscountTitle;
            dcnt.DiscountDetails = discount.DiscountDetails;
            foreach (string file in Request.Files)
            {
                var fileContent = Request.Files[file];
                if (fileContent != null && fileContent.ContentLength > 0)
                {
                    string pic = System.IO.Path.GetFileName(fileContent.FileName);
                    string path = System.IO.Path.Combine(Server.MapPath("~/images/"), pic);
                    fileContent.SaveAs(path);
                    dcnt.DiscountImage = "http://livingstud.com/images/" + fileContent.FileName;
                }
            }
            dcnt.DiscountedProductCategoryID = discount.DiscountedProductCategoryID;
            dcnt.DiscountProductID = discount.DiscountProductID;
            dcnt.DiscountVendorID = Convert.ToInt32(Session["CustId"]);
            dcnt.DiscountStartDate = discount.DiscountStartDate;
            dcnt.DiscountEndDate = discount.DiscountEndDate;
            dcnt.DiscountAmount = discount.DiscountAmount;
            dcnt.DiscountAmountType = discount.DiscountAmountType;
            dcnt.IsOnCategories = discount.IsOnCategories;
            dcnt.IsOnProducts = discount.IsOnProducts;
            dcnt.IsOnStore = discount.IsOnStore;
            dcnt.IsAllProducts = discount.IsAllProducts;
            dcnt.InsertDate = DateTime.Now.ToString();
            dcnt.UpdateDate = DateTime.Now.ToString();
            dcnt.IsActive = true;
            dcnt.IsDelete = false;
            dcnt.IsUpdate = true;
            db.Discounts.Add(dcnt);
            db.SaveChanges();
            ProductDetails pd = new ProductDetails();
            if (discount.IsOnProducts == true)
            {
                int lastProductId = discount.DiscountProductID;
                pd = db.ProductDetails.Where(x => x.ProductID == lastProductId).FirstOrDefault();

                //pd.IsDiscounted = true;
                //if (discount.DiscountAmountType == "1")
                //{
                //    pd.DiscountAmount = Math.Round(pd.ProductPrice, 0) * Convert.ToDecimal(discount.DiscountAmount) / 100;
                //}
                //else
                //{
                //    pd.DiscountAmount = Math.Round(pd.ProductPrice, 0) - Convert.ToDecimal(discount.DiscountAmount);

                //}

                db.Entry(pd).State = EntityState.Modified;
                db.SaveChanges();
            }
            if (discount.IsOnCategories == true) {
                List<ProductDetails> lstProd = new List<ProductDetails>();
                int catid = discount.DiscountedProductCategoryID;
                lstProd = db.ProductDetails.Where(x => x.ProductCategory == catid).ToList();
                if (lstProd.Count > 0) 
                {
                    for (int i = 0; i < lstProd.Count; i++)
                    {
                        int lastProductId = lstProd[i].ProductID;
                        pd = db.ProductDetails.Where(x => x.ProductID == lastProductId).FirstOrDefault();

                        //pd.IsDiscounted = true;
                        //if (discount.DiscountAmountType == "1")
                        //{
                        //    pd.DiscountAmount = Math.Round(pd.ProductPrice, 0) * Convert.ToDecimal(discount.DiscountAmount) / 100;
                        //}
                        //else
                        //{
                        //    pd.DiscountAmount = Math.Round(pd.ProductPrice, 0) - Convert.ToDecimal(discount.DiscountAmount);
                        //}

                        db.Entry(pd).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            if (discount.IsAllProducts == true) {
                List<ProductDetails> lstProd = new List<ProductDetails>();
                lstProd = db.ProductDetails.ToList();
                if (lstProd.Count > 0)
                {
                    for (int i = 0; i < lstProd.Count; i++)
                    {
                        int lastProductId = lstProd[i].ProductID;
                        pd = db.ProductDetails.Where(x => x.ProductID == lastProductId).FirstOrDefault();

                        //pd.IsDiscounted = true;
                        //if (discount.DiscountAmountType == "1")
                        //{
                        //    pd.DiscountAmount = Math.Round(pd.ProductPrice, 0) * Convert.ToDecimal(discount.DiscountAmount) / 100;
                        //}
                        //else
                        //{
                        //    pd.DiscountAmount = Math.Round(pd.ProductPrice, 0) - Convert.ToDecimal(discount.DiscountAmount);
                        //}

                        db.Entry(pd).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            return RedirectToAction("Index");
           
            return View(discount);
        }

        // GET: Discounts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Discount discount = db.Discounts.Find(id);
            if (discount == null)
            {
                return HttpNotFound();
            }
            return View(discount);
        }

        // POST: Discounts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DicountID,DiscountCode,DiscountTitle,DiscountDetails,DiscountType,DiscountedProductCategoryID,DiscountProductID,DiscountVendorID,DiscountStartDate,DiscountEndDate,DiscountAmount,DiscountAmountType,InsertDate,UpdateDate,IsActive,IsDelete,IsUpdate")] Discount discount)
        {
            if (ModelState.IsValid)
            {
                db.Entry(discount).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(discount);
        }

        // GET: Discounts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Discount discount = db.Discounts.Find(id);
            if (discount == null)
            {
                return HttpNotFound();
            }
            return View(discount);
        }

        // POST: Discounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Discount discount = db.Discounts.Find(id);
            db.Discounts.Remove(discount);
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
