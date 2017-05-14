﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kamala_Eye_Clinic.Models;
using System.Data;

namespace Kamala_Eye_Clinic.Controllers
{
    [Sessionclass]
    public class TreatmentController : Controller
    {
        // GET: Treatment
       
        public ViewResult PresentingComplentsMaster()
        {
            PresentingComplentsMaster obj = new Models.PresentingComplentsMaster();
            return View(obj);
        }

        [HttpPost]
        public ActionResult PresentingComplentsMaster(PresentingComplentsMaster obj, string SubmitBtn)
        {
            obj.CMD = SubmitBtn == "Save" ? "Save" : SubmitBtn == "Update" ? "Update" : "Delete";
            switch (obj.CMD)
            {
                case "Save":
                    obj.hid = Convert.ToInt32(Session["hospital"].ToString());
                    string[] str = obj.PerformAction();
                    ViewBag.msg = str[1];
                    return RedirectToAction("PresentingComplentsDetails");
                case "Update":
                    string[] str1 = obj.PerformAction();
                    ViewBag.msg = str1[2];
                    return RedirectToAction("PresentingComplentsDetails");
                case "Delete":
                    string[] str2 = obj.PerformAction();
                    ViewBag.msg = str2[3];
                    return RedirectToAction("PresentingComplentsDetails");
                default:
                    return View(obj);
            }
        }

        public ViewResult PresentingComplentsMasterUpdate(int hid, int complentid)
        {
            PresentingComplentsMaster obj = new PresentingComplentsMaster();

            if (complentid > 0)
            {
                obj.CMD = "Update";
                obj.complentid = complentid;
                obj.hid = hid;
                obj = obj.GetSingleComplent(complentid);
            }
            return View("PresentingComplentsMaster", obj);
        }

        [HttpPost]
        public ActionResult PresentingComplentsMasterUpdate(PresentingComplentsMaster obj, string SubmitBtn)
        {
            PresentingComplentsMaster(obj, SubmitBtn);
            if (SubmitBtn == "Delete") return Redirect("~/Treatment/PresentingComplentsMaster");
            return View("PresentingComplentsMaster", obj);
        }

        public ViewResult PresentingComplentsDetails(PresentingComplentsMaster obj, string cmd)
        {
            DBConnection cn = new DBConnection();
            obj.CMD = "View";
            obj.hid = Convert.ToInt32(Session["hospital"].ToString());
            string xml = Common.ToXML(obj);
            DataSet dataset = cn.ExecuteProcedure("SP_Complent", xml);
            return View(dataset);
        }


        /***********************           Treatment History And Duration            *****************/
        public ViewResult HistoryMaster()
        {
            HistoryMaster obj = new Models.HistoryMaster();
            return View(obj);
        }

        [HttpPost]
        public ActionResult HistoryMaster(HistoryMaster obj, string SubmitBtn)
        {
            obj.CMD = SubmitBtn == "Save" ? "Save" : SubmitBtn == "Update" ? "Update" : "Delete";
            switch (obj.CMD)
            {
                case "Save":
                    obj.hid = Convert.ToInt32(Session["hospital"].ToString());
                    string[] str = obj.PerformAction();
                    ViewBag.msg = str[1];
                    return RedirectToAction("HistoryDetails");
                case "Update":
                    string[] str1 = obj.PerformAction();
                    ViewBag.msg = str1[2];
                    return RedirectToAction("HistoryDetails");
                case "Delete":
                    string[] str2 = obj.PerformAction();
                    ViewBag.msg = str2[3];
                    return RedirectToAction("HistoryDetails");
                default:
                    return View(obj);
            }
        }

        public ViewResult HistoryMasterUpdate(int hid, int historyid)
        {
            HistoryMaster obj = new HistoryMaster();

            if (historyid > 0)
            {
                obj.CMD = "Update";
                obj.historyid = historyid;
                obj.hid = hid;
                obj = obj.GetSingleHistory(historyid);
            }
            return View("HistoryMaster", obj);
        }

