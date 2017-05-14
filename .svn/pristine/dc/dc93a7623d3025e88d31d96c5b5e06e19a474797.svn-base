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
    public class DoctorController : Controller
    {

        public ViewResult Registration()
        {
            DoctorMaster obj = new DoctorMaster();
            return View(obj);
        }
        public ViewResult Update(int doctorid)
        {
            DoctorMaster obj = new DoctorMaster();

            if (doctorid > 0)
            {
                obj.CMD = "Update";
                //obj = obj.GetSingleDoctor(doctorid);
                obj = obj.GetSingleDoctor(doctorid, Convert.ToInt32(Session["hospital"].ToString()));
            }
            return View("Registration", obj);
        }
        [HttpPost]
        public ActionResult Update(DoctorMaster obj, string SubmitBtn)
        {
            Registration(obj, SubmitBtn);
            if (SubmitBtn == "Delete") return Redirect("~/Doctor/DoctorDetails");
            return View("Registration", obj);
        }

        [HttpPost]
        public ActionResult Registration(DoctorMaster obj, string SubmitBtn)
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

        public ActionResult DoctorDetails()
        {
            DoctorMaster obj = new DoctorMaster();
            DBConnection cn = new DBConnection();
            obj.CMD = "View";
            obj.hid = Convert.ToInt32(Session["hospital"].ToString());
            string xml = Common.ToXML(obj);
            DataSet dataset = cn.ExecuteProcedure("SP_DoctorRegistration", xml);
            return View(dataset);
        }
        [HttpPost]
        public ActionResult DoctorDetails(FormCollection form, string submitBtn)
        {
            switch (submitBtn)
            {
                case "Search":
                    DoctorMaster obj = new Models.DoctorMaster();
                    if (form["did"] != "")
                    {
                        obj.doctorid = Convert.ToInt32(form["did"]);
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
                    DataSet dataset = cn.ExecuteProcedure("SP_DoctorRegistration", xml);
                    if (dataset.Tables[0].Rows[0][1].ToString() == "No Record Found")
                    {
                        ViewBag.Msg = "Record Not Found";
                    }
                    return View(dataset);
                case "ViewAll":
                    return RedirectToAction("DoctorDetails");
                default:
                    return View();
            }
        }

        public ActionResult DoctorSelection()
        {
            return View();
        }


    }
}