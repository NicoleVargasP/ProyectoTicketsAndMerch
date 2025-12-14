using Swashbuckle.AspNetCore.Annotations;
namespace TicketsAndMerch.Core.QueryFilters
{
    /// <summary>
    /// Filtros para la búsqueda de compras de merchandising.
    /// </summary>
    public class BuyMerchQueryFilter : PaginationQueryFilter
    {
        [SwaggerSchema("Id del usuario que realizó la compra")]
        public int? UserId { get; set; }

        [SwaggerSchema("Id del artículo de merchandising comprado")]
        public int? MerchId { get; set; }

        [SwaggerSchema("Estado del pago (Pendiente, Completado, Cancelado)")]
        public string? PaymentState { get; set; }

        [SwaggerSchema("Fecha mínima de la compra")]
        public DateTime? StartDate { get; set; }

        [SwaggerSchema("Fecha máxima de la compra")]
        public DateTime? EndDate { get; set; }
    }
}
