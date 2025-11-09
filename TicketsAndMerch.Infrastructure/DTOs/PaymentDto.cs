using System;
using System.Collections.Generic;

namespace TicketsAndMerch.Infrastructure.DTOs;

public partial class PaymentDto
{
    public int Id { get; set; }

    public int OrderId { get; set; }

    public string? Method { get; set; }

    public DateTime? PaymentDate { get; set; }

    public decimal? OrderAmount { get; set; }

    public string? PaymentState { get; set; }
}
