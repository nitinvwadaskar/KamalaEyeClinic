using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kamala_Eye_Clinic.Models
{
    public class DrugMaster
    {
        public int hid { get; set; }
        public int drugid { get; set; }

        [Required(ErrorMessage = "Category Name Required")]
        [Display(Name = "Category Name:")]
        public int categoryid { get; set; }

        [Required(ErrorMessage = "Drug Name Required")]
        [Display(Name = "Drug Name:")]
        public string drugname { get; set; }

        [Display(Name = "Description:")]
        public string ddescription { get; set; }

        public string CMD { get; set; }

        public string createby { get; set; }

        public string updateby { get; set; }


        public string[] PerformAction()
        {
            try
            {
                string xml = Common.ToXML(this);
                DBConnection con = new DBConnection();
                DataSet ds = con.ExecuteProcedure("SP_Drug", xml);
                return Common.CallbackMsg(ds);
            }
            catch (Exception e)
            {
                return new string[] { "Failed", e.Message };
            }
        }

        public DrugMaster GetSingleDrug(int drugid, int hid)
        {
            this.CMD = "View1";
            this.drugid = drugid;
            this.hid = hid;
            this.createby = this.updateby = HttpContext.Current.Session["UserName"].ToString();
            string xml = Common.ToXML(this);
            DBConnection con = new DBConnection();
            DataSet ds = con.ExecuteProcedure("SP_Drug", xml);
            if (ds != null)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    this.categoryid = Convert.ToInt32(ds.Tables[0].Rows[i]["categoryid"].ToString());
                    this.drugname = ds.Tables[0].Rows[i]["drugname"].ToString();
                    this.ddescription = ds.Tables[0].Rows[i]["ddescription"].ToString();
                }
            }
            return this;
        }

        public List<SelectListItem> Category = new List<SelectListItem>();

        public DrugMaster GetMultipleCategory(int hid)
        {
            DrugCategoryMaster obj = new DrugCategoryMaster();
            obj.CMD = "View";
            obj.hid = hid;
            string xml = Common.ToXML(obj);
            DBConnection con = new DBConnection();
            DataSet ds = con.ExecuteProcedure("SP_DrugCategory", xml);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                Category.Add(new SelectListItem { Text = ds.Tables[0].Rows[i][2].ToString(), Value = ds.Tables[0].Rows[i][1].ToString() });
            }
            return this;
        }


        public List<DrugMaster> GetAllDrug()
        {
            this.CMD = "View";
            this.hid = Convert.ToInt32(HttpContext.Current.Session["hospital"].ToString());
            string xml = Common.ToXML(this);
            DBConnection con = new DBConnection();
            DataSet ds = con.ExecuteProcedure("SP_Drug", xml);

            List<DrugMaster> Druglist = new List<DrugMaster>();
            if (ds != null)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DrugMaster dc = new DrugMaster();
                    dc.drugid = Convert.ToInt32(ds.Tables[0].Rows[i]["drugid"].ToString());
                    dc.drugname = ds.Tables[0].Rows[i]["drugname"].ToString();
                    Druglist.Add(dc);
                }
            }
            return Druglist;
        }

    }
}