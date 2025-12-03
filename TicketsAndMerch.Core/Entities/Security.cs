using System;
using System.Collections.Generic;
using TicketsAndMerch.Core.Enum;

namespace TicketsAndMerch.Core.Entities;

    public partial class Security : BaseEntity
    {
        public string Login { get; set; }
        public string Password { get; set; } = null!;

        public string Name { get; set; } = null!;

        public RoleType? Role { get; set; }
    }