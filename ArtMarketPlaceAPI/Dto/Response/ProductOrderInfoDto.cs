namespace ArtMarketPlaceAPI.Dto.Response
{
    public class ProductOrderInfoDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Reference {  get; set; } = string.Empty;
        public int Quantity { get; set; }
    }
}
