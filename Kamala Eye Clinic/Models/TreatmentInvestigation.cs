using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Kamala_Eye_Clinic.Models
{
    public class TreatmentInvestigation
    {
        public int invId { get; set; }
        public string invName { get; set; }
        public bool isChecked { get; set; }

        public List<TreatmentInvestigation> GetList()
        {
            return new List<TreatmentInvestigation>()
            {
                new TreatmentInvestigation() {invId=1,invName="CBC",isChecked=false },
                new TreatmentInvestigation() {invId=2,invName="HIV",isChecked=false },
                new TreatmentInvestigation() {invId=3,invName="UrineComp",isChecked=false },
                new TreatmentInvestigation() {invId=4,invName="R.B.Sugar",isChecked=false },
                new TreatmentInvestigation() {invId=5,invName="HbSag Disease",isChecked=false },
                new TreatmentInvestigation() {invId=6,invName="ESR",isChecked=false },
                new TreatmentInvestigation() {invId=7,invName="MT",isChecked=false },
                new TreatmentInvestigation() {invId=8,invName="Fitness Certificate",isChecked=false },
                new TreatmentInvestigation() {invId=9,invName="A/Scan",isChecked=false }
               
            };
        }
        


    }
}