using TicketsAndMerch.Core.Entities;

namespace TicketsAndMerch.Core.Interfaces
{
    public interface IMerchRepository : IBaseRepository<Merch>
    {
        Task<IEnumerable<Merch>> GetAllMerchAsync();
        Task<Merch> GetMerchByIdAsync(int id);
        Task<IEnumerable<Merch>> GetMerchInStockAsync();
    }
}
