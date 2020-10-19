using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace company.Models
{
    public class userlogin
    {
        [Display(Name = "Email")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Email id is required")]
        public string EMail { get; set; }
        [DataType(DataType.Password)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "password is required")]
        public string Password { get; set; }
        [Display(Name = "Remember Me")]
        public bool remeberme { get; set; }
    }
}