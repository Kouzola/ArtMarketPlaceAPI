using ArtMarketPlaceAPI.Dto.Response;
using Domain_Layer.Entities;

namespace ArtMarketPlaceAPI.Dto.Mappers
{
    public static class PaymentDetailMapper
    {
        public static PaymentDetailResponseDto MapToDto(this PaymentDetail paymentDetail)
        {
            return new PaymentDetailResponseDto
            {
                PaymentMethod = paymentDetail.PaymentMethod,
                PaymentDate = paymentDetail.PaymentDate,
                Amout = paymentDetail.Amount
            };
        }
    }
}
