using System;
using System.Collections.Generic;

namespace TicketsAndMerch.Core.Entities;

public partial class Payment : BaseEntity
{
   // public int PaymentId { get; set; }

    public int OrderId { get; set; }

    public string? Method { get; set; }

    public DateTime? PaymentDate { get; set; }

    public decimal? OrderAmount { get; set; }

    public string? PaymentState { get; set; }

    public virtual Order Order { get; set; } = null!;
}
