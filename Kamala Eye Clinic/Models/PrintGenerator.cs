
using System;
using System.Data;
using System.IO;
using System.Text;
using System.Web;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using System.Web.UI;

namespace Kamala_Eye_Clinic.Models
{
    public class PrintGenerator
    {
        public static string GenerateBillPDF(int billid, int patientid, int hid, DateTime billdate)
        {
            //Dummy data for Invoice (Bill).
            string HospitalName = "Kamla Eye Clinic";
            int billNo = billid;
            PatientBill obj = new PatientBill();
            DBConnection cn = new DBConnection();
            obj.CMD = "View1";
            obj.hid = hid;
            obj.billid = billid;
            obj.patientid = patientid;
            string xml = Common.ToXML(obj);
            DataSet dataset = cn.ExecuteProcedure("Sp_PatientFinalBill", xml);

            PatientMaster pm = new PatientMaster();
            pm.CMD = "View1";
            pm.hid = hid;
            pm.patientid = patientid;
            string xml1 = Common.ToXML(pm);
            DataSet ds = cn.ExecuteProcedure("SP_PatientRegistration", xml1);
            string patientname = ds.Tables[0].Rows[0]["title"] + " " + ds.Tables[0].Rows[0]["fname"] + " " + ds.Tables[0].Rows[0]["mname"] + " " + ds.Tables[0].Rows[0]["lname"];
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                {
                    StringBuilder sb = new StringBuilder();

                    //Generate Invoice (Bill) Header.
                    sb.Append("<div style='font-family:verdana'>");

                    sb.Append("<table width='600px'>");

                    sb.Append("<tr style='font-size:30px' colspan=2 align='center'><td>");
                    sb.Append(HospitalName);
                    sb.Append("</td></tr>");

                    sb.Append("<tr style='font-size:14px'><td>Bill Date #:");
                    sb.Append(billdate);
                    sb.Append("</td><td>Bill No #:");
                    sb.Append(billNo);
                    sb.Append("</td></tr></table><br/>");

                    sb.Append("<table border='1' width='650px' style='font-size:13px; border-collapse: collapse;'>");
                    sb.Append("<tr><td colspan = '4' style = 'background-color:#ddd; font-weight:bold'>PATIENT & HOSPITAL DETAILS</td></tr>");
                    sb.Append("<tr><td>Patient Name:</td><td>");
                    sb.Append(patientname);
                    sb.Append("</td><td>Hospital No:</td><td>");
                    sb.Append(hid);
                    sb.Append("</td></tr>");

                    sb.Append("<tr><td> Patient Age:</td><td> ");
                    sb.Append(ds.Tables[0].Rows[0]["age"]);
                    sb.Append("</td><td>Bed No:</td><td>");
                    sb.Append("-");
                    sb.Append("</td></tr>");

                    sb.Append("<tr><td> Address:</td><td>");
                    sb.Append("-");
                    sb.Append("</td><td>Admission Date:</td><td>");
                    sb.Append("-");
                    sb.Append("</td></tr>");

                    sb.Append("<tr><td> Consultant :</td><td>");
                    sb.Append("-");
                    sb.Append("</td><td>Discharge Date:</td><td>");
                    sb.Append("-");
                    sb.Append("</td></tr>");

                    sb.Append("</table><br/>");

                    //Generate Invoice (Bill) Items Grid.
                    sb.Append("<table border='1' width='650px' style='font-size:13px; border-collapse: collapse;'>");
                    sb.Append("<tr style='background-color:#ddd; font-weight:bold'><td>SR#</td><td>PARTICULARS#</td><td>QUANTITY#</td><td>RATE#</td><td>AMOUNT#</td></tr>");
                    decimal totalamt = 0;
                    for (int i = 0; i <= dataset.Tables[0].Rows.Count - 1; i++)
                    {
                        sb.Append("<tr>");
                        sb.Append("<td>");
                        sb.Append(dataset.Tables[0].Rows[i]["srno"]);
                        sb.Append("</td>");

                        sb.Append("<td>");
                        sb.Append(dataset.Tables[0].Rows[i]["particularname"]);
                        sb.Append("</td>");

                        sb.Append("<td>");
                        sb.Append(dataset.Tables[0].Rows[i]["qty"]);
                        sb.Append("</td>");

                        sb.Append("<td>");
                        sb.Append(Convert.ToDecimal(dataset.Tables[0].Rows[i]["charges"]));
                        sb.Append("</td>");

                        sb.Append("<td>");
                        sb.Append(Convert.ToDecimal(dataset.Tables[0].Rows[i]["subtotal"]));
                        sb.Append("</td>");
                        sb.Append("</tr>");
                        totalamt = totalamt + Convert.ToDecimal(dataset.Tables[0].Rows[i]["subtotal"]);
                    }
                    sb.Append("<tr>");
                    sb.Append("<td colspan = 3>");

                    sb.Append("<table style = 'font-size:13px'>");
                    sb.Append("<tr><td>COMMENTS/INSTRUCTIONS</td><tr>");
                    sb.Append("<tr><td>1</td></tr>");
                    sb.Append("<tr><td>2</td></tr>");
                    sb.Append("<tr><td>3</td></tr>");
                    sb.Append("<tr><td>4</td></tr>");
                    sb.Append("<tr><td>5</td></tr>");
                    sb.Append("</table></td>");

                    sb.Append("<td style = 'background-color:#ddd; font-weight:bold'>Total:</td>");
                    sb.Append("<td style = 'background-color:#ddd; font-weight:bold'>");
                    sb.Append(totalamt);
                    sb.Append("</td>");
                    sb.Append("</tr>");
                    sb.Append("</table><br/><span style='font-size:11px'>## This is computer generated print there no need of signaure.</span>");
                    sb.Append("</div>");

                    return sb.ToString();

                    ////Export HTML String as PDF.
                    //StringReader sr = new StringReader(sb.ToString());
                    //Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
                    //HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
                    //PdfWriter writer = PdfWriter.GetInstance(pdfDoc, HttpContext.Current.Response.OutputStream);
                    //pdfDoc.Open();
                    //htmlparser.Parse(sr);
                    //pdfDoc.Close();
                    //HttpContext.Current.Response.ContentType = "application/pdf";
                    //HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=Invoice_" + billNo + ".pdf");
                    //HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    //HttpContext.Current.Response.Write(pdfDoc);
                    //HttpContext.Current.Response.End();
                }
            }
        }

