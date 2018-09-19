using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.ViewModels
{
    public class SaveTenderViewModel
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public long TenderId { get; set; }
    }
}