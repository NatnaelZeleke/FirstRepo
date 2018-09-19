using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Infrastructure;
using Domain.Services;
using Microsoft.AspNet.Identity;
using Web.Infrastructure;
using Web.Services;
using Web.ViewModels;

namespace Web.Controllers
{

    [CustomAuthorize(Roles = "Admin, SubAdmin")]
    public class TenderController : Controller
    {

        private readonly ITenderTypeService _tenderTypeService;
        private readonly ITenderService _tenderService;
        private readonly INewsPaperService _newsPaperService;
        private readonly IConverter _converter;
        public TenderController(ITenderTypeService tenderTypeService,
            ITenderService tenderService,
            INewsPaperService newsPaperService,
            IConverter converter)
        {
            _tenderTypeService = tenderTypeService;
            _tenderService = tenderService;
            _newsPaperService = newsPaperService;
            _converter = converter;
        }
       
        public ActionResult Index()
        {
            var tenders = _tenderService.GetAllTenders();
            var model = _converter.TenderToTenderViewModels(tenders,User.Identity.GetUserId<long>());
            return View(model);
        }
        public ActionResult Tender(long id)
        {
            var model = _converter.TenderToTenderViewModels(_tenderService.GetTenderById(id), User.Identity.GetUserId<long>());
            return View(model);
        }
        public ActionResult AddTender()
        {
            var model = new TenderViewModel
            {
                TenderTypes = _converter.TenderTypeToTenderTypeViewModel(_tenderTypeService.GetAllTenderType()),
                NewsPapers = _converter.NewsPaperToNewsPaperViewModel(_newsPaperService.GetAllNewsPapers())
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddTender(TenderViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.TenderTypes = _converter.TenderTypeToTenderTypeViewModel(_tenderTypeService.GetAllTenderType());
                model.NewsPapers = _converter.NewsPaperToNewsPaperViewModel(_newsPaperService.GetAllNewsPapers()); 
                return View(model);
            }
            var tender = _converter.TenderViewModelToTender(model);
            tender.PostedOn = DateTime.Now;
            _tenderService.AddTender(tender);
            if (_tenderService.SaveService())
            {
                return RedirectToAction("Tender", new {id = tender.Id});
            }
            return RedirectToAction("Index");
        }

         
        public ActionResult EditTender(long id)
        {
            var model = _converter.TenderToTenderViewModels(_tenderService.GetTenderById(id), User.Identity.GetUserId<long>());
            model.TenderTypes = _converter.TenderTypeToTenderTypeViewModel(_tenderTypeService.GetAllTenderType());
            model.NewsPapers = _converter.NewsPaperToNewsPaperViewModel(_newsPaperService.GetAllNewsPapers());
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditTender(TenderViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.TenderTypes = _converter.TenderTypeToTenderTypeViewModel(_tenderTypeService.GetAllTenderType());
                model.NewsPapers = _converter.NewsPaperToNewsPaperViewModel(_newsPaperService.GetAllNewsPapers());
                return View(model);
            }
            var tender = _converter.TenderViewModelToTender(model);
            _tenderService.EditTender(tender);
            _tenderService.SaveService();
            return RedirectToAction("Tender", new {id = tender.Id});
        }

        public ActionResult DeleteTender(long id)
        {
            var model = _tenderService.GetTenderById(id);
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteTender(Tender model)
        {
            _tenderService.DeleteTender(model.Id);
            _tenderService.SaveService();
            return RedirectToAction("Index");
        }

    }
}