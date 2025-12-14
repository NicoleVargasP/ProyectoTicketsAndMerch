using TicketsAndMerch.Core.CustomEntities;
using TicketsAndMerch.Core.Entities;
using TicketsAndMerch.Core.QueryFilters;
namespace TicketsAndMerch.Core.Interfaces
{
    public interface IConcertService
    {
        Task<ResponseData> GetAllConcertsAsync(ConcertQueryFilter filters);
        Task<IEnumerable<Concert>> GetAllConcertsDapperAsync();
        Task<Concert> GetConcertByIdAsync(int id);
        Task AddConcertAsync(Concert concert);
        Task UpdateConcertAsync(Concert concert);
        Task DeleteConcertAsync(int id);

        //caso de uso
        Task<IEnumerable<Concert>> GetAvailableConcertsAsync(AvailableConcertQueryFilter filters);
    }
}
