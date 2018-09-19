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
    [CustomAuthorize(Roles = "User")]
    public class UserController : BaseController
    {
        private readonly ITenderTypeService _tenderTypeService;
        private readonly ITenderService _tenderService;
        private readonly IConverter _converter;
        private readonly IUserTenderTypeChoiceService _userTenderTypeChoiceService;
        private readonly IUserSavedTenderService _userSavedTenderService;
        private readonly IUserService _userService;
        private readonly INewsPaperService _newsPaperService;
        private readonly IAccountService _accountService;
        public UserController(ITenderTypeService tenderTypeService,
            ITenderService tenderService,
            IUserTenderTypeChoiceService userTenderTypeChoiceService,
            IUserSavedTenderService userSavedTenderService,
            IUserService userService,
            INewsPaperService newsPaperService,
            IConverter converter,
            IAccountService accountService)
        {
            _tenderTypeService = tenderTypeService;
            _tenderService = tenderService;
            _userTenderTypeChoiceService = userTenderTypeChoiceService;
            _userSavedTenderService = userSavedTenderService;
            _userService = userService;
            _newsPaperService = newsPaperService;
            _converter = converter;
            _accountService = accountService;
        }
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetSpecificTenders(GetTenderViewModel getTenderViewModel)
        {
            //check if the user is still active
            var user = _userService.GetUserById(User.Identity.GetUserId<long>());
            var account = user.Account;
            var status = 0;
            var utti = _converter.UserTenderTypeIdsToArray(GetCookieValue());
            var tenders = _tenderService.GetSpecificTenders(utti,getTenderViewModel.Skip,getTenderViewModel.Top);
            var tenderVm = _converter.TenderToTenderViewModels(tenders, User.Identity.GetUserId<long>());
            if (account.AccountExpirationDate < DateTime.Now){status = 1;}
            var model = new TenderWithStatusViewModel { Tenders = tenderVm, Status = status };//status == 0 is the user is still active
            return Json(model);
        }
        public ActionResult SelectTenderTypes()
        {
            var model = _converter.TenderTypeToTenderTypeViewModel(_tenderTypeService.GetAllTenderType());
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SelectTenderTypes(SelectTenderTypeViewModel model)
        {            
            var uttc = _converter.SelectedTenderTypeIdsToUserTenderTypeChoice(User.Identity.GetUserId<long>(),
                model.SelectedTenderTypeIds);
            _userTenderTypeChoiceService.AddUserTenderTypeChoice(uttc);
            _userTenderTypeChoiceService.SaveService();
            SetCookie(uttc.TenderTypeIds);
            return RedirectToAction("Index");
        }

        public ActionResult EditTenderTypes()
        {           
            var userTenderTypeChoice =
                _userTenderTypeChoiceService.GetUserTenderTypeChoiceById(User.Identity.GetUserId<long>());

            var selectedIds = new long[] {};
            if (userTenderTypeChoice != null)
            {
                selectedIds = _converter.UserTenderTypeIdsToArray(userTenderTypeChoice.TenderTypeIds);
            }
            
            var allTenderTypes = _tenderTypeService.GetAllTenderType();
            var model = _converter.TenderTypeToTenderTypeViewModel(selectedIds, allTenderTypes);
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditTenderTypeIdForEmail(SelectTenderTypeViewModel model)
        {
            var selectedId = model.SelectedTenderTypeIdForEmail;
            _accountService.EditTenderTypeIdForEmail(User.Identity.GetUserId<long>(), selectedId);
            _accountService.SaveService();
            return RedirectToAction("MyAccount");
        }

        public ActionResult EmailSettings()
        {
            var account = _accountService.GetAccountById(User.Identity.GetUserId<long>());
            long selectedId = 0;
            if (account.TenderTypeIdForEmail != null)
            {
                selectedId = (long) account.TenderTypeIdForEmail;
            }
            var idArray = new[]{selectedId};
            var allTenderTypes = _tenderTypeService.GetAllTenderType();
            var model = _converter.TenderTypeToTenderTypeViewModel(idArray, allTenderTypes);
            return View(model);

        }
        [HttpPost]
        public ActionResult SaveTender(SaveTenderViewModel model)
        {
            model.UserId = User.Identity.GetUserId<long>();
            var ust = _converter.SaveTenderViewModelToUserSaveTender(model);
            _userSavedTenderService.AddUserSavedTender(ust);
            if (_userSavedTenderService.SaveService())
            {
                return Json(0);
            }
            return Json(1);

        }

        [HttpPost]
        public ActionResult RemoveSavedTender(SaveTenderViewModel model)
        {
            _userSavedTenderService.DeleteUserSavedTender(User.Identity.GetUserId<long>(), model.TenderId);
            if (_userSavedTenderService.SaveService())
            {
                return Json(0);//sucess
            }
            return Json(1);
        }

        public ActionResult SavedTenders()
        {
            return View();
        }

//       [HttpPost]
        public ActionResult GroupedTender(GroupedTenderViewModel model)
        {
            if (model.GroupBy == 1)
            {
                //group by news paper 
                //tender selection id is the news paper id
                model.HeaderName =_newsPaperService.GetNewsPaperbyId(model.TenderSelectionId).NameAmharic;
            }
            else if (model.GroupBy == 2)
            {
                //group by tender type
                //tender selctin id here is the tender type id
                model.HeaderName = _tenderTypeService.GetTenderTypeById(model.TenderSelectionId).NameAmharic;
            }
            //         
            return View(model);
        }
        public ActionResult TenderReport()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetGroupedTender(GetTenderViewModel model)
        {
            //check if the user is still active
            var user = _userService.GetUserById(User.Identity.GetUserId<long>());
            var account = user.Account;
            var status = 0;             
            var tenders = _tenderService.GetGroupedTender(model.GroupBy, model.TenderSelectionId, model.Skip,
                model.Top);
            var tenderVm = _converter.TenderToTenderViewModels(tenders, User.Identity.GetUserId<long>());
            if (account.AccountExpirationDate < DateTime.Now) { status = 1; }
            var result = new TenderWithStatusViewModel { Tenders = tenderVm, Status = status };//status == 0 is the user is still active
            return Json(result);
        }

        [HttpPost]
        public ActionResult GetTenderReport()
        {
            var allTenders = _tenderService.GetAllTenders();
            var groupedTendersByNewsPaper = allTenders.GroupBy(t => t.NewsPaperId).Select(grp => grp.ToList());
            var groupedTendersByTenderType = allTenders.GroupBy(t => t.TenderTypeId).Select(grp => grp.ToList());
            var tenderByNewsPaper = _converter.TenderReportToNewsPaperViewModel(groupedTendersByNewsPaper).OrderByDescending(t => t.Count);
            var tenderByTenderType = _converter.TenderToTenderTypeViewModel(groupedTendersByTenderType)
                .OrderByDescending(t => t.Count);
            var allReport = new TenderReport
            {
                TenderWithNewsPaperReport = tenderByNewsPaper,
                TenderWithTenderTypeReport = tenderByTenderType
            };
            return Json(allReport);
        }
        [HttpPost]
        public ActionResult GetSavedTenders(GetTenderViewModel getTenderViewModel)
        {
            var ust = _userSavedTenderService.GetAllUserSavedTender(User.Identity.GetUserId<long>(),getTenderViewModel.Skip,getTenderViewModel.Top);
            var model = _converter.SavedTenderToTenderViewModel(ust);
            return Json(model);
        }
        public ActionResult MyAccount()
        {
            return View();
        }
        public ActionResult Settings()
        {
            var user = _userService.GetUserById(User.Identity.GetUserId<long>());
            var model = _converter.UserToUserViewModel(user);
            return View(model);
        }
        public ActionResult UserProfile()
        {
            var user = _userService.GetUserById(User.Identity.GetUserId<long>());
            if (user == null) return RedirectToAction("Index");
            var userVm = _converter.UserToUserViewModel(user);
            var account = _converter.AccountToAccountViewModel(user.Account);
            var userAccountViewModel = new UserAccountViewModel
            {
                User = userVm,
                Account = account
            };
            return View(userAccountViewModel);
        }


        
    }
}