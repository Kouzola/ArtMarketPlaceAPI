using ArtMarketPlaceAPI.Dto.Response;
using Domain_Layer.Entities;

namespace ArtMarketPlaceAPI.Dto.Mappers
{
    public static class OrderMapper
    {
        public static OrderResponseDto MapToDto(this Order order)
        {
            return new OrderResponseDto
            {
                Id = order.Id,
                Code = order.Code,
                OrderDate = order.OrderDate,
                Status = order.Status,
                ShippingOption = order.ShippingOption,
                CreatedAt = order.CreatedAt,
                UpdatedAt = order.UpdatedAt,
                Customer = order.Customer.MapToDto(),
                PaymentDetail = order.PaymentDetail?.MapToDto(),
                Shipments = order.Shipments.Select(s => s.MapToDto()).ToList(),
            };
        }
    }
}