        [HttpPost]
        public ActionResult HistoryMasterUpdate(HistoryMaster obj, string SubmitBtn)
        {
            HistoryMaster(obj, SubmitBtn);
            if (SubmitBtn == "Delete") return Redirect("~/Treatment/HistoryMaster");
            return View("HistoryMaster", obj);
        }

        public ViewResult HistoryDetails(HistoryMaster obj, string cmd)
        {
            DBConnection cn = new DBConnection();
            obj.CMD = "View";
            obj.hid = Convert.ToInt32(Session["hospital"].ToString());
            string xml = Common.ToXML(obj);
            DataSet dataset = cn.ExecuteProcedure("SP_History", xml);
            return View(dataset);
        }

        /***********************                     Occupation                      *****************/
        public ViewResult OccupationMaster()
        {
            OccupationMaster obj = new Models.OccupationMaster();
            return View(obj);
        }

        [HttpPost]
        public ActionResult OccupationMaster(OccupationMaster obj, string SubmitBtn)
        {
            obj.CMD = SubmitBtn == "Save" ? "Save" : SubmitBtn == "Update" ? "Update" : "Delete";
            switch (obj.CMD)
            {
                case "Save":
                    obj.hid = Convert.ToInt32(Session["hospital"].ToString());
                    string[] str = obj.PerformAction();
                    ViewBag.msg = str[1];
                    return RedirectToAction("OccupationDetails");
                case "Update":
                    string[] str1 = obj.PerformAction();
                    ViewBag.msg = str1[2];
                    return RedirectToAction("OccupationDetails");
                case "Delete":
                    string[] str2 = obj.PerformAction();
                    ViewBag.msg = str2[3];
                    return RedirectToAction("OccupationDetails");
                default:
                    return View(obj);
            }
        }

        public ViewResult OccupationMasterUpdate(int hid, int occupationid)
        {
            OccupationMaster obj = new OccupationMaster();

            if (occupationid > 0)
            {
                obj.CMD = "Update";
                obj.occupationid = occupationid;
                obj.hid = hid;
                obj = obj.GetSingleOccupation(occupationid);
            }
            return View("OccupationMaster", obj);
        }

        [HttpPost]
        public ActionResult OccupationMasterUpdate(OccupationMaster obj, string SubmitBtn)
        {
            OccupationMaster(obj, SubmitBtn);
            if (SubmitBtn == "Delete") return Redirect("~/Treatment/OccupationMaster");
            return View("OccupationMaster", obj);
        }

        public ViewResult OccupationDetails(OccupationMaster obj, string cmd)
        {
            DBConnection cn = new DBConnection();
            obj.CMD = "View";
            obj.hid = Convert.ToInt32(Session["hospital"].ToString());
            string xml = Common.ToXML(obj);
            DataSet dataset = cn.ExecuteProcedure("SP_Occupation", xml);
            return View(dataset);
        }

        /*****************************                 Presenting Diagnosis           *************************/

        public ViewResult PresentingDiagnosisMaster()
        {
            PresentingDiagnosisMaster obj = new Models.PresentingDiagnosisMaster();
            return View(obj);
        }

        [HttpPost]
        public ActionResult PresentingDiagnosisMaster(PresentingDiagnosisMaster obj, string SubmitBtn)
        {
            obj.CMD = SubmitBtn == "Save" ? "Save" : SubmitBtn == "Update" ? "Update" : "Delete";
            switch (obj.CMD)
            {
                case "Save":
                    obj.hid = Convert.ToInt32(Session["hospital"].ToString());
                    string[] str = obj.PerformAction();
                    ViewBag.msg = str[1];
                    return RedirectToAction("PresentingDiagnosisDetails");
                case "Update":
                    string[] str1 = obj.PerformAction();
                    ViewBag.msg = str1[2];
                    return RedirectToAction("PresentingDiagnosisDetails");
                case "Delete":
                    string[] str2 = obj.PerformAction();
                    ViewBag.msg = str2[3];
                    return RedirectToAction("PresentingDiagnosisDetails");
                default:
                    return View(obj);
            }
        }

