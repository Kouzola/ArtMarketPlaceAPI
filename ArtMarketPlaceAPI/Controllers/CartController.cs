using ArtMarketPlaceAPI.Dto.Mappers;
using Domain_Layer.Interfaces.Cart;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ArtMarketPlaceAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Customer")]
    public class CartController(ICartService cartService) : ControllerBase
    {
        private readonly ICartService _cartService = cartService;

        #region GET
        [HttpGet]
        public async Task<IActionResult> GetCustomerCart(int customerId)
        {
            var cart = await _cartService.GetCartByUserIdAsync(customerId);
            return Ok(cart.MapToDto());
        }
        #endregion

        #region POST
        [HttpPost]
        public async Task<IActionResult> AddItemToCart(int customerId, int productId, int quantity)
        {
            var cart = await _cartService.AddItemToCartAsync(customerId, productId, quantity);
            return Ok(cart.MapToDto());
        }
        #endregion

        #region PUT
        [HttpPut("{id:int}")]
        public async Task<IActionResult> RemoveItemInCart(int cartId, int productId,int quantity)
        {
            var cart = await _cartService.RemoveItemFromCartAsync(cartId, productId, quantity);
            return Ok(cart.MapToDto());
        }

        #endregion

        #region DELETE
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteCart(int cartId)
        {
            var isDeleted = await _cartService.DeleteCartAsync(cartId);
            if (!isDeleted) return NotFound("Cart not found!");
            return Ok("Cart deleted!");
        }
        #endregion
    }
}
