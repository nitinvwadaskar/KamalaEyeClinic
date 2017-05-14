using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Kamala_Eye_Clinic.Models
{
    public class BillMaster
    {
        public int hid { get; set; }
        public int billid { get; set; }
        public int patientid { get; set; }               
        
        [Required(ErrorMessage ="Total Amount is Required")]
        [Display(Name ="Total Amount:")]        
        public decimal totalamount { get; set; }

        public string CMD { get; set; }

        public string createby { get; set; }       
        public string updateby { get; set; }    
        
                    

    }
}