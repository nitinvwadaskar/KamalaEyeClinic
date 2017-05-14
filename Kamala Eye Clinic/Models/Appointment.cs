using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;

namespace Kamala_Eye_Clinic.Models
{
    public class Appointment
    {
        public int hid { get; set; }
        public int appointmentid { get; set; }

        [Display(Name = "Patient Id:")]
        public int patientid { get; set; }

        [Required(ErrorMessage = "Appointment Date is Required")]
        [Display(Name = "Date:")]
        public string appointment_dt { get; set; }

        [Required(ErrorMessage = "Title is Required")]
        [Display(Name = "Title:")]
        public string title { get; set; }

        [Required(ErrorMessage = "First Name is Required")]
        [Display(Name = "First Name:")]
        public string fname { get; set; }


        [Display(Name = "Middle Name:")]
        public string mname { get; set; }

        [Required(ErrorMessage = "Last Name is Required")]
        [Display(Name = "Last Name:")]
        public string lname { get; set; }

        [Required(ErrorMessage = "Contact No is Required")]
        [MaxLength(10, ErrorMessage = "Contact No Must Be 10 Digits.")]
        [MinLength(10, ErrorMessage = "Contact No Must Be 10 Digits.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Contact Number Must Be Digits.")]
        [Display(Name = "Contact No:")]
        public string contact { get; set; }

        [Required(ErrorMessage = "Address is Required")]
        [Display(Name = "Address:")]
        public string paddress { get; set; }

        public string CMD { get; set; }

        public string createby { get; set; }

        public string updateby { get; set; }

        public string fromdt { get; set; }

        public string todt { get; set; }

        public string[] PerformAction()
        {
            try
            {
                this.createby = this.updateby = HttpContext.Current.Session["UserName"].ToString();
                string xml = Common.ToXML(this);
                DBConnection con = new DBConnection();
                DataSet ds = con.ExecuteProcedure("SP_Appointment", xml);
                return Common.CallbackMsg(ds);
            }
            catch (Exception e)
            {
                return new string[] { "Failed", e.Message };
            }
        }

        public Appointment GetSingleAppointment(int appointmentid)
        {
            this.CMD = "View1";
            this.hid = Convert.ToInt32(HttpContext.Current.Session["hospital"]);
            this.appointmentid = appointmentid;
            string xml = Common.ToXML(this);
            DBConnection con = new DBConnection();
            DataSet ds = con.ExecuteProcedure("SP_Appointment", xml);
            if (ds != null)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {

                    this.appointment_dt = ds.Tables[0].Rows[i]["appointment_dt"].ToString();
                    this.patientid = Convert.ToInt32(ds.Tables[0].Rows[i]["patientid"].ToString());
                    this.fname = ds.Tables[0].Rows[i]["fname"].ToString();
                    this.mname = ds.Tables[0].Rows[i]["mname"].ToString();
                    this.lname = ds.Tables[0].Rows[i]["lname"].ToString();
                    this.contact = ds.Tables[0].Rows[i]["contact"].ToString();
                    this.paddress = ds.Tables[0].Rows[i]["paddress"].ToString();
                }
            }
            return this;
        }

    }
}