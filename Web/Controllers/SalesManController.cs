using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Services;
using Microsoft.AspNet.Identity;
using Web.Infrastructure;
using Web.Services;
using Web.ViewModels;

namespace Web.Controllers
{
    [CustomAuthorize(Roles = "Admin,SubAdmin,SalesMan")]
    public class SalesManController : Controller
    {


        private readonly IUsersBySalesService _usersBySales;
        private readonly IConverter _converter;

        public SalesManController(IUsersBySalesService usersBySales,IConverter converter)
        {
            _usersBySales = usersBySales;
            _converter = converter;
        }


        public ActionResult AddCustomer()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddCustomer(UsersBySalesViewModel model)
        {
            model.UserId = User.Identity.GetUserId<long>();
            var userBySales = _converter.UsersBySalesViewModelToUserBySale(model);
            _usersBySales.AddUserBySales(userBySales);
            _usersBySales.SaveService();
            return RedirectToAction("CustomerList");

        }

        public ActionResult CustomerList()
        {
            var usersBySales = _usersBySales.GetUserBySalesId(User.Identity.GetUserId<long>());
            var model = _converter.UsersBySalesToUsersBySalesViewModel(usersBySales);
            return View(model);
        }
        // GET: SalesMan
        public ActionResult Index()
        {
            return RedirectToAction("AddCustomer");
        }

        public ActionResult Account()
        {
            return View();
        }

        public ActionResult Settings()
        {
            return View();
        }

         
    }
}