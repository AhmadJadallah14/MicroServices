using Discount.API.Entity;
using Discount.API.Repositrise;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Discount.API.Controllers
{
    [ApiController]
    [Route("api/v1/[Controller]")]
    public class DiscountController : ControllerBase
    {
        private readonly IDiscountRepositry _discountRepositry;
        public DiscountController(IDiscountRepositry discountRepositry)
        {
            _discountRepositry = discountRepositry ?? throw new ArgumentException(nameof(discountRepositry));
        }
        [HttpGet("{ProductName}",Name = "GetDiscount")]
        [ProducesResponseType(typeof(Coupon),(int)HttpStatusCode.OK)]
        public async Task<ActionResult<Coupon>> GetDiscount(string ProductName)
        {
            var coupon  = await _discountRepositry.GetDiscount(ProductName);
            return Ok(coupon);
        }
        [HttpPost]
        [ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Coupon>> CreateDiscount(Coupon coupon)
        {
             await _discountRepositry.CreateDiscount(coupon);
            return CreatedAtRoute("GetDiscount",new {ProductName = coupon.ProductName});
        }
        [HttpPut]
        [ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Coupon>> UpdateDiscount([FromBody]Coupon coupon)
        {
            return Ok(await _discountRepositry.UpdateDiscount(coupon));
        }

        [HttpDelete("{ProductName}", Name = "DeleteDiscount")]
        [ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Coupon>> DeleteDiscount(string productName)
        {
            return Ok(await _discountRepositry.DeleteDiscount(productName));
        }



    }
}
