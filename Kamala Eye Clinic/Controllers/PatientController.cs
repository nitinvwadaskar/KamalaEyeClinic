﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kamala_Eye_Clinic.Models;
using System.Data;
using System.Data.SqlClient;

namespace Kamala_Eye_Clinic.Controllers
{
    [Sessionclass]
    public class PatientController : Controller
    {

        /****************************************** Patient Registration  **************************************/


        public ViewResult Registration()
        {
            PatientMaster obj = new PatientMaster();
            return View(obj);
        }

        public ViewResult Update(int patientid)
        {
            PatientMaster obj = new PatientMaster();

            if (patientid > 0)
            {
                obj.CMD = "Update";
                obj = obj.GetSinglePatient(patientid, Convert.ToInt32(Session["hospital"].ToString()));

            }
            return View("Registration", obj);
        }
        [HttpPost]
        public ActionResult Update(PatientMaster obj, string SubmitBtn)
        {
            Registration(obj, SubmitBtn);
            if (SubmitBtn == "Delete") return Redirect("~/Patient/PatientDetails");
            return View("Registration", obj);
        }

        [HttpPost]
        public ActionResult Registration(PatientMaster obj, string SubmitBtn)
        {
            obj.CMD = SubmitBtn == "Save" ? "Save" : SubmitBtn == "Update" ? "Update" : "Delete";
            obj.hid = Convert.ToInt32(Session["hospital"].ToString());
            switch (obj.CMD)
            {
                case "Save":
                    obj.hid = Convert.ToInt32(Session["hospital"].ToString());
                    string[] str = obj.PerformAction();
                    ViewBag.msg = str[1];
                    return View("Registration", obj);
                case "Update":
                    string[] str1 = obj.PerformAction();
                    ViewBag.msg = str1[2];
                    return View("Registration", obj);
                case "Delete":
                    string[] str2 = obj.PerformAction();
                    ViewBag.msg = str2[3];
                    return View("Registration", obj);
                default:
                    return View(obj);
            }

        }
        public ActionResult Select(int patientid)
        {
            PatientMaster obj = new PatientMaster();

            if (patientid > 0)
            {
                Session["PatientId"] = patientid;//hold only session id
                Session["PatientObj"] = obj = obj.GetSinglePatient(patientid, Convert.ToInt32(Session["hospital"].ToString()));//hold whole patient details            
            }
            return RedirectToAction("Dashboard", "Home");
        }

        public ActionResult PatientDetails()
        {
            PatientMaster obj = new PatientMaster();
            DBConnection cn = new DBConnection();
            obj.CMD = "View";
            obj.hid = Convert.ToInt32(Session["hospital"].ToString());
            string xml = Common.ToXML(obj);
            DataSet dataset = cn.ExecuteProcedure("SP_PatientView", xml);
            return View(dataset);
        }

        [HttpPost]
        public ActionResult PatientDetails(FormCollection form, string submitBtn)
        {
            switch (submitBtn)
            {
                case "Search":
                    PatientMaster obj = new Models.PatientMaster();
                    if (form["pid"] != "")
                    {
                        obj.patientid = Convert.ToInt32(form["pid"]);
                    }
                    else
                    {
                        obj.fname = form["fname"];
                        obj.lname = form["lname"];
                    }

                    obj.CMD = "View1";
                    obj.hid = Convert.ToInt32(Session["hospital"].ToString());
                    string xml = Common.ToXML(obj);
                    DBConnection cn = new DBConnection();
                    DataSet dataset = cn.ExecuteProcedure("SP_PatientView", xml);
                    if (dataset.Tables[0].Rows[0][1].ToString() == "No Record Found")
                    {
                        ViewBag.Msg = "Record Not Found";
                    }
                    return View(dataset);
                case "ViewAll":
                    return RedirectToAction("PatientDetails");
                default:
                    return View();
            }
        }

        public ActionResult PatientSelection()
        {
            return View();
        }


        /****************************************** Discharge Details    **************************************/


        public ViewResult Discharge()
        {
            Discharge obj = new Discharge();
            obj.GetMultipleDoctor();
            return View(obj);
        }

