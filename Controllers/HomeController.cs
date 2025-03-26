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

        public ActionResult Register() { 
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
                    return RedirectToAction("SignUp", "Home", new { a });
                }
                else
                {
                    return RedirectToAction("index", "Login", new {a});
                }
            }
            return RedirectToAction("Index", "Login",new{a});
        }
        public void genRNG(string Email)
        {
            Random random = new Random();
            int a = random.Next(1000000);
            string User = "kuroiryu1612@gmail.com";
            string to = User;
            string from = "minhphat1612@gmail.com";
            MailMessage message = new MailMessage(from, to);
            message.Subject = "Order Confirm";
            message.Body = @"Order Confirm "+a;
            try
            {
                using (SmtpClient smtp = new SmtpClient())
                {
                    smtp.Host = "smtp.gmail.com";
                    smtp.EnableSsl = true;
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    NetworkCredential NetworkCred = new NetworkCredential(from, "msvm rjrs vuro bkfd");
                    smtp.Credentials = NetworkCred;
                    smtp.Port = 587;
                    smtp.Send(message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception caught in CreateTestMessage2(): {0}",
                    ex.ToString());
            }
        }
        
    }
}