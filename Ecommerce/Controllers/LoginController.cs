using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ecommerce.Models;
using Ecommerce.DAL;
using Ecommerce.ModelsnPlace;

namespace Ecommerce.Controllers
{
    public class LoginController : Controller
    {
        private DBInteractor _DbExcecution = new DBInteractor();
        private EcommerceDBContect _dbCon = new EcommerceDBContect();
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(tbl_UserDetails _userDetails)
        {
            try
            {
                string _udresult = _DbExcecution.AppLogin(_userDetails);

                if (_udresult.Length > 0)
                {

                    if (_udresult != "Alredy Active session ")
                    {
                        Session["AuthID"] = _udresult.ToString();
                        return Redirect("http://localhost:17015/Home");
                        //return  RedirectToAction("Home");
                    }
                    else
                    {
                        Session["AuthID"] = _udresult.ToString();
                        return Redirect("http://localhost:36906/Dashbord");
                        //TempData["message"] = _udresult;
                        //return View();
                    }
                }
                else
                {
                    TempData["message"] = "Incorrect User id or Password !";
                    return View();
                }

            }
            catch (Exception _ex)
            {
                TempData["message"] = _ex.Message;
            }
            return View();
        }


        public ActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgetPassword(tbl_UserDetails _userDetails)
        {
            bool resetResponse = false;
            tbl_UserDetails _user = _dbCon.tbl_UserDetails.Where(c => c.UserEmailID == _userDetails.UserEmailID && c.IsActive == true).FirstOrDefault();
            if (_user != null)
            {
                string To = _userDetails.UserEmailID, UserID, Password, SMTPPort, Host;
                Random _rand = new Random();

                string token = _rand.Next(10, 99999).ToString();
                if (token == null)
                {
                    // If user does not exist or is not confirmed.  

                    return View("Index");

                }
                else
                {
                    //ResetPasswordModel resetmodel = new ResetPasswordModel();
                    //resetmodel.ReturnToken = token;
                    //resetmodel.Emailid = _userDetails.UserEmailID;
                    //resetmodel.IsActive = true;
                    //resetmodel.createdDate = DateTime.Now;
                    //_dbCon.ResetPasswordModel.Add(resetmodel);
                    //_dbCon.SaveChangesAsync();
                    //Create URL with above token  

                    var lnkHref = "<a href=" + Url.Action("ResetPassword", "Login", new { code = token }, "http") + ">Reset Password</a>";


                    //HTML Template for Send email  

                    string subject = "Resturant password Reset";

                    string body = "<br/>Please find the Password Reset Link. </b><br/>" + lnkHref;


                    //Get and set the AppSettings using configuration manager.  

                    EmailHelper.AppSettings(out UserID, out Password, out SMTPPort, out Host);


                    //Call send email methods.  

                    EmailHelper.SendEmail(UserID, subject, body, To, UserID, Password, SMTPPort, Host);
                    resetResponse = true;
                }


            }
            if (resetResponse)
            {
                TempData["message"] = "Password Rest Link Sent on your Email id";
            }
            else
            {
                TempData["message"] = "User Dosent Exist";
            }
            return View();
        }

    }
}