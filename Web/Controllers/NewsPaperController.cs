using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Infrastructure;
using Domain.Services;
using Web.Infrastructure;

namespace Web.Controllers
{
    [CustomAuthorize(Roles = "Admin")]
    public class NewsPaperController : Controller
    {

        private readonly INewsPaperService _newsPaperService;
        public NewsPaperController(INewsPaperService newsPaperService)
        {
            _newsPaperService = newsPaperService;
        }


        public ActionResult Index()
        {
            var model = _newsPaperService.GetAllNewsPapers();
            return View(model);
        }



        public ActionResult AddNewsPaper()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddNewsPaper(NewsPaper model)
        {
            _newsPaperService.AddNewsPaper(model);
            _newsPaperService.SaveService();
            return RedirectToAction("Index");
        }

        public ActionResult EditNewsPaper(long id)
        {
            var model = _newsPaperService.GetNewsPaperbyId(id);
            return View(model);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditNewsPaper(NewsPaper model)
        {
            _newsPaperService.EditNewsPaper(model);
            _newsPaperService.SaveService();
            return RedirectToAction("Index");
        }


        public ActionResult DeleteNewsPaper(long id)
        {
            var model = _newsPaperService.GetNewsPaperbyId(id);
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteNewsPaper(NewsPaper model)
        {
            _newsPaperService.DeleteNewsPaper(model.Id);
            _newsPaperService.SaveService();
            return RedirectToAction("Index");
        }
    }
}