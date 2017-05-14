using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;

namespace Kamala_Eye_Clinic.Models
{
    public class DrugCategoryMaster
    {
        public int hid { get; set; }
        public int categoryid { get; set; }

        [Required(ErrorMessage = "Blood Group is Required")]
        [Display(Name = "Category:")]
        public string categoryname { get; set; }

        [Display(Name = "Description:")]
        public string cdescription { get; set; }

        public string CMD { get; set; }

        public string[] PerformAction()
        {
            try
            {
                string xml = Common.ToXML(this);
                DBConnection con = new DBConnection();
                DataSet ds = con.ExecuteProcedure("SP_DrugCategory", xml);
                return Common.CallbackMsg(ds);
            }
            catch (Exception e)
            {
                return new string[] { "Failed", e.Message };
            }
        }

        public DrugCategoryMaster GetSingleDrugCategory(int categoryid)
        {
            this.CMD = "View1";
            this.categoryid = categoryid;
            string xml = Common.ToXML(this);
            DBConnection con = new DBConnection();
            DataSet ds = con.ExecuteProcedure("SP_DrugCategory", xml);
            if (ds != null)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    this.categoryname = ds.Tables[0].Rows[i]["categoryname"].ToString();
                    this.cdescription = ds.Tables[0].Rows[i]["cdescription"].ToString();
                }
            }

            return this;
        }

    }
}