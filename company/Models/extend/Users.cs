using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace company.Models
{
    [MetadataType(typeof(metadatauser))]
    public partial class Users
    {
        public string ConfirmPassword { get; set; }
       
    }
    public class metadatauser
    {
        [Display(Name = "First Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "First name  required")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Last name  required")]
        public string LastName { get; set; }
       
        [Display(Name = "Password")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Password  required")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "minimum 6 charcters required")]
        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Confirm Password  required")]
        [Compare("Password", ErrorMessage = "Confirm password and passsword is not match")]
        [DataType(DataType.Password)]
        public String ConfirmPassword { get; set; }

       
        [Display(Name = "Email")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Email  required")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "code  required")]
        public string code { get; set; }

    }
}