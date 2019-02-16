using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Ecommerce.Models
{
    [Table("tbl_UserDetails")]
    public class tbl_UserDetails
    {
        [Key]
        public int UserID { get; set; }

        public string UserCode { get; set; }

        public int UserType { get; set; }

        public string UserFirstName { get; set; }

        public string UserLastName { get; set; }

        public bool? IsActive { get; set; }

        public DateTime? CreatedOn { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public string CreatedBy { get; set; }

        public string UserContactNumber { get; set; }

        public string UserEmailID { get; set; }

        public string Documents { get; set; }

        public int? UserRoleID { get; set; }

        public string Password { get; set; }

    }

}