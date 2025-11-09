using TicketsAndMerch.Core.CustomEntities;
using TicketsAndMerch.Core.Entities;

namespace TicketsAndMerch.Core.Interfaces
{
    /// <summary>
    /// Define las operaciones de acceso a datos para la entidad BuyTicket.
    /// </summary>
    public interface IBuyTicketRepository : IBaseRepository<BuyTicket>
    {
        /// <summary>
        /// Obtiene todas las compras de tickets utilizando Dapper (solo lectura).
        /// </summary>
        /// <returns>Lista de compras registradas en el sistema.</returns>
        Task<IEnumerable<BuyTicket>> GetAllBuyTicketsAsync();

        /// <summary>
        /// Obtiene una compra de ticket por su identificador utilizando Dapper.
        /// </summary>
        /// <param name="id">Identificador único de la compra.</param>
        /// <returns>Instancia de BuyTicket si existe, de lo contrario null.</returns>
        Task<BuyTicket?> GetBuyTicketByIdAsync(int id);
    }
}
