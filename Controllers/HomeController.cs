using BaggageWeb.Models.ModelView;
using BaggageWeb.Models.Repositories;
using BaggageWeb.Models.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace BaggageWeb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.data = ProductRepository.Instance.All();
            return View();
        }
        public ActionResult ProductList()
        {
            ViewBag.product = ProductRepository.Instance.All();
            return View();
        }

    }
}