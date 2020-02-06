using Shopping.Core;
using Shopping.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shopping.Web.Controllers
{
    public class HomeController : Controller
    {
        IRepository<Product> productContext;
        IRepository<ProductCategory> categoryContext;

        public HomeController(IRepository<Product> _productContext, IRepository<ProductCategory> _categoryContext)
        {
            productContext = _productContext;
            categoryContext = _categoryContext;
        }


        public ActionResult Index()
        {
            List<Product> products = productContext.Collection().ToList();
            return View(products);
        }

        public ActionResult Details(string Id)
        {
            Product product = productContext.Find(Id);

            if(product == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(product);
            }
            
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}