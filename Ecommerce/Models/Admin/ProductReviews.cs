using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Ecommerce.Models
{
    [Table("ProductReviews")]
    public class ProductReviews
    {
        [Key]
        public int Rid {get;set;}
        public string ReviewMsg { get; set; }
        public int ProducctID { get; set; }
        public int Custid { get; set; }
        public int StarCount { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
        public bool IsUpdate { get; set; }
        public DateTime InsertDate { get; set; }
        public DateTime  LMDDate { get; set; }
    
    }
}