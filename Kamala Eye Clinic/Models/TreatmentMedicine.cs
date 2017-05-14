using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Kamala_Eye_Clinic.Models
{
    public class TreatmentMedicine
    {
        public int treatmentid { get; set; }
        public int patientid { get; set; }
        public int medicineid { get; set; }
        public int srno { get; set; }

        [Required(ErrorMessage = "Particular is Required")]
        [Display(Name = "Particular")]
        public string particular { get; set; }

        [Required(ErrorMessage = "Quantity is Required")]
        [Display(Name = "Quantity")]
        public int qty { get; set; }

        [Display(Name = "M")]
        public string m { get; set; }
        public bool mischecked { get; set; }

        [Display(Name = "A")]
        public string a { get; set; }
        public bool aischecked { get; set; }

        [Display(Name = "E")]
        public string e { get; set; }
        public bool eischecked { get; set; }

        [Display(Name = "N")]
        public string n { get; set; }
        public bool nischecked { get; set; }
    }
}