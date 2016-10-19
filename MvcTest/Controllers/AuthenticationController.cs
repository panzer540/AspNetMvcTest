using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcTestEntity.ViewModels;
using MvcTestEntity.Models;
using System.Web.Security;

namespace MvcApplication1.Controllers
{
    public class AuthenticationController : Controller
    {

        /// <summary>
        /// 默认登陆Login
        /// </summary>
        /// <returns></returns>
        public ActionResult Login( )
        {
            return View("Login");
        }

        /// <summary>
        /// 响应登陆请求函数DoLogin
        /// </summary>
        /// <param name="u"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DoLogin( UserDetails u )
        {
            if (ModelState.IsValid)
            {
                EmployeeBusinessLayer ebl = new EmployeeBusinessLayer();
                UserStatus status = ebl.GetUserValidity( u );
                bool IsAdmin = false;
                if (status == UserStatus.AuthenticatedAdmin)
                {
                    IsAdmin = true;
                }
                else if (status == UserStatus.AuthenticatedUser)
                {
                    IsAdmin = false;
                }
                else
                {
                    ModelState.AddModelError("LoginError", "Invalid Username or Password");
                    return View("Login");
                }

                FormsAuthentication.SetAuthCookie(u.UserName, false);

                
                //FormsAuthenticationTicket LoginTicket = new FormsAuthenticationTicket(
                //    1,
                //    u.UserName,
                //    DateTime.Now,
                //    DateTime.Now.AddDays(30d),
                //    false,
                //    string.Empty
                //    );
                //HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName,FormsAuthentication.Encrypt(LoginTicket) );
                //cookie.Secure = false;
                //cookie.Expires = LoginTicket.Expiration;
                //cookie.Path = FormsAuthentication.FormsCookiePath;
                //Response.Cookies.Add(cookie);
                
                       
                Session["IsAdmin"] = IsAdmin;
                return RedirectToAction("Index", "Employee");
            }
            else
            {
                return View("Login");
            }
        }

        /// <summary>
        /// 响应注销登陆
        /// </summary>
        /// <returns></returns>
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Login");
        }







    }
}