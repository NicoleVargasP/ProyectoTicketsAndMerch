using System;

namespace TicketsAndMerch.Infrastructure.DTOs
{
    public class UserOrderDto
    {
        public int OrderId { get; set; }
        public string OrderType { get; set; } = null!;
        public string ItemName { get; set; } = null!;
        public int Quantity { get; set; }
        public decimal TotalAmount { get; set; }
        public string PaymentState { get; set; } = null!;
        public DateTime PurchaseDate { get; set; }
    }
}
