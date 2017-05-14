using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;

namespace Kamala_Eye_Clinic.Models
{
    public class BillParticularMaster
    {
        public int hid { get; set; }
        public int particularid { get; set; }

        [Required(ErrorMessage = "Particular Name is Required")]
        [Display(Name = "Particular Name:")]
        public string name { get; set; }
               
        public string CMD { get; set; }


        public string[] PerformAction()
        {
            try
            {
                string xml = Common.ToXML(this);
                DBConnection con = new DBConnection();
                DataSet ds = con.ExecuteProcedure("SP_BillParticularMst", xml);
                return Common.CallbackMsg(ds);
            }
            catch (Exception e)
            {
                return new string[] { "Failed", e.Message };
            }
        }
        public BillParticularMaster GetSingleBillParticular(int particularid)
        {
            this.CMD = "View1";
            this.hid = 101;
            this.particularid = particularid;
            string xml = Common.ToXML(this);
            DBConnection con = new DBConnection();
            DataSet ds = con.ExecuteProcedure("SP_BillParticularMst", xml);
            if (ds != null)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    this.name = ds.Tables[0].Rows[i]["name"].ToString();                    
                }
            }

            return this;
        }
        public List<BillParticularMaster> GetAllBillParticular()
        {
            this.CMD = "View";
            this.hid = Convert.ToInt32(HttpContext.Current.Session["hospital"].ToString());
            string xml = Common.ToXML(this);
            DBConnection con = new DBConnection();
            DataSet ds = con.ExecuteProcedure("SP_BillParticularMst", xml);

            List<BillParticularMaster> particularlist = new List<BillParticularMaster>();
            if (ds != null)
            {
               
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    BillParticularMaster bp = new BillParticularMaster();
                    bp.particularid = Convert.ToInt32(ds.Tables[0].Rows[i]["particularid"].ToString());
                    bp.name = ds.Tables[0].Rows[i]["name"].ToString();
                    particularlist.Add(bp);
                }
            }

            return particularlist;
        }

    }
}