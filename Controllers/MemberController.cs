using BaggageWeb.Models.ModelView;
using BaggageWeb.Models.Repositories;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
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
            var orderlist = OrderRepository.Instance.AllUserOrder(member.Id);
            ViewBag.orderlist = orderlist;
            return View();
        }
        public ActionResult OrderCancel(OrderView item, int memberId = 0)
        {
            if (item != null)
            {
                OrderRepository.Instance.delete(item);
            }
            return RedirectToAction("OrderList", "Member", new { memberId = item.MemberId });
        }
        public ActionResult NewOrder(int memberId = 0, int productId = 0)
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
            return View();
        }
        public ActionResult OrderConfirm(OrderView item)
        {
            OrderRepository.Instance.ChangeOrderStatus(item.Id, 1);
            return RedirectToAction("OrderList", "Member");
        }
    }
}