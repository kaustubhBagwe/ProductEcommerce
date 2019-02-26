using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Ecommerce.Models
{
    [Table("FanBook")]
    public class FanBook
    {
        [Key]
        public int FanID { get; set; }
        public string FanbbokMsg { get; set; }
        public int CustID { get; set; }
        public int Productid { get; set; }
        public string FanBookTags { get; set; }
        public string FanBookImage { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
        public bool IsUpdate { get; set; }
        public DateTime InsertDate { get; set; }
        public DateTime LMDDate { get; set; }

    }
}