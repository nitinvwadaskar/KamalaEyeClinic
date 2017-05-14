using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Data.Sql;
using System.Data;
using System.Web.Mvc;

namespace Kamala_Eye_Clinic.Models
{
    public class DoctorMaster
    {
        public int hid { get; set; }
        public int doctorid { get; set; }
        public string reg_dt { get; set; }


        [Required(ErrorMessage = "Title is Required")]
        [Display(Name = "Title:")]
        public string title { get; set; }

        [Required(ErrorMessage = "First Name is Require")]
        [Display(Name = "First Name:")]
        [DataType(DataType.Text)]
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

        [Required(ErrorMessage = "Date of Birth is Required")]
        [Display(Name = "Date of Birth:")]
        public string dob { get; set; }


        [Display(Name = "Birthplace:")]
        [DataType(DataType.Text)]
        public string birthplace { get; set; }

        [Required(ErrorMessage = "Age is Required")]
        [Display(Name = "Age:")]
        public int age { get; set; }

        [Required(ErrorMessage = "Gender is Required")]
        [Display(Name = "Gender:")]
        public string gender { get; set; }

        [Required(ErrorMessage = "Marital Status is Required")]
        [Display(Name = "Marital Status:")]
        public string maritalstatus { get; set; }

        [Required(ErrorMessage = "Blood Group is Required")]
        [Display(Name = "Blood Group:")]
        public string bloodgroup { get; set; }

        [Display(Name = "Height:")]
        public decimal dheight { get; set; }

        [Display(Name = "Weight:")]
        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Invalid Weight")]
        public decimal dweight { get; set; }

        [Required(ErrorMessage = "Qualification is Required")]
        [Display(Name = "Qualification:")]
        public string qualification { get; set; }


        public string CMD { get; set; }


        public string[] PerformAction()
        {
            try
            {
                string xml = Common.ToXML(this);
                DBConnection con = new DBConnection();
                DataSet ds = con.ExecuteProcedure("SP_DoctorRegistration", xml);
                return Common.CallbackMsg(ds);
            }
            catch (Exception e)
            {
                return new string[] { "Failed", e.Message };
            }
        }

        public List<DoctorMaster> GetAllDoctor()
        {
            this.CMD = "View";
            this.hid = Convert.ToInt32(HttpContext.Current.Session["hospital"].ToString());
            string xml = Common.ToXML(this);
            DBConnection con = new DBConnection();
            DataSet ds = con.ExecuteProcedure("SP_DoctorRegistration", xml);

            List<DoctorMaster> Doctorlist = new List<DoctorMaster>();
            if (ds != null)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DoctorMaster om = new DoctorMaster();
                    om.doctorid = Convert.ToInt32(ds.Tables[0].Rows[i]["doctorid"].ToString());
                    om.fname = ds.Tables[0].Rows[i]["fname"].ToString();
                    om.lname = ds.Tables[0].Rows[i]["lname"].ToString();
                    Doctorlist.Add(om);
                }
            }
            return Doctorlist;
        }


        public DoctorMaster GetSingleDoctor(int doctorid, int hid)
        {
            this.CMD = "View1";
            this.hid = hid;
            this.doctorid = doctorid;
            string xml = Common.ToXML(this);
            DBConnection con = new DBConnection();
            DataSet ds = con.ExecuteProcedure("SP_DoctorRegistration", xml);
            if (ds != null)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    this.title = ds.Tables[0].Rows[i]["title"].ToString();
                    this.fname = ds.Tables[0].Rows[i]["fname"].ToString();
                    this.mname = ds.Tables[0].Rows[i]["mname"].ToString();
                    this.lname = ds.Tables[0].Rows[i]["lname"].ToString();
                    this.contact = ds.Tables[0].Rows[i]["contact"].ToString();
                    this.dob = ds.Tables[0].Rows[i]["dob"].ToString();
                    this.birthplace = ds.Tables[0].Rows[i]["birthplace"].ToString();
                    string age1 = ds.Tables[0].Rows[i]["age"].ToString();
                    this.age = age1 == "" ? 0 : Convert.ToInt32(age1);
                    this.gender = ds.Tables[0].Rows[i]["gender"].ToString();
                    this.maritalstatus = ds.Tables[0].Rows[i]["maritalstatus"].ToString();
                    this.bloodgroup = ds.Tables[0].Rows[i]["bloodgroup"].ToString();
                    string dheight1 = ds.Tables[0].Rows[i]["dheight"].ToString();
                    this.dheight = dheight1 == "" ? 0 : Convert.ToDecimal(dheight1);
                    string dweight1 = ds.Tables[0].Rows[i]["dweight"].ToString();
                    this.dweight = dweight1 == "" ? 0 : Convert.ToDecimal(dweight1);
                    this.qualification = ds.Tables[0].Rows[i]["qualification"].ToString();
                }
            }

            return this;
        }


    }
}