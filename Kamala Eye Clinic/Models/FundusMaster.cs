using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;

namespace Kamala_Eye_Clinic.Models
{
    public class FundusMaster
    {
        public int hid { get; set; }
        public int fundusid { get; set; }

        [Required(ErrorMessage = "Fundus is Required")]
        [Display(Name = "Fundus Name:")]
        public string fundusname { get; set; }

        [Display(Name = "Description:")]
        public string fundusdescription { get; set; }

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
                DataSet ds = con.ExecuteProcedure("SP_Fundus", xml);
                return Common.CallbackMsg(ds);
            }
            catch (Exception e)
            {
                return new string[] { "Failed", e.Message };
            }
        }

        public FundusMaster GetSingleFundus(int fundusid)
        {
            this.CMD = "View1";
            this.fundusid = fundusid;
            string xml = Common.ToXML(this);
            DBConnection con = new DBConnection();
            DataSet ds = con.ExecuteProcedure("SP_Fundus", xml);
            if (ds != null)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    this.fundusname = ds.Tables[0].Rows[i]["fundusname"].ToString();
                    this.fundusdescription = ds.Tables[0].Rows[i]["fundusdescription"].ToString();
                }
            }
            return this;
        }

        public List<FundusMaster> GetAllFundus()
        {
            this.CMD = "View";
            this.hid = Convert.ToInt32(HttpContext.Current.Session["hospital"].ToString());
            string xml = Common.ToXML(this);
            DBConnection con = new DBConnection();
            DataSet ds = con.ExecuteProcedure("SP_Fundus", xml);

            List<FundusMaster> Funduslist = new List<FundusMaster>();
            if (ds != null)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    FundusMaster f = new FundusMaster();
                    f.fundusid = Convert.ToInt32(ds.Tables[0].Rows[i]["fundusid"].ToString());
                    f.fundusname = ds.Tables[0].Rows[i]["fundusname"].ToString();
                    Funduslist.Add(f);
                }
            }
            return Funduslist;
        }

    }
}