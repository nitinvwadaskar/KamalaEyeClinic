using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Kamala_Eye_Clinic.Models
{
    public class TreatmentDiagnosis
    {
        public int treatmentid { get; set; }
        public int patientid { get; set; }
        public int diagnosisid { get; set; }
        public int srno { get; set; }

        [Display(Name = "Presenting Diagnosis:")]
        public string diagnosisname { get; set; }
    }
}