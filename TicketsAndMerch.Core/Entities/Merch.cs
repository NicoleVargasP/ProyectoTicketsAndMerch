using System;
using System.Collections.Generic;

namespace TicketsAndMerch.Core.Entities;

public partial class Merch
{
    public int MerchId { get; set; }

    public string MerchName { get; set; } = null!;

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public string? TypeMerch { get; set; }

    public int Stock { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
