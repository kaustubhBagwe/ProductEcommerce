using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Ecommerce.DAL;
using Ecommerce.Models;
using System.Net.Mail;
using PagedList;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using Ecommerce.Models.Admin;

namespace Ecommerce.Controllers
{
    public class OrdersController : Controller
    {
        private EcommerceDBContect db = new EcommerceDBContect();

        // GET: Orders
        public ActionResult Index(int? page)
        {
            return View(db.Order.ToList().OrderByDescending(x => x.OrderID).ToPagedList(page ?? 1, 10));
        }

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Order.Find(id);
            ViewBag.OrderItems = db.OrderedItems.Where(x => x.OrderID == order.OrderID).ToList();
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        public string OrderStatusUpdate(int OrderID, string Status)
        {
            Order ostatus = db.Order.Find(OrderID);
            ostatus.TransactStatus = Status.ToString();
            db.SaveChanges();
            string value = Status;
            int custid = ostatus.CustomerID;
            string _sub = string.Empty;
            CustomerDetails custemail = db.CustomerDetails.Where(x => x.CustID == custid).FirstOrDefault();
            System.Text.RegularExpressions.Regex expr = new Regex(@"^\d{10}$");
            if (!expr.IsMatch(custemail.CustEmail))
            {
                if (custemail.CustEmail != null || custemail.CustEmail != "")
                {
                    if (value == "Order Placed") { _sub = "Order Placed"; }
                    if (value == "Order InProgress") { _sub = "Order InProgress"; }
                    if (value == "Order shipped") { _sub = "Order Shipped"; }
                    if (value == "Order Delivered") { _sub = "Order Delivered"; }
                    if (value == "Order Cancel") { _sub = "Order Cancel"; }
                    string Emailtext = System.IO.File.ReadAllText(@"" + Server.MapPath("~/Template/Emailtemp.txt"));
                    string Emailtext1 = "";
                    string Emailtext2 = "";
                    Emailtext = Emailtext.Replace("@custname", custemail.CustFName + " " + custemail.CustLName);
                    Emailtext1 = Emailtext.Replace("@Orderno", OrderID.ToString());
                    Emailtext2 = Emailtext1.Replace("@Status", Status);

                    OTPEmailOder(custemail.CustEmail, Emailtext2, "LivingSTUD.com : #" + OrderID + " " + _sub);
                }
            }
            else
            {
                OTPMobileOrder(custemail.CustEmail, "Your #" + OrderID + " " + Status + " ,\r\n Track your order on http://livingstud.com/OrderTrack/" + OrderID + "\r\n Get Order details at  http://www.livingstud.com/orders/Paymentsuccessfull?orderid=" + OrderID+" \r\n continue shopping with us on http://www.livingstud.com  \r\n LivingStud.com team", "LivingSTUD.com " + Status + " : #" + OrderID);
            }
            OrderLog ol = new OrderLog();
            ol.OID = OrderID;
            ol.CustId = custemail.CustID.ToString();
            ol.Status = Status;
            ol.InsertDate = DateTime.Now;
            ol.Owner = "Kaustubh"; 
            if (Status == "Order InProgress") { ol.Owner = "Vishu"; }
            if (Status == "Order shipped") { ol.Owner = "Onkar"; }
            db.OrderLogs.Add(ol);
            db.SaveChanges();
            return "Order ID : " + OrderID + " Status Changed To : " + Status;
        }

