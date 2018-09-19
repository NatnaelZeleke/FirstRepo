using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Web.Models;
using Web.Infrastructure;


namespace Web.Extensions.Identity
{
    public static class SignInManagerExtensions
    {

        public static async Task RefreshUserIdentity(this ApplicationSignInManager signinManager, long userId)
        {
            var user = await signinManager.UserManager.FindByIdAsync(userId);

            await signinManager.SignInAsync(user, false, false);
        }

        public static async Task RefreshUserIdentity(this ApplicationSignInManager signInManager, ApplicationUser user)
        {
            await signInManager.SignInAsync(user, false, false);
        }
    }
}