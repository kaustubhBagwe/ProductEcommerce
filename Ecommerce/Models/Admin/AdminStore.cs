using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Ecommerce.Models.Admin
{
    [Table("StoreMaster")]
    public class AdminStore
    {
        [Key]
        public int Store_id { get; set; }
        [Required(ErrorMessage ="Store Name is required")]
        public string Store_Name { get; set; }
        public string StoreAddress { get; set; }
        public string StoreEmailID { get; set; }
        public string EmailID { get; set; }
        public string EmailPassword { get; set; }
        public string EmailPort { get; set; }
        public string EmailSMTP { get; set; }
        public string Currency { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool IsPrimary { get; set; }
        public string StoreImage { get; set; }
        public string Facebookpagelink { get; set; }
        public string GooglePlusLink { get; set; }
        public string Twitterlink { get; set; }
        public string Pinterestlink { get; set; }
        public string YoutubeLink { get; set; }
        public string InstagramLink { get; set; }
        public string MetaTitle { get; set; }
        public string MetaDescription { get; set; }

    }
}