using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CoreApiResponse;
using Basket.API.Repositories;
using Basket.API.Models;
using System.Net;

namespace Basket.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BasketController : BaseController
    {
        private readonly IBasketRepository _basketRepository;

        public BasketController(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ShoppingCart),(int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetBasket(string userName)
        {
            try
            {
                var basket = await _basketRepository.GetBasket(userName);
                if (basket == null)
                {
                    return CustomResult("Basket data not found", null, HttpStatusCode.NotFound);
                }
                return CustomResult("Basket data load successfully",basket);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, null, HttpStatusCode.BadRequest);

            }

        }

        [HttpPost]
        [ProducesResponseType(typeof(ShoppingCart),(int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateBasket([FromBody] ShoppingCart basket)
        {
            try
            {
                return CustomResult("Basket modified successfully", await _basketRepository.UpdateBasket(basket));
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, null, HttpStatusCode.BadRequest);
            }
        }

        [HttpDelete]
        [ProducesResponseType(typeof(void),(int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteBasket(string userName)
        {
            try
            {
                await _basketRepository.DeleteBasket(userName);
                return CustomResult("Basket deleted successfully");
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, null, HttpStatusCode.BadRequest);
            }
        }
    }
}
