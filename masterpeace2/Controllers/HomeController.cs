using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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


            return View();
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

    }
}