namespace TicketsAndMerch.Infrastructure.DTOs
{
    public class OrderDto
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public DateTime? DateOrder { get; set; }

        public string? State { get; set; }

        public int? MerchId { get; set; }

        public int? TicketId { get; set; }

        public decimal? OrderAmount { get; set; }

        public decimal? UnitPrice { get; set; }

        public string? OrderDetail { get; set; }

    }
}
