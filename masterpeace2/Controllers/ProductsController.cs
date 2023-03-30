using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;
using masterpeace2;
using Microsoft.AspNet.Identity;

namespace masterpeace2.Controllers
{
    public class ProductsController : Controller
    {
        private masterpeaceEntities1 db = new masterpeaceEntities1();

        // GET: Products
        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.Category).Include(p => p.Resturant);
            return View(products.ToList());
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
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
        public ActionResult Create([Bind(Include = "ID,Product_Name,Category_ID,Price,ResturantID,Image1,Image2,Image3,Image4")] Product product)
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

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.Category_ID = new SelectList(db.Categories, "ID", "Category_Name", product.Category_ID);
            ViewBag.ResturantID = new SelectList(db.Resturants, "ID", "UserId", product.ResturantID);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Product_Name,Category_ID,Price,ResturantID,Image1,Image2,Image3,Image4")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Category_ID = new SelectList(db.Categories, "ID", "Category_Name", product.Category_ID);
            ViewBag.ResturantID = new SelectList(db.Resturants, "ID", "UserId", product.ResturantID);
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
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

        public ActionResult TheDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            string welcome = "welcome";
            Session["quantity"] = welcome;
            Session["id"]= id;
            return View(product);
        }
        public ActionResult infominus(int id, string quantity,FormCollection form)
        {
            if (Session["countarrow"]!=null&& Convert.ToInt16(Session["countarrow"])>0)
            {
                int count = (int)(Session["countarrow"] ?? 0);
                count -= 1;
                Session["countarrow"] = count;
            }
                return RedirectToAction("TheDetails", new { id = id });
        }



          public ActionResult infoplus(int id, string quantity,FormCollection form)
        {

            int count = (int)(Session["countarrow"] ?? 0);
                count += 1;
                // Store the updated count value in session state
                Session["countarrow"] = count;

                return RedirectToAction("TheDetails", new { id = id });
        }


        [Authorize]
        public ActionResult AddCart(int id, [Bind(Include = "Cart_Id,Product_Id,User_Id,Qty,Total_Price")] Cart cart,string quantity)
        {
            var mainId =User.Identity.GetUserId();


            int quant =Convert.ToInt32(quantity);

            var totalPrice = db.Products.FirstOrDefault(c => c.ID == id).Price;
            cart.User_Id = mainId;
            cart.Qty = quant;
            cart.Product_Id = id;
            cart.Total_Price = totalPrice * quant;
                using (var db = new masterpeaceEntities1())
                {
                    db.Carts.Add(cart); 
                    db.SaveChanges();
                }

            Session["countarrow"]=1;
   
            return RedirectToAction("TheDetails", new { id = id});
        }


        public ActionResult Addtocart(int productid)
        {
            var cart = new List<Cart>();
            var product = db.Products.Find(productid);
            cart.Add(new Cart()
            {
                Product_Id = productid,
                Qty=1
            });
            Session["cart"] = cart;
            db.SaveChanges();
            return RedirectToAction("Index");
            //    db.Carts.Add(product);


            //Product product = db.Products.Find(id);
            //if (product == null)
            //{
            //    return HttpNotFound();
            //}
            //return View(product);
        }
        [Authorize]
        public ActionResult Cart(int? id)
        {
            var ID = User.Identity.GetUserId();
            var cart = db.Carts.Where(a=>a.User_Id==ID).ToList();
            return View(cart);


        }

        // GET: Products/cart/Delete
        //public ActionResult DeleteCart(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Product product = db.Products.Find(id);
        //    if (product == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(product);
        //}

        // POST: Products/cart/Delete
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult Deletecart(int id)
        //{
        //    Cart cart = db.Carts.Find(id);
        //    Product product = db.Products.Find(id);
        //    db.Products.Remove(product);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}
        public ActionResult Items()
        {

            return View();

        }

        public ActionResult SingleItem()
        {
            var item = db.Products;

            return PartialView("_SingleItem",item);
            //var cat = db.Products;

            //return PartialView("_Cards", cat);
        }

        public ActionResult Resturant()
        {
            var resturant = db.Resturants;

            return PartialView("_ResturantSlider", resturant);
            //var cat = db.Products;

            //return PartialView("_Cards", cat);
        }

        // GET: Products/Cart/Delete product
        //public ActionResult DeleteCart(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Product product = db.Products.Find(id);
        //    if (product == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(product);
        //}

        // POST: Products/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteCartConfirmed(int id)
        {
            Cart cart = db.Carts.Find(id);
            
            db.Carts.Remove(cart);
            db.SaveChanges();
            return RedirectToAction("Cart");
        }
        //[ChildActionOnly]
        public ActionResult GetTableRowCount()
        {
            var ID = User.Identity.GetUserId();
            int count = db.Carts.Count(o=>o.User_Id==ID);
            return Content(count.ToString());
        }
    }
}
