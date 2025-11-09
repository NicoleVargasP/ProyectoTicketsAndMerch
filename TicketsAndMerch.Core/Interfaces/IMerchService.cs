using TicketsAndMerch.Core.CustomEntities;
using TicketsAndMerch.Core.Entities;
using TicketsAndMerch.Core.QueryFilters;

namespace TicketsAndMerch.Core.Interfaces
{
    public interface IMerchService
    {
        Task<ResponseData> GetAllMerchAsync(MerchQueryFilter filters);
        Task<IEnumerable<Merch>> GetAllMerchDapperAsync();
        Task<Merch> GetMerchByIdAsync(int id);
        Task AddMerchAsync(Merch merch);
        Task UpdateMerchAsync(Merch merch);
        Task DeleteMerchAsync(int id);
    }
}
