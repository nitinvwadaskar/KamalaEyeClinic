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
    public class StockManagementController : Controller
    {
        public ActionResult StockLedger()
        {
            StockLedger obj = new Models.StockLedger();
            obj.getMultipleItems();
            obj.getMultipleUnit();
            return View(obj);
        }

        public ViewResult StockLedgerUpdate(int treatmentid)
        {
            StockLedger obj = new Models.StockLedger();

            if (treatmentid > 0)
            {
                obj.CMD = "Update";
                obj = obj.GetSingleStockItem(treatmentid);
                obj.getMultipleItems();
                obj.getMultipleUnit();
            }
            return View("StockLedger", obj);
        }

        [HttpPost]
        public ActionResult StockLedgerUpdate(StockLedger obj, string SubmitBtn)
        {
            StockLedger(obj, SubmitBtn);
            if (SubmitBtn == "Delete")
                return Redirect("~/StockManagement/ViewStockLeader");
            return View("StockLeader", obj);
        }

        [HttpPost]
        public ActionResult StockLedger(StockLedger obj, string SubmitBtn)
        {
            obj.CMD = SubmitBtn == "Save" ? "Save" : SubmitBtn == "Update" ? "Update" : "Delete";
            obj.hid = Convert.ToInt32(Session["hospital"].ToString());
            obj.getMultipleItems();
            obj.getMultipleUnit();
            switch (obj.CMD)
            {
                case "Save":
                    obj.hid = Convert.ToInt32(Session["hospital"].ToString());
                    string[] str = obj.PerformAction();
                    ViewBag.msg = str[1];                   
                    return View("StockLedger", obj);
                case "Update":
                    string[] str1 = obj.PerformAction();
                    ViewBag.msg = str1[2];                  
                    return View("StockLedger", obj);
                case "Delete":
                    string[] str2 = obj.PerformAction();
                    ViewBag.msg = str2[3];                 
                    return View("StockLedger", obj);
                default:
                    return View(obj);
            }
        }

        public ActionResult ViewStockLeader()
        {
            StockLedger obj = new Models.StockLedger();
            DBConnection cn = new DBConnection();
            obj.CMD = "View";
            obj.hid = Convert.ToInt32(Session["hospital"].ToString());
            string xml = Common.ToXML(obj);
            DataSet dataset = cn.ExecuteProcedure("SP_StockLedger", xml);
            return View(dataset);
        }


        /***************************************** Item Master *****************************************/

        public ActionResult Item()
        {
            ItemMaster obj = new Models.ItemMaster();
            obj.getMultipleUnit();
            return View(obj);
        }

        public ViewResult ItemUpdate(int itemid)
        {
            ItemMaster obj = new Models.ItemMaster();

            if (itemid > 0)
            {
                obj.CMD = "Update";
                obj = obj.GetSingleItem(itemid);
                obj.getMultipleUnit();
            }
            return View("Item", obj);
        }

        [HttpPost]
        public ActionResult ItemUpdate(ItemMaster obj, string SubmitBtn)
        {
            Item(obj, SubmitBtn);
            if (SubmitBtn == "Delete")
            return Redirect("~/StockManagement/ViewItem");
            return View("Item", obj);
        }

        [HttpPost]
        public ActionResult Item(ItemMaster obj, string SubmitBtn)
        {
            obj.CMD = SubmitBtn == "Save" ? "Save" : SubmitBtn == "Update" ? "Update" : "Delete";
            obj.hid = Convert.ToInt32(Session["hospital"].ToString());
            switch (obj.CMD)
            {
                case "Save":
                    obj.hid = Convert.ToInt32(Session["hospital"].ToString());
                    string[] str = obj.PerformAction();
                    ViewBag.msg = str[1];
                    obj.getMultipleUnit();
                    return View("Item", obj);
                case "Update":
                    string[] str1 = obj.PerformAction();
                    ViewBag.msg = str1[2];
                    obj.getMultipleUnit();
                    return View("Item", obj);
                case "Delete":
                    string[] str2 = obj.PerformAction();
                    ViewBag.msg = str2[3];
                    obj.getMultipleUnit();
                    return View("Item", obj);
                default:
                    return View(obj);
            }
        }

        public ActionResult ViewItem()
        {
            ItemMaster obj = new Models.ItemMaster();
            DBConnection cn = new DBConnection();
            obj.CMD = "View";
            obj.hid = Convert.ToInt32(Session["hospital"].ToString());
            string xml = Common.ToXML(obj);
            DataSet dataset = cn.ExecuteProcedure("SP_Item", xml);
            return View(dataset);
        }



        /***********************************  UOM ***********************************************/
        public ActionResult UOM()
        {
            UOM obj = new Models.UOM();           
            return View(obj);
        }

        public ViewResult Update(int unitid)
        {
            UOM obj = new Models.UOM();

            if (unitid > 0)
            {
                obj.CMD = "Update";
                obj = obj.GetSingleUnit(unitid);

            }
            return View("UOM", obj);
        }
        [HttpPost]
        public ActionResult Update(UOM obj, string SubmitBtn)
        {
            UOM(obj, SubmitBtn);
            if (SubmitBtn == "Delete") return Redirect("~/StockManagement/UOMView");
            return View("UOM", obj);
        }

        [HttpPost]
        public ActionResult UOM(UOM obj, string SubmitBtn)
        {
            obj.CMD = SubmitBtn == "Save" ? "Save" : SubmitBtn == "Update" ? "Update" : "Delete";
            obj.hid = Convert.ToInt32(Session["hospital"].ToString());
            switch (obj.CMD)
            {
                case "Save":
                    obj.hid = Convert.ToInt32(Session["hospital"].ToString());
                    string[] str = obj.PerformAction();
                    ViewBag.msg = str[1];
                    return View("UOM", obj);
                case "Update":
                    string[] str1 = obj.PerformAction();
                    ViewBag.msg = str1[2];
                    return View("UOM", obj);
                case "Delete":
                    string[] str2 = obj.PerformAction();
                    ViewBag.msg = str2[3];
                    return View("UOM", obj);
                default:
                    return View(obj);
            }

        }

        public ActionResult ViewUOM()
        {
            UOM obj = new Models.UOM();
            DBConnection cn = new DBConnection();
            obj.CMD = "View";
            obj.hid = Convert.ToInt32(Session["hospital"].ToString());
            string xml = Common.ToXML(obj);
            DataSet dataset = cn.ExecuteProcedure("SP_UOM", xml);
            return View(dataset);
        }

    }
}