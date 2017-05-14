using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kamala_Eye_Clinic.Models
{
    public class StockLedger
    {
        public int hid { get; set; }
        public int transactionid { get; set; }
        public int itemid { get; set; }

        [Required]
        [Display(Name ="Item Name:")]
        public string itemname { get; set; }

        [Required]
        [Display(Name = "Unit:")]
        public string unit { get; set; }

        [Required]
        [Display(Name = "Quantity:")]
        public int qty { get; set; }

        [Required]
        [Display(Name = "Type:")]
        public string type { get; set; }

        [Required]
        [Display(Name = "Opening Quantity:")]
        public int openingqty { get; set; }

        [Required]
        [Display(Name = "Closing Quantity:")]
        public int closingqty { get; set; }

        public string createby { get; set; }
        public string updateby { get; set; }
        public string CMD { get; set; }

        public string[] PerformAction()
        {
            try
            {
                this.createby = this.updateby = HttpContext.Current.Session["UserName"].ToString();
                string xml = Common.ToXML(this);
                DBConnection con = new DBConnection();
                System.Data.DataSet ds = con.ExecuteProcedure("SP_StockLedger", xml);
                return Common.CallbackMsg(ds);
            }
            catch (Exception e)
            {
                return new string[] { "Failed", e.Message };
            }
        }

        public StockLedger GetSingleStockItem(int transactionid)
        {
            this.CMD = "View1";
            this.hid = Convert.ToInt32(HttpContext.Current.Session["hospital"]);
            this.transactionid = transactionid;
            string xml = Common.ToXML(this);
            DBConnection con = new DBConnection();
            DataSet ds = con.ExecuteProcedure("SP_StockLedger", xml);
            if (ds != null)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    this.itemid = Convert.ToInt32(ds.Tables[0].Rows[i]["itemid"]);
                    this.itemname = ds.Tables[0].Rows[i]["itemname"].ToString();
                    this.unit = ds.Tables[0].Rows[i]["unit"].ToString();
                    this.qty = Convert.ToInt32(ds.Tables[0].Rows[i]["qty"]);
                    this.type =ds.Tables[0].Rows[i]["ttype"].ToString();
                    this.openingqty = Convert.ToInt32(ds.Tables[0].Rows[i]["openingqty"]);
                    this.closingqty = Convert.ToInt32(ds.Tables[0].Rows[i]["closingqty"]);
                }
            }
            return this;
        }

        public List<SelectListItem> units = new List<SelectListItem>();

        public StockLedger getMultipleUnit()
        {
            UOM obj = new UOM();
            obj.CMD = "View";
            obj.hid = Convert.ToInt32(HttpContext.Current.Session["hospital"]);
            string xml = Common.ToXML(obj);
            DBConnection con = new DBConnection();
            DataSet ds = con.ExecuteProcedure("SP_UOM", xml);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                units.Add(new SelectListItem { Text = ds.Tables[0].Rows[i]["uom"].ToString(), Value = ds.Tables[0].Rows[i]["unitid"].ToString() });
            }
            return this;
        }

        public List<SelectListItem> items = new List<SelectListItem>();

        public StockLedger getMultipleItems()
        {
            ItemMaster obj = new ItemMaster();
            obj.CMD = "View";
            obj.hid = Convert.ToInt32(HttpContext.Current.Session["hospital"]);
            string xml = Common.ToXML(obj);
            DBConnection con = new DBConnection();
            DataSet ds = con.ExecuteProcedure("SP_Item", xml);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                items.Add(new SelectListItem { Text = ds.Tables[0].Rows[i]["itemname"].ToString(), Value = ds.Tables[0].Rows[i]["itemid"].ToString() });
            }
            return this;
        }

    }
}