        public ViewResult PresentingDiagnosisMasterUpdate(int hid, int diagnosisid)
        {
            PresentingDiagnosisMaster obj = new PresentingDiagnosisMaster();

            if (diagnosisid > 0)
            {
                obj.CMD = "Update";
                obj.diagnosisid = diagnosisid;
                obj.hid = hid;
                obj = obj.GetSingleDiagnosis(diagnosisid);
            }
            return View("PresentingDiagnosisMaster", obj);
        }

        [HttpPost]
        public ActionResult PresentingDiagnosisMasterUpdate(PresentingDiagnosisMaster obj, string SubmitBtn)
        {
            PresentingDiagnosisMaster(obj, SubmitBtn);
            if (SubmitBtn == "Delete") return Redirect("~/Treatment/PresentingDiagnosisMaster");
            return View("PresentingDiagnosisMaster", obj);
        }

        public ViewResult PresentingDiagnosisDetails(PresentingDiagnosisMaster obj, string cmd)
        {
            DBConnection cn = new DBConnection();
            obj.CMD = "View";
            obj.hid = Convert.ToInt32(Session["hospital"].ToString());
            string xml = Common.ToXML(obj);
            DataSet dataset = cn.ExecuteProcedure("SP_Diagnosis", xml);
            return View(dataset);
        }

        /***************************************    Check-up **********************************/
        public ViewResult CheckupMaster()
        {
            CheckupMaster obj = new Models.CheckupMaster();
            return View(obj);
        }

        [HttpPost]
        public ActionResult CheckupMaster(CheckupMaster obj, string SubmitBtn)
        {
            obj.CMD = SubmitBtn == "Save" ? "Save" : SubmitBtn == "Update" ? "Update" : "Delete";
            switch (obj.CMD)
            {
                case "Save":
                    obj.hid = Convert.ToInt32(Session["hospital"].ToString());
                    string[] str = obj.PerformAction();
                    ViewBag.msg = str[1];
                    return RedirectToAction("CheckupDetails");
                case "Update":
                    string[] str1 = obj.PerformAction();
                    ViewBag.msg = str1[2];
                    return RedirectToAction("CheckupDetails");
                case "Delete":
                    string[] str2 = obj.PerformAction();
                    ViewBag.msg = str2[3];
                    return RedirectToAction("CheckupDetails");
                default:
                    return View(obj);
            }
        }

        public ViewResult CheckupMasterUpdate(int hid, int checkupid)
        {
            CheckupMaster obj = new CheckupMaster();

            if (checkupid > 0)
            {
                obj.CMD = "Update";
                obj.checkupid = checkupid;
                obj.hid = hid;
                obj = obj.GetSingleCheckup(checkupid);
            }
            return View("CheckupMaster", obj);
        }

        [HttpPost]
        public ActionResult CheckupMasterUpdate(CheckupMaster obj, string SubmitBtn)
        {
            CheckupMaster(obj, SubmitBtn);
            if (SubmitBtn == "Delete") return Redirect("~/Treatment/CheckupMaster");
            return View("CheckupMaster", obj);
        }

        public ViewResult CheckupDetails(CheckupMaster obj, string cmd)
        {
            DBConnection cn = new DBConnection();
            obj.CMD = "View";
            obj.hid = Convert.ToInt32(Session["hospital"].ToString());
            string xml = Common.ToXML(obj);
            DataSet dataset = cn.ExecuteProcedure("SP_Checkup", xml);
            return View(dataset);
        }


        /************************ FUNDUS *********************/

        public ViewResult FundusMaster()
        {
            FundusMaster obj = new Models.FundusMaster();
            return View(obj);
        }

