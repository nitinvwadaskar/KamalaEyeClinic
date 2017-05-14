using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Kamala_Eye_Clinic.Models
{
    public class User
    {
        public int hid { get; set; }

        [Required(ErrorMessage = "User Id is Required")]
        public int userid { get; set; }

        [DataType(DataType.Text, ErrorMessage = "User Name Contains Characters Only")]
        [StringLength(30, MinimumLength = 6)]
        [Required(ErrorMessage = "User Name is Required")]
        [Display(Name = "User Name:")]
        public string username { get; set; }

        [Required(ErrorMessage = "User Role is Required")]
        [Display(Name = "User Role:")]
        public string[] userrole { get; set; }

        [DataType(DataType.Password)]
        [StringLength(14, ErrorMessage = "Password Must be 8-14 Characters Long.", MinimumLength = 8)]
        [Required(ErrorMessage = "Password is Required")]
        [MembershipPassword()]
        public string password { get; set; }

        [System.ComponentModel.DataAnnotations.Compare("password")]
        [DataType(DataType.Password)]
        [StringLength(14, ErrorMessage = "Password Must be 8-14 Characters Long.", MinimumLength = 8)]
        [Required(ErrorMessage = "Confirm Password is Required")]
        [MembershipPassword()]
        [Display(Name = "Confirm Password:")]
        public string cpassword { get; set; }

        [Required(ErrorMessage = "First Name is Required")]
        [Display(Name = "First Name:")]
        public string fname { get; set; }

        [Display(Name = "Middle Name:")]
        public string mname { get; set; }

        [Required(ErrorMessage = "Last Name is Required")]
        [Display(Name = "Last Name:")]
        public string lname { get; set; }

        [DataType(DataType.PhoneNumber)]
        [RegularExpression("^[0-9]{10}$", ErrorMessage = "Mobile No Must be 10 Digits.")]
        [Required(ErrorMessage = "Mobile No is Required")]
        [Display(Name = "Mobile No:")]
        public string contact { get; set; }

        [DataType(DataType.EmailAddress)]
        [StringLength(128)]
        [Required(ErrorMessage = "Email Id is Required")]
        [Display(Name = "Email Id:")]
        public string email { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "BirthDate is Required")]
        [Display(Name = "BirthDate:")]
        public string dob { get; set; }

        [Required(ErrorMessage ="Old Password is Required")]
        [Display(Name ="Old Password:")]
        public string opassword { get; set;}



        public string CMD { get; set; }


        public string[] PerformAction()
            {
            try
            {
                string xml = Common.ToXML(this);
                DBConnection con = new DBConnection();
                DataSet ds = con.ExecuteProcedure("SP_UserRegistration", xml);
                return Common.CallbackMsg(ds);
            }
            catch (Exception e)
            {
                return new string[] { "Failed", e.Message };
            }
        }


      public  List<SelectListItem> Roles = new List<SelectListItem>();

        public User GetMultipleRole()
        {
            RoleMaster obj = new RoleMaster();
            obj.CMD = "View";
            obj.hid = 101;
            string xml = Common.ToXML(obj);
            DBConnection con = new DBConnection();
            DataSet ds = con.ExecuteProcedure("SP_UserRole", xml);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {               
                Roles.Add(new SelectListItem{Text =ds.Tables[0].Rows[i][2].ToString() ,Value = ds.Tables[0].Rows[i][1].ToString() });
            }
            return this;
        }
    }  

}