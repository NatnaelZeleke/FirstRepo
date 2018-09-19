using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using Domain.Services;
using Microsoft.AspNet.Identity;
using Web.Infrastructure;
using Web.Services;

namespace Web.Infrastructure
{
    public class BaseController:Controller
    {
        
        
        public HttpCookie GetCookie()
        {
            var cookie = Request.Cookies["_tenderTypeChoice"];
            if (cookie != null) return cookie;
            return new HttpCookie("_tenderTypeChoice")
            {
                Value = "",
                Expires = DateTime.Now.AddYears(1)
            };
        }

        public string GetCookieValue()
        {
            var cookie = GetCookie();
            return cookie.Value;//returns the current cookie tender type choice
        }

        public HttpCookie SetCookie(string tenderTypeChoice)
        {
             
            //Validate input
            //culture = CultureHelper.GetImplementedCulture(culture);
            //save culture in a cookie
            var cookie = Request.Cookies["_tenderTypeChoice"];
            if (cookie != null)
            {
                cookie.Value = tenderTypeChoice; //update cookie value
            }
            else
            {
                cookie = new HttpCookie("_tenderTypeChoice")
                {
                    Value = tenderTypeChoice,
                    Expires = DateTime.Now.AddYears(1)
                };
            }
            Response.Cookies.Add(cookie);
            return cookie;
        }
         
    }
}