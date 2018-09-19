using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Index()
        {
            if (User.IsInRole("Admin") || User.IsInRole("SubAdmin"))
            {
                return View("AdminError");
            }
            return View();
        }
    }
}