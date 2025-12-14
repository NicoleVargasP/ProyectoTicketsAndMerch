using TicketsAndMerch.Core.Entities;

namespace TicketsAndMerch.Core.Interfaces
{
    public interface IConcertRepository : IBaseRepository<Concert>
    {
        Task<IEnumerable<Concert>> GetAllConcertsAsync();
        Task<Concert> GetConcertByIdAsync(int id);

        //caso de uso
        Task<IEnumerable<Concert>> GetAvailableConcertsAsync();
    }
}
