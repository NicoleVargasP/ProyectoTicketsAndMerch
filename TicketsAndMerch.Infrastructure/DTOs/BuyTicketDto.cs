namespace TicketsAndMerch.Infrastructure.DTOs
{
    public class BuyTicketDto
    {
        public int UserId { get; set; }
        public int ConcertId { get; set; }
        public int Quantity { get; set; }
        public string PaymentMethod { get; set; } = null!;
        public decimal TotalAmount { get; set; } 
        public string PaymentState { get; set; } = "Pendiente"; 
    }
}