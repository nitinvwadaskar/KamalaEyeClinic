﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;

namespace Kamala_Eye_Clinic.Models
{
    public class OccupationMaster
    {
        public int hid { get; set; }
        public int occupationid { get; set; }

        [Required(ErrorMessage = "Occupation is Required")]
        [Display(Name = "Occupation Name:")]
        public string occupationname { get; set; }

        [Display(Name = "Description:")]
        public string occupationdescription { get; set; }

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
                DataSet ds = con.ExecuteProcedure("SP_Occupation", xml);
                return Common.CallbackMsg(ds);
            }
            catch (Exception e)
            {
                return new string[] { "Failed", e.Message };
            }
        }

        public OccupationMaster GetSingleOccupation(int occupationid)
        {
            this.CMD = "View1";
            this.occupationid = occupationid;
            string xml = Common.ToXML(this);
            DBConnection con = new DBConnection();
            DataSet ds = con.ExecuteProcedure("SP_Occupation", xml);
            if (ds != null)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    this.occupationname = ds.Tables[0].Rows[i]["occupationname"].ToString();
                    this.occupationdescription = ds.Tables[0].Rows[i]["occupationdescription"].ToString();
                }
            }
            return this;
        }

        public List<OccupationMaster> GetAllOccupation()
        {
            this.CMD = "View";
            this.hid = Convert.ToInt32(HttpContext.Current.Session["hospital"].ToString());
            string xml = Common.ToXML(this);
            DBConnection con = new DBConnection();
            DataSet ds = con.ExecuteProcedure("SP_Occupation", xml);

            List<OccupationMaster> Occupationlist = new List<OccupationMaster>();
            if (ds != null)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    OccupationMaster om = new OccupationMaster();
                    om.occupationid = Convert.ToInt32(ds.Tables[0].Rows[i]["occupationid"].ToString());
                    om.occupationname = ds.Tables[0].Rows[i]["occupationname"].ToString();
                    Occupationlist.Add(om);
                }
            }
            return Occupationlist;
        }

    }
}