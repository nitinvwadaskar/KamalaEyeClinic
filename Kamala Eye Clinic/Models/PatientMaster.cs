using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
namespace Kamala_Eye_Clinic.Models
{
    public class PatientMaster
    {
        public int hid { get; set; }
        public int patientid { get; set; }
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
        public decimal pheight { get; set; }

        [Display(Name = "Weight:")]
        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Invalid Weight")]
        public decimal pweight { get; set; }

        public string CMD { get; set; }

        internal static IEnumerable<PatientMaster> setPatientMasterSource()
        {
            throw new NotImplementedException();
        }

        public string[] PerformAction()
        {
            try
            {
                string xml = Common.ToXML(this);
                DBConnection con = new DBConnection();
                DataSet ds = con.ExecuteProcedure("SP_PatientRegistration", xml);
                return Common.CallbackMsg(ds);
            }
            catch (Exception e)
            {
                return new string[] { "Failed", e.Message };
            }
        }
        public PatientMaster GetSinglePatient(int patientid, int hid)
        {
            this.CMD = "View1";
            this.hid = hid;
            this.patientid = patientid;
            string xml = Common.ToXML(this);
            DBConnection con = new DBConnection();
            DataSet ds = con.ExecuteProcedure("SP_PatientRegistration", xml);
            if (ds != null)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
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
                    string pheight1 = ds.Tables[0].Rows[i]["pheight"].ToString();
                    this.pheight = pheight1 == "" ? 0 : Convert.ToDecimal(pheight1);
                    string pweight1 = ds.Tables[0].Rows[i]["pweight"].ToString();
                    this.pweight = pweight1 == "" ? 0 : Convert.ToDecimal(pweight1);
                }
            }

            return this;
        }

    }
}