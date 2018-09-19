using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.ViewModels
{

    public class SalesManViewModel
    {
        public UserViewModel User { get; set; }
        public IEnumerable<UsersBySalesViewModel> UsersBySales { get; set; }
        public int CustomersCount { get; set; }
    }
    public class UsersBySalesViewModel
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public bool Registered { get; set; }
        public bool Paid { get; set; }
    }
}