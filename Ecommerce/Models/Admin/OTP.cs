﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Ecommerce.Models
{   [Table("OTP")]
    public class OTP
    {
        [Key]
        public int otpid { get; set; }
        public string OTPCode { get; set; }
        public string OTPEmail { get; set; }
        public string OTPMobile { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
        public bool IsUpdate { get; set; }
        public DateTime InsertDate { get; set; }
        public DateTime LMDDate { get; set; }
    }
}