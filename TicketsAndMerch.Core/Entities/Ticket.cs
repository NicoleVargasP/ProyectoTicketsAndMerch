using System;
using System.Collections.Generic;

namespace TicketsAndMerch.Core.Entities
{
    /// <summary>
    /// Representa un ticket en el sistema.
    /// </summary>
    /// <remarks>
    /// Esta entidad almacena la información de los tickets asociados a conciertos 
    /// y es utilizada para las operaciones de venta y gestión de pedidos.
    /// </remarks>
    public partial class Ticket : BaseEntity
    {
        /// <summary>
        /// Identificador único del ticket.
        /// </summary>
        /// <example>1</example>
        // public int TicketId { get; set; }

        /// <summary>
        /// Identificador del concierto asociado.
        /// </summary>
        /// <example>3</example>
        public int ConcertId { get; set; }

        /// <summary>
        /// Precio del ticket.
        /// </summary>
        /// <example>150.00</example>
        public decimal Price { get; set; }

        /// <summary>
        /// Tipo de ticket (por ejemplo: VIP, General, Preferencial).
        /// </summary>
        /// <example>VIP</example>
        public string? TicketType { get; set; }

        /// <summary>
        /// Cantidad de entradas disponibles en stock.
        /// </summary>
        /// <example>120</example>
        public int Stock { get; set; }

        /// <summary>
        /// Concierto asociado al ticket.
        /// </summary>
        public virtual Concert Concert { get; set; } = null!;

        /// <summary>
        /// Pedidos que incluyen este ticket.
        /// </summary>
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
