using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;

namespace Kamala_Eye_Clinic.Models
{
    public class CheckupMaster
    {
        public int hid { get; set; }
        public int checkupid { get; set; }

        [Required(ErrorMessage = "Checkup is Required")]
        [Display(Name = "Checkup Name:")]
        public string checkupname { get; set; }

        [Display(Name = "Description:")]
        public string checkupdescription { get; set; }

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
                DataSet ds = con.ExecuteProcedure("SP_Checkup", xml);
                return Common.CallbackMsg(ds);
            }
            catch (Exception e)
            {
                return new string[] { "Failed", e.Message };
            }
        }

        public CheckupMaster GetSingleCheckup(int checkupid)
        {
            this.CMD = "View1";
            this.checkupid = checkupid;
            string xml = Common.ToXML(this);
            DBConnection con = new DBConnection();
            DataSet ds = con.ExecuteProcedure("SP_Checkup", xml);
            if (ds != null)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    this.checkupname = ds.Tables[0].Rows[i]["checkupname"].ToString();
                    this.checkupdescription = ds.Tables[0].Rows[i]["checkupdescription"].ToString();
                }
            }
            return this;
        }

        public List<CheckupMaster> GetAllCheckup()
        {
            this.CMD = "View";
            this.hid = Convert.ToInt32(HttpContext.Current.Session["hospital"].ToString());
            string xml = Common.ToXML(this);
            DBConnection con = new DBConnection();
            DataSet ds = con.ExecuteProcedure("SP_Checkup", xml);

            List<CheckupMaster> Checkuplist = new List<CheckupMaster>();
            if (ds != null)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    CheckupMaster ch = new CheckupMaster();
                    ch.checkupid = Convert.ToInt32(ds.Tables[0].Rows[i]["checkupid"].ToString());
                    ch.checkupname = ds.Tables[0].Rows[i]["checkupname"].ToString();
                    Checkuplist.Add(ch);
                }
            }
            return Checkuplist;
        }


    }
}