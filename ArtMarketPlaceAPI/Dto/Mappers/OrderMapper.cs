using ArtMarketPlaceAPI.Dto.Response;
using Domain_Layer.Entities;
using System.Runtime.CompilerServices;

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
                Shipments = order.Shipments.Select(s => s.Id).ToList(),
                ProductsOrderedInfo = order.OrderProducts.Select(op => op.MapToDtoForProductInfo()).ToList()
            };
        }

        public static OrderResponseForArtisanDto MapToDtoForArtisan(this Order order, int artisanId)
        {
            return new OrderResponseForArtisanDto
            {
                Id = order.Id,
                Code = order.Code,
                ShippingOption = order.ShippingOption,
                Status = order.Status,
                CreatedAt = order.CreatedAt,
                UpdatedAt = order.UpdatedAt,
                Customer = order.Customer.MapToDto(),
                ProductsOrderedInfo = order.OrderProducts.Where(op => op.Product.ArtisanId == artisanId).Select(op => op.MapToDtoForProductInfo()).ToList()
            };
        }

        private static ProductOrderInfoDto MapToDtoForProductInfo(this OrderProduct orderProduct)
        {
            return new ProductOrderInfoDto
            {
                Name = orderProduct.Product.Name,
                Id = orderProduct.Product.Id,
                Reference = orderProduct.Product.Reference,
                Quantity = orderProduct.Quantity

            };
        }
    }
}
