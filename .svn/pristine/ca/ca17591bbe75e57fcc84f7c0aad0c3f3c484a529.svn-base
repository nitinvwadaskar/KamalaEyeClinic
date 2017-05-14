using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace Kamala_Eye_Clinic.Models
{
    public class RegistrationDetails
    {
        public int patient_id;
        public DateTime reg_date;
        public DateTime reg_time;

        public string title { get; set; }
        public string first_name { get; set; }
        public string middle_name { get; set; }
        public string last_name { get; set; }
        public DateTime birth_date { get; set; }
        public string birth_place { get; set; }
        public int age { get; set; }
        public Gender Gender { get; set; }
        public string marital_status { get; set; }
        public string blood_group { get; set; }
        public string height { get; set; }
        public string weight { get; set; }
        public string responsible_person { get; set; }
        public string contact_no { get; set; }



        SqlConnection con = new SqlConnection("Server=.\\SQLExpress; integrated security=true; database=Hospital");
        SqlCommand cmd = new SqlCommand();
        SqlDataReader dr;


        public void InsertDetails(RegistrationDetails obj)
        {

            cmd.CommandText = "select patient_id+1 from Patient_Master where patient_id=(select max(patient_id) from Patient_Master)";
            cmd.Connection = con;

            try {
                con.Open();
                dr = cmd.ExecuteReader();
                if(dr.Read())
                {
                    patient_id = int.Parse(dr[0].ToString());
                }
                else
                {
                    patient_id = 1;
                }
                con.Close();
            }
            catch(Exception ex)
            {
                throw ex;
            }

            cmd.CommandText = "Insert Into Patient_Master Values ("+patient_id+",'"+reg_date.ToShortDateString()+"','"+reg_time.ToShortTimeString()+"','"+obj.title+"','"+obj.first_name+"','"+obj.middle_name+"','"+obj.last_name+"','"+obj.birth_date+"','"+obj.birth_place+"',"+obj.age+",'"+obj.Gender+"','"+obj.marital_status+"','"+obj.blood_group+"','"+obj.height+"','"+obj.weight+"','"+obj.responsible_person+"','"+obj.contact_no+"')";
            cmd.Connection = con;
            try {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();                
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }

    }

    public enum Gender
    {
        Male,
        Female
    }
}