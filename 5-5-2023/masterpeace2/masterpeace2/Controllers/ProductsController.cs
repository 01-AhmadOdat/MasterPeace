using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;
using System.Web.Services.Description;
using System.Web.UI.WebControls;
using System.Web.WebPages;
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
        public ActionResult Create([Bind(Include = "ID,Product_Name,Category_ID,Price,Image1,Image2,Image3,Image4")] Product product, HttpPostedFileBase ImageFile1, HttpPostedFileBase ImageFile2, HttpPostedFileBase ImageFile3, HttpPostedFileBase ImageFile4)
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
                    product.Image1 = fileName; // Set the category image property to the uploaded file name
                }
                if (ImageFile2 != null && ImageFile2.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(ImageFile2.FileName);
                    var path = Path.Combine(Server.MapPath("~/photos for masterpeace/"), fileName);
                    if (!Directory.Exists(Server.MapPath("~/photos for masterpeace/")))
                    {
                        Directory.CreateDirectory(Server.MapPath("~/photos for masterpeace/"));
                    }
                    ImageFile2.SaveAs(path);
                    product.Image2 = fileName; // Set the category image property to the uploaded file name
                }
                if (ImageFile3 != null && ImageFile3.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(ImageFile3.FileName);
                    var path = Path.Combine(Server.MapPath("~/photos for masterpeace/"), fileName);
                    if (!Directory.Exists(Server.MapPath("~/photos for masterpeace/")))
                    {
                        Directory.CreateDirectory(Server.MapPath("~/photos for masterpeace/"));
                    }
                    ImageFile3.SaveAs(path);
                    product.Image3 = fileName; // Set the category image property to the uploaded file name
                }
                if (ImageFile4 != null && ImageFile4.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(ImageFile4.FileName);
                    var path = Path.Combine(Server.MapPath("~/photos for masterpeace/"), fileName);
                    if (!Directory.Exists(Server.MapPath("~/photos for masterpeace/")))
                    {
                        Directory.CreateDirectory(Server.MapPath("~/photos for masterpeace/"));
                    }
                    ImageFile4.SaveAs(path);
                    product.Image4 = fileName; // Set the category image property to the uploaded file name
                }

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
            Session["img1"] = product.Image1;
            Session["img2"] = product.Image2;
            Session["img3"] = product.Image3;
            Session["img4"] = product.Image4;
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Product_Name,Category_ID,Price,ResturantID,Image1,Image2,Image3,Image4")] Product product, HttpPostedFileBase ImageFile1, HttpPostedFileBase ImageFile2, HttpPostedFileBase ImageFile3, HttpPostedFileBase ImageFile4)
        {
            if (ModelState.IsValid)
            {
                if (ImageFile1 == null)
                {
                    product.Image1 = Session["img1"].ToString();
                }
                if (ImageFile2 == null)
                {
                    product.Image2 = Session["img2"].ToString();
                }
                if (ImageFile3 == null)
                {
                    product.Image3 = Session["img3"].ToString();
                }
                if (ImageFile4 == null)
                {
                    product.Image4 = Session["img4"].ToString();
                }

                if (ImageFile1 != null && ImageFile1.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(ImageFile1.FileName);
                    var path = Path.Combine(Server.MapPath("~/photos for masterpeace/"), fileName);
                    if (!Directory.Exists(Server.MapPath("~/photos for masterpeace/")))
                    {
                        Directory.CreateDirectory(Server.MapPath("~/photos for masterpeace/"));
                    }
                    ImageFile1.SaveAs(path);
                    product.Image1 = fileName; // Set the category image property to the uploaded file name
                }
                if (ImageFile2 != null && ImageFile2.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(ImageFile2.FileName);
                    var path = Path.Combine(Server.MapPath("~/photos for masterpeace/"), fileName);
                    if (!Directory.Exists(Server.MapPath("~/photos for masterpeace/")))
                    {
                        Directory.CreateDirectory(Server.MapPath("~/photos for masterpeace/"));
                    }
                    ImageFile1.SaveAs(path);
                    product.Image2 = fileName; // Set the category image property to the uploaded file name
                }
                if (ImageFile3 != null && ImageFile3.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(ImageFile3.FileName);
                    var path = Path.Combine(Server.MapPath("~/photos for masterpeace/"), fileName);
                    if (!Directory.Exists(Server.MapPath("~/photos for masterpeace/")))
                    {
                        Directory.CreateDirectory(Server.MapPath("~/photos for masterpeace/"));
                    }
                    ImageFile3.SaveAs(path);
                    product.Image3 = fileName; // Set the category image property to the uploaded file name
                }
                if (ImageFile4 != null && ImageFile4.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(ImageFile4.FileName);
                    var path = Path.Combine(Server.MapPath("~/photos for masterpeace/"), fileName);
                    if (!Directory.Exists(Server.MapPath("~/photos for masterpeace/")))
                    {
                        Directory.CreateDirectory(Server.MapPath("~/photos for masterpeace/"));
                    }
                    ImageFile4.SaveAs(path);
                    product.Image4 = fileName; // Set the category image property to the uploaded file name
                }

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
            Session["id"] = id;
            if (TempData["sweet"] == "sweet")
            {
                TempData["swal_message"] = $"Your registration has been submitted and is waiting for approval. You will receive an email notification when your account has been accepted.";
                ViewBag.title = "success";
                ViewBag.icon = "success";
            }
            if (TempData["swal_message"] == "Added")
            {
                TempData["swal_message"] = $"Your registration has been submitted and is waiting for approval. You will receive an email notification when your account has been accepted.";
                ViewBag.title = "success";
                ViewBag.icon = "success";
            }
;

            return View(product);
        }
        public ActionResult infominus(int id, string quantity, FormCollection form)
        {
            if (Session["countarrow"] != null && Convert.ToInt16(Session["countarrow"]) > 0)
            {
                int count = (int)(Session["countarrow"] ?? 0);
                count -= 1;
                Session["countarrow"] = count;
            }
            return RedirectToAction("TheDetails", new { id = id });
        }



        public ActionResult infoplus(int id, string quantity, FormCollection form)
        {

            int count = (int)(Session["countarrow"] ?? 0);
            count += 1;
            // Store the updated count value in session state
            Session["countarrow"] = count;

            return RedirectToAction("TheDetails", new { id = id });
        }


        [Authorize]
        public ActionResult AddCart(int id, [Bind(Include = "Cart_Id,Product_Id,User_Id,Qty,Total_Price")] Cart cart, string quantity)
        {
            var mainId = User.Identity.GetUserId();


            int quant = Convert.ToInt32(quantity);

            var totalPrice = db.Products.FirstOrDefault(c => c.ID == id).Price;
            cart.User_Id = mainId;
            cart.Qty = quant;
            cart.Product_Id = id;
            cart.Total_Price = totalPrice * quant;
            using (var db = new masterpeaceEntities1())
            {
                if (quant != 0)
                {
                    db.Carts.Add(cart);
                    TempData["swal_message"] = "Added";
                    ViewBag.title = "success";
                    ViewBag.icon = "success";

                    db.SaveChanges();

                }



            }

            Session["countarrow"] = 1;

            return RedirectToAction("TheDetails", new { id = id });
        }


        public ActionResult Addtocart(int productid)
        {
            var cart = new List<Cart>
    ();
            var product = db.Products.Find(productid);
            cart.Add(new Cart()
            {
                Product_Id = productid,
                Qty = 1
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
            var cart = db.Carts.Where(a => a.User_Id == ID).ToList();
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

        //public ActionResult Items()
        //{

        //    return View();

        //}
        public ActionResult RestItems(int? id)
        {
            var product = db.Products.Where(p => p.ResturantID == id).ToList();

            return View(product);
        }

        public ActionResult SingleItem()
        {
            var item = db.Products;

            return PartialView("_SingleItem", item);
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
            int count = db.Carts.Count(o => o.User_Id == ID);
            return Content(count.ToString());
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult review(int? Id, [Bind(Include = "ID,UserId,ProductId,Description,Average")] Review review, string description, string rating)
        {
            if (ModelState.IsValid)
            {
                //id = db.Products.fi
                //review.DateTime = Convert.ToDateTime(date);
                DateTime date = DateTime.Parse(Request.Form["date"]);

                review.DateTime = Convert.ToDateTime(date);
                string userid = User.Identity.GetUserId();
                review.Description = description.ToString();
                review.UserId = userid;
                review.ProductId = Id;
                review.Average = Convert.ToInt32(rating);
                //using (var db = new masterpeaceEntities1())
                //{
                db.Reviews.Add(review);
                TempData["swal_message"] = $"Your registration has been submitted and is waiting for approval. You will receive an email notification when your account has been accepted.";
                ViewBag.title = "success";
                ViewBag.icon = "success";
                db.SaveChanges();
                //}

                //db.Entry(review).State = EntityState.Modified;
                //db.SaveChanges();
                TempData["sweet"] = "sweet";

            }
            //return RedirectToAction("TheDetails", "Products", new { id = id });
            return RedirectToAction("TheDetails", "Products", new { id = Id });
            //return View("TheDetails", new { id = id });


        }


        //public ActionResult review(int id, [Bind(Include = "ID,UserId,ProductId,Description,Average")] Review review, string description, string rating)
        //{
        //    var mainId = User.Identity.GetUserId();
        //    review.Description = description;
        //    review.Average = Convert.ToInt32(rating);
        //    review.ProductId = id;

        //    //cart.User_Id = mainId;
        //    //cart.Qty = quant;
        //    //cart.Product_Id = id;
        //    //cart.Total_Price = totalPrice * quant;
        //    using (var db = new masterpeaceEntities1())
        //    {
        //        db.Reviews.Add(review);
        //        db.SaveChanges();
        //    }



        //    return RedirectToAction("TheDetails", new { id = id });
        //}
        //public ActionResult Updatecart(int? Id, [Bind(Include = "ID,Name,Email,Subject,Message1")] Review review, string description, string rating)
        //{
        //}
        public ActionResult Updatecart(FormCollection form)
        {
            var userid = User.Identity.GetUserId();
            var Usercart = db.Carts.Where(user => user.User_Id == userid);

            foreach (var key in form.AllKeys)
            {
                if (key.StartsWith("quantity-"))
                {
                    int cartItemId = int.Parse(key.Replace("quantity-", ""));
                    int quantity = int.Parse(form[key]);
                    var cartItem = db.Carts.Find(cartItemId);
                    if (cartItem != null)
                    {
                        cartItem.Qty = quantity;
                    }
                }
            }

            db.SaveChanges();
            return RedirectToAction("Cart");

        }

        public ActionResult Searchacc(string search)
        {
            var x1 = db.Products.Where(x => (x.Product_Name.Contains(search) || x.Description.Contains(search) || x.Resturant.Name.Contains(search))).ToList();

            if (x1.Count() == 0)
            {
                return View("NotFound", x1);
            }
            else
            {
                return View("Index", x1);
            }
        }
    }
}
