using System;
using System.Collections.Generic;

namespace TicketsAndMerch.Core.Entities;
public partial class Concert
{
    public int ConcertId { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public string Location { get; set; } = null!;

    public DateOnly Date { get; set; }

    public int? AvailableTickets { get; set; }

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
