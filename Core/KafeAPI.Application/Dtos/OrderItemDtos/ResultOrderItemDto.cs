using KafeAPI.Domain.Entities;

namespace KafeAPI.Application.Dtos.OrderItemDtos
{
    public class ResultOrderItemDto
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int MenuItemId { get; set; }
        public DetailOrderItemDto MenuItem { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
