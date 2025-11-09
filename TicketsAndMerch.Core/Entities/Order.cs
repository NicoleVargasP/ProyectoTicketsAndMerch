using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketsAndMerch.Core.Entities;

public partial class Order : BaseEntity
{
    // public int OrderId { get; set; }

    public int UserId { get; set; }
    [ForeignKey("UserId")]

    public DateTime? DateOrder { get; set; }

    public string? State { get; set; }

    public int? MerchId { get; set; }
    [ForeignKey("MerchId")]

    public int? TicketId { get; set; }
    [ForeignKey("TicketId")]

    public decimal? OrderAmount { get; set; }

    public decimal? UnitPrice { get; set; }

    public string? OrderDetail { get; set; }

    public virtual Merch? Merch { get; set; }

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual Ticket? Ticket { get; set; }

    public virtual User User { get; set; } = null!;
}
