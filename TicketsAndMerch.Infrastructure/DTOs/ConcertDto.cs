namespace TicketsAndMerch.Infrastructure.DTOs
{
    public class ConcertDto
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public string? Description { get; set; }

        public string Location { get; set; } = null!;

        public DateTime Date { get; set; }

        public int? AvailableTickets { get; set; }
    }
}
