using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Infrastructure;
using Domain.Utils;

namespace Web.ViewModels
{
    public class UserViewModel
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public string  Email { get; set; }
        public string PhoneNumber { get; set; }

    }

    public class AccountViewModel
    {
        public long Id { get; set; }
        public DateTime RegisteredOn { get; set; }
        public DateTime AccountExpirationDate { get; set; }
        public long TenderTypeIdForEmail { get; set; }
    }

    public class UserAccountViewModel
    {
        public UserViewModel User { get; set; }
        public AccountViewModel Account { get; set; }
    }

    public class RenewUserAccountViewModel
    {
        public long Id { get; set; }
        public Enums.RenewalTimeSpan RenewalTimeSpan { get; set; }
        public string  Password { get; set; }
    }

    public class RegisteredUserViewModel
    {
        public List<UserAccountViewModel> ActiveUserAccountViewModel { get; set; }
        public List<UserAccountViewModel> InActiveUserAccountViewModel { get; set; }
    }
    
}