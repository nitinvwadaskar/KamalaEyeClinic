using Kamala_Eye_Clinic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kamala_Eye_Clinic.Controllers
{
    [Sessionclass]
    public class ReportController : Controller
    {
        public ActionResult Report()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Report(FormCollection form)
        {
            string fdate = form["fromdate"];
            string tdate = form["todate"];
            string rtype = form["rpttype"];
            string rpt = form["rpt"];
            switch (rpt)
            {
                case "Patient Registration":
                    return RedirectToAction("PatientRegistrationReport");
                case "Patient Appointments":
                    return RedirectToAction("PatientAppointmentReport");
                case "Patient Discharge":
                    return RedirectToAction("PatientDischargeReport");

                case "Consent Form":
                    return RedirectToAction("PatientConsentReport");

                case "Treatments":
                    return RedirectToAction("PatientTreatmentReport");

                default:
                    return View();
            }

        }

        public ActionResult PatientRegistrationReport()
        {
            return View();
        }

        public ActionResult PatientDischargeReport()
        {
            return View();
        }

        public ActionResult PatientAppointmentReport()
        {
            return View();
        }

        public ActionResult PatientConsentReport()
        {
            return View();
        }
        public ActionResult PatientTreatmentReport()
        {
            return View();
        }
    }
}