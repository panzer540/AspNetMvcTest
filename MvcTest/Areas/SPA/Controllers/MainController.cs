using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViewModel.SPA;
using OldViewModel = MvcTestEntity.ViewModels;

namespace MvcTest.Areas.SPA.Controllers
{
    public class MainController : Controller
    {
        // GET: SPA/Main
        public ActionResult Index()
        {
            MainViewModel v = new MainViewModel();
            v.UserName = User.Identity.Name;
            v.FooterData = new OldViewModel.FooterViewModel();
            v.FooterData.CompanyName = "Stack Technology Company";
            v.FooterData.Year = DateTime.Now.Year.ToString();
            return View("Index",v);
        }
    }
}