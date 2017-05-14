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
    public class BillingController : Controller
    {
        /****************************************** Patient Billing Details    **************************************/
        
        public ViewResult BillParticularMaster()
        {
            BillParticularMaster obj = new Models.BillParticularMaster();                       
            return View(obj);
        }

        [HttpPost]
        public ActionResult BillParticularMaster(BillParticularMaster obj, string SubmitBtn)
        {

            obj.CMD = SubmitBtn == "Save" ? "Save" : SubmitBtn == "Update" ? "Update" : "Delete";
            switch (obj.CMD)
            {
                case "Save":
                    obj.hid = Convert.ToInt32(Session["hospital"].ToString());
                    string[] str = obj.PerformAction();
                    ViewBag.msg = str[1];
                    return RedirectToAction("BillParticularDetails");
                case "Update":
                    string[] str1 = obj.PerformAction();
                    ViewBag.msg = str1[2];
                    return RedirectToAction("BillParticularDetails");
                case "Delete":
                    string[] str2 = obj.PerformAction();
                    ViewBag.msg = str2[3];
                    return RedirectToAction("BillParticularDetails");
                default:
                    return View(obj);
            }
        }

        public ViewResult BillParticularUpdate(int hid, int particularid)
        {
            BillParticularMaster obj = new BillParticularMaster();

            if (particularid > 0)
            {
                obj.CMD = "Update";
                obj.particularid = particularid;
                obj.hid = hid;
                obj = obj.GetSingleBillParticular(particularid);
            }
            return View("BillParticularMaster", obj);
        }
        [HttpPost]
        public ActionResult BillParticularUpdate(BillParticularMaster obj, string SubmitBtn)
        {
            BillParticularMaster(obj, SubmitBtn);
            if (SubmitBtn == "Delete") return Redirect("~/Billing/BillParticularDetails");
            return View("BillParticularMaster", obj);
        }

        public ViewResult BillParticularDetails(BillParticularMaster obj)
        {
            DBConnection cn = new DBConnection();
            obj.CMD = "View";
            obj.hid = Convert.ToInt32(Session["hospital"].ToString());
            string xml = Common.ToXML(obj);
            DataSet dataset = cn.ExecuteProcedure("SP_BillParticularMst", xml);
            return View(dataset);
        }


        /**********************************  Bill Details  *********************************/

       

    }
}