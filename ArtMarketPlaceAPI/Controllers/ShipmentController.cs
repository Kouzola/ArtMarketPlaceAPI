using ArtMarketPlaceAPI.Dto.Mappers;
using Domain_Layer.Entities;
using Domain_Layer.Interfaces.Order;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ArtMarketPlaceAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ShipmentController(IOrderService orderService) : ControllerBase
    {
        private readonly IOrderService _orderService = orderService;

        #region GET
        [HttpGet("by-order/{orderId:int}")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> GetAllShipmentOfAnOrder(int orderId)
        {
            //TODO : Check identité du customer
            var shipments = await _orderService.GetAllShipmentOfAnOrderAsync(orderId);
            return Ok(shipments.Select(s => s.MapToDto()));
        }

        [HttpGet("by-delivery/{deliveryPartner:int}")]
        [Authorize(Roles = "Delivery")]
        public async Task<IActionResult> GetAllShipmentOfADeliveryPartner(int deliveryPartner)
        {
            //TODO : Check identité du Delivery
            var shipments = await _orderService.GetAllShipmentOfAnDeliveryPartnerAsync(deliveryPartner);
            return Ok(shipments.Select(s => s.MapToDto()));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetShipmentById(int id)
        {
            //TODO : Check identité du customer, delivery
            var shipment = await _orderService.GetShipmentByIdAsync(id);
            return Ok(shipment.MapToDto());
        }

        [HttpGet("by-tracking")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> GetShipmentById(string trackingNumber)
        {
            //TODO : Check identité du customer
            var shipment = await _orderService.GetShipmentByTrackingNumberAsync(trackingNumber);
            return Ok(shipment.MapToDto());
        }
        #endregion

        #region PUT
        [HttpPut("status/{shipmentId:int}")]
        [Authorize(Roles = "Delivery")]
        public async Task<IActionResult> UpdateShipmentDeliveryStatus(int shipmentId, ShipmentStatus status)
        {
            //TODO : Check identité du customer
            var shipment = await _orderService.UpdateShipmentDeliveryStatusAsync(shipmentId, status);
            return Ok(shipment.MapToDto());
        }

        [HttpPut("estimatedArrivalDate/{shipmentId:int}")]
        [Authorize(Roles = "Delivery")]
        public async Task<IActionResult> UpdateEstimatedTimeArrival(int shipmentId, DateTime estimatedTime)
        {
            //TODO : Check identité du customer
            var shipment = await _orderService.UpdateEstimatedTimeArrivalAsync(shipmentId, estimatedTime);
            return Ok(shipment.MapToDto());
        }

        #endregion

        #region DELETE

        #endregion

    }
}
