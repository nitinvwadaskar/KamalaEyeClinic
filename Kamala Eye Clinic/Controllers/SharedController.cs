using Kamala_Eye_Clinic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kamala_Eye_Clinic.Controllers
{
    [Sessionclass]
    public class SharedController : Controller
    {
        public ActionResult Print()
        {
            ViewBag.str = TempData["Print"];                                
            return View();
        }
    }
}