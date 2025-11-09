using Microsoft.EntityFrameworkCore;
using TicketsAndMerch.Core.Entities;
using TicketsAndMerch.Core.Interfaces;
using TicketsAndMerch.Infrastructure.Data;

namespace TicketsAndMerch.Infrastructure.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(TicketsAndMerchContext context) : base(context)
        {
        }

        // Obtener todos los usuarios
        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _entities
                //.Include(u => u.Orders)
                .AsNoTracking()
                .ToListAsync();
        }

        // Obtener un usuario por ID
        public async Task<User?> GetUserByIdAsync(int id)
        {
            return await _entities
                //.Include(u => u.Orders)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        // Obtener usuario por correo electrónico
        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _entities
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        // Verificar existencia por correo
        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _entities.AnyAsync(u => u.Email == email);
        }
    }
}

