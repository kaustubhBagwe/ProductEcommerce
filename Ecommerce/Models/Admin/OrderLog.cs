using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Ecommerce.Models
{
    [Table("OrderLog")]
    public class OrderLog
    {
        [Key]
        public int OLogID { get; set; }
        public int OID { get; set; }
        public string CustId { get; set; }
        public string Status { get; set; }
        public DateTime InsertDate { get; set; }
        public string Owner { get; set; }

    }
}