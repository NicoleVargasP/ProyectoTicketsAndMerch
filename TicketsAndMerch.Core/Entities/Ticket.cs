using System;
using System.Collections.Generic;

namespace TicketsAndMerch.Core.Entities;

public partial class Ticket
{
    public int TicketId { get; set; }

    public int ConcertId { get; set; }

    public decimal Price { get; set; }

    public string? TicketType { get; set; }

    public int Stock { get; set; }

    public virtual Concert Concert { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
