using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Kamala_Eye_Clinic.Models
{
    public class BillParticulars
    {
        public int billid { get; set; }
        public int patientid { get; set; }
        public int particularid { get; set; }

        [Required(ErrorMessage = "Particular Name is Required")]
        [Display(Name = "Particular Name:")]
        public string particular { get; set; }

        [Required(ErrorMessage = "Quantity is Required")]
        [Display(Name = "Quantity:")]
        public int qty { get; set; }

        [Required(ErrorMessage = "Charges is Required")]
        [Display(Name = "Charges:")]
        public decimal charges { get; set; }

        [Required(ErrorMessage = "Total is Required")]
        [Display(Name = "Total:")]
        public decimal total { get; set; }


     
        public List<BillParticulars> ShowAllParticulars()
        {
            return null;
        }
    }
}