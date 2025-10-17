using TicketsAndMerch.Core.Entities;
using TicketsAndMerch.Core.Interfaces;
namespace TicketsAndMerch.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        // Método para registrar un usuario con reglas de negocio
        public async Task<User> AddUserAsync(User user)
        {
            // R1: Validar correo repetido
            var existingUser = await _userRepository.GetUserByEmailAsync(user.Email);
            if (existingUser != null)
            {
                throw new Exception("Ya existe un usuario registrado con ese correo electrónico.");
            }

            // R2: Campos obligatorios
            if (string.IsNullOrWhiteSpace(user.UserName))
                throw new Exception("El nombre de usuario es obligatorio.");

            if (string.IsNullOrWhiteSpace(user.Email))
                throw new Exception("El correo electrónico es obligatorio.");

            // R3: Contraseña mínima
            if (string.IsNullOrWhiteSpace(user.Contrasenia) || user.Contrasenia.Length < 6)
                throw new Exception("La contraseña debe tener al menos 6 caracteres.");

            // R4: Rol por defecto
            if (string.IsNullOrEmpty(user.Rol))
                user.Rol = "Cliente";

            await _userRepository.AddUserAsync(user);
            return user;
        }
        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllUsersAsync();
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _userRepository.GetUserByIdAsync(id);
        }

        public async Task UpdateUserAsync(User user)
        {
            var existing = await _userRepository.GetUserByIdAsync(user.UserId);
            if (existing == null)
                throw new Exception("El usuario no existe.");

            existing.UserName = user.UserName;
            existing.Email = user.Email;
            existing.Contrasenia = user.Contrasenia;
            existing.Rol = user.Rol;

            await _userRepository.UpdateUserAsync(existing);
        }

        public async Task DeleteUserAsync(User user)
        {
            await _userRepository.DeleteUserAsync(user);
        }

    }
        
}
