using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Kamala_Eye_Clinic.Models
{
    public class TreatmentDisease
    {
       public int treatmentid { get; set; }
       public int patientid { get; set; }
       public int srno { get; set; }

        [Required(ErrorMessage = "Disease Name is Required")]
        [Display(Name = "Disease Name:")]
        public string diseasename { get; set; }       
    }
}