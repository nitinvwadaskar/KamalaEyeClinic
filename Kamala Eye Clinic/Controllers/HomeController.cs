using Kamala_Eye_Clinic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
namespace Kamala_Eye_Clinic.Controllers
{
    [Sessionclass]
    public class HomeController : Controller
    {
        public ActionResult Dashboard()
        {
            return View();
        }
        public ActionResult Admin_Control_Panel()
        {
            return View();
        }
        public ActionResult ErrorPage()
        {
            return View();
        }
    }
}