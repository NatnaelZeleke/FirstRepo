using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Domain.Services;
using Microsoft.AspNet.Identity;
using Web.Infrastructure;
using Web.Models;
using Web.ViewModels;

namespace Web.Controllers
{
    public class ProfileController : Controller
    {

        public ApplicationUserManager UserManager { get; set; }
        public ApplicationSignInManager SignInManager { get; set; }
        private readonly IUserService _userService;
        public ProfileController(ApplicationUserManager userManager,
            ApplicationSignInManager signInManager,
            IUserService userService)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            _userService = userService;
        }


        public ActionResult EditPhoneNumber()
        {
            var user = _userService.GetUserById(User.Identity.GetUserId<long>());
            var model = new EditPhoneNumberViewModel { PhoneNumber = user.PhoneNumber };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditPhoneNumber(EditPhoneNumberViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            _userService.EditPhoneNumber(User.Identity.GetUserId<long>(), model.PhoneNumber);
            _userService.SaveService();
            return RedirectToAction("Settings","User");
        }

        public async Task<ActionResult> EditEmail()
        {
            var email = await UserManager.GetEmailAsync(User.Identity.GetUserId<long>());
            var model = new EditEmailViewModel {Email = email};
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
            await RefreshUser(oldUser);
            return RedirectToAction("Settings","User");
        }

        public ActionResult EditUserName()
        {
            var model = new EditUserNameViewModel { UserName = User.Identity.Name };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditUserName(EditUserNameViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            //check if the user name is not occupied yet
            var checkUser = _userService.GetUserByUserName(model.UserName);
            if (checkUser != null && checkUser.Id != User.Identity.GetUserId<long>())
            {
                ModelState.AddModelError("", "User Name is already taken. Please use another name.");
                return View(model);
            }
            var oldUser = await UserManager.FindByIdAsync(User.Identity.GetUserId<long>());
            oldUser.UserName = model.UserName;
            await RefreshUser(oldUser);
            return RedirectToAction("Settings","User");
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
            var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId<long>(), model.CurrentPassword,
                model.NewPassword);
            if (result.Succeeded)
            {
                return RedirectToAction("Settings","User");
            }
            ModelState.AddModelError("", "Current password is not correct.");
            return View(model);
        }

        // GET: Profile
        public ActionResult Index()
        {
            return View();
        }


    }
}