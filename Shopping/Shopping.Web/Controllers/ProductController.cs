using Shopping.Core;
using Shopping.Core.Models;
using Shopping.Core.ViewModels;
using Shopping.DataAccess.InMemory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shopping.Web.Controllers
{
    public class ProductController : Controller
    {
        IRepository<Product> context;
        IRepository<ProductCategory>  categoryContext;

        public ProductController(IRepository<Product> _productContext, IRepository<ProductCategory> _categoryContext)
        {
            context = _productContext;
            categoryContext = _categoryContext;
        }
        // GET: Product
        public ActionResult Index()
        {
            List<Product> products = context.Collection().ToList();
            return View(products);
        }

        public ActionResult Create()
        {
            ProductViewModel productVM = new ProductViewModel();
            productVM.Product = new Product();
            productVM.ProductCategories = categoryContext.Collection().ToList();
            return View(productVM);
        }

        [HttpPost]
        public ActionResult Create(ProductViewModel p)
        {
            if (!ModelState.IsValid)
            {
                return View(p.Product);
            }
            else
            {
                context.Insert(p.Product);
                context.Commit();

                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(string Id)
        {
            Product product = context.Find(Id);

            if (product == null)
            {
                return HttpNotFound();
            }
            else
            {
                ProductViewModel productVM = new ProductViewModel();
                productVM.Product = product;
                productVM.ProductCategories = categoryContext.Collection().ToList();
                return View(productVM);
            }
        }

        [HttpPost]
        public ActionResult Edit(ProductViewModel p, string Id)
        {
            Product productToEdit = context.Find(Id);

            if (productToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(p);
                }

                productToEdit.Category = p.Product.Category;
                productToEdit.Description = p.Product.Description;
                productToEdit.Image = p.Product.Image;
                productToEdit.Name = p.Product.Name;
                productToEdit.Price = p.Product.Price;

                context.Commit();

                return RedirectToAction("Index");
            }

        }

        public ActionResult Delete(string Id)
        {
            Product productToDelete = context.Find(Id);

            if (productToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(productToDelete);
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {
            Product productToDelete = context.Find(Id);

            if (productToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                context.Delete(Id);
                context.Commit();

                return RedirectToAction("Index");
            }
        }
    }
}