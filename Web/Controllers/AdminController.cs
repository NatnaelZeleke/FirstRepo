using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Domain.Infrastructure;
using Domain.Services;
using Domain.Utils;
using Microsoft.AspNet.Identity;
using Web.Infrastructure;
using Web.Models;
using Web.Services;
using Web.ViewModels;

namespace Web.Controllers
{
    [CustomAuthorize(Roles = "Admin, SubAdmin")]
    public class AdminController : Controller
    {
        public ApplicationUserManager UserManager { get; private set; }
        public ApplicationSignInManager SignInManager { get; private set; }
        private readonly IFileService _fileService;
//        private readonly IOcrService _ocrService;
        private readonly IUserService _userService;
        private readonly IUsersBySalesService _usersBySalesService;
        private readonly IConverter _converter;
        private readonly ITenderTypeService _tenderTypeService;
        private readonly ITenderService _tenderService;
        private readonly IAccountService _accountService;
        private readonly IEmailSenderService _emailSenderService;
        public AdminController(ApplicationUserManager userManager,
            ApplicationSignInManager signInManager,
            IUserService userService,
            IFileService fileService,
//            IOcrService ocrService,
            IUsersBySalesService usersBySalesService,
            IConverter converter,
            ITenderTypeService tenderTypeService,
            ITenderService tenderService,
            IAccountService accountService,
            IEmailSenderService emailSenderService)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            _userService = userService;
            _fileService = fileService;
//            _ocrService = ocrService;
            _usersBySalesService = usersBySalesService;
            _converter = converter;
            _tenderTypeService = tenderTypeService;
            _tenderService = tenderService;
            _accountService = accountService;
            _emailSenderService = emailSenderService;
        }
        public ActionResult Index()
        {
            return View();
        }
        [CustomAuthorize(Roles = "Admin")]
        public ActionResult SubAdminList()
        {
            var model = _userService.GetUsersByType(Enums.ProfileType.SubAdmin);
            return View(model);
        }


        [CustomAuthorize(Roles = "Admin")]
        public ActionResult AddSubAdmin()
        {
            return View();
        }
        [CustomAuthorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddSubAdmin(SignUpViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            var subAdmin = new ApplicationUser { UserName = model.FullName, Email = model.Email,PhoneNumber = model.PhoneNumber};
            var result = await UserManager.CreateAsync(subAdmin, model.Password);
            if (result.Succeeded)
            {
                await UserManager.AddToRoleAsync(subAdmin.Id, Enums.ProfileType.SubAdmin.ToString());
                return RedirectToAction("SubAdminList");
            }
            return View();
            //TODO handle exception here with error logging 
        }
        [CustomAuthorize(Roles = "Admin")]
        public ActionResult DeleteSubAdmin(long id)
        {
            var model = _userService.GetUserById(id);
            return View(model);
        }
        [CustomAuthorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteSubAdmin(User model)
        {
            if (_userService.DeleteUser(model.Id))
            {
                _userService.SaveService();
                return RedirectToAction("SubAdminList");
            }
            return View();
            //TODO handle exception here 
        }


        public ActionResult Setting()
        {
            var model = _userService.GetUserById(User.Identity.GetUserId<long>());
            return View(model);
        }

        public async Task<ActionResult> EditEmail()
        {
            var email = await UserManager.GetEmailAsync(User.Identity.GetUserId<long>());
            var model = new EditEmailViewModel { Email = email };
            return View(model);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditEmail(EditEmailViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            var checkUser = _userService.GetUserByEmailAddress(model.Email);
            if (checkUser != null && checkUser.Id != User.Identity.GetUserId<long>())
            {
                ModelState.AddModelError("", "User Email is already taken. Please use another email address.");
                return View(model);
            }
            var oldUser = await UserManager.FindByIdAsync(User.Identity.GetUserId<long>());
            oldUser.Email = model.Email;
            oldUser.UserName = model.Email;
            await RefreshUser(oldUser);
            return RedirectToAction("Setting");
        }
        public async Task RefreshUser(ApplicationUser user)
        {
            await UserManager.UpdateAsync(user);
            await SignInManager.SignInAsync(user, false, false);
        }


        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(EditPasswordViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            var user = await UserManager.ChangePasswordAsync(User.Identity.GetUserId<long>(), model.CurrentPassword,
                model.NewPassword);
            if (user != null && user.Succeeded)
            {
                return RedirectToAction("Setting");
            }
            ModelState.AddModelError("", "Invalid password change attempt!");
            return View(model);
        }


        public ActionResult ParseText()
        {
            return View();
        }

//        [HttpPost] 
//        public ActionResult ParseText(ParseTextViewModel model)
//        {
//            // 1 save the file in temp place
//            var tenderImgUrl = _fileService.SaveFileAsync(model.File.InputStream, model.File.FileName);
////            // 2 get the text from temp file 
//            var lang = model.Lang == 1 ? Lang.Amh : Lang.Eng;
//            var tenderImgText = _ocrService.ParseText(tenderImgUrl, lang);
////
////            //delete the file from temp place 
//            _fileService.DeleteFromTempFiles(tenderImgUrl);
//            return Json(tenderImgText);
//        }


        #region salesManController

        public ActionResult SalesManList()
        {
            var salesMans = _userService.GetUsersByType(Enums.ProfileType.SalesMan);
            var model = _converter.UsersToSalesViewModel(salesMans);
            return View(model);
        }

        public ActionResult SalesMan(long id)
        {
            var user = _userService.GetUserById(id);
            var userBySales = _usersBySalesService.GetUserBySalesId(id);
            var model = new SalesManViewModel
            {
                User = _converter.UserToUserViewModel(user),
                UsersBySales = _converter.UsersBySalesToUsersBySalesViewModel(userBySales),
                CustomersCount = userBySales.Count()
            };
            return View(model);
        }
        #endregion salesManController

        public ActionResult SendBulkEmail()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendBulkEmail(SendEmailViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            //First get list of tender types 
            var tenderTypes = _tenderTypeService.GetAllTenderType();
//            Parallel.ForEach(tenderTypes, async t =>
//            {
//                var userAccounts = _accountService.GetAccountsByTenderTypeIdForEmail(t.Id);
//                var tender = _tenderService.GetTenderByTenderTypeIdAndDate(t.Id, model.SelectionDate);
//                if (tender != null)
//                {
//                    //send email to all of the user
//                    await SendEmailToUsers(userAccounts, tender);
//                }
//            });
            foreach (var t in tenderTypes)
            {
                var userAccounts = _accountService.GetAccountsByTenderTypeIdForEmail(t.Id);
                var tender = _tenderService.GetTenderByTenderTypeIdAndDate(t.Id, model.SelectionDate);
                if (tender != null)
                {
                    //send email to all of the user
                    await SendEmailToUsers(userAccounts, tender);
                }
            }
            ViewBag.Result = "Success";
            return View("ConfirmSendEmailInBulk");
            //get list of users based on 
        }


        private async Task<bool> SendEmailToUsers(IEnumerable<Account> userAccounts, Tender tender)
        {
            foreach (var ua in userAccounts)
            {
                var header = "Chereta.net : Tenders published on " +
                             tender.NewsPaperPublishDate.ToString("MMMM dd yyyy");

                await _emailSenderService.SendEmailForActiveUsers(ua.Id, header, tender.TenderTitle);
            }
            return false;
        }


        #region userController

        public ActionResult UsersList()
        {
            var user = _userService.GetUsersByType(Enums.ProfileType.User);
            var model = _converter.UserToRegisteredUserViewModel(user);
            return View(model);

        }

        #endregion
    }
}