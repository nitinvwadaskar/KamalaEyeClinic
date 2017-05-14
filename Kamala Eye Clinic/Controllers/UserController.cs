using Kamala_Eye_Clinic.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kamala_Eye_Clinic.Controllers
{
    
    public class UserController : Controller
    {


        /* Create Update Delete and View User Roles */
        [Sessionclass]
        public ViewResult UserRole()
        {
            RoleMaster obj = new RoleMaster();
            return View(obj);
        }

        [HttpPost]
        [Sessionclass]
        public ActionResult UserRole(RoleMaster obj, string SubmitBtn)
        {
            obj.CMD = SubmitBtn == "Save" ? "Save" : SubmitBtn == "Update" ? "Update" : "Delete";
            obj.hid = Convert.ToInt32(Session["hospital"].ToString());
            string[] str = obj.PerformAction();
            ViewBag.msg = str[1];
            return View(obj);
        }
        [Sessionclass]
        public ViewResult Update(int roleid)
        {
            RoleMaster obj = new RoleMaster();

            if (roleid > 0)
            {
                obj.CMD = "Update";
                obj = obj.GetSingleRole(roleid);

            }
            return View("UserRole", obj);
        }

        
        [HttpPost]
        [Sessionclass]
        public ActionResult Update(RoleMaster obj, string SubmitBtn)
        {
            UserRole(obj, SubmitBtn);
            if (SubmitBtn == "Delete") return Redirect("~/User/RoleDetails");
            return View("UserRole", obj);
        }

        [Sessionclass]
        public ViewResult RoleDetails(RoleMaster obj, string cmd)
        {
            DBConnection cn = new DBConnection();
            obj.CMD = "View";
            string xml = Common.ToXML(obj);
            DataSet dataset = cn.ExecuteProcedure("SP_UserRole", xml);
            return View(dataset);
        }



        /* Create Update Delete and View Users.... */
        [Sessionclass]
        public ViewResult Registration()
        {
            User obj = new User();
            return View(obj.GetMultipleRole());
        }

        [Sessionclass]
        [HttpPost]
        public ActionResult Registration(User obj, string SubmitBtn)
        {

            obj.CMD = SubmitBtn == "Save" ? "Save" : SubmitBtn == "Update" ? "Update" : "Delete";
            obj.hid = Convert.ToInt32( Session["hospital"].ToString() );
            string[] str = obj.PerformAction();
            ViewBag.msg = str[1];
            return View(obj.GetMultipleRole());
        }


        public ViewResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User obj)
        {
            try
            {
                obj.CMD = "View1";
                string xml = Common.ToXML(obj);
                DBConnection con = new DBConnection();
                DataSet ds = con.ExecuteProcedure("SP_UserRegistration", xml);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    Session["hospital"] = ds.Tables[0].Rows[0]["hid"];
                    Session["UserName"] = ds.Tables[0].Rows[0]["username"];
                    return RedirectToAction("PatientSelection", "Patient");
                }
                else
                {
                    return RedirectToAction("Login");
                }
            }
            catch(Exception)
            {
                return RedirectToAction("Login");
            }
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Login");
        }

        [Sessionclass]
        public ViewResult ChangePassword()
        {
            User obj = new Models.User();
            return View(obj);
        }
        [HttpPost]
        [Sessionclass]
        public ActionResult ChangePassword(User obj, string SubmitBtn)
        {
            if (SubmitBtn == "Save")
            {
                obj.CMD = "Update";
            }
            obj.hid = Convert.ToInt32(Session["hospital"].ToString());
            DBConnection cn = new DBConnection();
            string xml = Common.ToXML(obj);
            DataSet dataset = cn.ExecuteProcedure("SP_ChangePassword", xml);
            if (dataset != null)
            {
                ViewBag.msg= dataset.Tables[0].Rows[0][1].ToString();
            }
            return View(obj);
        }


        public ActionResult ForgotPassword()
        {
            User obj = new Models.User();
            return View(obj);
        }

        [HttpPost]
        public ActionResult ForgotPassword(User obj)
        {
           
            obj.CMD = "Search";            
            DBConnection cn = new DBConnection();
            string xml = Common.ToXML(obj);
            DataSet dataset = cn.ExecuteProcedure("SP_ChangePassword", xml);
            if (dataset != null)
            {
                ViewBag.msg = dataset.Tables[0].Rows[0][0].ToString();
            }
            return View(obj);
        }

    }
}