using Squib.Data.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Squib.Frontend.Controllers
{
    public class ContactController : Controller
    {

        // GET: Contact
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Runadeal()
        {
            return View();
        }
    }
}