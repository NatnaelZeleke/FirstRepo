using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using Microsoft.AspNet.Identity;
using Web.Models;

namespace Web.Extensions.Identity
{
    public static class UserManagerExtensions
    {

        public static async Task<ApplicationUser> FindByEmailOrNameAsync(this UserManager<ApplicationUser,long> manager,string emailOrName)
        {
            if (emailOrName.Contains("@"))
            {
                return await manager.FindByEmailAsync(emailOrName);
            }
            return await manager.FindByNameAsync(emailOrName);
        }

        public static async Task<ApplicationUser> ChangeUserName(this UserManager<ApplicationUser,long> manager, string newUserName,long userId)
        {
            if (await manager.DoesUserNameExist(newUserName)) return null;
            var user = await manager.FindByIdAsync(userId);

            if (user == null) return null;
            var result = await manager.UpdateAsync(user);

            return !result.Succeeded ? null : user;
        }

        public static async Task<bool> DoesUserNameExist(this UserManager<ApplicationUser, long> manager,
            string userName)
        {
            return await manager.FindByNameAsync(userName) != null;
        }

        public static async Task<bool> DoesEmailExists(this UserManager<ApplicationUser, long> manager, string email)
        {
            return await manager.FindByEmailAsync(email) != null;
        }

        public static async Task<string> GetUserEmail(this UserManager<ApplicationUser, long> manager, long userId)
        {
            return await manager.GetEmailAsync(userId);
        }


         

    }
}