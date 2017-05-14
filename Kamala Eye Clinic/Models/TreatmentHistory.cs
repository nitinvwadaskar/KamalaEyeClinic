﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Kamala_Eye_Clinic.Models
{
    public class TreatmentHistory
    {
        public int treatmentid { get; set; }
        public int patientid { get; set; }
        public int historyid { get; set; }
        public int srno { get; set; }

        [Display(Name = "History:")]
        public string historyname { get; set; }

        [Display(Name = "From:")]
        public string from_dt { get; set; }

        [Display(Name = "To:")]
        public string to_dt { get; set; }
    }
}