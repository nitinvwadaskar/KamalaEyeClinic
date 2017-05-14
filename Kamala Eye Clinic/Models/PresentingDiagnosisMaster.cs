using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;

namespace Kamala_Eye_Clinic.Models
{
    public class PresentingDiagnosisMaster
    {
        public int hid { get; set; }
        public int diagnosisid { get; set; }

        [Required(ErrorMessage = "Diagnosis is Required")]
        [Display(Name = "Diagnosis Name:")]
        public string diagnosisname { get; set; }

        [Display(Name = "Description:")]
        public string diagnosisdescription { get; set; }

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
                DataSet ds = con.ExecuteProcedure("SP_Diagnosis", xml);
                return Common.CallbackMsg(ds);
            }
            catch (Exception e)
            {
                return new string[] { "Failed", e.Message };
            }
        }

        public PresentingDiagnosisMaster GetSingleDiagnosis(int diagnosisid)
        {
            this.CMD = "View1";
            this.diagnosisid = diagnosisid;
            string xml = Common.ToXML(this);
            DBConnection con = new DBConnection();
            DataSet ds = con.ExecuteProcedure("SP_Diagnosis", xml);
            if (ds != null)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    this.diagnosisname = ds.Tables[0].Rows[i]["diagnosisname"].ToString();
                    this.diagnosisdescription = ds.Tables[0].Rows[i]["diagnosisdescription"].ToString();
                }
            }
            return this;
        }

        public List<PresentingDiagnosisMaster> GetAllDiagnosis()
        {
            this.CMD = "View";
            this.hid = Convert.ToInt32(HttpContext.Current.Session["hospital"].ToString());
            string xml = Common.ToXML(this);
            DBConnection con = new DBConnection();
            DataSet ds = con.ExecuteProcedure("SP_Diagnosis", xml);

            List<PresentingDiagnosisMaster> PresentingDiagnosislist = new List<PresentingDiagnosisMaster>();
            if (ds != null)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    PresentingDiagnosisMaster dm = new PresentingDiagnosisMaster();
                    dm.diagnosisid = Convert.ToInt32(ds.Tables[0].Rows[i]["diagnosisid"].ToString());
                    dm.diagnosisname = ds.Tables[0].Rows[i]["diagnosisname"].ToString();
                    PresentingDiagnosislist.Add(dm);
                }
            }
            return PresentingDiagnosislist;
        }

    }
}