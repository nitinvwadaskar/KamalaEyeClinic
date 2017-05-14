using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kamala_Eye_Clinic.Models
{
    public class FollowupMaster
    {
        public int hid { get; set; }
        public int followupid { get; set; }
        public int patientid { get; set; }
        public DateTime create_dt { get; set; }
        public string createdby { get; set; }
        public DateTime update_dt { get; set; }
        public string updatedby { get; set; }


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

        public List<FollowupMaster> ShowAllFollowupPatient()
        {
            return null;
        }
    }
}