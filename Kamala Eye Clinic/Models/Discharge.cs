﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kamala_Eye_Clinic.Models
{
    public class Discharge
    {
        public int hid { get; set; }
        public int dischargeid { get; set; }
        public int patientid { get; set; }
        public string discharge_dt { get; set; }

        [Required(ErrorMessage = "Reason is Required")]
        [Display(Name = "Reason of Discharge:")]
        public string reasonofdischarge { get; set; }

        [Required(ErrorMessage = "In-charge Doctor is Required")]
        [Display(Name = "Doctor In-charge:")]
        public string doctorincharge { get; set; }

        [Required(ErrorMessage = "Operated Eye is Required")]
        [Display(Name = "Eye:")]
        public string eye { get; set; }

        [Display(Name = "Procedure/Operation:")]
        public string operation { get; set; }

        [Display(Name = "Treatment Advice at Discharge:")]
        public string treatmentadvice { get; set; }

        [Display(Name = "Treatment in  Words:")]
        public string treatmentinword { get; set; }

        public string CMD { get; set; }

        public int otid { get; set; }
        public string otdate { get; set; }

        public string createby { get; set; }
        public string updateby { get; set; }

        public string[] PerformAction()
        {
            try
            {
                this.createby = this.updateby = HttpContext.Current.Session["UserName"].ToString();
                string xml = Common.ToXML(this);
                DBConnection con = new DBConnection();
                DataSet ds = con.ExecuteProcedure("SP_Discharge", xml);
                return Common.CallbackMsg(ds);
            }
            catch (Exception e)
            {
                return new string[] { "Failed", e.Message };
            }
        }
        public Discharge GetSingleDischarge(int dischargeid)
        {
            this.CMD = "View1";
            this.hid =Convert.ToInt32(HttpContext.Current.Session["hospital"]);
            this.dischargeid = dischargeid;
            string xml = Common.ToXML(this);
            DBConnection con = new DBConnection();
            DataSet ds = con.ExecuteProcedure("SP_Discharge", xml);
            if (ds != null)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    
                    this.reasonofdischarge = ds.Tables[0].Rows[i]["reasonofdischarge"].ToString();
                    this.doctorincharge = ds.Tables[0].Rows[i]["doctorincharge"].ToString();
                    this.eye = ds.Tables[0].Rows[i]["eye"].ToString();
                    this.operation = ds.Tables[0].Rows[i]["operation"].ToString();
                    this.treatmentadvice = ds.Tables[0].Rows[i]["treatmentadvice"].ToString();
                    this.treatmentinword = ds.Tables[0].Rows[i]["treatmentinword"].ToString();                   
                }
            }
            return this;
        }

        public List<SelectListItem> Doctor = new List<SelectListItem>();

        public Discharge GetMultipleDoctor()
        {
            DoctorMaster obj = new DoctorMaster();
            obj.CMD = "View";
            obj.hid = Convert.ToInt32(HttpContext.Current.Session["hospital"].ToString());
            string xml = Common.ToXML(obj);
            DBConnection con = new DBConnection();
            DataSet ds = con.ExecuteProcedure("SP_DoctorRegistration", xml);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                Doctor.Add(new SelectListItem { Text = ds.Tables[0].Rows[i]["fname"].ToString() + " " + ds.Tables[0].Rows[i]["lname"].ToString(), Value = ds.Tables[0].Rows[i]["doctorid"].ToString() });
            }
            return this;
        }
    }
}