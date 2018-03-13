﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Squib.Backoffice.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        //
        // GET: /Dashboard/

        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

    }
}
