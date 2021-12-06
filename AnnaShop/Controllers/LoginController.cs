using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.DTO;
using System.Web.Security;
using Newtonsoft.Json;
using Facebook;
using System.Configuration;

namespace AnnaShop.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUser user;
        public LoginController(IUser user)
        {
            this.user = user;
        }
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        #region signup
        public ActionResult Register()
        {
            return View();
        }
        public string AddAcc(UserModel acc)
        {
            user.AddUser(acc);
            Response.Redirect("/Home/Index");
            return "Welcome to Annie Shop";
        }
        #endregion

        #region login/logout
        [HttpPost]
        public JsonResult userLogin(string tentk, string mk)
        {
            var u = user.GetSingle(tentk, mk);
            string result = "";
            if (u!=null)
            {
                result = "1";
                HttpCookie mycookie = new HttpCookie("LoginDetail");
                mycookie.Values["Username"] = u.UserName;
                mycookie.Values["Password"] = u.PassWord;
                mycookie.Expires = System.DateTime.Now.AddDays(365);
                Response.Cookies.Add(mycookie);
            }
            else
            {
                result = "Sai tài khoản hoặc mật khẩu!";
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Logout()
        {
            HttpCookie mycookie = Request.Cookies["LoginDetail"];
            mycookie.Expires = DateTime.Now.AddDays(-1);
            Response.Cookies.Add(mycookie);
            return View("Index");
        }
        #endregion
        public JsonResult Get_UserLogged()
        {
            UserModel u = new UserModel();
            if (Request.Cookies["LoginDetail"] != null)
            {
                string tentk = Request.Cookies["LoginDetail"].Values["Username"].ToString();
                string mk = Request.Cookies["LoginDetail"].Values["Password"].ToString();
                u = user.GetSingle(tentk, mk);
            }
            else
            {
                u = null;
            }
            return Json(u);
        }
        #region login with facebook
        private Uri RedirectUri
        {
            get
            {
                var uriBuilder = new UriBuilder(Request.Url);

                uriBuilder.Query = null;

                uriBuilder.Fragment = null;

                uriBuilder.Path = Url.Action("FacebookCallback");

                return uriBuilder.Uri;
            }
        }
        public ActionResult FacebookCallback(string code)
        {
            var fb = new FacebookClient();

            dynamic result = fb.Post("oauth/access_token", new
            {
                client_id = ConfigurationManager.AppSettings["FbAppId"],

                client_secret = ConfigurationManager.AppSettings["FbAppSecret"],

                redirect_uri = RedirectUri.AbsoluteUri,

                code = code
            });
            var accessToken = result.access_token;
            if (!string.IsNullOrEmpty(accessToken))
            {
                fb.AccessToken = accessToken;
                dynamic me = fb.Get("me?fields=link,first_name,currency,last_name,email,gender,locale,timezone,verified,picture,age_range");

                string email = me.email;
                string firstname = me.first_name;
                string lastname = me.last_name;

                UserModel us = new UserModel()
                {
                    Name = firstname + " " + lastname,
                    UserName = email,
                    PassWord = "123",
                    Email = email,
                    DateOfBirth = DateTime.Today,
                };
                var resultInsert = user.AddUserForFb(us);
                if(resultInsert > 0)
                {
                    HttpCookie mycookie = new HttpCookie("LoginDetail");
                    mycookie.Values["Username"] = us.UserName;
                    mycookie.Values["Password"] = us.PassWord;
                    mycookie.Expires = System.DateTime.Now.AddDays(365);
                    Response.Cookies.Add(mycookie);
                }
            }
            return RedirectToAction("Index", "Home");
        }
        public ActionResult LoginWithFb()
        {
            var fb = new FacebookClient();
            var loginurl = fb.GetLoginUrl(new
            {
                client_id = ConfigurationManager.AppSettings["FbAppId"],
                client_secret = ConfigurationManager.AppSettings["FbAppSecret"],
                redirect_uri = RedirectUri.AbsoluteUri,
                response_type = "code",
                scope = "email",
            });
            return Redirect(loginurl.AbsoluteUri);
        }
        #endregion

        public ActionResult Info()
        {
            return View();
        }
    }
}