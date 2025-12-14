using Swashbuckle.AspNetCore.Annotations;

namespace TicketsAndMerch.Core.QueryFilters
{
    public class AvailableConcertQueryFilter : PaginationQueryFilter
    {
        [SwaggerSchema("Buscar por nombre del concierto")]
        public string? Title { get; set; }

        [SwaggerSchema("Lugar del concierto")]
        public string? Location { get; set; }

        [SwaggerSchema("Fecha mínima")]
        public DateTime? MinDate { get; set; }

        [SwaggerSchema("Fecha máxima")]
        public DateTime? MaxDate { get; set; }
    }
}
