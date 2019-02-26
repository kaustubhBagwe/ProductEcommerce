using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Ecommerce.Models
{
    [Table("Mst_Color")]
    public class Color
    {
        [Key]
        public int ColorID { get; set; }
        public string ColorName { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
        public bool IsUpdate { get; set; }
        public DateTime InsertDate { get; set; }
        public DateTime LMDDate { get; set; }

    }
}