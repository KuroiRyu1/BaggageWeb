using BaggageWeb.Models.ModelView;
using BaggageWeb.Models.Repositories;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace BaggageWeb.Controllers
{
    public class MemberController : Controller
    {
        // GET: Member
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult OrderList()
        {
            var member = (MemberView)Session["acc"];
            var orderlist = new HashSet<OrderView>();
            if (member != null)
            {
                orderlist = OrderRepository.Instance.AllUserOrder(member.Id);
            }
            ViewBag.orderlist = orderlist;
            return View();
        }
        public ActionResult OrderCancel(OrderView item)
        {
            var member = (MemberView)Session["acc"];
            if (item != null)
            {
                OrderRepository.Instance.delete(item);
                if (item.OrderConfirm == 0)
                {
                    var product = ProductRepository.Instance.findById(item.ProductId);
                    ProductRepository.Instance.ChangeProductStatus(product.Id,1);
                }
            }
            return RedirectToAction("OrderList", "Member");
        }
        public ActionResult NewOrder(int productId = 0)
        {
            var member = (MemberView)Session["acc"];
            var product = new ProductView();
            if (productId != 0)
            {
                product = ProductRepository.Instance.findById(productId);
                ViewBag.product = product;
            }
            if (product.Active == 2)
            {
                return RedirectToAction("ProductList", "Member");
            }
            else
            {
                ProductRepository.Instance.ChangeProductStatus(productId, 2);
            }
            return View();
        }
        public ActionResult OrderCreate(OrderView entity, int memberId = 0)
        {
            if (memberId != 0)
            {
                var member1 = MemberRepository.Instance.findById(memberId);
                if (entity != null)
                {
                    OrderRepository.Instance.create(entity);
                }
                var orderlist = OrderRepository.Instance.AllUserOrder(memberId);
                ViewBag.data = member1;
                ViewBag.orderlist = orderlist;
                ProductRepository.Instance.ChangeProductStatus(memberId, 1);
            }
            return RedirectToAction("OrderList", "Member", new { memberId = memberId });
        }
        public ActionResult ProductList()
        {
            ViewBag.product = ProductRepository.Instance.All();
            return View();
        }
        public ActionResult ProductSearch()
        {
            string keyword = Request.QueryString["keyword"];
            return RedirectToAction("ProductListSearch", "Member", new { keyword = keyword });
        }
        public ActionResult ProductListSearch(string keyword)
        {
            HashSet<ProductView> product = ProductRepository.Instance.findByKeyWord(keyword);
            ViewBag.product = product;
            return View();
        }
        public ActionResult MemberInfo()
        {
            if (Session["acc"] != null)
            {
                return View();
            }
            return RedirectToAction("index", "Login");
        }
        public ActionResult ChangeEmail(string Email)
        {
            MemberView item = (MemberView)Session["acc"];
            if (Email != null)
            {
                MemberRepository.Instance.ChangeEmail(Email,item.Id);
            }
            return RedirectToAction("MemberInfo","Member");
        }
        public ActionResult OrderConfirm(OrderView item)
        {   
            OrderRepository.Instance.ChangeOrderStatus(item.Id, 1);
            ProductRepository.Instance.ChangeProductStatus(item.ProductId, 1);
            MessageSend();
            return RedirectToAction("OrderList", "Member");
        }
        public ActionResult ProductDetail(int productId=0)
        {
            var product =ProductRepository.Instance.findById(productId);
            if (product != null)
            {
                ViewBag.product = product;
            }
            return View();
        }
        public void MessageSend()
        {
            MemberView member = (MemberView)Session["acc"];
            string User = member.Email;
            string to = User;
            string from = "minhphat1612@gmail.com";
            MailMessage message = new MailMessage(from, to);
            message.Subject = "no-reply";
            message.Body = @"Order Confirm Success .";
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