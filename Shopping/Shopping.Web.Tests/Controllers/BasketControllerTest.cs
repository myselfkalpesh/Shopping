using System;
using System.Linq;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shopping.Core;
using Shopping.Core.Contracts;
using Shopping.Core.Models;
using Shopping.Core.ViewModels;
using Shopping.Services;
using Shopping.Web.Controllers;
using Shopping.Web.Tests.Mocks;

namespace Shopping.Web.Tests.Controllers
{
    [TestClass]
    public class BasketControllerTest
    {
        //To Check service
        [TestMethod]
        public void CanAddBasketItem()
        {
            // Arrange
            IRepository<Basket> baskets = new MockContext<Basket>();
            IRepository<Product> products = new MockContext<Product>();

            var httpContext = new MockHttpContext();
            
            IBasketService basketService = new BasketService(products, baskets);
            var controller = new BasketController(basketService);

            // Act
            basketService.AddToBasket(httpContext, "1");

            Basket basket = baskets.Collection().FirstOrDefault();

            // Assert
            Assert.IsNotNull(basket);
            Assert.AreEqual(1, basket.BasketItems.Count());
            Assert.AreEqual("1", basket.BasketItems.ToList().FirstOrDefault().ProductId);

        }
        
        //To Check controller--Same as above but instantiate controller and test it.
        [TestMethod]
        public void CanAddBasketItem1()
        {
            // Arrange(setup)
            IRepository<Basket> baskets = new MockContext<Basket>();
            IRepository<Product> products = new MockContext<Product>();

            var httpContext = new MockHttpContext();

            
            IBasketService basketService = new BasketService(products, baskets);
            var controller = new BasketController(basketService);
            controller.ControllerContext = new System.Web.Mvc.ControllerContext(httpContext, new System.Web.Routing.RouteData(), controller);

            // Act

            //basketService.AddToBasket(httpContext, "1");
            controller.AddToBasket("1");

            Basket basket = baskets.Collection().FirstOrDefault();

            // Assert
            Assert.IsNotNull(basket);
            Assert.AreEqual(1, basket.BasketItems.Count());
            Assert.AreEqual("1", basket.BasketItems.ToList().FirstOrDefault().ProductId);

        }

        [TestMethod]
        public void CanGetSummaryViewModel()
        {
            // Arrange
            IRepository<Basket> baskets = new MockContext<Basket>();
            IRepository<Product> products = new MockContext<Product>();

            products.Insert(new Product() { Id="1", Price=10.00m });
            products.Insert(new Product() { Id="2", Price=15.00m });
            products.Insert(new Product() { Id="3", Price=5.00m });

            Basket basket = new Basket();
            basket.BasketItems.Add(new BasketItem() { ProductId = "1", Quantity = 2 });
            basket.BasketItems.Add(new BasketItem() { ProductId = "2", Quantity = 3 });
            basket.BasketItems.Add(new BasketItem() { ProductId = "3", Quantity = 1 });
            baskets.Insert(basket);

            IBasketService basketService = new BasketService(products, baskets);

            var controller = new BasketController(basketService);
            var httpContext = new MockHttpContext();
            httpContext.Request.Cookies.Add(new System.Web.HttpCookie("eCommerceBasket") { Value = basket.Id });
            controller.ControllerContext = new System.Web.Mvc.ControllerContext(httpContext, new System.Web.Routing.RouteData(), controller);

            var result = controller.BasketSummary() as PartialViewResult;
            var basketSummary = (BasketSummaryViewModel)result.ViewData.Model;

            //Assert
            Assert.AreEqual(6, basketSummary.BasketCount);
            Assert.AreEqual(70, basketSummary.BasketTotal);

        }
    }
}
