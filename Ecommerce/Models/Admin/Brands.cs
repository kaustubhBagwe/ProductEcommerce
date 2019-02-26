using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Ecommerce.Models
{
    [Table("tbl_BrandMst")]
    public class Brands
    {
        [Key]
        public int BrandId { get; set; }
        public string BrandName { get; set; }
        public bool IsDelete { get; set; }
        public bool IsActive { get; set; }
        public bool IsUpdate { get; set; }

    }
}