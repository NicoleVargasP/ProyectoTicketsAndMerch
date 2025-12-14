using TicketsAndMerch.Core.CustomEntities;

namespace TicketsAndMerch.Core.Interfaces
{
    public interface IBuyMerchService
    {
        Task<IEnumerable<BuyMerch>> GetAllBuyMerchDapperAsync();
        Task<BuyMerch> GetBuyMerchByIdAsync(int id);
        Task<BuyMerch> BuyMerchAsync(BuyMerch buyMerch);
        Task DeleteBuyMerchAsync(int id);
    }
}
