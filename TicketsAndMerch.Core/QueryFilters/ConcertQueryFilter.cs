using Swashbuckle.AspNetCore.Annotations;
using System;

namespace TicketsAndMerch.Core.QueryFilters
{
    /// <summary>
    /// Filtros para la búsqueda de conciertos
    /// </summary>
    public class ConcertQueryFilter : PaginationQueryFilter
    {
        [SwaggerSchema("Título del concierto")]
        public string? Title { get; set; }

        [SwaggerSchema("Ubicación del concierto")]
        public string? Location { get; set; }

        [SwaggerSchema("Fecha del concierto")]
        public DateTime? Date { get; set; }
    }
}
