using TicketsAndMerch.Core.Entities;

namespace TicketsAndMerch.Core.Interfaces
{
    public interface IMerchRepository
    {
        Task<IEnumerable<Merch>> GetAllMerchAsync();
        Task<Merch> GetMerchByIdAsync(int id);
        Task AddMerchAsync(Merch merch);
        Task UpdateMerchAsync(Merch merch);
        Task DeleteMerchAsync(Merch merch);
    }
}
