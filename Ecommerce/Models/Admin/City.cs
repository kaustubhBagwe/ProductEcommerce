using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Ecommerce.Models
{
    [Table("Mst_City")]
    public class City
    {
        public int CityID { get; set; }
        public string CityName { get; set; }
        public int stateID { get; set; }
        public int Country { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
        public bool IsUpdate { get; set; }
        public DateTime InsertDate { get; set; }
        public DateTime LMDDate { get; set; }
    }
}