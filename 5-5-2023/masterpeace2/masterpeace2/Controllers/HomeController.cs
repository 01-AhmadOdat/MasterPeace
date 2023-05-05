using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace masterpeace2.Controllers
{
    public class HomeController : Controller
    {
        masterpeaceEntities1 db = new masterpeaceEntities1();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Index1()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult About1()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();

        }
        public ActionResult Contact1()
        {
            ViewBag.Message = "Your contact page.";

            return View();

        }
        public ActionResult Register()
        {
            ViewBag.Message = "Your contact page.";

            return View();

        }
        public ActionResult Signin()
        {
            ViewBag.Message = "Your contact page.";

            return View();

        }
        public ActionResult Category()
        {
            ViewBag.Message = "Your Category page.";

            return View();

        }
        public ActionResult ProductPage()
        {
            

            return View();

        }
        //// GET: TaskEmployees/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    HomeController taskEmployee = db.Products.Find(id);
        //    if (taskEmployee == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(taskEmployee);
        //}
        public ActionResult ProductPage2()
        {

            
            return View();

        }
        public ActionResult CartPage()
        {


            return View();
        }
        public ActionResult Checkout()
        {
            var userid = User.Identity.GetUserId();
            AspNetUser aspNetUser = db.AspNetUsers.Find(userid);
            if (aspNetUser != null)
            {
                return View(aspNetUser);
            }

            return View();
        }
        public ActionResult CheckOutSave(string fname, string lname, string email, string city, string payment,string phone , int? Quantity, int? OrderId, int? ProductId)
        {
            var userid = User.Identity.GetUserId();
            var userCart = db.Carts.Where(x => x.User_Id == userid).ToList();
            //AspNetUser aspNetUser = db.AspNetUsers.Find(id);
            Order order = new Order();
            order.OrderDate = DateTime.Now;
            order.UserId = userid;
            order.Email = email;
            order.FirstName = fname;
            order.LastName = lname;
            order.Phone = phone;
            //var user = db.AspNetUsers.Where(x => x.Id == userid);
            //user.firs
            //order.AspNetUser.LastName = lname;
            order.City = city;
            order.PaymentMethod = payment;
            //order.AspNetUser.Email = email;
            //order.AspNetUser.PhoneNumber = Phone.ToString();

            db.Orders.Add(order);
            //db.SaveChanges();
            foreach (var item in userCart)
            {
                order.ProductId = item.Product_Id;
                order.Quantity = item.Qty;
                order.TotalPrice = item.Total_Price;
                db.Orders.Add(order);
                db.SaveChanges();
            }

            var cart = db.Carts.Where(x => x.User_Id == userid).ToList();
            foreach (var item in cart)
            {
                db.Carts.Remove(item);
            }
            db.SaveChanges();
            return RedirectToAction("Index1", "Home");



        }

            public ActionResult Cards()
        {
            var cat = db.Products;

            return PartialView("_Cards",cat);
        }

        public ActionResult cart()
        {
            var cat = db.Products;

            return PartialView("_Cards", cat);
        }

        public ActionResult CreateMessage()
        {
            var cat = db.Products;

            return PartialView("_Cards", cat);
        }
        [Authorize]
        public ActionResult Feedback(string feedback)
        {
            var id = User.Identity.GetUserId();
            feedback newFeedback = new feedback();
            newFeedback.userId = id;
            newFeedback.text = feedback.ToString();
            db.feedbacks.Add(newFeedback);
            db.SaveChanges();
            return View("Index1");
        }
    }
}