using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;

namespace Kamala_Eye_Clinic.Models
{
    public class PatientBill
    {
        //bill table
        public int hid { get; set; }
        public int billid { get; set; }
        public int patientid { get; set; }
        public string date { get; set; }
        public decimal totalamount { get; set; }
        public string createby { get; set; }
        public string updateby { get; set; }
        public string CMD { get; set; }

        //bill particulars
        public List<BillParticulars> parlist { get; set; }
        

        public string PerformAction()
        {
            try
            {
                this.createby=this.updateby= Convert.ToString(HttpContext.Current.Session["UserName"]);
                string xml = Common.ToXML(this);
                DBConnection con = new DBConnection();
                DataSet ds = con.ExecuteProcedure("Sp_PatientFinalBill", xml);
                return ds.Tables[0].Rows[0][2].ToString();
            }
            catch (Exception)
            {
                return "Failed";
            }
        }
    }
}