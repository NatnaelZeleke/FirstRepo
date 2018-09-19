using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using Microsoft.AspNet.Identity;

namespace Web.Extensions.Identity
{
    public static class IdentityExtensions
    {
        public static string LastLogin(this IIdentity identity)
        {
            var time = ((ClaimsIdentity)identity).FindFirst("LastLogin").Value;
            return time;
        }

        public static long CurrentUserId(this IIdentity identity)
        {
            return identity.GetUserId<long>();
        }

        public static bool CanNavigate(this IIdentity identity)
        {
            var canNavigate = ((ClaimsIdentity)identity).FindFirst("CanNavigate").Value;
            return canNavigate.Contains("True");
        }
    }
}