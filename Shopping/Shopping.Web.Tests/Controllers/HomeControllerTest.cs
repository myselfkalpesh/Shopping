using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shopping.Core;
using Shopping.Core.Models;
using Shopping.Core.ViewModels;
using Shopping.Web;
using Shopping.Web.Controllers;

namespace Shopping.Web.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        

        [TestMethod]
        public void IndexPageDoesReturnProducts()
        {
            // Arrange
            IRepository<Product> productContext = new Mocks.MockContext<Product>();
            IRepository<ProductCategory> productCategoryContext = new Mocks.MockContext<ProductCategory>();
            
            productContext.Insert(new Product());
            HomeController controller = new HomeController(productContext, productCategoryContext);

            // Act
            ViewResult result = controller.Index() as ViewResult;
            var viewModel = (ProductListViewModel)result.ViewData.Model;
            
            // Assert
            Assert.AreEqual(1, viewModel.Products.Count());
            
        }

        
    }
}
