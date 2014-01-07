﻿using System.Web.Mvc;
using set_basic_aspnet_mvc.Models;

namespace set_basic_aspnet_mvc.Controllers
{
    public class UserController : BaseController
    {
        [HttpGet, AllowAnonymous]
        public ViewResult New()
        {
            return View();
        }

        [HttpGet, AllowAnonymous]
        public ViewResult Login()
        {
            return View(new LoginModel());
        }

        [HttpGet]
        public ViewResult Logout()
        {
            return View();
        }

        [HttpGet, AllowAnonymous]
        public ViewResult PasswordReset()
        {
            return View();
        }

        [HttpGet, AllowAnonymous]
        public ViewResult PasswordChange(string email, string token)
        {
            return View();
        }

        [HttpGet]
        public ViewResult Detail()
        {
            return View();
        }
    }
}