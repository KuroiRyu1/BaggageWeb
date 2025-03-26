using BaggageWeb.Models.ModelView;
using BaggageWeb.Models.Repositories;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace BaggageWeb.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ProductList()
        {
            ViewBag.product = ProductRepository.Instance.All();
            return View();
        }
        public ActionResult RequestOrder()
        {
            var orderlist = OrderRepository.Instance.All();
            ViewBag.orderlist = orderlist;
            return View();
        }
        public ActionResult AddPriceToOrder(OrderView order1, int orderId)
        {
            OrderView order = OrderRepository.Instance.findById(orderId);
            ProductView product = new ProductView();
            if (order1 != null)
            {
                int productId = order1.ProductId;
                product = ProductRepository.Instance.findById(productId);
            }
            ViewBag.product = product;
            ViewBag.order = order;
            return View();
        }
        public ActionResult AddPrice(OrderView order, int price)
        {
            int orderId = 0;
            if (Request.QueryString["Price"] != null)
            {
                price = int.Parse(Request.QueryString["Price"]);
            }
            if (Request.QueryString["orderId"] != null)
            {
                orderId = int.Parse(Request.QueryString["orderId"]);
            }
            OrderRepository.Instance.ChangePrice(orderId, price);
            return RedirectToAction("RequestOrder", "Admin");
        }
        public ActionResult OrderConfirmList()
        {
            return View();
        }
        public ActionResult OrderLog()
        {
            return View();
        }
        public ActionResult AdminInfo()
        {
            return View();
        }
        public ActionResult ProductCreate()
        {
            return View();
        }
        public ActionResult ProductCreate2(HttpPostedFileBase Img, ProductView entity)
        {
            if (entity != null)
            {
                try
                {
                    if (Img != null)
                    {
                        string newFileName = $"{DateTime.Now.Ticks.ToString()}{Img.FileName}";
                        string fullPathSave = $"{Server.MapPath(Url.Content("~/content/images"))}\\{newFileName}";
                        Img.SaveAs(fullPathSave);
                        entity.ImageName = newFileName;
                        ProductRepository.Instance.create(entity);
                    }
                }
                catch (Exception e)
                {
                }
            }
            return RedirectToAction("Index");
        }
    }
}