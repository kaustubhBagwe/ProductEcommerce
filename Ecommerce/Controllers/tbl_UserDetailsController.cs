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
using Ecommerce.Models;

namespace Ecommerce.Controllers
{
    public class tbl_UserDetailsController : Controller
    {
        private EcommerceDBContect db = new EcommerceDBContect();

        // GET: tbl_UserDetails
        public async Task<ActionResult> Index()
        {
            return View(await db.tbl_UserDetails.ToListAsync());
        }

        // GET: tbl_UserDetails/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_UserDetails tbl_UserDetails = await db.tbl_UserDetails.FindAsync(id);
            if (tbl_UserDetails == null)
            {
                return HttpNotFound();
            }
            return View(tbl_UserDetails);
        }

        // GET: tbl_UserDetails/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: tbl_UserDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "UserID,UserCode,UserType,UserFirstName,UserLastName,IsActive,CreatedOn,UpdatedDate,CreatedBy,UserContactNumber,UserEmailID,Documents,UserRoleID,Password")] tbl_UserDetails tbl_UserDetails)
        {
            if (ModelState.IsValid)
            {
                db.tbl_UserDetails.Add(tbl_UserDetails);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(tbl_UserDetails);
        }

        // GET: tbl_UserDetails/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_UserDetails tbl_UserDetails = await db.tbl_UserDetails.FindAsync(id);
            if (tbl_UserDetails == null)
            {
                return HttpNotFound();
            }
            return View(tbl_UserDetails);
        }

        // POST: tbl_UserDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "UserID,UserCode,UserType,UserFirstName,UserLastName,IsActive,CreatedOn,UpdatedDate,CreatedBy,UserContactNumber,UserEmailID,Documents,UserRoleID,Password")] tbl_UserDetails tbl_UserDetails)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_UserDetails).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(tbl_UserDetails);
        }

        // GET: tbl_UserDetails/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_UserDetails tbl_UserDetails = await db.tbl_UserDetails.FindAsync(id);
            if (tbl_UserDetails == null)
            {
                return HttpNotFound();
            }
            return View(tbl_UserDetails);
        }

        // POST: tbl_UserDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            tbl_UserDetails tbl_UserDetails = await db.tbl_UserDetails.FindAsync(id);
            db.tbl_UserDetails.Remove(tbl_UserDetails);
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
