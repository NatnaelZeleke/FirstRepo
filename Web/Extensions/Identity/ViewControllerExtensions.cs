using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using Domain.Utils;
 

namespace Web.Extensions.Identity
{
    public static class ViewControllerExtensions
    {
        public static bool IsAdmin(this WebPageRenderingBase page)
        {
            return page.Page.User.IsInRole(Enums.ProfileType.Admin.ToString());
        }
    }
}