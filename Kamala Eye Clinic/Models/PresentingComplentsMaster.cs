using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;

namespace Kamala_Eye_Clinic.Models
{
    public class PresentingComplentsMaster
    {
        public int hid { get; set; }
        public int complentid { get; set; }

        [Required(ErrorMessage = "Complent is Required")]
        [Display(Name = "Complent Name:")]
        public string complentname { get; set; }

        [Display(Name = "Description:")]
        public string complentdescription { get; set; }

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
                DataSet ds = con.ExecuteProcedure("SP_Complent", xml);
                return Common.CallbackMsg(ds);
            }
            catch (Exception e)
            {
                return new string[] { "Failed", e.Message };
            }
        }

        public PresentingComplentsMaster GetSingleComplent(int complentid)
        {
            this.CMD = "View1";
            this.complentid = complentid;
            string xml = Common.ToXML(this);
            DBConnection con = new DBConnection();
            DataSet ds = con.ExecuteProcedure("SP_Complent", xml);
            if (ds != null)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    this.complentname = ds.Tables[0].Rows[i]["complentname"].ToString();
                    this.complentdescription = ds.Tables[0].Rows[i]["complentdescription"].ToString();
                }
            }
            return this;
        }

        public List<PresentingComplentsMaster> GetAllPresentingComplent()
        {
            this.CMD = "View";
            this.hid = Convert.ToInt32(HttpContext.Current.Session["hospital"].ToString());
            string xml = Common.ToXML(this);
            DBConnection con = new DBConnection();
            DataSet ds = con.ExecuteProcedure("SP_Complent", xml);

            List<PresentingComplentsMaster> PresentingComplentlist = new List<PresentingComplentsMaster>();
            if (ds != null)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    PresentingComplentsMaster pc = new PresentingComplentsMaster();
                    pc.complentid = Convert.ToInt32(ds.Tables[0].Rows[i]["complentid"].ToString());
                    pc.complentname = ds.Tables[0].Rows[i]["complentname"].ToString();
                    PresentingComplentlist.Add(pc);
                }
            }
            return PresentingComplentlist;
        }

    }
}