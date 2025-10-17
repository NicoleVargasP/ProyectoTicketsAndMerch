using TicketsAndMerch.Core.Entities;

namespace TicketsAndMerch.Core.Interfaces
{
    public interface IConcertRepository
    {
        Task<IEnumerable<Concert>> GetAllConcertsAsync();
        Task<Concert> GetConcertByIdAsync(int id);
        Task AddConcertAsync(Concert concert);
        Task UpdateConcertAsync(Concert concert);
        Task DeleteConcertAsync(Concert concert);
    }
}
