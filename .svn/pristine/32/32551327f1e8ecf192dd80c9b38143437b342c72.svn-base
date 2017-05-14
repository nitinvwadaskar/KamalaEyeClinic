using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kamala_Eye_Clinic.Models;
using System.Data;
using System.Data.SqlClient;

namespace Kamala_Eye_Clinic.Controllers
{
    [Sessionclass]
    public class SisterController : Controller
    {

        public ViewResult Registration()
        {
            SisterMaster obj = new SisterMaster();
            return View(obj);
        }
        public ViewResult Update(int sisterid)
        {
            SisterMaster obj = new SisterMaster();

            if (sisterid > 0)
            {
                obj.CMD = "Update";
                //obj = obj.GetSingleSister(sisterid);
                obj = obj.GetSingleSister(sisterid, Convert.ToInt32(Session["hospital"].ToString()));

            }
            return View("Registration", obj);
        }
        [HttpPost]
        public ActionResult Update(SisterMaster obj, string SubmitBtn)
        {
            Registration(obj, SubmitBtn);
            if (SubmitBtn == "Delete") return Redirect("~/Sister/SisterDetsils");
            return View("Registration", obj);
        }

        [HttpPost]
        public ActionResult Registration(SisterMaster obj, string SubmitBtn)
        {
            obj.CMD = SubmitBtn == "Save" ? "Save" : SubmitBtn == "Update" ? "Update" : "Delete";
            obj.hid = Convert.ToInt32(Session["hospital"].ToString());
            switch (obj.CMD)
            {
                case "Save":
                    string[] str = obj.PerformAction();
                    ViewBag.msg = str[1];
                    return View("Registration", obj);
                case "Update":
                    string[] str1 = obj.PerformAction();
                    ViewBag.msg = str1[2];
                    return View("Registration", obj);
                case "Delete":
                    string[] str2 = obj.PerformAction();
                    ViewBag.msg = str2[3];
                    return View("Registration", obj);
                default:
                    return View(obj);
            }

        }

        public ActionResult SisterDetails()
        {
            SisterMaster obj = new SisterMaster();
            DBConnection cn = new DBConnection();
            obj.CMD = "View";
            obj.hid = Convert.ToInt32(Session["hospital"].ToString());
            string xml = Common.ToXML(obj);
            DataSet dataset = cn.ExecuteProcedure("SP_SisterRegistration", xml);
            return View(dataset);
        }
        [HttpPost]
        public ActionResult SisterDetails(FormCollection form, string submitBtn)
        {
            switch (submitBtn)
            {
                case "Search":
                    SisterMaster obj = new Models.SisterMaster();
                    if (form["sid"] != "")
                    {
                        obj.sisterid = Convert.ToInt32(form["sid"]);
                    }
                    else
                    {
                        obj.fname = form["fname"];
                        obj.lname = form["lname"];
                    }

                    obj.CMD = "View1";
                    obj.hid = Convert.ToInt32(Session["hospital"].ToString());
                    string xml = Common.ToXML(obj);
                    DBConnection cn = new DBConnection();
                    DataSet dataset = cn.ExecuteProcedure("SP_SisterRegistration", xml);
                    return View(dataset);
                case "ViewAll":
                    return RedirectToAction("SisterDetails");
                default:
                    return View();
            }
        }
        public ActionResult SisterSelection()
        {
            return View();
        }
    }
}