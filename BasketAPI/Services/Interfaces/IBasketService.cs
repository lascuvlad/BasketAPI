using BasketAPI.Models;
using BasketAPI.Models.Details;
using BasketAPI.Models.Requests;
using Microsoft.AspNetCore.Mvc;

namespace BasketAPI.Services.Interfaces
{
    public interface IBasketService
    {
        ActionResult? TryAddBasket(CreateBasketRequest request);
        ActionResult? TryAddBasketArticle(int basketId, AddBasketArticleRequest request);
        Basket? TryFindBasket(int basketId);
        bool TryUpdateBasketStatus(Basket basket, UpdateBasketStatusRequest request);
        BasketDetails GetBasketDetails(int basketId);
    }
}
