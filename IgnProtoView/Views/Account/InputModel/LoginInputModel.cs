using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IgnProtoView.Views.Account.InputModel
{
    public class LoginInputModel
    {
        [Required]
        [StringLength(60)]
        [Display(Name = "Username")]
        public string UserName { get; set; }

        //[Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        //[DataType(DataType.Password)]
        //[Display(Name = "Confirm Password")]
        //[Compare("Password", ErrorMessage = "The password and confirmation password do not match")]
        //public string ConfirmPassword { get; set; }

        public bool RememberMe { get; set; }
    }
}
