using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace Kamala_Eye_Clinic.Models
{
    public class Common
    {
        public static string ToXML(object obj)
        {
            var stringwriter = new System.IO.StringWriter();
            var serializer = new XmlSerializer(obj.GetType());
            serializer.Serialize(stringwriter, obj);
            return stringwriter.ToString();
        }

        public static string[] CallbackMsg(DataSet ds)
        {
            string[] str = new string[4];
            if(ds!=null)
            {
                str[0] = "Success";
                str[1] = ds.Tables[0].Rows[0][1].ToString();
                str[2] = ds.Tables[0].Rows[0][1].ToString();
                str[3] = ds.Tables[0].Rows[0][1].ToString();
            }
            else
            {
                str[0] = "Failed";
                str[1] = ds.Tables[0].Rows[0][1].ToString();
                str[2] = ds.Tables[0].Rows[0][1].ToString();
                str[3] = ds.Tables[0].Rows[0][1].ToString();
            }
            return str;
        }
    }
}