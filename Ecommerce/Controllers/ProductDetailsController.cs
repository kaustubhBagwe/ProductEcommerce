    using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Ecommerce.DAL;
using Ecommerce.Models.Admin;

namespace Ecommerce.Controllers
{
    public class ProductDetailsController : Controller
    {
        private EcommerceDBContect db = new EcommerceDBContect();

        // GET: ProductDetails
        public async Task<ActionResult> Index()
        {
            return View(await db.ProductDetails.ToListAsync());
        }

        // GET: ProductDetails/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductDetails productDetails = await db.ProductDetails.FindAsync(id);
            if (productDetails == null)
            {
                return HttpNotFound();
            }
            return View(productDetails);
        }

        // GET: ProductDetails/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ProductID,StoreID,ProdreferencetID,ProductCode,ProductName,ProductDescription,ProductCategory,ProductSubCategory,ProductSize,ProductColor,ProductBrand,ProductQuantity,ProductTag,ProductSKUCode,ProductMetatagdescription,ProductMetatagKey,ProductMetataAuthor,ProductPrice,CID,VAT,ProductImageId,ProductWeight,ProductStar,IsActive,IsDelete,IsUpdate,InsertDate,LMDDate")] ProductDetails productDetails)
        {
            if (ModelState.IsValid)
            {
                db.ProductDetails.Add(productDetails);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(productDetails);
        }

        // GET: ProductDetails/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductDetails productDetails = await db.ProductDetails.FindAsync(id);
            if (productDetails == null)
            {
                return HttpNotFound();
            }
            return View(productDetails);
        }

        // POST: ProductDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ProductID,StoreID,ProdreferencetID,ProductCode,ProductName,ProductDescription,ProductCategory,ProductSubCategory,ProductSize,ProductColor,ProductBrand,ProductQuantity,ProductTag,ProductSKUCode,ProductMetatagdescription,ProductMetatagKey,ProductMetataAuthor,ProductPrice,CID,VAT,ProductImageId,ProductWeight,ProductStar,IsActive,IsDelete,IsUpdate,InsertDate,LMDDate")] ProductDetails productDetails)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productDetails).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(productDetails);
        }

        // GET: ProductDetails/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductDetails productDetails = await db.ProductDetails.FindAsync(id);
            if (productDetails == null)
            {
                return HttpNotFound();
            }
            return View(productDetails);
        }

        // POST: ProductDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ProductDetails productDetails = await db.ProductDetails.FindAsync(id);
            db.ProductDetails.Remove(productDetails);
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
