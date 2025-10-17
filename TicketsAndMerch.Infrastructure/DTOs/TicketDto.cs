namespace TicketsAndMerch.Infrastructure.DTOs
{
    public partial class TicketDto
    {
        public int TicketId { get; set; }

        public int ConcertId { get; set; }

        public decimal Price { get; set; }

        public string? TicketType { get; set; }

        public int Stock { get; set; }
    }
}
