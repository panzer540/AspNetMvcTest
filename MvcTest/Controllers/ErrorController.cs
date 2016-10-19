using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcApplication1.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        [AllowAnonymous]
        public ActionResult Index()
        {
            Exception e = new Exception("Invalid Controller or/and Action Name");
            HandleErrorInfo ErrInfo = new HandleErrorInfo(e, "Unknown", "Unknown");
            return View("Index", ErrInfo);
        }

        [AllowAnonymous]
        public ActionResult _404()
        {
            return View("_404");
        }
    }
}