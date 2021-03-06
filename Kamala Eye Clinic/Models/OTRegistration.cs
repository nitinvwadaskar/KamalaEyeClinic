﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kamala_Eye_Clinic.Models
{
    public class OTRegistration
    {
        public int hid { get; set; }
        public int patientid { get; set; }
        public int regid { get; set; }

        [Display(Name = "OT Date:")]
        public string otdate { get; set; }

        [Display(Name = "Form(AM/PM):")]
        public string durationfrom { get; set; }

        [Display(Name = "To(AM/PM):")]
        public string durationto { get; set; }

        [Display(Name = "Types of Anaesthesia:")]
        public string typeofanaesthesia { get; set; }

        [Display(Name = "Provisional Diagnosis:")]
        public string provisionaldiagnosis { get; set; }

        [Display(Name = "Operating Surgeon:")]
        public string operatingsurgeon { get; set; }

        [Display(Name = "Anaesthetist:")]
        public string anaesthetist { get; set; }

        [Display(Name = "Assisting Doctor:")]
        public string assistingdoctor { get; set; }

        [Display(Name = "Assisting Nurse:")]
        public string assistingnurse { get; set; }

        [Display(Name = "Remarks:")]
        public string remarks { get; set; }

        [Display(Name = "Material H.P.E:")]
        public string material_HPE { get; set; }

        public string oreference { get; set; }
        public string opower { get; set; }
        public string sn { get; set; }
        
        public string createby { get; set; }
        public string updateby { get; set; }

        public string isbilldone { get; set; }

        public string CMD { get; set; }


        public string[] PerformAction()
        {
            try
            {
                this.createby = this.updateby = HttpContext.Current.Session["UserName"].ToString();
                this.patientid = Convert.ToInt32(HttpContext.Current.Session["Patientid"]);
                this.isbilldone = "false";
                string xml = Common.ToXML(this);
                DBConnection con = new DBConnection();
                DataSet ds = con.ExecuteProcedure("sp_otregistration", xml);
                return Common.CallbackMsg(ds);
            }
            catch (Exception e)
            {
                return new string[] { "Failed", e.Message };
            }
        }
        public OTRegistration GetSingleOT(int regid, int hid)
        {
            this.CMD = "View1";
            this.hid = hid;
            this.regid = regid;            
            this.patientid = Convert.ToInt32(HttpContext.Current.Session["patientid"]);
            string xml = Common.ToXML(this);
            DBConnection con = new DBConnection();
            DataSet ds = con.ExecuteProcedure("sp_otregistration", xml);
            if (ds != null)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    this.otdate = ds.Tables[0].Rows[i]["otdate"].ToString();
                    this.durationfrom = ds.Tables[0].Rows[i]["durationfrom"].ToString();
                    this.durationto = ds.Tables[0].Rows[i]["durationto"].ToString();
                    this.typeofanaesthesia = ds.Tables[0].Rows[i]["typeofanaesthesia"].ToString();
                    this.provisionaldiagnosis = ds.Tables[0].Rows[i]["provisionaldiagnosis"].ToString();
                    this.oreference = ds.Tables[0].Rows[i]["oreference"].ToString();
                    this.opower = ds.Tables[0].Rows[i]["opower"].ToString();
                    this.sn = ds.Tables[0].Rows[i]["sn"].ToString();
                    this.operatingsurgeon = ds.Tables[0].Rows[i]["operatingsurgeon"].ToString();
                    this.anaesthetist = ds.Tables[0].Rows[i]["anaesthetist"].ToString();
                    this.assistingdoctor = ds.Tables[0].Rows[i]["assistingdoctor"].ToString();
                    this.assistingnurse = ds.Tables[0].Rows[i]["assistingnurse"].ToString();
                    this.remarks = ds.Tables[0].Rows[i]["remarks"].ToString();
                    this.material_HPE = ds.Tables[0].Rows[i]["material_HPE"].ToString();
                    this.isbilldone = ds.Tables[0].Rows[i]["isbilldone"].ToString();
                }
            }
            return this;
        }


        public List<SelectListItem> doctor = new List<SelectListItem>();

        public OTRegistration GetMultipleDoctor()
        {
            DoctorMaster obj = new DoctorMaster();
            obj.CMD = "View";
            obj.hid = Convert.ToInt32(HttpContext.Current.Session["hospital"].ToString());
            string xml = Common.ToXML(obj);
            DBConnection con = new DBConnection();
            DataSet ds = con.ExecuteProcedure("SP_DoctorRegistration", xml);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                doctor.Add(new SelectListItem { Text = ds.Tables[0].Rows[i]["fname"].ToString() + " " + ds.Tables[0].Rows[i]["lname"].ToString(), Value = ds.Tables[0].Rows[i]["fname"].ToString() + " " + ds.Tables[0].Rows[i]["lname"].ToString() });
            }
            return this;
        }


        public List<SelectListItem> nurse = new List<SelectListItem>();

        public OTRegistration GetMultipleNurse()
        {
            SisterMaster obj = new SisterMaster();
            obj.CMD = "View";
            obj.hid = Convert.ToInt32(HttpContext.Current.Session["hospital"].ToString());
            string xml = Common.ToXML(obj);
            DBConnection con = new DBConnection();
            DataSet ds = con.ExecuteProcedure("SP_SisterRegistration", xml);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                nurse.Add(new SelectListItem { Text = ds.Tables[0].Rows[i]["fname"].ToString() + " " + ds.Tables[0].Rows[i]["lname"].ToString(), Value = ds.Tables[0].Rows[i]["sisterid"].ToString() });
            }
            return this;
        }

    }
}