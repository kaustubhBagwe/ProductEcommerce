using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


namespace Ecommerce.Models
{
    [Table("WishList")]
    public class WishList
    {
        [Key]
        public int WID { get; set; }
        public int PID { get; set; }
        public int CID { get; set; }
        public int VID { get; set; }
        public string PCOST { get; set; }
        public DateTime InsertDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }

    }
}