        public ViewResult DischargeUpdate(int dischargeid)
        {
            Discharge obj = new Discharge();

            if (dischargeid > 0)
            {
                obj.CMD = "Update";
                obj.patientid = Convert.ToInt32(Session["patientid"]);
                obj.hid = Convert.ToInt32(Session["hospital"]);
                obj.GetMultipleDoctor();
                obj = obj.GetSingleDischarge(dischargeid);
            }
            return View("Discharge", obj);
        }
        [HttpPost]
        public ActionResult DischargeUpdate(Discharge obj, string SubmitBtn)
        {
            Discharge(obj, SubmitBtn);
            if (SubmitBtn == "Delete") return Redirect("~/Patient/DischargeDetails");
            return View("Discharge", obj);
        }

        [HttpPost]
        public ActionResult Discharge(Discharge obj, string SubmitBtn)
        {

            obj.CMD = SubmitBtn == "Save" ? "Save" : SubmitBtn == "Update" ? "Update" : "Delete";
            switch (obj.CMD)
            {
                case "Save":
                    obj.hid = Convert.ToInt32(Session["hospital"].ToString());
                    obj.patientid = Convert.ToInt32(Session["patientid"].ToString());
                    string[] str = obj.PerformAction();
                    string[] arr = new string[2] { "success", str[1] };
                    ViewBag.msgarr = arr;
                    obj.GetMultipleDoctor();
                    return RedirectToAction("DischargeDetails");
                case "Update":
                    string[] str1 = obj.PerformAction();
                    string[] arr1 = new string[2] { "success", str1[2] };
                    ViewBag.msgarr = arr1;
                    obj.GetMultipleDoctor();
                    return RedirectToAction("DischargeDetails");
                case "Delete":
                    string[] str2 = obj.PerformAction();
                    ViewBag.msg = str2[3];
                    obj.GetMultipleDoctor();
                    return RedirectToAction("DischargeDetails");
                default:
                    return View(obj);
            }
        }

        public ViewResult DischargeDetails(Discharge obj)
        {
            DBConnection cn = new DBConnection();
            obj.CMD = "View";
            obj.hid = Convert.ToInt32(Session["hospital"].ToString());
            obj.patientid = Convert.ToInt32(Session["patientid"].ToString());
            string xml = Common.ToXML(obj);
            DataSet dataset = cn.ExecuteProcedure("SP_Discharge", xml);
            return View(dataset);
        }


        public ActionResult PrintDischarge(int dischargeid)
        {
            if (dischargeid > 0)
            {
                Session["dischargeid"] = dischargeid;
                string str = PrintGenerator.GenerateDischargePDF();
                TempData["Print"] = str;
                return RedirectToAction("Print", "Shared");
            }
            else
            {
                return RedirectToAction("Discharge", "Patient");
            }

        }

        /****************************************** Patient Appointment Details    **************************************/

        public ViewResult Appointment(Appointment obj)
        {
            obj.CMD = null;
            return View(obj);
        }

        [HttpPost]
        public ActionResult Appointment(Appointment obj, string SubmitBtn)
        {
            obj.CMD = SubmitBtn == "Save" ? "Save" : SubmitBtn == "Update" ? "Update" : "Delete";
            switch (obj.CMD)
            {
                case "Save":
                    obj.hid = Convert.ToInt32(Session["hospital"].ToString());
                    string[] str = obj.PerformAction();
                    ViewBag.msg = str[1];
                    return View("Appointment", obj);
                case "Update":
                    string[] str1 = obj.PerformAction();
                    ViewBag.msg = str1[2];
                    return View("Appointment", obj);
                case "Delete":
                    string[] str2 = obj.PerformAction();
                    ViewBag.msg = str2[3];
                    return View("Appointment", obj);
                default:
                    return View(obj);
            }
        }

        public ActionResult SetAppointment(int pid)
        {

            PatientMaster obj = new PatientMaster();

            if (pid > 0)
            {
                Session["PatientId"] = pid;//hold only session id
                Session["PatientObj"] = obj = obj.GetSinglePatient(pid, Convert.ToInt32(Session["hospital"].ToString()));//hold whole patient details            
            }
            return RedirectToAction("Appointment", "Patient", obj);
        }

        public ViewResult AppointmentUpdate(int hid, int appointmentid)
        {
            Appointment obj = new Appointment();

            if (appointmentid > 0)
            {
                obj.CMD = "Update";
                obj.appointmentid = appointmentid;
                obj.hid = hid;
                obj = obj.GetSingleAppointment(appointmentid);
            }
            return View("Appointment", obj);
        }
        [HttpPost]
        public ActionResult AppointmentUpdate(Appointment obj, string SubmitBtn)
        {

            Appointment(obj, SubmitBtn);
            if (SubmitBtn == "Delete") return Redirect("~/Patient/AppointmentDetails");
            return View("Appointment");
        }

