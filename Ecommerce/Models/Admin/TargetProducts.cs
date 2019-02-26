using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Ecommerce.Models
{
    [Table("tbl_targetProducts")]
    public class TargetProducts
    {
        [Key]
        public int ID { get; set; }
        public int TargetProductCategory { get; set; }
        public int TargetProductSubCategory { get; set; }
        public string insertDate { get; set; }

    }
}