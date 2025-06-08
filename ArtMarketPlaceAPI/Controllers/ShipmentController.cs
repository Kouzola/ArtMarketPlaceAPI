using ArtMarketPlaceAPI.Dto.Mappers;
using Business_Layer.Services;
using Domain_Layer.Entities;
using Domain_Layer.Interfaces.Order;
using Domain_Layer.Interfaces.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ArtMarketPlaceAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ShipmentController(IOrderService orderService, IProductService productService) : ControllerBase
    {
        private readonly IOrderService _orderService = orderService;
        private readonly IProductService _productService = productService;

        #region GET
        [HttpGet("by-order/{orderId:int}")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> GetAllShipmentOfAnOrder(int orderId)
        {
            var currentUserId = User.FindFirst("id")?.Value;
            var order = await _orderService.GetOrderByIdAsync(orderId);
            if (currentUserId != order.CustomerId.ToString()) return Forbid();

            var shipments = await _orderService.GetAllShipmentOfAnOrderAsync(orderId);
            return Ok(shipments.Select(s => s.MapToDto()));
        }

        [HttpGet("by-orderAndProduct/{orderId:int}")]
        [Authorize(Roles = "Artisan")]
        //Juste un productId suffit car, c'est un packet par produit meme si y en a plusieurs pour un meme artiste.
        public async Task<IActionResult> GetAllShipmentOfAnOrderAndProduct(int orderId, [FromQuery] int productId)
        {
            var currentUserId = User.FindFirst("id")?.Value;
            var order = await _orderService.GetOrderByIdAsync(orderId);
            //Si on trouve pas de produit appartenant à l'artisan, il est pas autorisé a recevoir des données sur les shipments de cette commande
            if (!order.OrderProducts.Any(op => op.Product.ArtisanId.ToString() == currentUserId)) return Forbid();

            var shipments = await _orderService.GetAllShipmentOfAnOrderAsync(orderId);
            var productsOfAnArtisan = await _productService.GetProductsByArtisanAsync(int.Parse(currentUserId!));
            //FILTRE POUR AVOIR QUE LES SHIPMENT QUI CONTIENNENT LES PRODUITS DE L'ARTISAN FAISANT LA REQUETE.
            var matchingShipments = shipments.Where(s => s.Products.Any(p => p.Id == productId))
                                    .Select(s => {
                                        var dto = s.MapToDto();
                                        dto.Products = dto.Products.Where(p => p == productId).ToList();
                                        return dto;
                                    });

            return Ok(matchingShipments);
        }

        [HttpGet("by-delivery/{deliveryPartner:int}")]
        [Authorize(Roles = "Delivery")]
        public async Task<IActionResult> GetAllShipmentOfADeliveryPartner(int deliveryPartner)
        {
            var currentUserId = User.FindFirst("id")?.Value;
            if (currentUserId != deliveryPartner.ToString()) return Forbid();

            var shipments = await _orderService.GetAllShipmentOfAnDeliveryPartnerAsync(deliveryPartner);
            return Ok(shipments.Select(s => s.MapToDto()));
        }

        [HttpGet("{id:int}")]
        [Authorize(Roles = "Delivery")]
        public async Task<IActionResult> GetShipmentById(int id)
        {
            var shipment = await _orderService.GetShipmentByIdAsync(id);

            var currentUserId = User.FindFirst("id")?.Value;
            if (currentUserId != shipment.DeliveryPartnerId.ToString()) return Forbid();

            return Ok(shipment.MapToDto());
        }

        //Supprimer ??
        [HttpGet("by-tracking")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> GetShipmentByTrackingNumber(string trackingNumber)
        {
            //TODO : Check identité du customer
            var shipment = await _orderService.GetShipmentByTrackingNumberAsync(trackingNumber);
            return Ok(shipment.MapToDto());
        }
        #endregion

        #region PUT
        [HttpPut("status/{shipmentId:int}")]
        [Authorize(Roles = "Delivery")]
        public async Task<IActionResult> UpdateShipmentDeliveryStatus(int shipmentId, [FromBody] ShipmentStatus status)
        {
            var shipment = await _orderService.GetShipmentByIdAsync(shipmentId);

            var currentUserId = User.FindFirst("id")?.Value;
            if (currentUserId != shipment.DeliveryPartnerId.ToString()) return Forbid();

            shipment = await _orderService.UpdateShipmentDeliveryStatusAsync(shipmentId, status);
            return Ok(shipment.MapToDto());
        }

        [HttpPut("estimatedArrivalDate/{shipmentId:int}")]
        [Authorize(Roles = "Delivery")]
        public async Task<IActionResult> UpdateEstimatedTimeArrival(int shipmentId, DateTime estimatedTime)
        {
            var shipment = await _orderService.GetShipmentByIdAsync(shipmentId);

            var currentUserId = User.FindFirst("id")?.Value;
            if (currentUserId != shipment.DeliveryPartnerId.ToString()) return Forbid();

            shipment = await _orderService.UpdateEstimatedTimeArrivalAsync(shipmentId, estimatedTime);
            return Ok(shipment.MapToDto());
        }

        #endregion

        #region DELETE

        #endregion

    }
}
