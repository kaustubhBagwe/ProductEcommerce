using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Ecommerce.Models.Admin
{
    [Table("ProductDetails")]
    public class ProductDetails
    {
        [Key]
        public int ProductID { get; set; }

        public string StoreID { get; set; }

        public string ProdreferencetID { get; set; }

        public string ProductCode { get; set; }

        public string ProductName { get; set; }

        public string ProductDescription { get; set; }

        public int? ProductCategory { get; set; }

        public int? ProductSubCategory { get; set; }

        public string ProductSize { get; set; }

        public string ProductColor { get; set; }

        public int? ProductBrand { get; set; }

        public int? ProductQuantity { get; set; }

        public string ProductTag { get; set; }

        public string ProductSKUCode { get; set; }

        public string ProductMetatagdescription { get; set; }

        public string ProductMetatagKey { get; set; }

        public string ProductMetataAuthor { get; set; }

        public decimal? ProductPrice { get; set; }

        public int? CID { get; set; }

        public decimal? VAT { get; set; }

        public int? ProductImageId { get; set; }

        public decimal? ProductWeight { get; set; }

        public int? ProductStar { get; set; }

        public bool? IsActive { get; set; }

        public bool? IsDelete { get; set; }

        public bool? IsUpdate { get; set; }

        public DateTime? InsertDate { get; set; }

        public DateTime? LMDDate { get; set; }

    }

}