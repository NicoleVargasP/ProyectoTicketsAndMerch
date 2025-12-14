using TicketsAndMerch.Core.CustomEntities;

namespace TicketsAndMerch.Core.Interfaces
{
    public interface IBuyMerchRepository : IBaseRepository<BuyMerch>
    {
        Task<IEnumerable<BuyMerch>> GetAllBuyMerchAsync();
        Task<BuyMerch?> GetBuyMerchByIdAsync(int id);
    }
}
