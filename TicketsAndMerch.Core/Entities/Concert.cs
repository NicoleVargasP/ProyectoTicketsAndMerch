using System;
using System.Collections.Generic;

namespace TicketsAndMerch.Core.Entities
{
    /// <summary>
    /// Representa un concierto en el sistema.
    /// </summary>
    /// <remarks>
    /// Esta entidad almacena la información principal de un concierto,
    /// incluyendo título, descripción, ubicación, fecha y la cantidad de tickets disponibles.
    /// </remarks>
    public partial class Concert : BaseEntity
    {
        /// <summary>
        /// Título del concierto.
        /// </summary>
        /// <example>Rock Fest 2025</example>
        public string Title { get; set; } = null!;

        /// <summary>
        /// Descripción detallada del concierto.
        /// </summary>
        /// <example>El festival de rock más grande del año.</example>
        public string? Description { get; set; }

        /// <summary>
        /// Lugar donde se llevará a cabo el concierto.
        /// </summary>
        /// <example>Estadio Nacional, La Paz</example>
        public string Location { get; set; } = null!;

        /// <summary>
        /// Fecha programada del concierto.
        /// </summary>
        /// <example>2025-12-15</example>
        public DateOnly Date { get; set; }

        /// <summary>
        /// Cantidad de tickets disponibles para el concierto.
        /// </summary>
        /// <example>5000</example>
        public int? AvailableTickets { get; set; }

        /// <summary>
        /// Colección de tickets asociados a este concierto.
        /// </summary>
        public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
    }
}