        public string OTPEmailOder(string Email, string EmailBody, string Subject)
        {
            string IsEmailSend = "false";
            try
            {
                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress("26366020k@gmail.com");//shoplootle@gmail.com
                    mail.To.Add(Email);
                    mail.Subject = Subject;
                    mail.Body = EmailBody;
                    mail.IsBodyHtml = true;
                    using (SmtpClient smtp = new SmtpClient())//465 //587
                    {
                        smtp.EnableSsl = true;
                        smtp.UseDefaultCredentials = false;
                        //smtp.Credentials = new NetworkCredential("shoplootle@gmail.com", "nayananm291193");
                        smtp.Credentials = new NetworkCredential("26366020k@gmail.com", "KauBagwe21");
                        smtp.Host = "smtp.gmail.com";
                        smtp.Port = 587;
                        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                        smtp.Send(mail);
                        IsEmailSend = "true";
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return IsEmailSend;
        }
        public string OTPMobileOrder(string Number, string EmailBody, string Subject)
        {
            string IsEmailSend = "false";

            try
            {

                //Your authentication key
                string authKey = "144054A8Is1H8TDV58be6289";
                //Multiple mobiles numbers separated by comma
                string mobileNumber = Number;
                //Sender ID,While using route4 sender id should be 6 characters long.
                string senderId = "SHPLOL";
                //Your message to send, Add URL encoding here.

                string message = HttpUtility.UrlEncode("LivingStud.com : " + EmailBody);

                //Prepare you post parameters
                StringBuilder sbPostData = new StringBuilder();
                sbPostData.AppendFormat("authkey={0}", authKey);
                sbPostData.AppendFormat("&mobiles={0}", mobileNumber);
                sbPostData.AppendFormat("&message={0}", message);
                sbPostData.AppendFormat("&sender={0}", senderId);
                sbPostData.AppendFormat("&route={0}", "4");

                try
                {

                    //Call Send SMS API
                    string sendSMSUri = "http://api.msg91.com/api/sendhttp.php";
                    //Create HTTPWebrequest
                    HttpWebRequest httpWReq = (HttpWebRequest)WebRequest.Create(sendSMSUri);
                    //Prepare and Add URL Encoded data
                    UTF8Encoding encoding = new UTF8Encoding();
                    byte[] data = encoding.GetBytes(sbPostData.ToString());
                    //Specify post method
                    httpWReq.Method = "POST";
                    httpWReq.ContentType = "application/x-www-form-urlencoded";
                    httpWReq.ContentLength = data.Length;
                    using (Stream stream = httpWReq.GetRequestStream())
                    {
                        stream.Write(data, 0, data.Length);
                    }
                    //Get the response
                    HttpWebResponse response = (HttpWebResponse)httpWReq.GetResponse();
                    StreamReader reader = new StreamReader(response.GetResponseStream());
                    string responseString = reader.ReadToEnd();

                    //Close the response
                    reader.Close();
                    response.Close();
                }
                catch (SystemException ex)
                {

                }

                IsEmailSend = "true";
            }
            catch (Exception ex)
            {

            }
            return IsEmailSend;
        }
        public ActionResult OrderTrack()
        {

            return View();
        }
        [HttpPost]
        public ActionResult OrderTrack(int? OrderID)
        {
            Order ostatus = db.Order.Find(OrderID);
            if (ostatus == null)
            {
                ViewBag.Status = "Invalid Order ID";
            }
            else { ViewBag.Status = ostatus.TransactStatus; }



            return View();
        }
        // GET: Orders/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Order order)
        {
            if (ModelState.IsValid)
            {
                db.Order.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(order);
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Order.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderID,CustomerID,ProducID,ColorID,SizeID,Quantity,CompanyName,ContactFName,ContactLName,ContactTitle,Address1,Address2,Order_City,Order_State,PostalCode,Country,Phone,Fax,Email,WebSite,PaymentMethods,TranactionID,OrderDate,RequiredDate,ShipedDate,VAT,TransactStatus,ErrMsg,Fulfilled,PaidAount,PaymentDate,IsActive,IsDelete,IsUpdate,InsertDate,LMDDate")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(order);
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Order.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Order.Find(id);
            db.Order.Remove(order);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Checkout()
        {
            var url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
            ViewBag.ReturnUrl = url;
            List<Order> Lastor = (List<Order>)Session["CartLst"];
            //chk quantity
            List<Order> sessionProductList = (List<Order>)Session["CartLst"];
            if (sessionProductList != null)
            {
                string noquantityprod = "";
                for (int i = 0; i < Convert.ToInt32(sessionProductList.Count); i++)
                {
                    int pipd = sessionProductList[i].ProducID;
                    var qytnp = db.ProductDetails.Where(x => x.ProductID == pipd).Select(c => c.ProductQuantity).ToList();
                    if (qytnp[0] > 0)
                    {
                    }
                    else
                    {
                        noquantityprod += noquantityprod + sessionProductList[i].ProducID + ",";
                        Session["noproduct"] = noquantityprod;
                        //break;
                    }
                }
            }

            //chk quantity
            if (Lastor != null)
            {
                ViewBag.PCount = Lastor.Count;
                decimal Costval = 0;
                for (int i = 0; i < Lastor.Count; i++)
                {
                    Costval = Lastor[i].PaidAount + Costval;
                }
                ViewBag.pTotalCost = Costval;
                string[] fvl = Costval.ToString().Split('.');
                string d1 = fvl[0] + "00";
                string d3 = d1;

                string toshow = d3.ToString() + ".00";
                ViewBag.pTotalCostdec = toshow;
                TempData["pTotalCost"] = Costval;
                ViewBag.citylist = db.State.ToList();
            }
            else
            {
                ViewBag.PCount = 0;
            }
            return View(Lastor);
        }
        public JsonResult CityList(string sid) // its a GET, not a POST
        {
            int SIDd = Convert.ToInt32(sid);
            var cities = db.City.Where(t => t.stateID == SIDd).Select(c => new
            {
                ID = c.CityID,
                Text = c.CityName,
                stateid = c.stateID
            });
            return Json(cities, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public bool OTPEmail(string Email, string EmailBody, string Subject)
        {
            bool IsEmailSend = false;

            int lastCustId = 0;
            if (CheckCustomeexist(Email))
            {
                lastCustId = 0000;

            }
            else
            {

                try
                {
                    using (MailMessage mail = new MailMessage())
                    {
                        mail.From = new MailAddress("26366020k@gmail.com");//shoplootle@gmail.com
                        mail.To.Add(Email);
                        mail.Subject = Subject;
                        int _min = 1000;
                        int _max = 9999;
                        Random _rdm = new Random();
                        int rnum = _rdm.Next(_min, _max);
                        mail.Body = EmailBody + rnum;
                        mail.IsBodyHtml = true;
                        //  mail.Attachments.Add(new Attachment("C:\\file.zip"));
                        using (SmtpClient smtp = new SmtpClient())//465 //587
                        {
                            smtp.EnableSsl = true;
                            smtp.UseDefaultCredentials = false;
                            //smtp.Credentials = new NetworkCredential("shoplootle@gmail.com", "nayananm291193");
                            smtp.Credentials = new NetworkCredential("26366020k@gmail.com", "KauBagwe21");

                            smtp.Host = "smtp.gmail.com";
                            smtp.Port = 587;
                            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                            smtp.Send(mail);
                            IsEmailSend = true;
                            OTP otpcode = new OTP();
                            otpcode.OTPCode = rnum.ToString();
                            otpcode.OTPEmail = Email;
                            otpcode.OTPMobile = Email;
                            otpcode.IsActive = true;
                            otpcode.IsDelete = false;
                            otpcode.IsUpdate = false;
                            otpcode.InsertDate = DateTime.Now;
                            otpcode.LMDDate = DateTime.Now;
                            db.OTP.Add(otpcode);
                            db.SaveChanges();
                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }
            return IsEmailSend;
        }

        public bool OTPMobile(string Number, string EmailBody, string Subject)
        {
            bool IsEmailSend = false;
            int lastCustId = 0;
            if (false)
            {
                lastCustId = 0000;

            }
            else
            {
                try
                {

                    //Your authentication key
                    string authKey = "144054A8Is1H8TDV58be6289";
                    //Multiple mobiles numbers separated by comma
                    string mobileNumber = Number;
                    //Sender ID,While using route4 sender id should be 6 characters long.
                    string senderId = "SHPLOL";
                    //Your message to send, Add URL encoding here.
                    int _min = 1000;
                    int _max = 9999;
                    Random _rdm = new Random();
                    int rnum = _rdm.Next(_min, _max);
                    string message = HttpUtility.UrlEncode("Livingstud.com OTP - " + EmailBody + rnum);

                    //Prepare you post parameters
                    StringBuilder sbPostData = new StringBuilder();
                    sbPostData.AppendFormat("authkey={0}", authKey);
                    sbPostData.AppendFormat("&mobiles={0}", mobileNumber);
                    sbPostData.AppendFormat("&message={0}", message);
                    sbPostData.AppendFormat("&sender={0}", senderId);
                    sbPostData.AppendFormat("&route={0}", "4");

                    try
                    {
                        OTP otpcode = new OTP();
                        otpcode.OTPCode = rnum.ToString();
                        otpcode.OTPEmail = Number;
                        otpcode.OTPMobile = Number;
                        otpcode.IsActive = true;
                        otpcode.IsDelete = false;
                        otpcode.IsUpdate = false;
                        otpcode.InsertDate = DateTime.Now;
                        otpcode.LMDDate = DateTime.Now;
                        db.OTP.Add(otpcode);
                        db.SaveChanges();

                        //Call Send SMS API
                        string sendSMSUri = "http://api.msg91.com/api/sendhttp.php";
                        //Create HTTPWebrequest
                        HttpWebRequest httpWReq = (HttpWebRequest)WebRequest.Create(sendSMSUri);
                        //Prepare and Add URL Encoded data
                        UTF8Encoding encoding = new UTF8Encoding();
                        byte[] data = encoding.GetBytes(sbPostData.ToString());
                        //Specify post method
                        httpWReq.Method = "POST";
                        httpWReq.ContentType = "application/x-www-form-urlencoded";
                        httpWReq.ContentLength = data.Length;
                        using (Stream stream = httpWReq.GetRequestStream())
                        {
                            stream.Write(data, 0, data.Length);
                        }
                        //Get the response
                        HttpWebResponse response = (HttpWebResponse)httpWReq.GetResponse();
                        StreamReader reader = new StreamReader(response.GetResponseStream());
                        string responseString = reader.ReadToEnd();

                        //Close the response
                        reader.Close();
                        response.Close();
                    }
                    catch (SystemException ex)
                    {

                    }

                    IsEmailSend = true;
                }
                catch (Exception ex)
                {

                }
            }
            return IsEmailSend;
        }
        public static string GetResponse(string sURL)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(sURL); request.MaximumAutomaticRedirections = 4;
            request.Credentials = CredentialCache.DefaultCredentials;
            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse(); Stream receiveStream = response.GetResponseStream();
                StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8); string sResponse = readStream.ReadToEnd();
                response.Close();
                readStream.Close();
                return sResponse;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        public bool IsOTPChk(string OTPCHK, string OtpEmail)
        {
            bool IsOTPChk = false;
            try
            {
                var result = db.OTP.Select(x => new { otpid = x.otpid, otpcode = x.OTPCode, otpEmail = x.OTPEmail, otpMobile = x.OTPMobile, otpIsActive = x.IsActive }).Where(s => s.otpcode == OTPCHK).ToList();
                for (int i = 0; i < result.Count(); i++)
                {
                    if (result[i].otpcode == OTPCHK && result[i].otpEmail == OtpEmail && result[i].otpMobile == OtpEmail)
                    {
                        if (result[i].otpIsActive == true)
                        {
                            OTP f = db.OTP.Find(Convert.ToInt32(result[i].otpid));
                            f.IsActive = false;
                            db.SaveChanges();
                            IsOTPChk = true;
                        }
                    }
                }
                //if(result)
            }
            catch (Exception ex)
            {

            }
            return IsOTPChk;
        }

        public bool CheckCustomeexist(string cust)
        {
            bool iscustexist = false;
            if (cust.Length > 0)
            {
                CustomerDetails cd = db.CustomerDetails.Where(c => c.CustPhone == cust).FirstOrDefault();
                if (cd != null)
                {
                    if (cd.CustID != null)
                    {
                        iscustexist = true;
                    }
                }
            }
            return iscustexist;
        }
        public bool CheckCustomeexistNumber(string cust)
        {
            bool iscustexist = false;
            if (cust.Length > 0)
            {
                CustomerDetails cd = db.CustomerDetails.Where(c => c.CustEmail == cust).FirstOrDefault();

                // CustomerDetails cd = db.CustomerDetails.Select(i => i.CustPhone==cust).First();
                if (cd != null)
                {
                    if (cd.CustID != null)
                    {
                        iscustexist = true;
                    }
                }
            }
            return iscustexist;
        }

        public int CreateNewCustomer(string Emailormobile, string password)
        {

            CustomerDetails cd = new CustomerDetails();
            cd.CustEmail = Emailormobile;
            cd.Password = password;
            cd.IsActive = true;
            cd.IsDelete = false;
            cd.IsUpdate = false;
            cd.InsertDate = DateTime.Now;
            cd.LMDDate = DateTime.Now;
            cd.role = 4;
            db.CustomerDetails.Add(cd);
            db.SaveChanges();
            System.Text.RegularExpressions.Regex expr = new Regex(@"^\d{10}$");
            if (expr.IsMatch(Emailormobile))
            {
                //Your authentication key
                string authKey = "144054A8Is1H8TDV58be6289";
                //Multiple mobiles numbers separated by comma
                string mobileNumber = Emailormobile;
                //Sender ID,While using route4 sender id should be 6 characters long.
                string senderId = "SHPLOL";
                //Your message to send, Add URL encoding here.
                int _min = 1000;
                int _max = 9999;
                Random _rdm = new Random();
                int rnum = _rdm.Next(_min, _max);
                string message = HttpUtility.UrlEncode("Livingstud.com : Registration Thank you " + Emailormobile + " For Registring with us . Please Continue Shopping with us http://www.Livingstud.com");

                //Prepare you post parameters
                StringBuilder sbPostData = new StringBuilder();
                sbPostData.AppendFormat("authkey={0}", authKey);
                sbPostData.AppendFormat("&mobiles={0}", mobileNumber);
                sbPostData.AppendFormat("&message={0}", message);
                sbPostData.AppendFormat("&sender={0}", senderId);
                sbPostData.AppendFormat("&route={0}", "4");

                try
                {

                    //Call Send SMS API
                    string sendSMSUri = "http://api.msg91.com/api/sendhttp.php";
                    //Create HTTPWebrequest
                    HttpWebRequest httpWReq = (HttpWebRequest)WebRequest.Create(sendSMSUri);
                    //Prepare and Add URL Encoded data
                    UTF8Encoding encoding = new UTF8Encoding();
                    byte[] data = encoding.GetBytes(sbPostData.ToString());
                    //Specify post method
                    httpWReq.Method = "POST";
                    httpWReq.ContentType = "application/x-www-form-urlencoded";
                    httpWReq.ContentLength = data.Length;
                    using (Stream stream = httpWReq.GetRequestStream())
                    {
                        stream.Write(data, 0, data.Length);
                    }
                    //Get the response
                    HttpWebResponse response = (HttpWebResponse)httpWReq.GetResponse();
                    StreamReader reader = new StreamReader(response.GetResponseStream());
                    string responseString = reader.ReadToEnd();

                    //Close the response
                    reader.Close();
                    response.Close();
                }
                catch (SystemException ex)
                {

                }

            }
            else
            {
                try
                {
                    using (MailMessage mail = new MailMessage())
                    {
                        mail.From = new MailAddress("shoplootle@gmail.com");
                        mail.To.Add(Emailormobile);
                        mail.Subject = "Livingstud.com : Registration";

                        mail.Body = "Livingstud.com : Registration Thank you " + Emailormobile + " For Registring with us . Please Continue Shopping with us http://www.Livingstud.com";
                        mail.IsBodyHtml = true;
                        //  mail.Attachments.Add(new Attachment("C:\\file.zip"));
                        using (SmtpClient smtp = new SmtpClient())//465 //587
                        {
                            smtp.EnableSsl = true;
                            smtp.UseDefaultCredentials = false;
                            smtp.Credentials = new NetworkCredential("shoplootle@gmail.com", "nayananm291193");
                            smtp.Host = "smtp.gmail.com";
                            smtp.Port = 587;
                            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                            smtp.Send(mail);

                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }

            int lastCustId = cd.CustID;
            Session["CustId"] = lastCustId;



            return lastCustId;
        }

        public bool UpdateNewCustomer(string FirstName, string LastName, string CustState, string CustCity, string Pincode, string address, string Phone)
        {
            bool flag = false;
            CustomerDetails cd = db.CustomerDetails.Find(Convert.ToInt32(Session["CustId"]));
            cd.CustName = FirstName;
            cd.CustFName = FirstName;
            cd.CustLName = LastName;
            cd.CustState = Convert.ToInt32(CustState);
            cd.CustCity = Convert.ToInt32(CustCity);
            cd.CustPostalCode = Pincode;
            cd.CustAddress1 = address;
            cd.CustPhone = Phone;
            cd.LMDDate = DateTime.Now;
            //  db.CustomerDetails.Add(cd);
            db.SaveChanges();
            int lastCustId = cd.CustID;
            Session["UpCustId"] = lastCustId;
            if (lastCustId > 0)
            {
                flag = true;
            }
            return flag;
        }

        public int MakePayment()
        {
            int orderid = 0;
            if (Session["CustId"] != "" || Session["CustId"] != null)
            {
                CustomerDetails cd = db.CustomerDetails.Find(Convert.ToInt32(Session["CustId"]));
                int custid = Convert.ToInt32(Session["CustId"].ToString());
                int twopercent = 0;
                List<Order> or = db.Order.Where(x => x.CustomerID == custid).ToList();
                if (or.Count == 0)
                {
                    twopercent = Convert.ToInt32(TempData["pTotalCost"]) * 2 / 100;
                    cd.WalletMoney = twopercent;
                    db.Entry(cd).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
                Order NewOrder = new Order();
                NewOrder.CustomerID = cd.CustID;
                NewOrder.ProducID = 0;
                NewOrder.ColorID = 0;
                NewOrder.SizeID = 0;
                NewOrder.Quantity = 0;
                NewOrder.CompanyName = cd.CustFName ?? "";
                NewOrder.ContactFName = cd.CustFName ?? "";
                NewOrder.ContactLName = cd.CustLName ?? "";
                NewOrder.ContactTitle = "";
                NewOrder.Address1 = cd.CustAddress1;
                NewOrder.Address2 = cd.CustAddress2 ?? "";
                NewOrder.Order_City = cd.CustCity??0;
                NewOrder.Order_State = cd.CustState ?? 0;
                NewOrder.PostalCode = cd.CustPostalCode;
                NewOrder.Country = cd.CustCountry ?? 0;
                NewOrder.Phone = cd.CustPhone ?? "";
                NewOrder.Fax = cd.CustFax ?? "";
                NewOrder.Email = cd.CustEmail;
                NewOrder.WebSite = cd.CustWebSite ?? "";
                NewOrder.PaymentMethods = "ONLINE";
                NewOrder.TranactionID = 0;
                NewOrder.OrderDate = DateTime.Now;
                NewOrder.RequiredDate = DateTime.Now.AddDays(7);
                NewOrder.ShipedDate = DateTime.Now;
                NewOrder.TransactStatus = "Order Placed";
                NewOrder.ErrMsg = "NA";
                NewOrder.PaymentAmount = Convert.ToDecimal(TempData["pTotalCost"]);
                NewOrder.PaidAount = 0;
                NewOrder.PaymentDate = DateTime.Now;
                NewOrder.IsActive = true;
                NewOrder.IsDelete = false;
                NewOrder.IsUpdate = false;
                NewOrder.InsertDate = DateTime.Now;
                NewOrder.LMDDate = DateTime.Now;
                db.Order.Add(NewOrder);
                db.SaveChanges();
                orderid = NewOrder.OrderID;
                if (orderid > 0)
                {
                    List<Order> sessionProductList = (List<Order>)Session["CartLst"];
                    if (sessionProductList != null)
                    {
                        for (int i = 0; i < Convert.ToInt32(sessionProductList.Count); i++)
                        {

                            OrderedItems oi = new OrderedItems();
                            ProductDetails pdetails = db.ProductDetails.Find(Convert.ToInt32(sessionProductList[i].ProducID));
                            ProductDetails pds = db.ProductDetails.Find(Convert.ToInt32(sessionProductList[i].ProducID));

                            oi.OrderID = orderid;
                            oi.ProductID = Convert.ToInt32(sessionProductList[i].ProducID);
                            oi.ProductCode = pdetails.ProductCode;
                            oi.ProductName = pdetails.ProductName;
                            oi.ProductCategory = pdetails.ProductCategory??0;
                            oi.ProductSubCategory = pdetails.ProductSubCategory ?? 0;
                            oi.ProductSize = sessionProductList[i].SizeID.ToString();
                            oi.ProductColor = sessionProductList[i].ColorID.ToString();
                            oi.ProductQuantity = Convert.ToInt32(sessionProductList[i].Quantity);
                            oi.ProductPrice = sessionProductList[i].VAT;
                            oi.VAT = pdetails.VAT ?? 0;
                            oi.ProductWeight = pdetails.ProductWeight ?? 0;
                            oi.IsActive = true;
                            oi.IsDelete = false;
                            oi.IsUpdate = false;
                            oi.InsertDate = DateTime.Now;
                            oi.LMDDate = DateTime.Now;
                            db.OrderedItems.Add(oi);
                            db.SaveChanges();
                            //int remainingqty = qytnp[0] - oi.ProductQuantity;
                            //pds.ProductQuantity = remainingqty;
                            //db.Entry(pds).State = EntityState.Modified;
                            //db.SaveChanges();
                            Session["MyCartval"] = 0;


                        }
                    }
                }

                if (cd.CustPhone != null)
                {
                    //Your authentication key
                    string authKey = "144054A8Is1H8TDV58be6289";
                    //Multiple mobiles numbers separated by comma
                    string mobileNumber = cd.CustPhone;
                    //Sender ID,While using route4 sender id should be 6 characters long.
                    string senderId = "SHPLOL";
                    //Your message to send, Add URL encoding here.
                    int _min = 1000;
                    int _max = 9999;
                    Random _rdm = new Random();
                    int rnum = _rdm.Next(_min, _max);
                    string message = HttpUtility.UrlEncode("livingstud.com : Order Thank you " + cd.CustPhone + " For livingstud with us . Your Order id is  " + orderid + ", Track Order http://livingstud.com/Orders/OrderTrack , Please Continue Shopping with us http://www.livingstud.com");

                    //Prepare you post parameters
                    StringBuilder sbPostData = new StringBuilder();
                    sbPostData.AppendFormat("authkey={0}", authKey);
                    sbPostData.AppendFormat("&mobiles={0}", mobileNumber);
                    sbPostData.AppendFormat("&message={0}", message);
                    sbPostData.AppendFormat("&sender={0}", senderId);
                    sbPostData.AppendFormat("&route={0}", "4");

                    try
                    {

                        //Call Send SMS API
                        string sendSMSUri = "http://api.msg91.com/api/sendhttp.php";
                        //Create HTTPWebrequest
                        HttpWebRequest httpWReq = (HttpWebRequest)WebRequest.Create(sendSMSUri);
                        //Prepare and Add URL Encoded data
                        UTF8Encoding encoding = new UTF8Encoding();
                        byte[] data = encoding.GetBytes(sbPostData.ToString());
                        //Specify post method
                        httpWReq.Method = "POST";
                        httpWReq.ContentType = "application/x-www-form-urlencoded";
                        httpWReq.ContentLength = data.Length;
                        using (Stream stream = httpWReq.GetRequestStream())
                        {
                            stream.Write(data, 0, data.Length);
                        }
                        //Get the response
                        HttpWebResponse response = (HttpWebResponse)httpWReq.GetResponse();
                        StreamReader reader = new StreamReader(response.GetResponseStream());
                        string responseString = reader.ReadToEnd();

                        //Close the response
                        reader.Close();
                        response.Close();
                    }
                    catch (SystemException ex)
                    {

                    }

                }
                if (cd.CustEmail != null)
                {
                    try
                    {
                        using (MailMessage mail = new MailMessage())
                        {
                            mail.From = new MailAddress("26366020k@gmail.com");//shoplootle@gmail.com
                            mail.To.Add(cd.CustEmail);
                            mail.Subject = "livingstud.com : Thank you " + cd.CustFName + " For Shopping with us";
                            string ifmurl = "http://www.livingstud.com/orders/Paymentsuccessfull?orderid=" + orderid;
                            //mail.Body = "ShopLootle.com : Thank you " + cd.CustFName + " For Shopping with us . Your Order id is  " + orderid + ", Track Order http://livingstud.com/Orders/OrderTrack , Please Continue Shopping with us http://wwwLivingstud.com <br><iframe src=" + ifmurl + ">Sorry your browser does not support inline frames.<a href="+ifmurl+" target='_blank'>Click here for Purchase Receipt</a> </iframe>";
                            Order or1 = db.Order.Find(orderid);
                            if (or1.OrderID > 0)
                            {
                                List<OrderedItems> oi = db.OrderedItems.Where(n => n.OrderID == orderid).ToList();
                                ViewBag.Items = oi;


                            }
                            string mybodyforloop = "";
                            string mybody = "<html><head><link href='http://livingstud.com/css/bootstrap.css' type='text/css' rel='stylesheet' media='all'><link href='http://livingstud.com/css/style.css' type='text/css' rel='stylesheet' media='all'></head><body>Dear " + cd.CustFName + ",<br><br>Thank you for Shopping with us !<br> Your Order id is  " + orderid + ", Track Order http://livingstud.com/Orders/OrderTrack , <br>Please Continue Shopping with us http://www.Livingstud.com <br><div id='myinvoice'><div class='container'><div class='row'><div class='well col-xs-10 col-sm-10 col-md-6 col-xs-offset-1 col-sm-offset-1 col-md-offset-3'><div class='row'><center><h1>Receipt</h1></center><div class='col-xs-6 col-sm-6 col-md-6'><address><h4>Livingstud.com</h4>Email : shoplootle@gmail.com<br><abbr title='Phone'>P:</abbr> +918097471959</address></div><div class='col-xs-6 col-sm-6 col-md-6 text-right'><p><em>order Date: " + or1.InsertDate + "</em></p><p><em>Receipt #: " + orderid + "</em></p></div></div><div class='row'><div class='text-center'></div><table class='table table-hover'><thead><tr><th>Product</th><th>Qty</th><th class='text-center'>Price</th><th class='text-center'>Total</th></tr></thead><tbody>";
                            foreach (var items in ViewBag.Items)
                            {
                                mybodyforloop += mybodyforloop + "<tr><td class='col-md-9'><em>" + items.ProductName + "</em></td><td class='col-md-1' style='text-align: center'> " + items.ProductQuantity + " </td><td class='col-md-1 text-center'>₹ " + items.ProductPrice + "</td><td class='col-md-1 text-center'>₹ " + items.ProductPrice + "</td></tr>";
                            }
                            mybody = mybody + mybodyforloop + "<tr><td></td><td></td><td class='text-right'><h4><strong>Total: </strong></h4></td><td class='text-center text-danger'><h4><strong>₹ " + or1.PaymentAmount + "</strong></h4></td></tr></tbody></table></div><div class='row'><div class='text-center'><h1>Delivery Details</h1></div><table class='table table-hover'><thead><tr><th>Contact Details</th></tr></thead><tbody><tr><td class='col-md-9'><em>" + or1.ContactFName + "</em><em>" + or1.ContactLName + "</em><em>" + or1.Phone + "</em><em>, " + cd.CustEmail + "</em></td></tr></tbody></table><table class='table table-hover'><thead><tr><th>Address</th></tr></thead><tbody><tr><td class='col-md-9'><em>" + or1.Address1 + "<br />" + or1.Address2 + "</em></td></tr></tbody></table></div></div></div></div></div></body></html>";
                            mail.Body = mybody;
                            mail.IsBodyHtml = true;
                            //  mail.Attachments.Add(new Attachment("C:\\file.zip"));
                            using (SmtpClient smtp = new SmtpClient())//465 //587
                            {
                                smtp.EnableSsl = true;
                                smtp.UseDefaultCredentials = false;
                                smtp.Credentials = new NetworkCredential("shoplootle@gmail.com", "nayananm291193");
                                smtp.Host = "smtp.gmail.com";
                                smtp.Port = 587;
                                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                                smtp.Send(mail);

                            }
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                }


            }
            OrderLog ol = new OrderLog();
            ol.OID = orderid;
            ol.CustId = Session["CustId"].ToString();
            ol.Status = "Order Placed";
            ol.InsertDate = DateTime.Now;
            ol.Owner = "Vishu";
            db.OrderLogs.Add(ol);
            db.SaveChanges();
            return orderid;
        }

        public ActionResult Paymentsuccessfull(int orderid)
        {
            try
            {

                Order or = db.Order.Find(orderid);
                if (or.OrderID > 0)
                {
                    List<OrderedItems> oi = db.OrderedItems.Where(n => n.OrderID == orderid).ToList();
                    ViewBag.Items = oi;


                }


                return View(or);

            }
            catch (Exception ex) { }
            return View();
        }
    }
}
