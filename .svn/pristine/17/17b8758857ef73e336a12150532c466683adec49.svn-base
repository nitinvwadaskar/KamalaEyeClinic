using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;

namespace Kamala_Eye_Clinic.Models
{
    public class AdviceMaster
    {
        public int hid { get; set; }
        public int adviceid { get; set; }

        [Required(ErrorMessage = "Advice is Required")]
        [Display(Name = "Advice Name:")]
        public string advicename { get; set; }

        [Display(Name = "Description:")]
        public string advicedescription { get; set; }

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
                DataSet ds = con.ExecuteProcedure("SP_Advice", xml);
                return Common.CallbackMsg(ds);
            }
            catch (Exception e)
            {
                return new string[] { "Failed", e.Message };
            }
        }

        public AdviceMaster GetSingleAdvice(int adviceid)
        {
            this.CMD = "View1";
            this.adviceid = adviceid;
            string xml = Common.ToXML(this);
            DBConnection con = new DBConnection();
            DataSet ds = con.ExecuteProcedure("SP_Advice", xml);
            if (ds != null)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    this.advicename = ds.Tables[0].Rows[i]["advicename"].ToString();
                    this.advicedescription = ds.Tables[0].Rows[i]["advicedescription"].ToString();
                }
            }
            return this;
        }

        public List<AdviceMaster> GetAllAdvice()
        {
            this.CMD = "View";
            this.hid = Convert.ToInt32(HttpContext.Current.Session["hospital"].ToString());
            string xml = Common.ToXML(this);
            DBConnection con = new DBConnection();
            DataSet ds = con.ExecuteProcedure("SP_Advice", xml);

            List<AdviceMaster> Advicelist = new List<AdviceMaster>();
            if (ds != null)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    AdviceMaster ch = new AdviceMaster();
                    ch.adviceid = Convert.ToInt32(ds.Tables[0].Rows[i]["adviceid"].ToString());
                    ch.advicename = ds.Tables[0].Rows[i]["advicename"].ToString();
                    Advicelist.Add(ch);
                }
            }
            return Advicelist;
        }


    }
}