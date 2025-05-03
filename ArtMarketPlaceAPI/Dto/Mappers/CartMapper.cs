using ArtMarketPlaceAPI.Dto.Response;
using Domain_Layer.Entities;

namespace ArtMarketPlaceAPI.Dto.Mappers
{
    public static class CartMapper
    {
        public static CartResponseDto MapToDto(this Cart cart)
        {
            return new CartResponseDto
            {
                Id = cart.Id,
                UserId = cart.UserId,
                Items = cart.Items.Select(ci => ci.MapToDto()).ToList(),
                CreatedAt = cart.CreatedAt,
                UpdatedAt = cart.UpdatedAt,
            };
        }
    }

    public static class CartItemMapper
    {
        public static CartItemResponseDto MapToDto(this CartItem cartItem)
        {
            return new CartItemResponseDto
            {
                Id = cartItem.Id,
                CartId = cartItem.CartId,
                Product = cartItem.Product.MapToDto(),
                Quantity = cartItem.Quantity,
            };
        }
    }
}
