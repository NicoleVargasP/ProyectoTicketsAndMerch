using Swashbuckle.AspNetCore.Annotations;
using System;

namespace TicketsAndMerch.Core.QueryFilters
{
    /// <summary>
    /// Filtros para la búsqueda de compras de tickets.
    /// </summary>
    public class BuyTicketQueryFilter : PaginationQueryFilter
    {
        [SwaggerSchema("Id del usuario que realizó la compra")]
        public int? UserId { get; set; }

        [SwaggerSchema("Id del ticket comprado")]
        public int? TicketId { get; set; }

        [SwaggerSchema("Estado del pago (Pendiente, Completado, Cancelado)")]
        public string? PaymentState { get; set; }

        [SwaggerSchema("Fecha mínima de la compra")]
        public DateTime? StartDate { get; set; }

        [SwaggerSchema("Fecha máxima de la compra")]
        public DateTime? EndDate { get; set; }

        [SwaggerSchema("Id del concierto al que pertenece la compra")]
        public int? ConcertId { get; set; }
    }
}
