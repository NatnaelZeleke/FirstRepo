using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.ViewModels
{
    public class LandingPageViewModel
    {
        public IEnumerable<TenderViewModel> Tenders { get; set; }
        public IEnumerable<NewsPaperViewModel> NewsPapers { get; set; }
    }
}