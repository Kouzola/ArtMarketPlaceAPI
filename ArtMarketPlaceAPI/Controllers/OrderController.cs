using ArtMarketPlaceAPI.Dto.Mappers;
using Domain_Layer.Interfaces.Order;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ArtMarketPlaceAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class OrderController(IOrderService orderService) : ControllerBase
    {
        private readonly IOrderService _orderService = orderService;

        #region GET
        [HttpGet("by-CustomerId")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> GetAllCustomerOrder(int customerId)
        {
            var orders = await _orderService.GetAllOrderOfCustomerAsync(customerId);
            return Ok(orders.Select(o => o.MapToDto()));
        }

        [HttpGet("by-ArtisanId")]
        [Authorize(Roles = "Artisan")]
        public async Task<IActionResult> GetAllArtisanOrder(int artisanId)
        {
            var orders = await _orderService.GetAllOrderForAnArtisanAsync(artisanId);
            return Ok(orders.Select(o => o.MapToDto()));
        }

        #endregion

        #region POST

        #endregion

        #region PUT

        #endregion

        #region DELETE

        #endregion
    }
}
