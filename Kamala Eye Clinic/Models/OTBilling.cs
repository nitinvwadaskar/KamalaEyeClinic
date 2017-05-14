using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kamala_Eye_Clinic.Models
{
    public class OTBilling
    {
        public int patientid { get; set; }
        public int hid { get; set; }
        public int billid { get; set; }
        public string billdate { get; set; }
        public string address { get; set; }
        public string PAN_No { get; set; }
        public string consultant { get; set; }
        public string TPA { get; set; }
        public string CCN { get; set; }
        public string admitdatetime { get; set; }
        public string disdatetime { get; set; }
        public string finaldiaagnosis { get; set; }
        public string no_of_days { get; set; }
        public string day_care { get; set; }
        public decimal totalamount { get; set; }
        public string paymentmode { get; set; }
        public string createby { get; set; }
        public string updateby { get; set; }

        public int otid { get; set; }
        public string otdate { get; set; }

        public string CMD { get; set; }



        public List<BillParticulars> parlist { get; set; }


        public string PerformAction()
        {
            try
            {
                this.createby = this.updateby = Convert.ToString(HttpContext.Current.Session["UserName"]);
                string xml = Common.ToXML(this);
                DBConnection con = new DBConnection();
                DataSet ds = con.ExecuteProcedure("Sp_OTBill", xml);
                return ds.Tables[0].Rows[0][2].ToString();
            }
            catch (Exception)
            {
                return "Failed";
            }
        }
       
        public OTBilling GetAdmitdetails(int regid)
        {
            OTRegistration obj = new OTRegistration();
            obj.CMD = "View";
            obj.regid = regid;
            obj.hid = Convert.ToInt32(HttpContext.Current.Session["hospital"].ToString());
            string xml = Common.ToXML(obj);
            DBConnection con = new DBConnection();
            DataSet ds = con.ExecuteProcedure("SP_OTRegistration", xml);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                this.otid = Convert.ToInt32(ds.Tables[0].Rows[i]["regid"]);
                this.admitdatetime = this.otdate = ds.Tables[0].Rows[i]["otdate"].ToString();
                this.consultant = ds.Tables[0].Rows[i]["operatingsurgon"].ToString();

            }
            return this;
        }

        public OTBilling GetDischargedetails(int regid)
        {
            Discharge obj = new Discharge();
            obj.CMD = "View";
            obj.otid = regid;
            obj.patientid = Convert.ToInt32(HttpContext.Current.Session["patientid"]);
            obj.hid = Convert.ToInt32(HttpContext.Current.Session["hospital"].ToString());
            string xml = Common.ToXML(obj);
            DBConnection con = new DBConnection();
            DataSet ds = con.ExecuteProcedure("SP_Discharge", xml);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                this.disdatetime = ds.Tables[0].Rows[i]["discharge_dt"].ToString();
            }
            return this;
        }
    }
}