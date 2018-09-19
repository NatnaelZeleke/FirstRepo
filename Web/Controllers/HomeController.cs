using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Services;
using Web.Services;
using Web.ViewModels;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConverter _converter;
        private readonly ITenderService _tenderService; 
        public HomeController(IConverter converter,ITenderService tenderService)
        {
            _converter = converter;
            _tenderService = tenderService;
        }
        public ActionResult Index()
        {
            var groupedTenders = _tenderService.GetTenderReport();
            var newsPaperViewModel = _converter.TenderReportToNewsPaperViewModel(groupedTenders).OrderByDescending( t => t.Count);
            var tenders = _tenderService.GetAllTenders().Take(4);
            var tenderViewModels = _converter.TenderToTenderViewModels(tenders, 0);
            var model = new LandingPageViewModel
            {
                Tenders = tenderViewModels,
                NewsPapers = newsPaperViewModel
            };
            return View(model);
             
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

    }
}