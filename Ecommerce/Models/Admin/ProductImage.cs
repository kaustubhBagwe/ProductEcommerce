using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Ecommerce.Models
{
    [Table("ProductImage")]
    public class ProductImage
    {
        [Key]
        public int ImageID { get; set; }
        public int IProductID { get; set; }
        public string ImageName { get; set; }
        public string ImagePath { get; set; }
        public string ImageDescription { get; set; }
        public int ILike { get; set; }
        public int IDislike { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
        public bool IsUpdate { get; set; }
        public DateTime? InsertDate { get; set; }
        public DateTime? LMDDate { get; set; }

    }
}