using BasketAPI.Helpers;
using BasketAPI.Models.Details;
using BasketAPI.Models.Requests;
using BasketAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using static BasketAPI.Helpers.Enums;

namespace BasketAPI.Controllers
{
    [Route("api")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        #region Dependency injection
        private readonly IBasketService _basketService;

        public BasketController(IBasketService basketService)
        {
            _basketService = basketService;
        }
        #endregion Dependency injection

        #region CRUDs
        [HttpPost("baskets")]
        public ActionResult CreateBasket([FromBody, Required] CreateBasketRequest request)
        {
            #region Validations
            if (request.Customer == null)
                return NotFound(EndpointErrors.CustomerIsMandatory);
            #endregion Validations

            try
            {
                var tryAddBasket = _basketService.TryAddBasket(request);
                if (tryAddBasket != null)
                    return BadRequest(EndpointErrors.AddBasketFailed);
                else
                    return Ok(SuccessMessage.AddSuccess);
            }
            catch
            {
                return BadRequest(EndpointErrors.InternalServerError);
            }
        }

        [HttpPost("baskets/{id}/article-line")]
        public ActionResult AddBasketArticle([FromRoute(Name = "id"), Required] int basketId,
            [FromBody, Required] AddBasketArticleRequest request)
        {
            #region Validations
            if (request.ArticleName == null)
                return NotFound(EndpointErrors.ArticleNameIsMandatory);

            if (request.Price == null)
                return NotFound(EndpointErrors.ArticlePriceIsMandatory);

            var findBasket = _basketService.TryFindBasket(basketId);
            if (findBasket == null)
                return NotFound(EndpointErrors.BasketNotFound);

            if (findBasket.Status?.ToLower() == PermittedBasketStatus.Closed.ToString().ToLower())
                return BadRequest(EndpointErrors.BaskedClosed);
            #endregion Validations

            try
            {
                var tryAddBasketArticle = _basketService.TryAddBasketArticle(basketId, request);
                if (tryAddBasketArticle != null)
                    return BadRequest(EndpointErrors.AddBasketArticleFailed);
                else
                    return Ok(SuccessMessage.AddSuccess);
            }
            catch
            {
                return BadRequest(EndpointErrors.InternalServerError);
            }
        }

        [HttpPost("baskets/{id}")]
        public ActionResult UpdateBasketStatus([FromRoute(Name = "id"), Required] int basketId, [FromBody, Required] UpdateBasketStatusRequest request)
        {
            #region Validations
            if (request.Status == null)
                return NotFound(EndpointErrors.StatusIsMandatory);

            var findBasket = _basketService.TryFindBasket(basketId);
            if (findBasket == null)
                return NotFound(EndpointErrors.BasketNotFound);

            if (request.Status.ToLower() != PermittedBasketStatus.Opened.ToString().ToLower() &&
                request.Status.ToLower() != PermittedBasketStatus.Closed.ToString().ToLower())
                return BadRequest(EndpointErrors.BasketInvalidStatus);

            if (findBasket.Status?.ToLower() == request.Status.ToLower())
                return BadRequest(EndpointErrors.SameStatus(request.Status));
            #endregion Validations

            try
            {
                var tryAddBasketArticle = _basketService.TryUpdateBasketStatus(findBasket, request);
                if (!tryAddBasketArticle)
                    return BadRequest(EndpointErrors.UpdateBasketStatusFailed);
                else
                    return Ok(SuccessMessage.UpdateSuccess);
            }
            catch
            {
                return BadRequest(EndpointErrors.InternalServerError);
            }
        }

        [HttpGet("baskets/{id}")]
        public ActionResult<BasketDetails> GetBasket([FromRoute(Name = "id"), Required] int basketId)
        {
            #region Validations
            var findBasket = _basketService.TryFindBasket(basketId);
            if (findBasket == null)
                return NotFound(EndpointErrors.BasketNotFound);
            #endregion Validations
            
            try
            {
                var basketDetails = _basketService.GetBasketDetails(findBasket.Id);
                if (basketDetails == null)
                    return NotFound(EndpointErrors.FailedToBasketDetails);
                else
                    return basketDetails;
            }
            catch
            {
                return BadRequest(EndpointErrors.InternalServerError);
            }
        }
        #endregion CRUDs
    }
}
