using Swashbuckle.AspNetCore.Annotations;

namespace TicketsAndMerch.Core.QueryFilters
{
    public class UserOrderQueryFilter : PaginationQueryFilter
    {
        [SwaggerSchema("Tipo de orden (Ticket | Merch)")]
        public string? OrderType { get; set; }

        [SwaggerSchema("Estado de la orden (Pendiente, Completada, Cancelada)")]
        public string? Status { get; set; }
    }
}
