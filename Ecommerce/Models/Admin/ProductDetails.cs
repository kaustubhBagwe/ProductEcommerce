using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ecommerce.Models.Admin
{
    [Table("ProductDetails")]
    public class ProductDetails
    {
        [Key]
        public int ProductID { get; set; }
        [Display(Name = "Store")]
        public string StoreID { get; set; }
        [Display(Name = "Reference ID")]
        public string ProdreferencetID { get; set; }
        [Display(Name = "Product Code")]
        public string ProductCode { get; set; }
        [Display(Name = "Product Name")]
        [Required(ErrorMessage = "Product Name Required")]
        public string ProductName { get; set; }
       
        [Display(Name = "Description")]
        [AllowHtml]
        public string ProductDescription { get; set; }
        [Display(Name = "Category")]
        public int? ProductCategory { get; set; }
        [Display(Name = "Product Sub Category")]
        public int? ProductSubCategory { get; set; }
        [Display(Name = "Product Size")]
        public int? ProductSize { get; set; }
        [Display(Name = "Product Color")]
        public string ProductColor { get; set; }
        [Display(Name = "Manufacture")]
        public int? ProductBrand { get; set; }
        [Display(Name = "Quantity")]
        public int? ProductQuantity { get; set; }
        [Display(Name = "Tags")]
        public string ProductTag { get; set; }
        [Display(Name = "SKU")]
        public string ProductSKUCode { get; set; }
        [Display(Name = "Meta Description")]
        public string ProductMetatagdescription { get; set; }
        [Display(Name = "Meta Key Description")]
        public string ProductMetatagKey { get; set; }
        [Display(Name = "Meta Author Description")]
        public string ProductMetataAuthor { get; set; }
        [Display(Name = "Price")]
        public decimal? ProductPrice { get; set; }

        public int? CID { get; set; }

        public decimal? VAT { get; set; }

        public int? ProductImageId { get; set; }
        [Display(Name = "Weight")]
        public decimal? ProductWeight { get; set; }
     
        public int? ProductStar { get; set; }

        public bool? IsActive { get; set; }

        public bool? IsDelete { get; set; }

        public bool? IsUpdate { get; set; }

        public DateTime? InsertDate { get; set; }

        public DateTime? LMDDate { get; set; }
        [Display(Name = "Short Description")]
        public string ShortDescription { get; set; }
        public string GTIN { get; set; }
        [Display(Name = "Customs Tariff Number")]
        public string Customs_Tariff_Number { get; set; }
        public bool? IsAllowedforCustomerReviews { get; set; }
        public bool? IsShowonHomePage { get; set; }
        public bool? IsPublished { get; set; }
        [Display(Name = "Delievery Time")]
        public string DelieveryTime { get; set; }
        public bool? IsFreeShiping { get; set; }
        [Display(Name = "OLD Price")]
        public decimal? OldPrice { get; set; }
        public bool? IsBuybuttonAvail { get; set; }
        public bool? IsWishListbuttonAvail { get; set; }
        public bool? IsCallForPriceAvail { get; set; }
        public int? DiscountID { get; set; }
        [Display(Name = "URL Alias")]
        public string URLAlias { get; set; }

        [ForeignKey("ProductSize")]
        public Size _size_RK { get; set; }
    }

}