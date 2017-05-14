using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Kamala_Eye_Clinic.Models
{
    public class HospitalMaster
    {
        public int hid { get; set; }


        [Required(ErrorMessage ="Hospital Name is Required")]
        [Display(Name = "Hospital Name:")]
        public string hname { get; set; }

        [Required(ErrorMessage = "Hospital Type is Required")]
        [Display(Name = "Hospital Type:")]
        public string htype { get; set; }

        [Required(ErrorMessage = "Hospital Tag is Required")]
        [Display(Name = "Tag:")]
        public string htag { get; set; }

        public string hlogo1 { get; set; }

        public string hlogo2 { get; set; }

        public string hlogo3 { get; set; }


        [Display(Name = "Address Line 1:")]
        public string address1 { get; set; }

        [Display(Name = "Address Line 2:")]
        public string address2 { get; set; }

        [Required(ErrorMessage = "City is Required")]
        [Display(Name = "City:")]
        public string city { get; set; }

        [Required(ErrorMessage = "State is Required")]
        [Display(Name = "State:")]
        public string hstate { get; set; }

        [Required(ErrorMessage = "Country is Required")]
        [Display(Name = "Country:")]
        public string country { get; set; }

        [Required(ErrorMessage = "Contact is Required")]
        [Display(Name = "Contact No 1:")]
        public string contact1 { get; set; }

        [Display(Name = "Contact No 2:")]
        public string contact2 { get; set; }


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

        public List<HospitalMaster> ShowAllHospitals()
        {
            return null;
        }
    }
}