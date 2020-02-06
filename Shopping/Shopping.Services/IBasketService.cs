using Shopping.Core.ViewModels;
using System.Collections.Generic;
using System.Web;

namespace Shopping.Services
{
    public interface IBasketService
    {
        void AddtoBasket(HttpContextBase httpContext, string productId);
        List<BasketItemViewModel> GetBasketItems(HttpContextBase httpContext);
        BasketSummaryViewModel GetBasketSummary(HttpContextBase httpContext);
        void RemoveFromBasket(HttpContextBase httpContext, string itemId);
    }
}