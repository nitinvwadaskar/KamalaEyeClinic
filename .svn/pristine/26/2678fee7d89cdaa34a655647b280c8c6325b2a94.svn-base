using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Kamala_Eye_Clinic.Report
{
    public partial class ReportView : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string rptName = "Report1";
            ReportViewer1.SizeToReportContent = true;
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Report/" + rptName+".rdlc");
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportDataSource rs = new ReportDataSource("KECDataSet", GetData());
            ReportViewer1.LocalReport.DataSources.Add(rs);
            ReportViewer1.LocalReport.Refresh();

        }
        private DataSet GetData()
        {
            Kamala_Eye_Clinic.Models.DBConnection con = new Models.DBConnection();
            DataSet ds= con.ExecuteQuery("select * from tbl_patientmst");
            return ds;
        }
    }
}