using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Ecommerce.Models
{
    [Table("Discount")]
    public class Discount
    {
        [Key]
        public int DicountID { get; set; }
        public string StoreID { get; set; }
        public string DiscountCode { get; set; }
        public string DiscountTitle { get; set; }
        public string DiscountDetails { get; set; }
        public string DiscountImage { get; set; }
        public int DiscountedProductCategoryID { get; set; }
        public int DiscountProductID { get; set; }
        public int DiscountVendorID { get; set; }
        public DateTime DiscountStartDate { get; set; }
        public DateTime DiscountEndDate { get; set; }
        public string DiscountAmount { get; set; }
        public string DiscountAmountType { get; set; }
        public string InsertDate { get; set; }
        public string UpdateDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
        public bool IsUpdate { get; set; }
        public bool IsOnStore { get; set; }
        public bool IsOnCategories { get; set; }
        public bool IsOnProducts { get; set; }
        public bool IsAllProducts { get; set; }
    }
}