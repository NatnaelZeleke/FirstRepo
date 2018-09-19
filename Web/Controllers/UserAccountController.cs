using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Domain.Infrastructure;
using Domain.Services;
using Domain.Utils;
using Web.Infrastructure;
using Web.Services;
using Web.ViewModels;

namespace Web.Controllers
{
    [CustomAuthorize(Roles = "Admin,SubAdmin")]
    public class UserAccountController : Controller
    {


        private readonly IAccountService _accountService;
        private readonly IUserService _userService;
        private readonly IConverter _converter;
        private readonly IEmailSenderService _emailSenderService;

        public UserAccountController(IAccountService accountService,
            IUserService userService, 
            IConverter converter,
            IEmailSenderService emailSenderService)
        {
            _accountService = accountService;
            _userService = userService;
            _converter = converter;
            _emailSenderService = emailSenderService;
        }

        public ActionResult Index()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetUserAccount(UserViewModel model)
        {

            var user = _userService.GetUserByEmailAddress(model.Email);
            if (user == null) return RedirectToAction("Index");
            var userVm = _converter.UserToUserViewModel(user);
            var account = _converter.AccountToAccountViewModel(_accountService.GetAccountById(user.Id));
            var userAccountViewModel = new UserAccountViewModel
            {
                User = userVm,
                Account = account
            };
            return View(userAccountViewModel);
        }

        public ActionResult GetUserAccount(long id)
        {
            var user = _userService.GetUserById(id);
            if (user == null) return RedirectToAction("Index");
            var userVm = _converter.UserToUserViewModel(user);
            var account = _converter.AccountToAccountViewModel(_accountService.GetAccountById(user.Id));
            var userAccountViewModel = new UserAccountViewModel
            {
                User = userVm,
                Account = account
            };
            return View(userAccountViewModel);
        }

        public ActionResult RenewAccount(long id)
        {
            var user = _userService.GetUserById(id);           
            return View(user);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RenewAccount(RenewUserAccountViewModel model)
        {
            _accountService.RenewAccount(model.Id,model.RenewalTimeSpan);
            var result = _accountService.SaveService();
            //send account renewal confirmation email 
            var user = _userService.GetUserById(model.Id);
            var password = "";
            if (model.Password != null)
            {
                password = "Password = " + model.Password;
            }
            var timespan = "";
            timespan = model.RenewalTimeSpan == Enums.RenewalTimeSpan.SixMonth ? "6 month" : " 1 year";
            await _emailSenderService.SendAccountRenewalEmail(model.Id, user.UserName, user.Email, password, timespan);
            return RedirectToAction("Confirmation", new {id = model.Id, result});
        }
        public ActionResult DeRenewAccount(long id)
        {
            var user = _userService.GetUserById(id);
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeRenewAccount(UserViewModel model)
        {
            _accountService.DeRenewAccount(model.Id);
            var result = _accountService.SaveService();
            return RedirectToAction("Confirmation", new {id = model.Id,result});
        }
        public ActionResult Confirmation(long id, bool result)
        {
            ViewBag.Message = result ? "RESULT: SUCCESS" : "RESULT:FAILED";
            var user = _userService.GetUserById(id); 
            var userVm = _converter.UserToUserViewModel(user);
            var account = _converter.AccountToAccountViewModel(user.Account);
            var userAccountViewModel = new UserAccountViewModel
            {
                User = userVm,
                Account = account
            };
            return View(userAccountViewModel);
        }



        public ActionResult DeleteUser(long id)
        {
            var model = _userService.GetUserById(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteUser(User model)
        {
            _userService.DeleteUser(model.Id);
            ViewBag.Message = "";
            if (_userService.SaveService())
            {
                ViewBag.Message = "You have deleted the user successfuly";
            }
            else
            {
                ViewBag.Message = "Failed to delete the user";
            }
            return View("DeletionConfirmation");

        }
    }


}