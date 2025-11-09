using Swashbuckle.AspNetCore.Annotations;

namespace TicketsAndMerch.Core.QueryFilters
{
    /// <summary>
    /// Filtros para tickets
    /// </summary>
    public class TicketQueryFilter : PaginationQueryFilter
    {
        [SwaggerSchema("Id del concierto")]
        public int? ConcertId { get; set; }

        [SwaggerSchema("Tipo de ticket (VIP, General, etc.)")]
        public string? TicketType { get; set; }

        [SwaggerSchema("Precio mínimo del ticket")]
        public decimal? MinPrice { get; set; }

        [SwaggerSchema("Precio máximo del ticket")]
        public decimal? MaxPrice { get; set; }
    }
}
