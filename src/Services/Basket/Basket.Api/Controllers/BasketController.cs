using Basket.Api.Entity;
using Basket.Api.Repositries;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Basket.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[Controller]")]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepositry _repositry;

        public BasketController(IBasketRepositry repositry)
        {
            _repositry = repositry;
        }

        [HttpGet("{userName}", Name = "GetBasket")]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCart>> GetBasket(string userName)
        {
            var basket = await _repositry.GetBasket(userName);
            return Ok(basket ?? new ShoppingCart(userName));
        }
        [HttpPost]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCart>> UpdateBasket([FromBody] ShoppingCart basket )
        {
            return Ok(await _repositry.UpdateBasket(basket));
        }
        [HttpDelete]
        public async Task<ActionResult> DeleteBasket(string userName)
        {
            await _repositry.DeleteBasket(userName);
            return Ok();
        }
    }
}
