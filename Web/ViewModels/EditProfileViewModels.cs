using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.ViewModels
{
   
    public class EditEmailViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }

    public class EditUserNameViewModel
    {
        [Required]
        public string UserName { get; set; }
    }
    public class EditPhoneNumberViewModel
    {
        [Required]
        public string PhoneNumber { get; set; }
    }
    public class EditPasswordViewModel
    {
        [Required]
        public string CurrentPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmNewPassword { get; set; }
    }


    public class SendEmailViewModel
    {
        [Required]
        public DateTime SelectionDate { get; set; }
    }
}