using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;

namespace Kamala_Eye_Clinic.Models
{
    public class ConsentMaster
    {
        public int hid { get; set; }
        public int consentid { get; set; }
        public int patientid { get; set; }


        [Required(ErrorMessage = "Visitor Name is Required")]
        [Display(Name = "Visitor Name:")]
        public string visitorname { get; set; }

        [Required(ErrorMessage = "Witness Name is Required")]
        [Display(Name = "Witness Name:")]
        public string witnessname { get; set; }

        [Required(ErrorMessage = "Place is Required")]
        [Display(Name = "Place:")]
        public string place { get; set; }

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
                DataSet ds = con.ExecuteProcedure("SP_Consent", xml);
                return Common.CallbackMsg(ds);
            }
            catch (Exception e)
            {
                return new string[] { "Failed", e.Message };
            }
        }
        public ConsentMaster GetSingleConsent(int consentid)
        {
            this.CMD = "View1";
            this.hid = Convert.ToInt32(HttpContext.Current.Session["hospital"].ToString());
            this.consentid = consentid;
            this.patientid = Convert.ToInt32(HttpContext.Current.Session["patientid"].ToString());
            string xml = Common.ToXML(this);
            DBConnection con = new DBConnection();
            DataSet ds = con.ExecuteProcedure("SP_Consent", xml);
            if (ds != null)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {

                    this.visitorname = ds.Tables[0].Rows[i]["visitorname"].ToString();
                    this.witnessname = ds.Tables[0].Rows[i]["witnessname"].ToString();
                    this.place = ds.Tables[0].Rows[i]["place"].ToString();
                }
            }
            return this;
        }

    }
}