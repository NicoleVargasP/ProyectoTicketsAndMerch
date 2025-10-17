﻿namespace TicketsAndMerch.Infrastructure.DTOs
{
    public class ConcertDto
    {
        public int ConcertId { get; set; }

        public string Title { get; set; } = null!;

        public string? Description { get; set; }

        public string Location { get; set; } = null!;

        public DateOnly Date { get; set; }

        public int? AvailableTickets { get; set; }
    }
}
