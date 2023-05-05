using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using masterpeace2;
using Microsoft.AspNet.Identity;

namespace masterpeace2.Controllers
{
    public class ResturantsController : Controller
    {
        private masterpeaceEntities1 db = new masterpeaceEntities1();

        // GET: Resturants
        public ActionResult Index()
        {
            var resturants = db.Resturants.Include(r => r.AspNetUser);
            return View(resturants.ToList());
        }

        // GET: Resturants/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Resturant resturant = db.Resturants.Find(id);
            if (resturant == null)
            {
                return HttpNotFound();
            }
            return View(resturant);
        }

        // GET: Resturants/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email");
            return View();
        }

        // POST: Resturants/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,UserId,Name,Address,PhoneNumber,Image")] Resturant resturant)
        {
            if (ModelState.IsValid)
            {
                db.Resturants.Add(resturant);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email", resturant.UserId);
            return View(resturant);
        }

        // GET: Resturants/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Resturant resturant = db.Resturants.Find(id);
             Session["img"] = resturant.Image;
            if (resturant == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email", resturant.UserId);
            return View(resturant);
        }

        // POST: Resturants/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,UserId,Name,Address,PhoneNumber,Image")] Resturant resturant, HttpPostedFileBase ImageFile1)
        {
            if (ModelState.IsValid)
            {
                if (ImageFile1 != null && ImageFile1.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(ImageFile1.FileName);
                    var path = Path.Combine(Server.MapPath("~/photos for masterpeace/"), fileName);
                    if (!Directory.Exists(Server.MapPath("~/photos for masterpeace/")))
                    {
                        Directory.CreateDirectory(Server.MapPath("~/photos for masterpeace/"));
                    }
                    ImageFile1.SaveAs(path);
                    resturant.Image = fileName; // Set the category image property to the uploaded file name
                }
                else if (ImageFile1 == null)
                {
                    resturant.Image = Session["img"].ToString();
                }

                db.Entry(resturant).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email", resturant.UserId);
            return View(resturant);
        }

        // GET: Resturants/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Resturant resturant = db.Resturants.Find(id);
            if (resturant == null)
            {
                return HttpNotFound();
            }
            return View(resturant);
        }

        // POST: Resturants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Resturant resturant = db.Resturants.Find(id);
            db.Resturants.Remove(resturant);
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

        //public ActionResult resInfo(string x)
        //{
        //    //var resid = User.Identity.GetUserId(); 
        //    //var resturant = db.Resturants.Where(r => r.UserId== resid);
        //    return View(x);
        //}
        public ActionResult ResInfo()
        {
            var id = User.Identity.GetUserId();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //AspNetUser user = db.AspNetUsers.Find(id);
            Resturant rest = db.Resturants.Where(o=>o.UserId== id).FirstOrDefault();
            if (rest == null)
            {
                return HttpNotFound();
            }
            return View(rest);
        }

        public ActionResult Restproduct()
        {
            var id = User.Identity.GetUserId();
            //var products = db.Products.Include(p => p.Category).Include(p => p.Resturant).Where(p=>p.r);
            var products = db.Products.Where(p=>p.Resturant.UserId== id);
            return View("Restproduct",products.ToList());
        }

        public ActionResult RestCreate()
        {
            ViewBag.Category_ID = new SelectList(db.Categories, "ID", "Category_Name");
            ViewBag.ResturantID = new SelectList(db.Resturants, "ID", "UserId");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResCreate([Bind(Include = "ID,Product_Name,Category_ID,Price,ResturantID,Image1,Image2,Image3,Image4")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Category_ID = new SelectList(db.Categories, "ID", "Category_Name", product.Category_ID);
            ViewBag.ResturantID = new SelectList(db.Resturants, "ID", "UserId", product.ResturantID);
            return View(product);
        }







        public ActionResult RestOrders()
        {
            var id = User.Identity.GetUserId();
            //var products = db.Products.Include(p => p.Category).Include(p => p.Resturant).Where(p=>p.r);
            var orders = db.Orders.Where(p=>p.Product.Resturant.UserId==id& p.IsAccepted==null);
            return View("RestOrders", orders.ToList());
        }

        public ActionResult AcceptOrders(int id)
        {
            var ID = User.Identity.GetUserId();
            //var products = db.Products.Include(p => p.Category).Include(p => p.Resturant).Where(p=>p.r);
            var allorders = db.Orders.Where(p => p.Product.Resturant.UserId == ID & p.IsAccepted == null);
            var orders = db.Orders.Find(id);
            orders.IsAccepted = true;
            db.SaveChanges();
            //var orders = db.Orders.Where(p => p.ID==id).Select(p => p.Status);
            //orders.status
            return View("RestOrders",allorders);
        }

        public ActionResult RejectOrders(int id)
        {
            var ID = User.Identity.GetUserId();
            //var products = db.Products.Include(p => p.Category).Include(p => p.Resturant).Where(p=>p.r);
            var allorders = db.Orders.Where(p => p.Product.Resturant.UserId == ID & p.IsAccepted == null);
            var orders = db.Orders.Find(id);
            orders.IsAccepted = false;
            db.SaveChanges();
            //var orders = db.Orders.Where(p => p.ID==id).Select(p => p.Status);
            //orders.status
            return View("RestOrders", allorders);
        }

        public ActionResult AcceptedOrders()
        {
            var id = User.Identity.GetUserId();
            //var products = db.Products.Include(p => p.Category).Include(p => p.Resturant).Where(p=>p.r);
            var orders = db.Orders.Where(p => p.Product.Resturant.UserId == id & p.IsAccepted == true);
            return View( orders.ToList());
        }


    }
}
