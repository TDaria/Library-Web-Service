﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace Library.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {    
        public ActionResult Index()
        {
            return View();
        }
    }
}