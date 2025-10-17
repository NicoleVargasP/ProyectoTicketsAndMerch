using Microsoft.EntityFrameworkCore;
using TicketsAndMerch.Core.Entities;
using TicketsAndMerch.Core.Interfaces;
using TicketsAndMerch.Infrastructure.Data;

namespace TicketsAndMerch.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly TicketsAndMerchContext _context;

        public UserRepository(TicketsAndMerchContext context)
        {
            _context = context;
        }

        // Obtener todos los usuarios
        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            var users = await _context.Users
                // .Include(u => u.Orders) // Incluye las órdenes del usuario
                .AsNoTracking() 
                .ToListAsync();

            return users;
        }



        // Obtener un usuario por ID
        public async Task<User> GetUserByIdAsync(int id)
        {
            var user = await _context.Users
               // .Include(u => u.Orders)
                .FirstOrDefaultAsync(u => u.UserId == id);

            return user;
        }

        // Agregar un nuevo usuario
        public async Task AddUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        // Actualizar un usuario existente
        public async Task UpdateUserAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        // Eliminar un usuario
        public async Task DeleteUserAsync(User user)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
        // Verificar Email 
        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

    }
}
