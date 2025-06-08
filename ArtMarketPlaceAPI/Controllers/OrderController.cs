using ArtMarketPlaceAPI.Dto.Mappers;
using ArtMarketPlaceAPI.Dto.Request;
using Azure.Core;
using Domain_Layer.Entities;
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
        [HttpGet("by-CustomerId/{customerId:int}")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> GetAllCustomerOrder(int customerId)
        {
            var currentUserId = User.FindFirst("id")?.Value;
            if (currentUserId != customerId.ToString()) return Forbid();
            var orders = await _orderService.GetAllOrderOfCustomerAsync(customerId);
            return Ok(orders.Select(o => o.MapToDto()));
        }

        [HttpGet("by-ArtisanId/{artisanId:int}")]
        [Authorize(Roles = "Artisan")]
        public async Task<IActionResult> GetAllArtisanOrder(int artisanId)
        {
            var currentUserId = User.FindFirst("id")?.Value;
            if (currentUserId != artisanId.ToString()) return Forbid();
            var orders = await _orderService.GetAllOrderForAnArtisanAsync(artisanId);
            return Ok(orders.Select(o => o.MapToDtoForArtisan(artisanId)));
        }

        [HttpGet("{id:int}")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            var currentUserId = User.FindFirst("id")?.Value;
            if (currentUserId != order.CustomerId.ToString()) return Forbid();
            return Ok(order.MapToDto());
        }

        [HttpGet]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> GetOrderByCode(string code)
        {
            var order = await _orderService.GetOrderByCodeAsync(code);
            var currentUserId = User.FindFirst("id")?.Value;
            if (currentUserId != order.CustomerId.ToString()) return Forbid();
            return Ok(order.MapToDto());
        }

        [HttpGet("price/{orderId:int}")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> GetOrderTotalPrice(int orderId)
        {
            var order = await _orderService.GetOrderByIdAsync(orderId);
            var currentUserId = User.FindFirst("id")?.Value;
            if (currentUserId != order.CustomerId.ToString()) return Forbid();

            var price = await _orderService.GetOrderTotalPriceAsync(orderId);
            return Ok(price);
        }
        #endregion

        #region POST
        [HttpPost]
        [Authorize(Roles = "Customer")]
        //AJOUTER LE SHIPPING OPTION ET DONC DANS LE CART AUSSI
        public async Task<IActionResult> CreateOrderFromCart(OrderRequestDto request)
        {
            var currentUserId = User.FindFirst("id")?.Value;
            if (currentUserId != request.CustomerId.ToString()) return Forbid();

            var order = await _orderService.CreateOrderFromCartAsync(request.CartId, request.CustomerId,request.ShippingOption);
            return Ok(order.MapToDto());
        }
        #endregion

        #region PUT
        [HttpPut("pay/{orderId:int}")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> PayOrder(int orderId, PaymentDetailRequestDto request)
        {
            var paymentDetail = new PaymentDetail
            {
                PaymentMethod = request.PaymentMethod,
                Amount = request.Amount,
                OrderId = orderId,
                PaymentDate = DateTime.Now
            };
            var order = await _orderService.PayOrderAsync(orderId, paymentDetail);
            return Ok(order.MapToDto());
        }

        [HttpPut("ship/{orderId:int}")]
        [Authorize(Roles = "Artisan")]
        public async Task<IActionResult> ShipOrder(int orderId, OrderRequestDto request)
        {
            var currentUserId = User.FindFirst("id")?.Value;
            if (currentUserId != request.ArtisanId.ToString()) return Forbid();
            var isShipped = await _orderService.ShipOrderAsync(orderId, request.DeliveryPartnerId, request.ArtisanId);
            if (isShipped) return Ok();
            return BadRequest();
        }

        [HttpPut("productValidate/{orderId:int}")]
        [Authorize(Roles = "Artisan")]
        public async Task<IActionResult> ValidateProductsInOrder(int orderId, [FromBody] int artisanId)
        {
            var currentUserId = User.FindFirst("id")?.Value;
            if (currentUserId != artisanId.ToString()) return Forbid();

            var isValidated = await _orderService.ValidateProductsInOrderAsync(orderId, artisanId);
            if (isValidated) return Ok();
            return BadRequest();
        }
        #endregion

        #region DELETE
        [HttpDelete("{orderId:int}")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> CancelOrder(int orderId)
        {
            var order = await _orderService.GetOrderByIdAsync(orderId);

            var currentUserId = User.FindFirst("id")?.Value;
            if (currentUserId != order.CustomerId.ToString()) return Forbid();

            var isCancel = await _orderService.CancelOrderAsync(orderId);
            if (isCancel) return Ok(new { message = "Order cancel" });
            return BadRequest(new { message = "Order cancel" });
        }
        #endregion
    }
}
