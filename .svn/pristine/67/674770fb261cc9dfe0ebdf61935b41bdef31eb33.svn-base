using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;

namespace Kamala_Eye_Clinic.Models
{
    public class UOM
    {  
        public int hid { get; set; }
        public int unitid { get; set; }

        [Required]
        [Display(Name = "Unit Name:")]
        public string unitname { get; set; }

        [Required]
        [Display(Name = "Unit of Measurement:")]
        public string uom { get; set; }

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
                DataSet ds = con.ExecuteProcedure("SP_UOM", xml);
                return Common.CallbackMsg(ds);
            }
            catch (Exception e)
            {
                return new string[] { "Failed", e.Message };
            }
        }
        public UOM GetSingleUnit(int unitid)
        {
            this.CMD = "View1";
            this.hid = Convert.ToInt32(HttpContext.Current.Session["hospital"]);
            this.unitid = unitid;
            string xml = Common.ToXML(this);
            DBConnection con = new DBConnection();
            DataSet ds = con.ExecuteProcedure("SP_UOM", xml);
            if (ds != null)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    this.unitname = ds.Tables[0].Rows[i]["unitname"].ToString();
                    this.uom = ds.Tables[0].Rows[i]["uom"].ToString();
                }
            }
            return this;
        }
    }
}