﻿namespace ArtMarketPlaceAPI.Dto.Request
{
    public class CartRequestDto
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public int CustomizationId { get; set; }
    }
}
