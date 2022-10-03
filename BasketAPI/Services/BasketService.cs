using AutoMapper;
using BasketAPI.Models;
using BasketAPI.Models.Details;
using BasketAPI.Models.Requests;
using BasketAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BasketAPI.Services
{
    public class BasketService : IBasketService
    {
        #region Dependency injection
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _db;

        public BasketService(IMapper mapper, ApplicationDbContext db)
        {
            _mapper = mapper;
            _db = db;
        }
        #endregion Dependency injection

        #region Methods
        public ActionResult? TryAddBasket(CreateBasketRequest request)
        {
            using var transaction = _db.Database.BeginTransaction();

            try
            {
                var newBasket = _mapper.Map<Basket>(request);
                var basket = _db.Baskets.Add(newBasket).Entity;

                _db.SaveChanges();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw new Exception("Internal server error.");
            }
            return null;
        }

        public ActionResult? TryAddBasketArticle(int basketId, AddBasketArticleRequest request)
        {
            using var transaction = _db.Database.BeginTransaction();

            try
            {
                var newBasketArticle = _mapper.Map<Article>(request);
                newBasketArticle.BasketId = basketId;
                var basketArticle = _db.Articles.Add(newBasketArticle).Entity;

                _db.SaveChanges();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                var error = ex.InnerException;
                transaction.Rollback();
                throw new Exception("Internal server error.");
            }
            return null;
        }

        public bool TryUpdateBasketStatus(Basket basket, UpdateBasketStatusRequest request)
        {
            using var transaction = _db.Database.BeginTransaction();

            try
            {
                var updateBasket = _mapper.Map(request, basket);
                request.Status = basket.Status;

                _db.Entry(updateBasket).State = EntityState.Modified;
                _db.SaveChanges();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new Exception("Internal server error.");
            }
            return true;
        }

        public BasketDetails GetBasketDetails(int basketId)
        {
            var basketDetails = _db.Baskets
                .Include(article => article.Article)
                .Where(basket => basket.Id == basketId).FirstOrDefault();

            var basket = _mapper.Map<BasketDetails>(basketDetails);

            if (basket.PaysVAT)
            {
                basket.TotalGross = basket.TotalNet * 11 / 10;
            }
            else
                basket.TotalGross = basket.TotalNet;

            return basket;

        }

        public Basket? TryFindBasket(int basketId)
        {
            var basket = _db.Baskets.Where(basket => basket.Id == basketId).FirstOrDefault();
            if (basket == null)
                return null;
            else
                return basket;
        }
        #endregion Methods
    }
}