        public static string GenerateOTBillPDF()
        {
            return "";
        }

        public static string GenerateTreatmentPDF()
        {

            string HospitalName = "Kamla Eye Clinic";
            //Get Treatment Details
            Treatment obj = new Treatment();
            obj.CMD = "View1";
            obj.treatmentid = Convert.ToInt32(HttpContext.Current.Session["treatmentid"]);
            obj.patientid = Convert.ToInt32(HttpContext.Current.Session["patientid"]);
            obj.hid = Convert.ToInt32(HttpContext.Current.Session["hospital"]);
            string xml = Common.ToXML(obj);
            DBConnection con = new DBConnection();
            DataSet d = con.ExecuteProcedure("SP_Treatment", xml);

            //Get Patient Details
            PatientMaster pm = new PatientMaster();
            pm.CMD = "View1";
            pm.hid = Convert.ToInt32(HttpContext.Current.Session["hospital"]);
            pm.patientid = Convert.ToInt32(HttpContext.Current.Session["patientid"]);
            string xml1 = Common.ToXML(pm);
            DataSet ds = con.ExecuteProcedure("SP_PatientRegistration", xml1);
            string patientname = ds.Tables[0].Rows[0]["title"] + " " + ds.Tables[0].Rows[0]["fname"] + " " + ds.Tables[0].Rows[0]["mname"] + " " + ds.Tables[0].Rows[0]["lname"];

            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                {
                    StringBuilder sb = new StringBuilder();

                    //Generate Invoice (Treatment) Header.
                    sb.Append("<div style='font-family:verdana'>");

                    sb.Append("<table width='700px'>");

                    sb.Append("<tr style='font-size:30px'><td colspan=2>");
                    sb.Append(HospitalName);
                    sb.Append("</td></tr>");

                    sb.Append("<tr style='font-size:14px'><td>Treatment Date #:");
                    sb.Append(d.Tables[0].Rows[0]["create_dt"]);
                    sb.Append("</td><td>Treatment Id #:");
                    sb.Append(d.Tables[0].Rows[0]["treatmentid"]);
                    sb.Append("</td></tr></table><br/>");

                    sb.Append("<table border='1' width='700px' style='font-size:13px; border-collapse:collapse;'>");
                    sb.Append("<tr><td colspan ='4' style='background-color:#ddd; font-weight:bold'> PATIENT DETAILS </td></tr>");
                    sb.Append("<tr><td>Patient Name:</td><td>");
                    sb.Append(patientname);
                    sb.Append("</td><td>Patient Id:</td><td>");
                    sb.Append(ds.Tables[0].Rows[0]["patientid"]);
                    sb.Append("</td></tr>");

                    sb.Append("<tr><td>Patient Age:</td><td >");
                    sb.Append(ds.Tables[0].Rows[0]["age"]);
                    sb.Append("</td><td>Consultant</td><td>");
                    sb.Append(d.Tables[0].Rows[0]["dfname"] + "&nbsp;" + d.Tables[0].Rows[0]["dlname"]);
                    sb.Append("</td></tr>");

                    sb.Append("<tr><td>Address:</td>");
                    sb.Append("<td>-</td>");
                    sb.Append("<td>Contact No :</td><td>");
                    sb.Append(ds.Tables[0].Rows[0]["contact"]);
                    sb.Append("</td></tr>");
                    sb.Append("</table><br/>");

                    //Generate Treatment
                    sb.Append("<table border='0' width='700px' style='font-size:13px; border-collapse: collapse;'>");
                    sb.Append("<tr style = 'background-color:#ddd; font-weight:bold'>");


                    // Investigations
                    sb.Append("<td colspan ='5'> Investigations :</td></tr>");
                    sb.Append("<tr><td colspan ='5'>");
                    for (int i = 0; i <= d.Tables[3].Rows.Count - 1; i++)
                    {
                        if (i != 0)
                        {
                            sb.Append("&nbsp;&nbsp;");
                        }
                        sb.Append(i + 1);
                        sb.Append(".&nbsp;");
                        sb.Append(d.Tables[3].Rows[i]["diseasename"]);
                        if (i == d.Tables[3].Rows.Count - 1)
                        {
                            sb.Append(".");
                        }
                    }
                    sb.Append("</td></tr>");

                    //Presenting Complains
                    sb.Append("<tr style = 'background-color:#ddd; font-weight:bold'>");
                    sb.Append("<td colspan ='5'> Presenting Complains :</td></tr>");
                    sb.Append("<tr><td colspan ='5'>");
                    for (int i = 0; i <= d.Tables[5].Rows.Count - 1; i++)
                    {
                        if (i != 0)
                        {
                            sb.Append("&nbsp;&nbsp;");
                        }
                        sb.Append(i + 1);
                        sb.Append(".&nbsp;");
                        sb.Append(d.Tables[5].Rows[i]["complentsname"]);
                        if (i == d.Tables[5].Rows.Count - 1)
                        {
                            sb.Append(".");
                        }
                    }
                    sb.Append("</td></tr>");


                    //Provisional Diagnosis                   
                    sb.Append("<tr style = 'background-color:#ddd; font-weight:bold'>");
                    sb.Append("<td colspan ='5'> Provisional Diagnosis :</td></tr>");
                    sb.Append("<tr><td colspan ='5'>");
                    for (int i = 0; i <= d.Tables[6].Rows.Count - 1; i++)
                    {
                        if (i != 0)
                        {
                            sb.Append("&nbsp;&nbsp;");
                        }
                        sb.Append(i + 1);
                        sb.Append(".&nbsp;");
                        sb.Append(d.Tables[6].Rows[i]["disgnosisname"]);
                        if (i == d.Tables[6].Rows.Count - 1)
                        {
                            sb.Append(".");
                        }
                    }
                    sb.Append("</td></tr>");

                    //Previous Disease   
                    sb.Append("<tr style='background-color:#ddd; font-weight:bold'>");

                    sb.Append("<td colspan ='5'> Previous Disease and Duration:</td></tr>");
                    sb.Append("<tr style ='font-weight:bold'><td>Sr.No#</td><td>Disease#</td><td>From Date#</td><td>To Date#</td></tr>");
                    sb.Append("<tr><td colspan='5'></td></tr>");
                    for (int i = 0; i <= d.Tables[8].Rows.Count - 1; i++)
                    {
                        sb.Append("<td>");
                        sb.Append(i + 1);
                        sb.Append("</td><td>");
                        sb.Append(d.Tables[8].Rows[i]["historyname"]);
                        sb.Append("</td><td>");
                        sb.Append(d.Tables[8].Rows[i]["from_dt"]);
                        sb.Append("</td><td>");
                        sb.Append(d.Tables[8].Rows[i]["to_dt"]);
                        sb.Append("</td>");
                    }
                    sb.Append("</tr>");

                    //Tn And Vn Details

                    sb.Append("<tr style = 'background-color:#ddd; font-weight:bold' >");
                    sb.Append("<td colspan ='5'> Tn And Vn :</td></tr>");
                    sb.Append("<tr><td></td><td><u>cgl</u></td><td>cgl</td><td>Using glasses</td><td>cph</td></tr>");

                    sb.Append("<tr><td colspan='5'></td></tr>");
                    for (int i = 0; i <= d.Tables[10].Rows.Count - 1; i++)
                    {

                        sb.Append("<tr><td>Vn-RE</td>");
                        sb.Append("<td>");
                        sb.Append(d.Tables[10].Rows[0]["vn_re_clg"]);
                        sb.Append("</td>");
                        sb.Append("<td>");
                        sb.Append(d.Tables[10].Rows[0]["vn_re_clg_v"]);
                        sb.Append("</td>");
                        sb.Append("<td>");
                        sb.Append(d.Tables[10].Rows[0]["vn_re_ug"]);
                        sb.Append("</td>");
                        sb.Append("<td>");
                        sb.Append(d.Tables[10].Rows[0]["vn_re_cph"]);
                        sb.Append("</td></tr>");


                        sb.Append("<tr><td>Vn-LE</td>");
                        sb.Append("<td>");
                        sb.Append(d.Tables[10].Rows[0]["vn_le_clg"]);
                        sb.Append("</td>");
                        sb.Append("<td>");
                        sb.Append(d.Tables[10].Rows[0]["vn_le_clg_v"]);
                        sb.Append("</td>");
                        sb.Append("<td>");
                        sb.Append(d.Tables[10].Rows[0]["vn_le_ug"]);
                        sb.Append("</td>");
                        sb.Append("<td>");
                        sb.Append(d.Tables[10].Rows[0]["vn_le_cph"]);
                        sb.Append("</td></tr>");


                        sb.Append("<tr><td>Tn-RE</td>");
                        sb.Append("<td>");
                        sb.Append(d.Tables[10].Rows[0]["tn_re_clg"]);
                        sb.Append("</td>");
                        sb.Append("<td>");
                        sb.Append(d.Tables[10].Rows[0]["tn_re_clg_t"]);
                        sb.Append("</td>");
                        sb.Append("<td>");
                        sb.Append(d.Tables[10].Rows[0]["vn_ug_add"]);
                        sb.Append("</td>");
                        sb.Append("<td>");
                        sb.Append(d.Tables[10].Rows[0]["tn_re_cph"]);
                        sb.Append("</td></tr>");

                        sb.Append("<tr><td>Tn-LE</td>");
                        sb.Append("<td>");
                        sb.Append(d.Tables[10].Rows[0]["tn_le_clg"]);
                        sb.Append("</td>");
                        sb.Append("<td>");
                        sb.Append(d.Tables[10].Rows[0]["tn_le_clg_t"]);
                        sb.Append("</td>");
                        sb.Append("<td>");
                        sb.Append(d.Tables[10].Rows[0]["tn_le_cph"]);
                        sb.Append("</td></tr>");
                    }

                    //S/L
                    sb.Append("<tr style = 'background-color:#ddd; font-weight:bold'>");
                    sb.Append("<td colspan ='5'> S/L :</td></tr>");
                    sb.Append("<tr><td colspan ='5'>");
                    for (int i = 0; i <= d.Tables[7].Rows.Count - 1; i++)
                    {
                        if (i != 0)
                        {
                            sb.Append("&nbsp;&nbsp;");
                        }
                        sb.Append(i + 1);
                        sb.Append(".&nbsp;");
                        sb.Append(d.Tables[7].Rows[i]["checkupname"]);
                        if (i == d.Tables[7].Rows.Count - 1)
                        {
                            sb.Append(".");
                        }
                    }
                    sb.Append("</td></tr>");

                    //Fundus (F)
                    sb.Append("<tr style = 'background-color:#ddd; font-weight:bold'>");
                    sb.Append("<td colspan ='5'> Fundus (F) :</td></tr>");
                    sb.Append("<tr><td colspan ='5'>");
                    for (int i = 0; i <= d.Tables[4].Rows.Count - 1; i++)
                    {
                        if (i != 0)
                        {
                            sb.Append("&nbsp;&nbsp;");
                        }
                        sb.Append(i + 1);
                        sb.Append(".&nbsp;");
                        sb.Append(d.Tables[4].Rows[i]["fundusname"]);
                        if (i == d.Tables[4].Rows.Count - 1)
                        {
                            sb.Append(".");
                        }
                    }
                    sb.Append("</td></tr>");


                    //Medicine   
                    sb.Append("<tr style='background-color:#ddd; font-weight:bold'>");

                    sb.Append("<td colspan ='5'> Previous Disease and Duration:</td></tr>");
                    sb.Append("<tr style ='font-weight:bold' align='center'><td>Sr.No #</td><td>Medicine #</td><td>Quantity #</td><td>Timeing #</td></tr>");
                    sb.Append("<tr><td colspan='5'></td></tr>");
                    for (int i = 0; i <= d.Tables[1].Rows.Count - 1; i++)
                    {
                        sb.Append("<tr align='center'><td>");
                        sb.Append(i + 1);
                        sb.Append("</td><td>");
                        sb.Append(d.Tables[1].Rows[i]["particular"]);
                        sb.Append("</td><td>");
                        sb.Append(d.Tables[1].Rows[i]["quantity"]);
                        sb.Append("</td><td>");
                        sb.Append("&nbsp;&nbsp;");
                        sb.Append(d.Tables[1].Rows[i]["m"]);
                        sb.Append("&nbsp;&nbsp;");
                        sb.Append(d.Tables[1].Rows[i]["a"]);
                        sb.Append("&nbsp;&nbsp;");
                        sb.Append(d.Tables[1].Rows[i]["e"]);
                        sb.Append("&nbsp;&nbsp;");
                        sb.Append(d.Tables[1].Rows[i]["n"]);
                        sb.Append("&nbsp;&nbsp;");
                        sb.Append("</td></tr>");
                    }

                    //Advice
                    sb.Append("<tr style = 'background-color:#ddd; font-weight:bold'>");
                    sb.Append("<td colspan ='5'> Advice:</td></tr>");
                    sb.Append("<tr><td colspan ='5'>");
                    for (int i = 0; i <= d.Tables[9].Rows.Count - 1; i++)
                    {
                        if (i != 0)
                        {
                            sb.Append("&nbsp;&nbsp;");
                        }
                        sb.Append(i + 1);
                        sb.Append(".&nbsp;");
                        sb.Append(d.Tables[9].Rows[i]["advicename"]);
                        if (i == d.Tables[9].Rows.Count - 1)
                        {
                            sb.Append(".");
                        }
                    }
                    sb.Append("</td></tr>");


                    //Reports
                    sb.Append("<tr style='background-color:#ddd; font-weight:bold'>");

                    sb.Append("<td colspan ='5'>Reports:</td></tr>");
                    sb.Append("<tr style ='font-weight:bold' align='center'><td>Sr.No#</td><td>Report#</td><td>date#</td></tr>");
                    sb.Append("<tr align='center'><td colspan='5'><td>");
                    for (int i = 0; i <= d.Tables[0].Rows.Count - 1; i++)
                    {
                        if (d.Tables[0].Rows[i]["isBloodTest"].ToString() == "Y")
                        {
                            sb.Append("</tr align='center'><td>");
                            sb.Append("1");
                            sb.Append("</td><td>");
                            sb.Append("Blood Test");
                            sb.Append("</td><td>");
                            sb.Append(d.Tables[0].Rows[i]["bloodtest_dt"]);
                            sb.Append("</td></tr>");
                        }

                        if (d.Tables[0].Rows[i]["isXRay"].ToString() == "Y")
                        {
                            sb.Append("</tr align='center'><td>");
                            sb.Append("2");
                            sb.Append("</td><td>");
                            sb.Append("X-Ray");
                            sb.Append("</td><td>");
                            sb.Append(d.Tables[0].Rows[i]["xray_dt"]);
                            sb.Append("</td></tr>");
                        }
                    }
                    sb.Append("</td></tr>");

                    //Tn And Vn Details

                    sb.Append("<tr style = 'background-color:#ddd; font-weight:bold' >");
                    sb.Append("<td colspan ='5'> Eye Vision :</td></tr>");

                    sb.Append("<tr><td colspan='5'>");
                    sb.Append("<table border='1'width='700px' style='border-collapse:collapse;'>");
                    sb.Append("<tr align='center'><td>Eye</td><td colspan = '3'>Right</td><td colspan ='3'>Left</td></tr>");

                    for (int i = 0; i <= d.Tables[2].Rows.Count - 1; i++)
                    {
                        sb.Append("<tr align='center'><td>Vn</td><td colspan ='3'>6/");
                        sb.Append(d.Tables[2].Rows[0]["right_vn"]);
                        sb.Append("</td><td colspan ='3'>6/");
                        sb.Append(d.Tables[2].Rows[0]["left_vn"]);
                        sb.Append("</td></tr>");
                        sb.Append("<tr align='center'><td></td><td>Sph.</td><td>Cyl.</td><td>Axis.</td><td>Sph.</td><td>Cyl.</td><td>Axis.</td></tr>");

                        sb.Append("<tr align='center'><td>Dist.</td><td>");
                        sb.Append(d.Tables[2].Rows[0]["right_dist_sph"]);
                        sb.Append("</td><td>");
                        sb.Append(d.Tables[2].Rows[0]["right_dist_cyl"]);
                        sb.Append("</td><td>");
                        sb.Append(d.Tables[2].Rows[0]["right_dist_axis"]);
                        sb.Append("</td><td>");
                        sb.Append(d.Tables[2].Rows[0]["left_dist_sph"]);
                        sb.Append("</td><td>");
                        sb.Append(d.Tables[2].Rows[0]["left_dist_cyl"]);
                        sb.Append("</td><td>");
                        sb.Append(d.Tables[2].Rows[0]["left_dist_axis"]);
                        sb.Append("</td></tr>");

                        sb.Append("<tr align='center'><td>Near.</td><td>");
                        sb.Append(d.Tables[2].Rows[0]["right_near_sph"]);
                        sb.Append("</td><td>");
                        sb.Append(d.Tables[2].Rows[0]["right_near_cyl"]);
                        sb.Append("</td><td>");
                        sb.Append(d.Tables[2].Rows[0]["right_near_axis"]);
                        sb.Append("</td><td>");
                        sb.Append(d.Tables[2].Rows[0]["left_near_sph"]);
                        sb.Append("</td><td>");
                        sb.Append(d.Tables[2].Rows[0]["left_near_cyl"]);
                        sb.Append("</td><td>");
                        sb.Append(d.Tables[2].Rows[0]["left_near_axis"]);
                        sb.Append("</td></tr>");
                    }

                    sb.Append("</table></td></tr>");

                    sb.Append("<tr><td colspan='5'></td></tr>");

                    //Followup
                    sb.Append("<tr style='background-color:#ddd; font-weight:bold'>");
                    sb.Append("<td colspan='2'>Follow-up Date :</td><td colspan='3'>");
                    for (int i = 0; i <= d.Tables[0].Rows.Count - 1; i++)
                    {
                        if (d.Tables[0].Rows[0]["isFollowup"].ToString() == "Y")
                        {
                            sb.Append(d.Tables[0].Rows[i]["followup_dt"]);
                        }
                        else
                        {
                            sb.Append("-");
                        }
                    }
                    sb.Append("</td></tr></table>");
                    sb.Append("<br><span style='font-size:11px'>## This is computer generated print there no need of signaure.</span>");
                    return sb.ToString();
                }
            }
        }

        public static string GenerateDischargePDF()
        {
            //Get Patient Details
            string HospitalName = "Kamla Eye Clinic";
            DBConnection con = new DBConnection();
            PatientMaster pm = new PatientMaster();
            pm.CMD = "View1";
            pm.hid = Convert.ToInt32(HttpContext.Current.Session["hospital"]);
            pm.patientid = Convert.ToInt32(HttpContext.Current.Session["patientid"]);
            string xml1 = Common.ToXML(pm);
            DataSet ds = con.ExecuteProcedure("SP_PatientRegistration", xml1);
            string patientname = ds.Tables[0].Rows[0]["title"] + " " + ds.Tables[0].Rows[0]["fname"] + " " + ds.Tables[0].Rows[0]["mname"] + " " + ds.Tables[0].Rows[0]["lname"];


            //Get Discharge Details
            Discharge obj = new Discharge();
            obj.CMD = "View1";
            obj.dischargeid = Convert.ToInt32(HttpContext.Current.Session["dischargeid"]);
            obj.patientid = Convert.ToInt32(HttpContext.Current.Session["patientid"]);
            obj.hid = Convert.ToInt32(HttpContext.Current.Session["hospital"]);
            string xml = Common.ToXML(obj);
            DataSet d = con.ExecuteProcedure("SP_Discharge", xml);


            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                {
                    StringBuilder sb = new StringBuilder();

                    //Generate Header.
                    sb.Append("<div style='font-family:verdana'>");

                    sb.Append("<table width='700px'>");

                    sb.Append("<tr style='font-size:30px' align='center'><td colspan=2>");
                    sb.Append(HospitalName);
                    sb.Append("</td></tr>");
                    sb.Append("<tr><td colspan='2'></td></tr>");
                    sb.Append("<tr style='font-size:20px' align='center'><td colspan=2>");
                    sb.Append("Discharge Card");
                    sb.Append("</td></tr>");
                    sb.Append("<tr><td colspan='2'></td></tr>");
                    sb.Append("<tr style='font-size:18px'><td style='font-weight:bold'>Discharge Date #:");
                    sb.Append(d.Tables[0].Rows[0]["discharge_dt"]);
                    sb.Append("</td><td style='font-weight:bold'>Discharge Id #:");
                    sb.Append(d.Tables[0].Rows[0]["dischargeid"]);
                    sb.Append("</td></tr></table><br/>");

                    sb.Append("<table border='1' width='700px' style='font-size:18px; border-collapse:collapse;'>");
                    sb.Append("<tr><td colspan ='4' style='background-color:#ddd; font-weight:bold'> PATIENT DETAILS </td></tr>");
                    sb.Append("<tr><td>Patient Name:</td><td style='font-weight:bold'>");
                    sb.Append(patientname);
                    sb.Append("</td><td>Patient Id:</td><td style='font-weight:bold'>");
                    sb.Append(ds.Tables[0].Rows[0]["patientid"]);
                    sb.Append("</td></tr>");

                    sb.Append("<tr><td>Patient Age:</td><td >");
                    sb.Append(ds.Tables[0].Rows[0]["age"]);
                    sb.Append("</td><td>Consultant</td><td>");
                    sb.Append(d.Tables[0].Rows[0]["doctorincharge"]);
                    sb.Append("</td></tr>");

                    sb.Append("<tr><td>Address:</td>");
                    sb.Append("<td>-</td>");
                    sb.Append("<td>Contact No :</td><td>");
                    sb.Append(ds.Tables[0].Rows[0]["contact"]);
                    sb.Append("</td></tr>");
                    sb.Append("</table><br/>");

                    //Generate Discharge card 
                    sb.Append("<table border='1' width='700px' style='font-size:18px; border-collapse: collapse;'>");
                    sb.Append("<tr><td colspan ='2' style='background-color:#ddd; font-weight:bold'>DISCHARGE SUMMARY</td></tr>");
                    DateTime dt = Convert.ToDateTime(d.Tables[1].Rows[0]["otdate"]);
                    sb.Append("<tr><td>ADMIT DATE:</td><td>");
                    sb.Append(dt.ToShortDateString());
                    sb.Append("</td></tr>");
                    sb.Append("<tr><td>ADMIT TIME:</td><td>");
                    sb.Append(dt.ToShortTimeString());
                    sb.Append("</td></tr>");
                    DateTime ddt=Convert.ToDateTime(d.Tables[0].Rows[0]["discharge_dt"]);
                    sb.Append("<tr><td>DISCHARGE DATE:</td><td>");
                    sb.Append(ddt.ToShortDateString());
                    sb.Append("</td></tr>");
                    sb.Append("<tr><td>DISCHARGE TIME:</td><td>");
                    sb.Append(ddt.ToShortTimeString());
                    sb.Append("</td></tr>");
                    sb.Append("<tr><td>EYE:</td><td>");
                    sb.Append(d.Tables[0].Rows[0]["eye"]);
                    sb.Append("</td></tr></table><br/>");

                    //Pre Operative Medicine
                    sb.Append("<table border='1' width='700px' style='font-size:18px; border-collapse: collapse;'>");
                    sb.Append("<tr><td colspan='2' style='background-color:#ddd; font-weight:bold'>PRE OPERATIVE MEDICINE</td></tr>");
                    sb.Append("<tr style ='font-size:18px'>");
                    sb.Append("<td>");
                    sb.Append("<ol Start='1'>");
                    sb.Append("<li>MOXISURGE E/D</li>");
                    sb.Append("<li>SUNEPHRINE-H</li>");
                    sb.Append("<li>BROMVUE</li>");
                    sb.Append("<ol>");
                    sb.Append("</td>");

                    sb.Append("<td style ='font-size:14px'>");
                    sb.Append("<ul>");
                    sb.Append("<li>000</li>");
                    sb.Append("<li>(2Hrs. BEFORE SURGERY)</li>");
                    sb.Append("<li>EVERY 10 MIN. ALTERNATE</li>");
                    sb.Append("</ul>");
                    sb.Append("</td>");
                    sb.Append("</tr>");

                    sb.Append("<tr>");
                    sb.Append("<td>");
                    sb.Append("<ol Start='4'>");
                    sb.Append("<li>TAB RANTAC 150MG 1-1</li>");
                    sb.Append("<li>TAB ZOXAN 500MG 1-1</li>");
                    sb.Append("<li>TAB I MEG-3</li>");
                    sb.Append("</ol>");
                    sb.Append("</td>");
                    sb.Append("<td style ='font-size:14px'>");
                    sb.Append("<ul>");
                    sb.Append("<li>(1 DAY BEFORE SURGERY)</li>");
                    sb.Append("<li>AFTER FOOD</li>");
                    sb.Append("<li>(1 DAY BEFORE SURGERY)</li>");
                    sb.Append("</ul>");
                    sb.Append("</td>");
                    sb.Append("</tr>");
                    sb.Append("</table>");


                    //Post Operative  Medicine

                    sb.Append("<br/>");

                    sb.Append("<table border='1' width='700px' style='font-size:18px; border-collapse: collapse;'>");
                    sb.Append("<tr><td colspan='2' style='background-color:#ddd; font-weight:bold'>POST OPERATIVE MEDICINE</td></tr>");
                    sb.Append("<tr>");
                    sb.Append("<td>");
                    sb.Append("<ol Start='1'>");
                    sb.Append("<li>PREOMET E/D 00</li>");
                    sb.Append("<li>MOXISURGE E/D 00</li>");
                    sb.Append("<li>BROMVUE</li>");
                    sb.Append("<li>TAB RANTAC 150MG 1-1</li>");
                    sb.Append("<li>TAB ZOXAN 500MG 1-1</li>");
                    sb.Append("<li>TAB I MEG-3 OD</li>");
                    sb.Append("<li>TAB SHREELOC / ANP PLUS</li>");
                    sb.Append("</ol>");
                    sb.Append("</td>");
                    sb.Append("<td>");
                    sb.Append("<ul>");
                    sb.Append("<li>35 DAYS</li>");
                    sb.Append("<li>10 DAYS</li>");
                    sb.Append("<li>10 DAYS</li>");
                    sb.Append("<li>2 DAYS</li>");
                    sb.Append("<li>2 DAYS</li>");
                    sb.Append("<li>9 DAYS</li>");
                    sb.Append("<li>1 SOS</li>");
                    sb.Append("</ul>");
                    sb.Append("</td>");
                    sb.Append("</tr>");
                    sb.Append("</table>");
                    sb.Append("<br><span style='font-size:11px'>## This is computer generated print there no need of signaure.</span>");

                    return sb.ToString();
                }
            }
        }

        public static string GenerateConsentPDF()
        {
            //Get Patient Details
            string HospitalName = "Kamla Eye Clinic";
            DBConnection con = new DBConnection();
            PatientMaster pm = new PatientMaster();
            pm.CMD = "View1";
            pm.hid = Convert.ToInt32(HttpContext.Current.Session["hospital"]);
            pm.patientid = Convert.ToInt32(HttpContext.Current.Session["patientid"]);
            string xml1 = Common.ToXML(pm);
            DataSet ds = con.ExecuteProcedure("SP_PatientRegistration", xml1);
            string patientname = ds.Tables[0].Rows[0]["title"] + " " + ds.Tables[0].Rows[0]["fname"] + " " + ds.Tables[0].Rows[0]["mname"] + " " + ds.Tables[0].Rows[0]["lname"];


            //Get Consent Details
            ConsentMaster obj = new ConsentMaster();
            obj.CMD = "View1";
            obj.consentid = Convert.ToInt32(HttpContext.Current.Session["consentid"]);
            obj.patientid = Convert.ToInt32(HttpContext.Current.Session["patientid"]);
            obj.hid = Convert.ToInt32(HttpContext.Current.Session["hospital"]);
            string xml = Common.ToXML(obj);
            DataSet d = con.ExecuteProcedure("SP_Consent", xml);

            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                {
                    StringBuilder sb = new StringBuilder();

                    sb.Append("<div style='font-family:verdana'>");

                    sb.Append("<table width='700px'>");

                    sb.Append("<tr style='font-size:30px' align='center'><td colspan=2>");
                    sb.Append(HospitalName);
                    sb.Append("</td></tr>");
                    sb.Append("<tr><td colspan='2'></td></tr>");
                    sb.Append("<tr style='font-size:20px' align='center'><td colspan=2>");
                    sb.Append("Consent Form");
                    sb.Append("</td></tr>");
                    sb.Append("<tr><td colspan='2'></td></tr>");
                    sb.Append("<tr style='font-size:18px'><td style='font-weight:bold'>Consent Date #:");
                    sb.Append(d.Tables[0].Rows[0]["create_dt"]);
                    sb.Append("</td><td style='font-weight:bold'>Consent Id #:");
                    sb.Append(d.Tables[0].Rows[0]["consentid"]);
                    sb.Append("</td></tr></table><br/>");

                    sb.Append("<table border='0' width='700px' style='font-size:22px; border-collapse: collapse;'>");
                    sb.Append("<tr><td>Patient Name:</td><td>");
                    sb.Append(patientname);
                    sb.Append("</td></tr><tr><td>Age:</td><td>");
                    sb.Append(ds.Tables[0].Rows[0]["age"]);
                    sb.Append("</td></tr><tr><td>Patient Signature:</td><td>____________________________</td></tr>");
                    sb.Append("<tr><td>Visitors Name:</td><td>");
                    sb.Append(d.Tables[0].Rows[0]["visitorname"]);
                    sb.Append("</td></tr><tr><td>Witness Signature:</td><td>____________________________</td></tr>");

                    DateTime dt = Convert.ToDateTime(d.Tables[0].Rows[0]["create_dt"].ToString());
                    sb.Append("<tr><td>Date:</td><td>");
                    sb.Append(dt.ToLongDateString());
                    sb.Append("</td></tr><tr><td>Time:</td><td>");
                    sb.Append(dt.ToShortTimeString());
                    sb.Append("</td></tr><tr><td>Place:</td><td>");
                    sb.Append(d.Tables[0].Rows[0]["place"]);
                    sb.Append("</td></tr></table>");
                    sb.Append("<br/><br/><br/>");
                    sb.Append("<table border='0' width='700px' style='font-size:22px; border-collapse: collapse;'>");
                    sb.Append("<tr><td>Sergeon Doctor's Name:</td><td>DR.RAJENDRA JAIN</td></tr>");
                    sb.Append("<tr><td>Sergeon Doctor's Address:</td><td>G-4, Indira Complex, 60 feet Road, Bhayandar(West), Dist. Thane</td></tr>");
                    sb.Append("<tr><td>Doctor's Signature:</td><td>____________________________</td></tr>");
                    sb.Append("</table>");
                    sb.Append("<br/><br/><br/>");

                    sb.Append("<table border='0' width='700px' style='font-size:22px; border-collapse: collapse;'>");
                    sb.Append("<tr><td colspan='2'>If required your physician will afurnish any additional information you require to be certain you are fully informed about the operation and the lens.</td></tr>");
                    sb.Append("<tr><td colspan='2'>_______________________________________________________________________</td></tr>");
                    sb.Append("<tr><td colspan='2'>_______________________________________________________________________</td></tr>");
                    sb.Append("<tr><td colspan='2'>_______________________________________________________________________</td></tr>");
                    sb.Append("<tr><td colspan='2'>_______________________________________________________________________</td></tr>");
                    sb.Append("</table>");
                    return sb.ToString();
                }
            }
        }


    }
}