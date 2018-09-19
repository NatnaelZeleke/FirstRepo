using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Infrastructure;
using Domain.Services;
using Web.Infrastructure;
using Web.Services;
using Web.ViewModels;

namespace Web.Controllers
{
    [CustomAuthorize(Roles = "Admin, SubAdmin")]
    public class TenderTypeController : Controller
    {

        private readonly ITenderTypeService _tenderTypeService;
        private readonly IConverter _converter;
        public TenderTypeController(ITenderTypeService tenderTypeService,IConverter converter)
        {
            _tenderTypeService = tenderTypeService;
            _converter = converter;
        }


        public ActionResult Index()
        {
            var model = _tenderTypeService.GetAllTenderType();
            return View(model);
        }

        public ActionResult AddTenderType()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddTenderType(TenderTypeViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            _tenderTypeService.AddTenderType(_converter.TenderTypeViewModelToTenderType(model));
            _tenderTypeService.SaveService();
            return RedirectToAction("Index");
        }


        public ActionResult EditTenderType(long id)
        {
            var tenderType = _tenderTypeService.GetTenderTypeById(id);
            var model = _converter.TenderTypeToTenderTypeViewModel(tenderType);
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditTenderType(TenderTypeViewModel model)
        {
            if (!ModelState.IsValid) return View(model);             
            _tenderTypeService.EditTenderType(_converter.TenderTypeViewModelToTenderType(model));
            _tenderTypeService.SaveService();
            return RedirectToAction("Index");
        }

        public ActionResult DeleteTenderType(long id)
        {
            var model = _tenderTypeService.GetTenderTypeById(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteTenderType(TenderType model)
        {
            _tenderTypeService.DeleteTenderType(model.Id);
            _tenderTypeService.SaveService();
            return RedirectToAction("Index");
        }



    }
}