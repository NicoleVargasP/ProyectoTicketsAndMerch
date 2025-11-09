using Swashbuckle.AspNetCore.Annotations;
using System;

namespace TicketsAndMerch.Core.QueryFilters
{
    /// <summary>
    /// Filtros para pagos
    /// </summary>
    public class PaymentQueryFilter : PaginationQueryFilter
    {
        [SwaggerSchema("Método de pago (Tarjeta, Efectivo, etc.)")]
        public string? Method { get; set; }

        [SwaggerSchema("Estado del pago (Pagado, Pendiente, Rechazado)")]
        public string? PaymentState { get; set; }

        [SwaggerSchema("Fecha mínima de pago")]
        public DateTime? StartDate { get; set; }

        [SwaggerSchema("Fecha máxima de pago")]
        public DateTime? EndDate { get; set; }
    }
}
