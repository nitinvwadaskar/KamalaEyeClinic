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
    public class MedicinesController : Controller
    {
        /****************************************** Medicine Category Details    **************************************/

        public ViewResult DrugCategoryMaster()
        {
            DrugCategoryMaster obj = new Models.DrugCategoryMaster();
            return View(obj);
        }

        [HttpPost]
        public ActionResult DrugCategoryMaster(DrugCategoryMaster obj, string SubmitBtn)
        {

            obj.CMD = SubmitBtn == "Save" ? "Save" : SubmitBtn == "Update" ? "Update" : "Delete";
            switch (obj.CMD)
            {
                case "Save":
                    obj.hid = Convert.ToInt32(Session["hospital"].ToString());
                    string[] str = obj.PerformAction();
                    ViewBag.msg = str[1];
                    return RedirectToAction("DrugCategoryDetails");
                case "Update":
                    string[] str1 = obj.PerformAction();
                    ViewBag.msg = str1[2];
                    return RedirectToAction("DrugCategoryDetails");
                case "Delete":
                    string[] str2 = obj.PerformAction();
                    ViewBag.msg = str2[3];
                    return RedirectToAction("DrugCategoryDetails");
                default:
                    return View(obj);
            }
        }

        public ViewResult DrugCategoryMasterUpdate(int hid, int categoryid)
        {
            DrugCategoryMaster obj = new DrugCategoryMaster();

            if (categoryid > 0)
            {
                obj.CMD = "Update";
                obj.categoryid = categoryid;
                obj.hid = hid;
                obj = obj.GetSingleDrugCategory(categoryid);
            }
            return View("DrugCategoryMaster", obj);
        }
        [HttpPost]
        public ActionResult DrugCategoryMasterUpdate(DrugCategoryMaster obj, string SubmitBtn)
        {
            DrugCategoryMaster(obj, SubmitBtn);
            if (SubmitBtn == "Delete") return Redirect("~/Medicines/DrugCategoryDetails");
            return View("DrugCategoryMaster", obj);
        }

        public ViewResult DrugCategoryDetails(DrugCategoryMaster obj, string cmd)
        {
            DBConnection cn = new DBConnection();
            obj.CMD = "View";
            obj.hid = Convert.ToInt32(Session["hospital"].ToString());
            string xml = Common.ToXML(obj);
            DataSet dataset = cn.ExecuteProcedure("SP_DrugCategory", xml);
            return View(dataset);
        }


        /****************************************** Medicines Details    **************************************/

        public ViewResult DrugMaster(DrugMaster obj)
        {
            return View(obj.GetMultipleCategory(Convert.ToInt32(Session["hospital"].ToString())));
        }

        [HttpPost]
        public ActionResult DrugMaster(DrugMaster obj, string SubmitBtn)
        {

            obj.CMD = SubmitBtn == "Save" ? "Save" : SubmitBtn == "Update" ? "Update" : "Delete";
            switch (obj.CMD)
            {
                case "Save":
                    obj.hid = Convert.ToInt32(Session["hospital"].ToString());
                    string[] str = obj.PerformAction();
                    ViewBag.msg = str[1];
                    return RedirectToAction("DrugDetails");
                case "Update":
                    string[] str1 = obj.PerformAction();
                    ViewBag.msg = str1[2];
                    return RedirectToAction("DrugDetails");
                case "Delete":
                    string[] str2 = obj.PerformAction();
                    ViewBag.msg = str2[3];
                    return RedirectToAction("DrugDetails");
                default:
                    return View(obj);
            }
        }

        public ViewResult DrugMasterUpdate(int hid, int drugid)
        {
            DrugMaster obj = new DrugMaster();

            if (drugid > 0)
            {
                obj.CMD = "Update";
                obj.drugid = drugid;
                obj.hid = hid;
                obj.GetMultipleCategory(hid);
                obj = obj.GetSingleDrug(drugid, hid);
            }
            return View("DrugMaster", obj);
        }
        [HttpPost]
        public ActionResult DrugMasterUpdate(DrugMaster obj, string SubmitBtn)
        {
            DrugMaster(obj, SubmitBtn);
            if (SubmitBtn == "Delete") return Redirect("~/Medicines/DrugDetails");
            return View("DrugMaster", obj);
        }

        public ViewResult DrugDetails(DrugMaster obj, string cmd)
        {
            DBConnection cn = new DBConnection();
            obj.CMD = "View";
            obj.hid = Convert.ToInt32(Session["hospital"].ToString());
            string xml = Common.ToXML(obj);
            DataSet dataset = cn.ExecuteProcedure("SP_Drug", xml);
            return View(dataset);
        }



    }
}