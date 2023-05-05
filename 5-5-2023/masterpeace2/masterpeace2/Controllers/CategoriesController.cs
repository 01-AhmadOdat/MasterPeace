using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using masterpeace2;

namespace masterpeace2.Controllers
{
    public class CategoriesController : Controller
    {
        private masterpeaceEntities1 db = new masterpeaceEntities1();

        // GET: Categories
        public ActionResult Index()
        {
            return View(db.Categories.ToList());
        }

        // GET: Categories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // GET: Categories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "ID,Category_Name,Category_Image,Discription")] Category category, HttpPostedFileBase ImageFile)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        if (ImageFile != null && ImageFile.ContentLength > 0)
        //        {
        //            var fileName = Path.GetFileName(ImageFile.FileName);
        //            var path = Path.Combine(Server.MapPath("~/photos for masterpeace/"), fileName);
        //            ImageFile.SaveAs(path); 
        //            //userViewModel.ImagePath = fileName;
        //        }


        //        db.Categories.Add(category);
        //        db.Categories.Add(ImageFile);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(category);
        //}


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Category_Name,Category_Image,Discription")] Category category, HttpPostedFileBase ImageFile)
        {
            if (ModelState.IsValid)
            {
                if (ImageFile != null && ImageFile.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(ImageFile.FileName);
                    var path = Path.Combine(Server.MapPath("~/photos for masterpeace/"), fileName);
                    if (!Directory.Exists(Server.MapPath("~/photos for masterpeace/")))
                    {
                        Directory.CreateDirectory(Server.MapPath("~/photos for masterpeace/"));
                    }
                    ImageFile.SaveAs(path);
                    category.Category_Image = fileName; // Set the category image property to the uploaded file name
                }

                db.Categories.Add(category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(category);
        }
        









        // GET: Categories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            Session["img"] = category.Category_Image;
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Category_Name,Category_Image,Discription")] Category category, HttpPostedFileBase ImageFile)
        {
            if (ModelState.IsValid)
            {
                if (ImageFile != null && ImageFile.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(ImageFile.FileName);
                    var path = Path.Combine(Server.MapPath("~/photos for masterpeace/"), fileName);
                    if (!Directory.Exists(Server.MapPath("~/photos for masterpeace/")))
                    {
                        Directory.CreateDirectory(Server.MapPath("~/photos for masterpeace/"));
                    }
                    ImageFile.SaveAs(path);
                    category.Category_Image = fileName; // Set the category image property to the uploaded file name
                }else if (ImageFile == null)
                {
                    category.Category_Image = Session["img"].ToString();
                }
                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }

        // GET: Categories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            if(TempData["Mosab"] == "Thanks")
            {
                string sweetAlertMessage = "Are you sure you want to block this doctor?";
                string sweetAlertTitle = "Confirm Block";
                string sweetAlertIcon = "warning";
                string sweetAlertCancelButton = "Cancel";

                // Update the TempData and ViewBag variables
                TempData["swal_message"] = sweetAlertMessage;
                ViewBag.title = sweetAlertTitle;
                ViewBag.icon = sweetAlertIcon;
                ViewBag.cancelButton = sweetAlertCancelButton;

            }

            return View(category);
        }
        public ActionResult firstconfirm(int? id)
        {

            //TempData["swal_message"] = $"Dr-Your registration has been submitted and is waiting for approval. You will receive an email notification when your account has been accepted.";
            //ViewBag.title = "success";
            //ViewBag.icon = "success";
            //ViewBag.cancelButton = "Cancel";

            string sweetAlertMessage = "Are you sure you want to block this doctor?";
            string sweetAlertTitle = "Confirm Block";
            string sweetAlertIcon = "warning";
            string sweetAlertCancelButton = "Cancel";

            // Update the TempData and ViewBag variables
            TempData["swal_message"] = sweetAlertMessage;
            ViewBag.title = sweetAlertTitle;
            ViewBag.icon = sweetAlertIcon;
            ViewBag.cancelButton = sweetAlertCancelButton;
            TempData["Mosab"] = "Thanks";
            //return RedirectToAction("~/Categories/Delete");
            return RedirectToAction("Delete", "Categories", new { id = id });
        }

        // POST: Categories/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Category category = db.Categories.Find(id);
            db.Categories.Remove(category);


            //TempData["swal_message"] = $"Dr-Your registration has been submitted and is waiting for approval. You will receive an email notification when your account has been accepted.";
            //ViewBag.title = "success";
            //ViewBag.icon = "success";
            db.SaveChanges();
            //return RedirectToAction("~/Categories/Delete");
            return RedirectToAction("Index", "Categories");
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
