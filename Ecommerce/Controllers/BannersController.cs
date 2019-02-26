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
using System.Threading.Tasks;

namespace Ecommerce.Controllers
{
    public class BannersController : Controller
    {
        private EcommerceDBContect db = new EcommerceDBContect();

        // GET: Banners
        public ActionResult Index()
        {
            return View(db.Banners.ToList());
        }

        // GET: Banners/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Banner banner = db.Banners.Find(id);
            if (banner == null)
            {
                return HttpNotFound();
            }
            return View(banner);
        }

        // GET: Banners/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Banners/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Create(int id)
        {
            try
            {

                foreach (string file in Request.Files)
                {
                    var fileContent = Request.Files[file];

                    if (fileContent != null && fileContent.ContentLength > 0)
                    {
                        string pic = System.IO.Path.GetFileName(fileContent.FileName);
                        string path = System.IO.Path.Combine(Server.MapPath("~/images/"), pic);
                        fileContent.SaveAs(path);
                        Banner newProdimg = new Banner();
                        newProdimg.BannerImgePath = "http://livingstud.com/images/" + pic;
                        newProdimg.IsActive = true;
                        newProdimg.IsDelete = false;
                        newProdimg.IsUpdate = false;
                        newProdimg.InsertDate = DateTime.Now;
                        newProdimg.LMDDate = DateTime.Now;
                        db.Banners.Add(newProdimg);
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json("Upload failed");
            }
            return Json("File uploaded successfully");
        }

        [HttpPost]
        public async Task<JsonResult> UploadFile(int id)
        {
            try
            {

                foreach (string file in Request.Files)
                {
                    var fileContent = Request.Files[file];
                    foreach (string key in Request.Form)
                    {
                        id = Convert.ToInt32(Request.Form["id"]);
                    }

                    if (fileContent != null && fileContent.ContentLength > 0)
                    {
                        string pic = System.IO.Path.GetFileName(fileContent.FileName);
                        string path = System.IO.Path.Combine(Server.MapPath("~/images/"), pic);
                        // file is uploaded
                        fileContent.SaveAs(path);
                        Banner newProdimg = new Banner();
                        newProdimg.BannerImgePath = "http://www.livingstud.com/images/" + pic;
                        newProdimg.IsActive = true;
                        newProdimg.IsDelete = false;
                        newProdimg.IsUpdate = false;
                        newProdimg.InsertDate = DateTime.Now;
                        newProdimg.LMDDate = DateTime.Now;
                        db.Banners.Add(newProdimg);
                        db.SaveChanges();

                    }
                }
            }
            catch (Exception)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json("Upload failed");
            }

            return Json("File uploaded successfully");
        }

        [HttpPost]
        public async Task<JsonResult> UploadFileUpdate(int id, string isActive,string img)
        {
            try
            {
                bool flag = false;
                if (isActive == "true")
                {
                    flag = true;
                }
                Banner newProdimg = new Banner();
                foreach (string key in Request.Form)
                {
                    id = Convert.ToInt32(Request.Form["id"]);
                }
                newProdimg.BannerImgePath = img;
                foreach (string file in Request.Files)
                {
                    var fileContent = Request.Files[file];

                    if (fileContent != null && fileContent.ContentLength > 0)
                    {
                        string pic = System.IO.Path.GetFileName(fileContent.FileName);
                        string path = System.IO.Path.Combine(Server.MapPath("~/images/"), pic);
                        // file is uploaded
                        fileContent.SaveAs(path);

                        newProdimg.BannerImgePath = "http://www.livingstud.com/images/" + pic;
                    }
                }
                newProdimg.BannerId = id;
                if (flag == true)
                {
                    newProdimg.IsActive = true;
                }
                else
                {
                    newProdimg.IsActive = false;
                }
                newProdimg.IsDelete = false;
                newProdimg.IsUpdate = false;
                newProdimg.InsertDate = DateTime.Now;
                newProdimg.LMDDate = DateTime.Now;
                db.Entry(newProdimg).State = EntityState.Modified;
                //db.Banners.Add(newProdimg);
                db.SaveChanges();



            }
            catch (Exception)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json("Upload failed");
            }

            return Json("File uploaded successfully");
        }
        // GET: Banners/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Banner banner = db.Banners.Find(id);
            if (banner == null)
            {
                return HttpNotFound();
            }
            return View(banner);
        }

        // POST: Banners/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Banner banner)
        {
            if (ModelState.IsValid)
            {
                db.Entry(banner).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(banner);
        }

        // GET: Banners/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Banner banner = db.Banners.Find(id);
            if (banner == null)
            {
                return HttpNotFound();
            }
            return View(banner);
        }

        // POST: Banners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Banner banner = db.Banners.Find(id);
            db.Banners.Remove(banner);
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
