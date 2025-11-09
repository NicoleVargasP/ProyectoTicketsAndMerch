using System.Collections.Generic;
using System.Threading.Tasks;
using TicketsAndMerch.Core.Entities;

namespace TicketsAndMerch.Core.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User?> GetUserByIdAsync(int id);
        Task<User?> GetUserByEmailAsync(string email);
        Task<bool> EmailExistsAsync(string email);
    }
}