        public ActionResult AppointmentDetails()
        {
            Appointment obj = new Models.Appointment();
            DBConnection cn = new DBConnection();
            obj.CMD = "View";
            obj.hid = Convert.ToInt32(Session["hospital"].ToString());
            string xml = Common.ToXML(obj);
            DataSet dataset = cn.ExecuteProcedure("SP_Appointment", xml);
            return View(dataset);
        }

        [HttpPost]
        public ActionResult AppointmentDetails(FormCollection form, string submitBtn)
        {
            switch (submitBtn)
            {
                case "Search":
                    Appointment obj = new Models.Appointment();

                    obj.fromdt = form["fromdate"];
                    obj.todt = form["todate"];
                    if (obj.fromdt != "" && obj.todt != "")
                    {
                        obj.CMD = "View2";
                        obj.hid = Convert.ToInt32(Session["hospital"].ToString());
                        string xml = Common.ToXML(obj);
                        DBConnection cn = new DBConnection();
                        DataSet dataset = cn.ExecuteProcedure("SP_Appointment", xml);
                        return View(dataset);
                    }
                    else
                    {
                        return RedirectToAction("AppointmentDetails");
                    }
                case "ViewAll":
                    return RedirectToAction("AppointmentDetails");
                default:
                    return View();
            }
        }
        /****************************************** Consent Form Details    **************************************/

        public ViewResult ConsentForm()
        {
            ConsentMaster obj = new ConsentMaster();
            return View(obj);
        }

        [HttpPost]
        public ActionResult ConsentForm(ConsentMaster obj, string SubmitBtn)
        {

            obj.CMD = SubmitBtn == "Save" ? "Save" : SubmitBtn == "Update" ? "Update" : "Delete";
            obj.hid = Convert.ToInt32(Session["hospital"].ToString());
            obj.patientid = Convert.ToInt32(Session["patientid"].ToString());
            switch (obj.CMD)
            {
                case "Save":
                    string[] str = obj.PerformAction();
                    ViewBag.msg = str[1];
                    return RedirectToAction("ConsentDetails");
                case "Update":

                    string[] str1 = obj.PerformAction();
                    ViewBag.msg = str1[2];
                    return RedirectToAction("ConsentDetails");
                case "Delete":
                    string[] str2 = obj.PerformAction();
                    ViewBag.msg = str2[3];
                    return RedirectToAction("ConsentDetails");
                default:
                    return View(obj);
            }
        }

        public ViewResult ConsentFormUpdate(int consentid)
        {
            ConsentMaster obj = new ConsentMaster();

            if (consentid > 0)
            {
                obj.CMD = "Update";
                obj.consentid = consentid;
                obj.hid = Convert.ToInt32(Session["hospital"]);
                obj = obj.GetSingleConsent(consentid);
            }
            return View("ConsentForm", obj);
        }
        [HttpPost]
        public ActionResult ConsentFormUpdate(ConsentMaster obj, string SubmitBtn)
        {
            ConsentForm(obj, SubmitBtn);
            if (SubmitBtn == "Delete") return Redirect("~/Patient/ConsentDetails");
            return View("ConsentForm", obj);
        }

        public ViewResult ConsentDetails(ConsentMaster obj, string cmd)
        {
            DBConnection cn = new DBConnection();
            obj.CMD = "View";
            obj.hid = Convert.ToInt32(Session["hospital"].ToString());
            obj.patientid = Convert.ToInt32(Session["patientid"].ToString());
            string xml = Common.ToXML(obj);
            DataSet dataset = cn.ExecuteProcedure("SP_Consent", xml);
            return View(dataset);
        }

        public ActionResult PrintConsent(int consentid)
        {
            if (consentid > 0)
            {
                Session["consentid"] = consentid;
                string str = PrintGenerator.GenerateConsentPDF();
                TempData["Print"] = str;
                return RedirectToAction("Print", "Shared");
            }
            else
            {
                return RedirectToAction("ConsentForm", "Patient");
            }

        }

        /********************************* Bill Details ********************************************/


        public ActionResult Bill()
        {
            Session["BillParticulars"] = null;
            ViewBag.BillParticulars = new List<BillParticulars>();
            ViewBag.Total = 0;
            return View();
        }

