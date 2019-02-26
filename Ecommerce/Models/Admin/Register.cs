using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Ecommerce.Models
{
   
    public class Register
    {
       
        public int CustID { get; set; }
        public string Password { get; set; }
        public string CustName { get; set; }
        public string CustFName { get; set; }
        public string CustLName { get; set; }
        public string CusttTitle { get; set; }
        public string CustAddress1 { get; set; }
        public string CustAddress2 { get; set; }
        public int CustCity { get; set; }
        public int CustState { get; set; }
        public string CustPostalCode { get; set; }
        public int CustCountry { get; set; }
        public string CustPhone { get; set; }
        public string CustFax { get; set; }
        public string CustEmail { get; set; }
        public string CustWebSite { get; set; }
        public string CreaditCardNumber { get; set; }
        public string CardType { get; set; }
        public int CardExpMonth { get; set; }
        public int CardExpYear { get; set; }
        public decimal WalletMoney { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
        public bool IsUpdate { get; set; }
        public DateTime InsertDate { get; set; }
        public DateTime LMDDate { get; set; }
        public int roll { get; set; }
        public bool? IsSubScribe { get; set; }

    }
}