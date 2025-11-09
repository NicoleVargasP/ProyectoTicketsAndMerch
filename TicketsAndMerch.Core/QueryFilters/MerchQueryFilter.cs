using Swashbuckle.AspNetCore.Annotations;

namespace TicketsAndMerch.Core.QueryFilters
{
    /// <summary>
    /// Filtros para productos de merchandising
    /// </summary>
    public class MerchQueryFilter : PaginationQueryFilter
    {
        [SwaggerSchema("Nombre del producto")]
        public string? MerchName { get; set; }

        [SwaggerSchema("Tipo de merchandising (camiseta, poster, etc.)")]
        public string? TypeMerch { get; set; }

        [SwaggerSchema("Precio mínimo del producto")]
        public decimal? MinPrice { get; set; }

        [SwaggerSchema("Precio máximo del producto")]
        public decimal? MaxPrice { get; set; }
    }
}
