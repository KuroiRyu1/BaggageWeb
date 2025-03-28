using BaggageWeb.Models.Daos;
using BaggageWeb.Models.ModelView;
using BaggageWeb.Models.Repositories;
using BaggageWeb.Models.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;

namespace BaggageWeb.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            MemberView item = (MemberView)Session["acc"];
            if (item!=null)
            {
                string username = item.UserName;
                string password = item.Password;
                MemberView mv = LoginRepository.Instance.checkLogin(username, password);
                if (mv != null)
                {
                    Session["acc"] = mv;
                    switch (mv.IdAuthentication)
                    {
                        case 1: return RedirectToAction("index", "Admin");
                        case 2: return RedirectToAction("index", "Home");
                    }
                }
            }
            return View();
        }
        public ActionResult acc_checked()
        {
            HttpCookie cookie = Request.Cookies["User"];
            string username = Request.Form["UserName"];
            string password = Request.Form["Password"];
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return RedirectToAction("Login", new { username, password });
            }
            MemberView mv = LoginRepository.Instance.checkLogin(username, password);
            if (mv != null)
            {
                cookie = new HttpCookie("User");
                cookie["username"] = mv.UserName;
                cookie["password"] = mv.Password;
                cookie.Expires = DateTime.Now.AddDays(10);
                HttpContext.Response.Cookies.Add(cookie);
                Session["acc"] = mv;
                switch (mv.IdAuthentication)
                {
                    case 1: return RedirectToAction("index", "Admin");
                    case 2: return RedirectToAction("index", "Home");
                }
            }
            return RedirectToAction("Index", "Home", new { username, password });

        }
        public static string GetCookie(HttpContext context, string key)
        {
            string value = string.Empty;

            var cookie = context.Request.Cookies[key];

            if (cookie != null)
            {
                if (string.IsNullOrWhiteSpace(cookie.Value))
                {
                    return value;
                }
                value = cookie.Value;
            }

            return value;
        }
        public ActionResult LogOut()
        {
            Session.Remove("acc");
            var cookie = Request.Cookies["User"];
            cookie.Expires = DateTime.Now.AddDays(-1);
            Response.Cookies.Add(cookie);
            return RedirectToAction("Index", "Login");
        }
        public ActionResult Register()
        {
            return View();
        }
        public ActionResult RegisterConfirm(MemberView entity)
        {
            string a = "";
            if (entity != null)
            {
                a = MemberRepository.Instance.SignUp(entity);
                if (a.Equals(StringValue.Message_Create_Account_Failed))
                {
                    return RedirectToAction("Register", "Login", new { a });
                }
                else
                {
                    return RedirectToAction("index", "Login", new { a });
                }
            }
            return RedirectToAction("Index", "Login", new { a });
        }
    }
}