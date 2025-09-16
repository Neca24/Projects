namespace Entities.DTO
{
    public class CartItemDTO
    {
        public int EventId { get; set; }
        public string? EventName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
