using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Ecommerce.Models
{
    [Table("OrderDetails")]
    public class Order
    {
        [Key]
        public int OrderID { get; set; }
        public string StoreID { get; set; } 
        public int CustomerID { get; set; }
        public int ProducID { get; set; }
        public int ColorID { get; set; }
        public int SizeID { get; set; }
        public int Quantity { get; set; }
        public string CompanyName { get; set; }
        public string ContactFName { get; set; }
        public string ContactLName { get; set; }
        public string ContactTitle { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public int Order_City { get; set; }
        public int Order_State { get; set; }
        public string PostalCode { get; set; }
        public int Country { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string WebSite { get; set; }
        public string PaymentMethods { get; set; }
        public int TranactionID { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime RequiredDate { get; set; }
        public DateTime ShipedDate { get; set; }
        public decimal VAT { get; set; }
        public string TransactStatus { get; set; }
        public string ErrMsg { get; set; }
        public bool Fulfilled { get; set; }
        public decimal PaymentAmount { get; set; }
        public decimal PaidAount { get; set; }
        public DateTime PaymentDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
        public bool IsUpdate { get; set; }
        public DateTime? InsertDate { get; set; }
        public DateTime? LMDDate { get; set; }
    }
}