using System;
using System.Collections.Generic;

namespace TicketsAndMerch.Core.Entities;

public partial class User : BaseEntity
{
   // public int UserId { get; set; }

    public string UserName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Contrasenia { get; set; } = null!;

    public string Rol { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