        [HttpPost]
        public ActionResult FundusMaster(FundusMaster obj, string SubmitBtn)
        {
            obj.CMD = SubmitBtn == "Save" ? "Save" : SubmitBtn == "Update" ? "Update" : "Delete";
            switch (obj.CMD)
            {
                case "Save":
                    obj.hid = Convert.ToInt32(Session["hospital"].ToString());
                    string[] str = obj.PerformAction();
                    ViewBag.msg = str[1];
                    return RedirectToAction("FundusDetails");
                case "Update":
                    string[] str1 = obj.PerformAction();
                    ViewBag.msg = str1[2];
                    return RedirectToAction("FundusDetails");
                case "Delete":
                    string[] str2 = obj.PerformAction();
                    ViewBag.msg = str2[3];
                    return RedirectToAction("FundusDetails");
                default:
                    return View(obj);
            }
        }

        public ViewResult FundusMasterUpdate(int hid, int fundusid)
        {
            FundusMaster obj = new FundusMaster();

            if (fundusid > 0)
            {
                obj.CMD = "Update";
                obj.fundusid = fundusid;
                obj.hid = hid;
                obj = obj.GetSingleFundus(fundusid);
            }
            return View("FundusMaster", obj);
        }

        [HttpPost]
        public ActionResult FundusMasterUpdate(FundusMaster obj, string SubmitBtn)
        {
            FundusMaster(obj, SubmitBtn);
            if (SubmitBtn == "Delete") return Redirect("~/Treatment/FundusMaster");
            return View("FundusMaster", obj);
        }

        public ViewResult FundusDetails(FundusMaster obj, string cmd)
        {
            DBConnection cn = new DBConnection();
            obj.CMD = "View";
            obj.hid = Convert.ToInt32(Session["hospital"].ToString());
            string xml = Common.ToXML(obj);
            DataSet dataset = cn.ExecuteProcedure("SP_Fundus", xml);
            return View(dataset);
        }

        /*********************** Advice Details ****************************/

        public ViewResult AdviceMaster()
        {
            AdviceMaster obj = new Models.AdviceMaster();
            return View(obj);
        }

        [HttpPost]
        public ActionResult AdviceMaster(AdviceMaster obj, string SubmitBtn)
        {
            obj.CMD = SubmitBtn == "Save" ? "Save" : SubmitBtn == "Update" ? "Update" : "Delete";
            switch (obj.CMD)
            {
                case "Save":
                    obj.hid = Convert.ToInt32(Session["hospital"].ToString());
                    string[] str = obj.PerformAction();
                    ViewBag.msg = str[1];
                    return RedirectToAction("AdviceDetails");
                case "Update":
                    string[] str1 = obj.PerformAction();
                    ViewBag.msg = str1[2];
                    return RedirectToAction("AdviceDetails");
                case "Delete":
                    string[] str2 = obj.PerformAction();
                    ViewBag.msg = str2[3];
                    return RedirectToAction("AdviceDetails");
                default:
                    return View(obj);
            }
        }

        public ViewResult AdviceMasterUpdate(int hid, int adviceid)
        {
            AdviceMaster obj = new AdviceMaster();

            if (adviceid > 0)
            {
                obj.CMD = "Update";
                obj.adviceid = adviceid;
                obj.hid = hid;
                obj = obj.GetSingleAdvice(adviceid);
            }
            return View("AdviceMaster", obj);
        }

        [HttpPost]
        public ActionResult AdviceMasterUpdate(AdviceMaster obj, string SubmitBtn)
        {
            AdviceMaster(obj, SubmitBtn);
            if (SubmitBtn == "Delete") return Redirect("~/Treatment/AdviceMaster");
            return View("AdviceMaster", obj);
        }

        public ViewResult AdviceDetails(AdviceMaster obj, string cmd)
        {
            DBConnection cn = new DBConnection();
            obj.CMD = "View";
            obj.hid = Convert.ToInt32(Session["hospital"].ToString());
            string xml = Common.ToXML(obj);
            DataSet dataset = cn.ExecuteProcedure("SP_Advice", xml);
            return View(dataset);
        }

    }
}