using ArtMarketPlaceAPI.Dto.Response;
using Domain_Layer.Entities;

namespace ArtMarketPlaceAPI.Dto.Mappers
{
    public static class ShipmentMapper
    {
        public static ShipmentResponseDto MapToDto(Shipment shipment)
        {
            return new ShipmentResponseDto
            {
                Id = shipment.Id,
                ShippingDate = shipment.ShippingDate,
                EstimatedArrivalDate = shipment.EstimatedArrivalDate,
                ArrivalDate = shipment.ArrivalDate,
                TrackingNumber = shipment.TrackingNumber,
                Status = shipment.Status,
                CreatedAt = shipment.CreatedAt,
                UpdatedAt = shipment.UpdatedAt,
                OrderCode = shipment.Order.Code,
                DeliveryPartner = shipment.DeliveryPartner.MapToDto(),
                Products = shipment.Products.Select(p => p.MapToDto()).ToList(),
            };
        }
    }
}
