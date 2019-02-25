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
    public class AdminStoresController : Controller
    {
        private EcommerceDBContect db = new EcommerceDBContect();

        // GET: AdminStores
        public async Task<ActionResult> Index()
        {
            return View(await db.AdminStores.ToListAsync());
        }

        // GET: AdminStores/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdminStore adminStore = await db.AdminStores.FindAsync(id);
            if (adminStore == null)
            {
                return HttpNotFound();
            }
            return View(adminStore);
        }

        // GET: AdminStores/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminStores/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AdminStore adminStore)
        {
            if (ModelState.IsValid)
            {
                adminStore.CreatedOn = DateTime.Now;
                db.AdminStores.Add(adminStore);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(adminStore);
        }

        // GET: AdminStores/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdminStore adminStore = await db.AdminStores.FindAsync(id);
            if (adminStore == null)
            {
                return HttpNotFound();
            }
            return View(adminStore);
        }

        // POST: AdminStores/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(AdminStore adminStore)
        {
            if (ModelState.IsValid)
            {
                db.Entry(adminStore).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(adminStore);
        }

        // GET: AdminStores/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdminStore adminStore = await db.AdminStores.FindAsync(id);
            if (adminStore == null)
            {
                return HttpNotFound();
            }
            return View(adminStore);
        }

        // POST: AdminStores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            AdminStore adminStore = await db.AdminStores.FindAsync(id);
            db.AdminStores.Remove(adminStore);
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
