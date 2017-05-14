using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kamala_Eye_Clinic.Models;
using System.Data;

namespace Kamala_Eye_Clinic.Controllers
{
    [Sessionclass]
    public class OTController : Controller
    {
        public ActionResult OTRegistration()
        {
            OTRegistration obj = new OTRegistration();
            obj.GetMultipleDoctor();
            obj.GetMultipleNurse();
            return View(obj);
        }

        public ViewResult OTRegistrationUpdate(int regid)
        {
            OTRegistration obj = new OTRegistration();

            if (regid > 0)
            {
                obj.CMD = "Update";
                obj = obj.GetSingleOT(regid, Convert.ToInt32(Session["hospital"].ToString()));
                obj.GetMultipleDoctor();
                obj.GetMultipleNurse();
            }
            return View("OTRegistration", obj);
        }


        [HttpPost]
        public ActionResult OTRegistrationUpdate(OTRegistration obj, string SubmitBtn)
        {
            OTRegistration(obj, SubmitBtn);
            if (SubmitBtn == "Delete") return Redirect("~/Patient/PatientOTDetails");
            return View("OTRegistration", obj);
        }

        [HttpPost]
        public ActionResult OTRegistration(OTRegistration obj, string SubmitBtn)
        {
            obj.CMD = SubmitBtn == "Save" ? "Save" : SubmitBtn == "Update" ? "Update" : "Delete";
            obj.hid = Convert.ToInt32(Session["hospital"].ToString());
            switch (obj.CMD)
            {
                case "Save":
                    obj.hid = Convert.ToInt32(Session["hospital"].ToString());
                    string[] str = obj.PerformAction();
                    ViewBag.msg = str[1];
                    string[] arr = new string[2] { "success", str[1] };
                    ViewBag.msgarr = arr;
                    obj.GetMultipleDoctor();
                    obj.GetMultipleNurse();
                    return View("OTRegistration", obj);
                case "Update":
                    string[] str1 = obj.PerformAction();
                    ViewBag.msg = str1[2];
                    string[] arrup = new string[2] { "success", str1[2] };
                    ViewBag.msgarr = arrup;
                    obj.GetMultipleDoctor();
                    obj.GetMultipleNurse();
                    return View("OTRegistration", obj);
                case "Delete":
                    string[] str2 = obj.PerformAction();
                    ViewBag.msg = str2[3];
                    string[] arrdel = new string[2] { "success", str2[3] };
                    ViewBag.msgarr = arrdel;
                    obj.GetMultipleDoctor();
                    obj.GetMultipleNurse();
                    return View("OTRegistration", obj);
                default:
                    return View(obj);
            }

        }

        public ActionResult OTDetails()
        {
            OTRegistration obj = new OTRegistration();
            DBConnection cn = new DBConnection();
            obj.CMD = "View";
            obj.hid = Convert.ToInt32(Session["hospital"].ToString());
            obj.patientid = Convert.ToInt32(Session["patientid"].ToString());
            string xml = Common.ToXML(obj);
            DataSet dataset = cn.ExecuteProcedure("sp_otregistration", xml);
            return View(dataset);
        }


        /**************************************   OT Billing  ***************************************************/

        public ActionResult Bill(int regid)

        {
            OTBilling obj = new OTBilling();
            Session["OTBillParticulars"] = null;                       
            ViewBag.BillParticulars = new List<BillParticulars>();
            obj.GetAdmitdetails(regid);
            obj.GetDischargedetails(regid);
            ViewBag.Total = 0;           
            return View(obj);
        }

        [HttpPost]
        public ActionResult Bill(FormCollection form, string SubmitBtn)
        {
            List<BillParticulars> par = new List<BillParticulars>();
            if (Session["OTBillParticulars"] != null)
                par = (List<BillParticulars>)Session["OTBillParticulars"];
            else
                par = new List<BillParticulars>();

            switch (SubmitBtn)
            {
                case "Add":
                    BillParticulars p = new BillParticulars();
                    p.particular = form["particular"];
                    p.charges = Convert.ToDecimal(form["charges"]);
                    p.total = p.charges;
                    if (par.Where(q => q.particular == p.particular).Count() >= 1)
                        ViewBag.msg = "Particular already added.";
                    else
                        par.Add(p);
                    Session["OTBillParticulars"] = par;
                    break;
                case "Delete":
                    int index = Convert.ToInt32(form["srno"]);
                    par.RemoveAt(index);
                    break;
                case "Clear":
                    par = new List<BillParticulars>();
                    Session["OTBillParticulars"] = par;
                    break;
            }
            ViewBag.Total = par.Select(q => q.total).Sum();
            ViewBag.BillParticulars = Session["OTBillParticulars"];
            return View();
        }

        [HttpPost]
        public ActionResult Checkout(FormCollection form, string SubmitBtn)
        {
            try
            {
                List<BillParticulars> par = new List<BillParticulars>();
                if (Session["OTBillParticulars"] != null)
                {
                    PatientBill pb = new PatientBill();
                    if (SubmitBtn == "Checkout") { pb.CMD = "Save"; } else { pb.CMD = SubmitBtn; }

                    pb.parlist = par = (List<BillParticulars>)Session["OTBillParticulars"];
                    pb.totalamount = par.Select(q => q.total).Sum();
                    pb.patientid = Convert.ToInt32(Session["patientid"] == null ? '0' : Session["patientid"]);
                    pb.hid = Convert.ToInt32(Session["hospital"] == null ? '0' : Session["hospital"]);
                    string billid = pb.PerformAction();
                    string str = PrintGenerator.GenerateBillPDF(Convert.ToInt32(billid.ToString()), pb.patientid, pb.hid, DateTime.Now);
                    TempData["Print"] = str;
                    return RedirectToAction("Print", "Shared");
                }
                else
                {
                    return RedirectToAction("Bill", "OT");
                }
            }
            catch (Exception)
            {
                return RedirectToAction("Bill", "OT");
            }
        }

        public ActionResult ViewBills(int billid)
        {
            if (billid > 0)
            {
                Session["billid"] = billid;
                string str = PrintGenerator.GenerateOTBillPDF();
                TempData["Print"] = str;
                return RedirectToAction("Print", "Shared");
            }
            else
            {
                return RedirectToAction("Bill", "OT");
            }

        }

        public ViewResult BillDetails()
        {
            OTBilling obj = new OTBilling();
            DBConnection cn = new DBConnection();
            obj.CMD = "View";
            obj.hid = Convert.ToInt32(Session["hospital"].ToString());
            obj.patientid = Convert.ToInt32(Session["patientid"].ToString());
            string xml = Common.ToXML(obj);
            DataSet dataset = cn.ExecuteProcedure("sp_otbill", xml);
            return View(dataset);
        }

    }

}