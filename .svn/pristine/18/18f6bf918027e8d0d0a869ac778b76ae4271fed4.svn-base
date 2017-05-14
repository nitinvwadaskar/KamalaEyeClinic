using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Data;

namespace Kamala_Eye_Clinic.Models
{
    public class RoleMaster
    {
        public int hid { get; set; }
        public int roleid { get; set; }

        [Required(ErrorMessage ="Role Name is Required")]
        [Display(Name ="Role Name:")]
        public string rolename { get; set; }

        public string CMD { get; set; }

        public string[] PerformAction()
        {
            try
            {
                string xml = Common.ToXML(this);
                DBConnection con = new DBConnection();
                DataSet ds = con.ExecuteProcedure("SP_UserRole", xml);
                return Common.CallbackMsg(ds);
            }
            catch (Exception e)
            {
                return new string[] { "Failed", e.Message };
            }
        }
        public RoleMaster GetSingleRole(int roleid)
        {
            this.CMD = "View1";
            this.hid = 101;
            this.roleid = roleid;
            string xml = Common.ToXML(this);
            DBConnection con = new DBConnection();
            DataSet ds = con.ExecuteProcedure("SP_UserRole", xml);
            if (ds != null)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {                  
                    this.rolename = ds.Tables[0].Rows[i]["rolename"].ToString();                    
                }
            }
            return this;
        }

    }
}