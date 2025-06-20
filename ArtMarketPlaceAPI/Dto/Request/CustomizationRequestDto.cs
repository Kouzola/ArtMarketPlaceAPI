﻿namespace ArtMarketPlaceAPI.Dto.Request
{
    public class CustomizationRequestDto
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public double Price { get; set; }
        public int ProductId { get; set; }
    }
}
