using System;

namespace TicketsAndMerch.Core.CustomEntities
{
    public class UserOrder
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public string OrderType { get; set; } = null!; // Ticket | Merch
        public string ItemName { get; set; } = null!;
        public int Quantity { get; set; }
        public decimal TotalAmount { get; set; }
        public string PaymentState { get; set; } = null!;
        public DateTime PurchaseDate { get; set; }
    }
}
