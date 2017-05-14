using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Kamala_Eye_Clinic.Models
{
    public class PatientInsuranceMaster
    {
        public int patientid { get; set; }
        public int srno { get; set; }

        [Required(ErrorMessage = "Insurance Id is Required")]
        [Display(Name = "Insurance Id:")]
        public string insuranceid { get; set; }

        [Required(ErrorMessage = "Insurance Company Name is Required")]
        [Display(Name = "Insurance Company:")]
        public string icompanyname { get; set; }

        [Required(ErrorMessage = "Employee Id is Required")]
        [Display(Name = "Employee Id:")]
        public string employeeid { get; set; }

        [Required(ErrorMessage = "Company Name is Required")]
        [Display(Name = "Company Name:")]
        public string ecompanyname { get; set; }

        [Required(ErrorMessage = "Company Address is Required")]
        [Display(Name = "Company Address:")]
        public string ecompanyaddress { get; set; }


        private string Save()
        {
            return "";
        }
        private string Update()
        {
            return "";
        }

        private string Delete()
        {
            return "";
        }

        public List<PatientInsuranceMaster> ShowAllPatientInsurance()
        {
            return null;
        }
    }
}