        [HttpPost]
        public ActionResult Bill(FormCollection form, string SubmitBtn)
        {
            List<BillParticulars> par = new List<BillParticulars>();
            if (Session["BillParticulars"] != null)
                par = (List<BillParticulars>)Session["BillParticulars"];
            else
                par = new List<BillParticulars>();

            switch (SubmitBtn)
            {

                case "Add":
                    BillParticulars p = new BillParticulars();
                    p.particular = form["particular"];
                    p.qty = Convert.ToInt32(form["qty"]);
                    p.charges = Convert.ToDecimal(form["charges"]);
                    p.total = p.qty * p.charges;
                    if (par.Where(q => q.particular == p.particular).Count() >= 1)
                        ViewBag.msg = "Particular already added.";
                    else
                        par.Add(p);
                    Session["BillParticulars"] = par;
                    break;
                case "Delete":
                    int index = Convert.ToInt32(form["srno"]);
                    par.RemoveAt(index);
                    break;
                case "Clear":
                    par = new List<BillParticulars>();
                    Session["BillParticulars"] = par;
                    break;
            }
            ViewBag.Total = par.Select(q => q.total).Sum();
            ViewBag.BillParticulars = Session["BillParticulars"];
            return View();
        }

        [HttpPost]
        public ActionResult Checkout(FormCollection form, string SubmitBtn)
        {
            try
            {
                List<BillParticulars> par = new List<BillParticulars>();
                if (Session["BillParticulars"] != null)
                {
                    PatientBill pb = new PatientBill();
                    if (SubmitBtn == "Checkout") { pb.CMD = "Save"; } else { pb.CMD = SubmitBtn; }

                    pb.parlist = par = (List<BillParticulars>)Session["BillParticulars"];
                    pb.totalamount = par.Select(q => q.total).Sum();
                    pb.patientid = Convert.ToInt32(Session["patientid"] == null ? '0' : Session["patientid"]);
                    pb.hid = Convert.ToInt32(Session["hospital"] == null ? '0' : Session["hospital"]);
                    string billid = pb.PerformAction();
                    string str = PrintGenerator.GenerateBillPDF(Convert.ToInt32(billid.ToString()), pb.patientid, pb.hid, DateTime.Now);
                    TempData["Print"] = str;
                    return RedirectToAction("Print", "Shared");
                }
                else
                {
                    return RedirectToAction("Bill", "Patient");
                }
            }
            catch (Exception)
            {
                return RedirectToAction("Bill", "Patient");
            }
        }

        public ActionResult ViewBills(int billid, DateTime billdate)
        {
            if (billid > 0)
            {
                string str = PrintGenerator.GenerateBillPDF(billid, Convert.ToInt32(Session["patientid"].ToString()), Convert.ToInt32(Session["hospital"].ToString()), billdate);
                TempData["Print"] = str;
                return RedirectToAction("Print", "Shared");
            }
            else
            {
                return View();
            }

        }

        public ViewResult BillDetails()
        {
            PatientBill obj = new PatientBill();
            DBConnection cn = new DBConnection();
            obj.CMD = "View";
            obj.hid = Convert.ToInt32(Session["hospital"].ToString());
            obj.patientid = Convert.ToInt32(Session["patientid"].ToString());
            string xml = Common.ToXML(obj);
            DataSet dataset = cn.ExecuteProcedure("Sp_PatientFinalBill", xml);
            return View(dataset);
        }

        /************************** Treatment Details******************************/
        public ViewResult TreatmentHistory()
        {
            Treatment obj = new Treatment();
            DBConnection cn = new DBConnection();
            obj.CMD = "View";
            obj.hid = Convert.ToInt32(Session["hospital"].ToString());
            obj.patientid = Convert.ToInt32(Session["patientid"].ToString());
            string xml = Common.ToXML(obj);
            DataSet dataset = cn.ExecuteProcedure("SP_Treatment", xml);
            return View(dataset);
        }

