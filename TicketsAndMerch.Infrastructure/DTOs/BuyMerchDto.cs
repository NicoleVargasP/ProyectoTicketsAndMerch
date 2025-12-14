// TicketsAndMerch.Infrastructure.DTOs/BuyMerchDto.cs
namespace TicketsAndMerch.Infrastructure.DTOs
{
    public class BuyMerchDto
    {
        public int UserId { get; set; }
        public int MerchId { get; set; }
        public int Quantity { get; set; }
        public string PaymentMethod { get; set; } = null!;
        public decimal TotalAmount { get; set; }
        public string PaymentState { get; set; } = "Pendiente";
    }
}
