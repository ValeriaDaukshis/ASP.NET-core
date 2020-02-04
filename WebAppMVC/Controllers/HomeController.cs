using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppMVC.App_Start;

namespace WebAppMVC.Controllers
{
    public class HomeController : Controller
    {
        private IScreenService _service;

        public HomeController(IScreenService service)
        { 
            _service = service;
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            int[] result = new int[] {1, 2, 3, 4, 5};
            return View(_service.GetScreens());
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}