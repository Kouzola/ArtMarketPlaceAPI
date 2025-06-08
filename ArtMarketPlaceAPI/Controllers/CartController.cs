using ArtMarketPlaceAPI.Dto.Mappers;
using ArtMarketPlaceAPI.Dto.Request;
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
        [HttpGet("{customerId:int}")]
        public async Task<IActionResult> GetCustomerCart(int customerId)
        {
            var currentUserId = User.FindFirst("id")?.Value;
            if (currentUserId != customerId.ToString()) return Forbid();

            var cart = await _cartService.GetCartByUserIdAsync(customerId);
            return Ok(cart.MapToDto());
        }
        #endregion

        #region POST
        [HttpPost]
        public async Task<IActionResult> AddItemToCart(CartRequestDto request)
        {
            var currentUserId = User.FindFirst("id")?.Value;
            if (currentUserId != request.UserId.ToString()) return Forbid();

            var cart = await _cartService.AddItemToCartAsync(request.UserId, request.ProductId, request.Quantity,request.CustomizationId);
            return Ok(cart.MapToDto());
        }
        #endregion

        #region PUT
        [HttpPut("{cartId:int}")]
        public async Task<IActionResult> RemoveItemInCart(int cartId, CartRequestDto request)
        {
            var cartToUpdate = await _cartService.GetCartByIdAsync(cartId);

            var currentUserId = User.FindFirst("id")?.Value;
            if (currentUserId != cartToUpdate.UserId.ToString()) return Forbid();

            var cart = await _cartService.RemoveItemFromCartAsync(cartId, request.ProductId, request.Quantity);
            return Ok(cart.MapToDto());
        }

        #endregion

        #region DELETE
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteCart(int cartId)
        {
            var cart = await _cartService.GetCartByIdAsync(cartId);

            var currentUserId = User.FindFirst("id")?.Value;
            if (currentUserId != cart.UserId.ToString()) return Forbid();

            var isDeleted = await _cartService.DeleteCartAsync(cartId);
            if (!isDeleted) return NotFound("Cart not found!");
            return Ok("Cart deleted!");
        }
        #endregion
    }
}
