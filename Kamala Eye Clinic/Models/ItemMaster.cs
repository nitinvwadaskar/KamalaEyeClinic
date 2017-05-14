using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kamala_Eye_Clinic.Models
{
    public class ItemMaster
    {
        public int itemid { get; set; }
        public int hid { get; set; }

        [Required]
        [Display(Name = "Item Name:")]
        public string itemname { get; set; }

        [Required]
        [Display(Name = "Unit:")]
        public string unit { get; set; }

        [Required]
        [Display(Name = "Rate:")]
        public decimal rate { get; set; }

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
                System.Data.DataSet ds = con.ExecuteProcedure("SP_Item", xml);
                return Common.CallbackMsg(ds);
            }
            catch (Exception e)
            {
                return new string[] { "Failed", e.Message };
            }
        }
        public ItemMaster GetSingleItem(int itemid)
        {
            this.CMD = "View1";
            this.hid = Convert.ToInt32(HttpContext.Current.Session["hospital"]);
            this.itemid = itemid;
            string xml = Common.ToXML(this);
            DBConnection con = new DBConnection();
            DataSet ds = con.ExecuteProcedure("SP_Item", xml);
            if (ds != null)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    this.itemname = ds.Tables[0].Rows[i]["itemname"].ToString();
                    this.unit = ds.Tables[0].Rows[i]["unit"].ToString();
                    this.rate = Convert.ToDecimal(ds.Tables[0].Rows[i]["rate"]);
                }
            }
            return this;
        }

        public List<SelectListItem> units = new List<SelectListItem>();

        public ItemMaster getMultipleUnit()
        {
            UOM obj = new UOM();
            obj.CMD = "View";
            obj.hid = Convert.ToInt32(HttpContext.Current.Session["hospital"]);
            string xml = Common.ToXML(obj);
            DBConnection con = new DBConnection();
            DataSet ds = con.ExecuteProcedure("SP_UOM", xml);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                units.Add(new SelectListItem { Text = ds.Tables[0].Rows[i]["uom"].ToString(), Value = ds.Tables[0].Rows[i]["unitid"].ToString()});
            }
            return this;
        }

    }
}