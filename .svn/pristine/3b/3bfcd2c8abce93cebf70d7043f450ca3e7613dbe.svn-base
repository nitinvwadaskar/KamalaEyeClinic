using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;

namespace Kamala_Eye_Clinic.Models
{
    public class HistoryMaster
    {
        public int hid { get; set; }
        public int historyid { get; set; }

        [Required(ErrorMessage = "History is Required")]
        [Display(Name = "History Name:")]
        public string historyname { get; set; }

        [Display(Name = "Description:")]
        public string historydescription { get; set; }

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
                DataSet ds = con.ExecuteProcedure("SP_History", xml);
                return Common.CallbackMsg(ds);
            }
            catch (Exception e)
            {
                return new string[] { "Failed", e.Message };
            }
        }

        public HistoryMaster GetSingleHistory(int historyid)
        {
            this.CMD = "View1";
            this.historyid = historyid;
            string xml = Common.ToXML(this);
            DBConnection con = new DBConnection();
            DataSet ds = con.ExecuteProcedure("SP_History", xml);
            if (ds != null)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    this.historyname = ds.Tables[0].Rows[i]["historyname"].ToString();
                    this.historydescription = ds.Tables[0].Rows[i]["historydescription"].ToString();
                }
            }
            return this;
        }

        public List<HistoryMaster> GetAllHistory()
        {
            this.CMD = "View";
            this.hid = Convert.ToInt32(HttpContext.Current.Session["hospital"].ToString());
            string xml = Common.ToXML(this);
            DBConnection con = new DBConnection();
            DataSet ds = con.ExecuteProcedure("SP_History", xml);

            List<HistoryMaster> Historylist = new List<HistoryMaster>();
            if (ds != null)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    HistoryMaster om = new HistoryMaster();
                    om.historyid = Convert.ToInt32(ds.Tables[0].Rows[i]["historyid"].ToString());
                    om.historyname = ds.Tables[0].Rows[i]["historyname"].ToString();
                    Historylist.Add(om);
                }
            }
            return Historylist;
        }
    }
}