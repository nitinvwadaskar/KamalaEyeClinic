using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;

namespace Kamala_Eye_Clinic.Models
{
    public class SisterMaster
    {
        public int hid { get; set; }
        public int sisterid { get; set; }
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
        public decimal sheight { get; set; }

        [Display(Name = "Weight:")]
        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Invalid Weight")]
        public decimal sweight { get; set; }

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
                DataSet ds = con.ExecuteProcedure("SP_SisterRegistration", xml);
                return Common.CallbackMsg(ds);
            }
            catch (Exception e)
            {
                return new string[] { "Failed", e.Message };
            }
        }


        public List<SisterMaster> GetAllSister()
        {
            this.CMD = "View";
            this.hid = Convert.ToInt32(HttpContext.Current.Session["hospital"].ToString());
            string xml = Common.ToXML(this);
            DBConnection con = new DBConnection();
            DataSet ds = con.ExecuteProcedure("SP_SisterRegistration", xml);

            List<SisterMaster> Sisterlist = new List<SisterMaster>();
            if (ds != null)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    SisterMaster om = new SisterMaster();
                    om.sisterid = Convert.ToInt32(ds.Tables[0].Rows[i]["sisterid"].ToString());
                    om.fname = ds.Tables[0].Rows[i]["fname"].ToString();
                    om.lname = ds.Tables[0].Rows[i]["lname"].ToString();
                    Sisterlist.Add(om);
                }
            }
            return Sisterlist;
        }

        public SisterMaster GetSingleSister(int sisterid, int hid)
        {
            this.CMD = "View1";
            this.hid = hid;
            this.sisterid = sisterid;
            string xml = Common.ToXML(this);
            DBConnection con = new DBConnection();
            DataSet ds = con.ExecuteProcedure("SP_SisterRegistration", xml);
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
                    string sheight1 = ds.Tables[0].Rows[i]["sheight"].ToString();
                    this.sheight = sheight1 == "" ? 0 : Convert.ToDecimal(sheight1);
                    string sweight1 = ds.Tables[0].Rows[i]["sweight"].ToString();
                    this.sweight = sweight1 == "" ? 0 : Convert.ToDecimal(sweight1);
                    this.qualification = ds.Tables[0].Rows[i]["qualification"].ToString();
                }
            }

            return this;
        }


    }
}