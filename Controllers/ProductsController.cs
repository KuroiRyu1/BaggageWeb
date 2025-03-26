using BaggageWeb.Models.ModelView;
using BaggageWeb.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BaggageWeb.Controllers
{
    public class ProductsController : Controller
    {
        // GET: Products
        public ActionResult Index()
        {
            ViewBag.data = ProductRepository.Instance.All();
            return View();
        }

        public ActionResult Edit(int id) {
            ViewBag.data = ProductRepository.Instance.findById(id);
            return View();
        }
        public ActionResult Edit2(HttpPostedFileBase Img, ProductView entity)
        {
            if (entity != null)
            {
                
                try
                {
                    string oldFileName = ProductRepository.Instance.findById(entity.Id).ImageName;
                    string oldfullPath = $"{Server.MapPath(Url.Content("~/content/images"))}\\{oldFileName}";
                    if (Img != null)
                    {
                        if (System.IO.File.Exists(oldfullPath))
                        {
                            System.IO.File.Delete(oldfullPath);
                            string newFileName = $"{DateTime.Now.Ticks.ToString()}{Img.FileName}";
                            string fullPathSave = $"{Server.MapPath(Url.Content("~/content/images"))}\\{newFileName}";
                            Img.SaveAs(fullPathSave);
                            entity.ImageName = newFileName;
                            ProductRepository.Instance.create(entity);
                        }
                    }
                    else
                    {
                        entity.ImageName = oldFileName;
                        ProductRepository.Instance.create(entity);
                    }
                }
                catch (Exception e)
                {
                }
            }

            return RedirectToAction("index", "Products");
        }
    }
}