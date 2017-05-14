using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;

namespace Kamala_Eye_Clinic.Models
{
    public class TreatmentFundus
    {
        public int treatmentid { get; set; }
        public int patientid { get; set; }
        public int fundusid { get; set; }
        public int srno { get; set; }

        [Display(Name = "Fundus:")]
        public string fundusname { get; set; }

        public List<TreatmentFundus> GetList()
        {
            FundusMaster f = new FundusMaster();
            List < FundusMaster > fmst = f.GetAllFundus();

            List<TreatmentFundus> fundus = new List<TreatmentFundus>();
           
            for (int i = 0; i <= fmst.Count - 1; i++)
            {
                TreatmentFundus tf = new TreatmentFundus();
                tf.fundusid = fmst[i].fundusid;
                tf.fundusname = fmst[i].fundusname;
                fundus.Add(tf);
            }            
            return fundus;
        }
    }   
}