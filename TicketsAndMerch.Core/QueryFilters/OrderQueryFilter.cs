using Swashbuckle.AspNetCore.Annotations;
using System;

namespace TicketsAndMerch.Core.QueryFilters
{
    /// <summary>
    /// Filtros para órdenes
    /// </summary>
    public class OrderQueryFilter : PaginationQueryFilter
    {
        [SwaggerSchema("Id del usuario que realizó la orden")]
        public int? UserId { get; set; }

        [SwaggerSchema("Estado de la orden (Pendiente, Completada, Cancelada, etc.)")]
        public string? State { get; set; }

        [SwaggerSchema("Fecha mínima de la orden")]
        public DateTime? StartDate { get; set; }

        [SwaggerSchema("Fecha máxima de la orden")]
        public DateTime? EndDate { get; set; }
    }
}

