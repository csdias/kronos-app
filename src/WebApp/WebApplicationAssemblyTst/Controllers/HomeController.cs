using ClassLibraryAssemblyTst;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace WebApplicationAssemblyTst.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ClassTst classTst = new ClassTst();

            var teste =  classTst.SerializeMe();

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}