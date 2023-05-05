using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using masterpeace2;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace masterpeace2.Controllers
{
    public class AspNetUsersController : Controller
    {
        private masterpeaceEntities1 db = new masterpeaceEntities1();

        // GET: AspNetUsers
        public ActionResult Index()
        {
            

            return View(db.AspNetUsers.ToList());
        }

        // GET: AspNetUsers/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUser aspNetUser = db.AspNetUsers.Find(id);
            if (aspNetUser == null)
            {
                return HttpNotFound();
            }
            return View(aspNetUser);
        }

        // GET: AspNetUsers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AspNetUsers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName,FirstName,LastName")] AspNetUser aspNetUser)
        {
            if (ModelState.IsValid)
            {
                db.AspNetUsers.Add(aspNetUser);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(aspNetUser);
        }

        // GET: AspNetUsers/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUser aspNetUser = db.AspNetUsers.Find(id);
            if (aspNetUser == null)
            {
                return HttpNotFound();
            }
            return View(aspNetUser);
        }

        // POST: AspNetUsers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName,FirstName,LastName")] AspNetUser aspNetUser)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aspNetUser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(aspNetUser);
        }

        // GET: AspNetUsers/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUser aspNetUser = db.AspNetUsers.Find(id);
            if (aspNetUser == null)
            {
                return HttpNotFound();
            }
            return View(aspNetUser);
        }

        ////editing from user side>>>>>>>
        public ActionResult UserEdit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUser aspNetUser = db.AspNetUsers.Find(id);
            if (aspNetUser == null)
            {
                return HttpNotFound();
            }
            return View(aspNetUser);
        }

        // POST: AspNetUsers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UserEdit([Bind(Include = "Id,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName,FirstName,LastName")] AspNetUser aspNetUser)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aspNetUser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("UserProfile");
            }
            return View(aspNetUser);
        }                   

        //// End editing from user side>>>>>>>





        // POST: AspNetUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            AspNetUser aspNetUser = db.AspNetUsers.Find(id);
            db.AspNetUsers.Remove(aspNetUser);
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

        //public ActionResult UserProfile(string? id)
        //{
        //    //AspNetUser us = db.AspNetUsers.Find(id);
        //    var ID = User.Identity.GetUserId();
        //    var us = db.AspNetUsers.Where(a => a.Id == id);
        //    //var user = db.AspNetUsers;
        //    return View(us);
        //}
        //public ActionResult UserProfile(string id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    AspNetUser aspNetUser = db.AspNetUsers.Find(id);
        //    if (aspNetUser == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View("UserProfile", aspNetUser);
        //}
        public ActionResult UserProfile()
        {
            var id = User.Identity.GetUserId();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUser aspNetUser = db.AspNetUsers.Find(id);
            if (aspNetUser == null)
            {
                return HttpNotFound();
            }
            return View(aspNetUser);
        }

        public ActionResult Orders(int? id)
        {
            
            var ID = User.Identity.GetUserId();           
            var order = db.Orders.Where(a => a.UserId == ID).ToList();
            return View("Orders", order);
        }

        public ActionResult dashrest(int? id)
        {

            var ID = User.Identity.GetUserId();
            var resturant = db.Resturants;
            return View("dashrest", resturant);
        }

        //public ActionResult dashrest(int? id)
        //{

        //    var ID = User.Identity.GetUserId();
        //    var resturant = db.Resturants;
        //    return View("dashrest", resturant);
        //}

        public ActionResult dashuser(int? id)
        {

            //var ID = User.Identity.GetUserId();
            var Users = db.AspNetUsers;
            return View("dashuser", Users);
        }

        public ActionResult adminOrder(int? id)
        {

            //var ID = User.Identity.GetUserId();
            var order = db.Orders;
            return View(order);
        }

        public ActionResult AcceptedOrders()
        {
            var id = User.Identity.GetUserId();
            //var products = db.Products.Include(p => p.Category).Include(p => p.Resturant).Where(p=>p.r);
            var orders = db.Orders.Where(p => p.IsAccepted == true);
            return View(orders.ToList());
        }

        public ActionResult Searchacc(string search)
        {
            //var x2 = db.Orders.Where(x => (x.Product.Product_Name.Contains(search) || x.FirstName.Contains(search) || x.LastName.Contains(search) /*|| x.OrderDate.ToString().Contains(search)*/)).ToList();
            var x2 = db.Orders.Where(x => (x.Product.Product_Name.Contains(search) || x.FirstName.Contains(search) || x.Product.Resturant.Name.Contains(search))).ToList();
            if (x2.Count() == 0)
            {
                return View("NotFound", x2);
            }
            else
            {
                return View("adminOrder", x2);
            }
            
        }
        public ActionResult SearchUser(string search)
        {
            var u = db.AspNetUsers.Where(x => (x.FirstName.Contains(search) || x.LastName.Contains(search) || x.Email.Contains(search))).ToList();

            if (u.Count() == 0)
            {
                return View("UserNotFound", u);
            }
            else
            {
                return View("dashuser",u);
            }
        }

    }
}
