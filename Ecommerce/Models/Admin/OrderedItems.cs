using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Ecommerce.Models
{
    [Table("OrderItems")]
    public class OrderedItems
    {
        [Key]
        public int OrderItemsID { get; set; }
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public int ProductCategory { get; set; }
        public int ProductSubCategory { get; set; }
        public string ProductSize { get; set; }
        public string ProductColor { get; set; }
        public int ProductQuantity { get; set; }
        public decimal ProductPrice { get; set; }
        public decimal VAT { get; set; }
        public decimal ProductWeight { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
        public bool IsUpdate { get; set; }
        public DateTime InsertDate { get; set; }
        public DateTime LMDDate { get; set; }
    }
}