        public ActionResult Treatment(int treatmentid = 0)
        {
            Session.Remove("TreatInvestigationList");
            Session.Remove("TreatmentMedicine");
            Session.Remove("TreatmentComplents");
            Session.Remove("TreatmentComplents");
            Session.Remove("TreatmentDiagnosis");
            Session.Remove("TreatmentHistory");
            Session.Remove("TreatmentCheckups");
            Session.Remove("TreatmentFundus");
            Session.Remove("TreatmentAdvice");

            Treatment t = new Treatment();
            if (treatmentid > 0)
            {
                t.CMD = "View1";
                t.hid = Convert.ToInt32(Session["hospital"]);
                t.patientid = Convert.ToInt32(Session["PatientId"]);
                t.treatmentid = treatmentid;
                Treatment t1 = t.GetSingleTreatment();
                return View(t1);
            }
            else
            {
                TreatmentInvestigation ti = new TreatmentInvestigation();
                Session["TreatInvestigationList"] = t.TreatInvestigationList = ti.GetList();

                TreatmentMedicine tm = new TreatmentMedicine();
                tm.mischecked = tm.aischecked = tm.eischecked = tm.nischecked = false;

                return View(t);
            }
        }

        [HttpPost]
        public ActionResult Treatment(FormCollection form, Treatment obj, string SubmitBtn, int tabindex)
        {
            try
            {
                ViewBag.tabindex = tabindex;
                int resetflag = 0;
                obj.CMD = SubmitBtn;

                obj.hid = Convert.ToInt32(Session["hospital"]);
                obj.patientid = Convert.ToInt32(Session["PatientId"]);

                var doctorarr = form["doctor"].ToString().Split(':');
                obj.dfname = doctorarr[1];
                obj.dlname = doctorarr[2];



                //Medicines
                List<TreatmentMedicine> tm = new List<TreatmentMedicine>();
                if (Session["TreatmentMedicine"] != null)
                    tm = (List<TreatmentMedicine>)Session["TreatmentMedicine"];
                else
                    tm = new List<TreatmentMedicine>();

                //Complents
                List<TreatmentComplents> tc = new List<TreatmentComplents>();
                if (Session["TreatmentComplents"] != null)
                    tc = (List<TreatmentComplents>)Session["TreatmentComplents"];
                else
                    tc = new List<TreatmentComplents>();

                //Diagnosis
                List<TreatmentDiagnosis> td = new List<TreatmentDiagnosis>();
                if (Session["TreatmentDiagnosis"] != null)
                    td = (List<TreatmentDiagnosis>)Session["TreatmentDiagnosis"];
                else
                    td = new List<TreatmentDiagnosis>();

                //History
                List<TreatmentHistory> th = new List<TreatmentHistory>();
                if (Session["TreatmentHistory"] != null)
                    th = (List<TreatmentHistory>)Session["TreatmentHistory"];
                else
                    th = new List<TreatmentHistory>();

                //Checkup
                List<TreatmentCheckups> tch = new List<TreatmentCheckups>();
                if (Session["TreatmentCheckups"] != null)
                    tch = (List<TreatmentCheckups>)Session["TreatmentCheckups"];
                else
                    tch = new List<TreatmentCheckups>();

                //Fundus
                List<TreatmentFundus> tf = new List<TreatmentFundus>();
                if (Session["TreatmentFundus"] != null)
                    tf = (List<TreatmentFundus>)Session["TreatmentFundus"];
                else
                    tf = new List<TreatmentFundus>();

                //Advice
                List<TreatmentAdvice> ta = new List<TreatmentAdvice>();
                if (Session["TreatmentAdvice"] != null)
                    ta = (List<TreatmentAdvice>)Session["TreatmentAdvice"];
                else
                    ta = new List<TreatmentAdvice>();


                switch (SubmitBtn)
                {
                    case "Continue":
                        TempData["CMD"] = SubmitBtn; return RedirectToAction("Treatment", obj);

                    case "addmedicine":
                        //Medicines
                        TreatmentMedicine t = new TreatmentMedicine();
                        t.a = obj.TreatMedicine.aischecked == true ? "A" : "";
                        t.m = obj.TreatMedicine.mischecked == true ? "M" : "";
                        t.e = obj.TreatMedicine.eischecked == true ? "E" : "";
                        t.n = obj.TreatMedicine.nischecked == true ? "N" : "";
                        var arr = form["particular"].ToString().Split(':');
                        t.medicineid = Convert.ToInt32(arr[0]);
                        t.particular = arr[1];
                        t.qty = obj.TreatMedicine.qty;
                        t.srno = obj.srno;
                        if (tm.Where(q => q.particular == t.particular).Count() >= 1)
                            ViewBag.msg = "Particular already added.";
                        else
                            tm.Add(t);
                        obj.TreatMedicineList = tm;
                        Session["TreatmentMedicine"] = tm;

                        break;

                    case "addcomplent":
                        //Complents
                        TreatmentComplents tcmp = new TreatmentComplents();
                        var complentarr = form["complents"].ToString().Split(':');
                        tcmp.complentid = Convert.ToInt32(complentarr[0]);
                        tcmp.complentname = complentarr[1];
                        tcmp.srno = obj.srno;
                        if (tc.Where(q => q.complentname == tcmp.complentname).Count() >= 1)
                            ViewBag.complentmsg = "Complent already added.";
                        else
                            tc.Add(tcmp);

                        obj.TreatComplentsList = tc;
                        Session["TreatmentComplents"] = tc;
                        ViewBag.tab = "tab1";
                        break;

                    case "adddiagnosis":
                        //Diagnosis
                        TreatmentDiagnosis tdig = new TreatmentDiagnosis();
                        var diagnosisarr = form["diagnosis"].ToString().Split(':');
                        tdig.diagnosisid = Convert.ToInt32(diagnosisarr[0]);
                        tdig.diagnosisname = diagnosisarr[1];
                        tdig.srno = obj.srno;
                        if (td.Where(q => q.diagnosisname == tdig.diagnosisname).Count() >= 1)
                            ViewBag.diagnosismsg = "Diagnosis already added.";
                        else
                            td.Add(tdig);

                        obj.TreatDiagnosisList = td;
                        Session["TreatmentDiagnosis"] = td;
                        break;

                    case "addhistory":
                        //Diagnosis
                        TreatmentHistory thistory = new TreatmentHistory();
                        var historyarr = form["history"].ToString().Split(':');
                        thistory.historyid = Convert.ToInt32(historyarr[0]);
                        thistory.historyname = historyarr[1];
                        thistory.from_dt = obj.from_dt;
                        thistory.to_dt = obj.to_dt;
                        thistory.srno = obj.srno;
                        if (th.Where(q => q.historyname == thistory.historyname).Count() >= 1)
                            ViewBag.historymsg = "Disease already added.";
                        else
                            th.Add(thistory);

                        obj.TreatHistoryList = th;
                        Session["TreatmentHistory"] = th;
                        ViewBag.tab = "tab2";
                        break;

                    case "addcheckups":
                        //Diagnosis
                        TreatmentCheckups tcheckup = new TreatmentCheckups();
                        var checkuparr = form["checkups"].ToString().Split(':');
                        tcheckup.checkupid = Convert.ToInt32(checkuparr[0]);
                        tcheckup.checkupname = checkuparr[1];
                        tcheckup.srno = obj.srno;
                        if (tch.Where(q => q.checkupname == tcheckup.checkupname).Count() >= 1)
                            ViewBag.checkupmsg = "Checkup already added.";
                        else
                            tch.Add(tcheckup);

                        obj.TreatCheckupList = tch;
                        Session["TreatmentCheckups"] = tch;
                        break;

                    case "addfundus":
                        //Diagnosis
                        TreatmentFundus tfundus = new TreatmentFundus();
                        var fundusarr = form["fundus"].ToString().Split(':');
                        tfundus.fundusid = Convert.ToInt32(fundusarr[0]);
                        tfundus.fundusname = fundusarr[1];
                        tfundus.srno = obj.srno;
                        if (tch.Where(q => q.checkupname == tfundus.fundusname).Count() >= 1)
                            ViewBag.fundusmsg = "Fundus already added.";
                        else
                            tf.Add(tfundus);

                        obj.TreatFundusList = tf;
                        Session["TreatmentFundus"] = tf;
                        break;

                    case "addadvice":
                        //Diagnosis
                        TreatmentAdvice tad = new TreatmentAdvice();
                        var adarr = form["advice"].ToString().Split(':');
                        tad.adviceid = Convert.ToInt32(adarr[0]);
                        tad.advicename = adarr[1];
                        tad.srno = obj.srno;
                        if (ta.Where(q => q.advicename == tad.advicename).Count() >= 1)
                            ViewBag.fundusmsg = "Advice already added.";
                        else
                            ta.Add(tad);
                        obj.TreatAdviceList = ta;
                        Session["TreatmentAdvice"] = ta;
                        break;

                    case "Delete":
                        //Medicines
                        int index = Convert.ToInt32(form["medicinesrno"]);
                        tm.RemoveAt(index);
                        Session["TreatmentMedicine"] = obj.TreatMedicineList = tm;
                        break;
                    case "DeleteComplent":
                        //Complents
                        int complentsrno = Convert.ToInt32(form["complentsrno"]);
                        tc.RemoveAt(complentsrno);
                        Session["TreatmentComplents"] = obj.TreatComplentsList = tc;
                        break;
                    case "DeleteDiagnosis":
                        //Diagnosis
                        int diagnosissrno = Convert.ToInt32(form["diagsrno"]);
                        td.RemoveAt(diagnosissrno);
                        Session["TreatmentDiagnosis"] = obj.TreatDiagnosisList = td;
                        break;
                    case "DeleteHistory":
                        //History
                        int historysrno = Convert.ToInt32(form["historysrno"]);
                        th.RemoveAt(historysrno);
                        Session["TreatmentHistory"] = obj.TreatHistoryList = th;
                        break;

                    case "DeleteCheckup":
                        //History
                        int checkupsrno = Convert.ToInt32(form["checkupsrno"]);
                        tch.RemoveAt(checkupsrno);
                        Session["TreatmentCheckup"] = obj.TreatCheckupList = tch;
                        break;

                    case "DeleteFundus":
                        //History
                        int fundussrno = Convert.ToInt32(form["fundussrno"]);
                        tf.RemoveAt(fundussrno);
                        Session["TreatmentFundus"] = obj.TreatFundusList = tf;
                        break;

                    case "DeleteAdvice":
                        //History
                        int advicesrno = Convert.ToInt32(form["advicesrno"]);
                        ta.RemoveAt(advicesrno);
                        Session["TreatmentAdvice"] = obj.TreatAdviceList = ta;
                        break;

                    case "Reset":
                        resetflag = 1;
                        break;

                    case "Save":

                        Session["TreatmentMedicine"] = obj.TreatMedicineList = tm;
                        Session["TreatmentComplents"] = obj.TreatComplentsList = tc;
                        Session["TreatmentDiagnosis"] = obj.TreatDiagnosisList = td;
                        Session["TreatmentHistory"] = obj.TreatHistoryList = th;
                        Session["TreatmentCheckups"] = obj.TreatCheckupList = tch;
                        Session["TreatmentFundus"] = obj.TreatFundusList = tf;
                        Session["treatmentAdvice"] = obj.TreatAdviceList = ta;
                        string treatmentid = obj.PerformAction();
                        Session["treatmentid"] = treatmentid;
                        string str = PrintGenerator.GenerateTreatmentPDF();
                        TempData["Print"] = str;
                        return RedirectToAction("Print", "Shared");

                    case "Update":
                        Session["TreatmentMedicine"] = obj.TreatMedicineList = tm;
                        Session["TreatmentComplents"] = obj.TreatComplentsList = tc;
                        Session["TreatmentDiagnosis"] = obj.TreatDiagnosisList = td;
                        Session["TreatmentHistory"] = obj.TreatHistoryList = th;
                        Session["TreatmentCheckups"] = obj.TreatCheckupList = tch;
                        Session["TreatmentFundus"] = obj.TreatFundusList = tf;
                        Session["treatmentAdvice"] = obj.TreatAdviceList = ta;
                        string treatid = obj.PerformAction();
                        Session["treatmentid"] = treatid;
                        string str1 = PrintGenerator.GenerateTreatmentPDF();
                        TempData["Print"] = str1;
                        return RedirectToAction("Print", "Shared");
                }

                if (resetflag == 1)
                    return Redirect("/Patient/Treatment");


                if (obj.TreatInvestigationList != null)
                {
                    Session["TreatInvestigationList"] = obj.TreatInvestigationList;
                }
                else
                {
                    obj.TreatInvestigationList = (List<TreatmentInvestigation>)Session["TreatInvestigationList"];
                }
            }
            catch (Exception e)
            {
                string[] arr = new string[2] { "danger", "Something Went Wrong! Try later." + e.Message };

                ViewBag.msgarr = arr;

            }

            return View(obj);
        }


        public ActionResult PrintTreatment(int treatmentid)
        {
            if (treatmentid > 0)
            {
                Session["treatmentid"] = treatmentid;
                string str = PrintGenerator.GenerateTreatmentPDF();
                TempData["Print"] = str;
                return RedirectToAction("Print", "Shared");
            }
            else
            {
                return RedirectToAction("Treatment", "Patient");
            }

        }

    }
}