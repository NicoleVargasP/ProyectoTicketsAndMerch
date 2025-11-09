using System.Net;
using TicketsAndMerch.Core.CustomEntities;
using TicketsAndMerch.Core.Entities;
using TicketsAndMerch.Core.Exceptions;
using TicketsAndMerch.Core.Interfaces;
using TicketsAndMerch.Core.QueryFilters;

namespace TicketsAndMerch.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseData> GetAllUsersAsync(UserQueryFilter filters)
        {
            var users = await _unitOfWork.UserRepository.GetAll();

            if (!string.IsNullOrEmpty(filters.UserName))
                users = users.Where(x => x.UserName.ToLower().Contains(filters.UserName.ToLower()));

            if (!string.IsNullOrEmpty(filters.Email))
                users = users.Where(x => x.Email.ToLower().Contains(filters.Email.ToLower()));

            var pagedUsers = PagedList<object>.Create(users, filters.PageNumber, filters.PageSize);

            return new ResponseData()
            {
                Messages = new[] { new Message { Type = "Information", Description = "Usuarios recuperados correctamente." } },
                Pagination = pagedUsers,
                StatusCode = HttpStatusCode.OK
            };
        }

        public async Task<IEnumerable<User>> GetAllUsersDapperAsync()
        {
            var users = await _unitOfWork.UserRepository.GetAll();
            return users;
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _unitOfWork.UserRepository.GetById(id);
        }

        public async Task<User> AddUserAsync(User user)
        {
            if (string.IsNullOrWhiteSpace(user.UserName))
                throw new BussinessException("El nombre de usuario es obligatorio.");

            if (string.IsNullOrWhiteSpace(user.Email))
                throw new BussinessException("El correo electrónico es obligatorio.");

            if (string.IsNullOrWhiteSpace(user.Contrasenia) || user.Contrasenia.Length < 6)
                throw new BussinessException("La contraseña debe tener al menos 6 caracteres.");

            await _unitOfWork.UserRepository.Add(user);
            await _unitOfWork.SaveChangesAsync();
            return user;
        }

        public async Task UpdateUserAsync(User user)
        {
            await _unitOfWork.UserRepository.Update(user);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(int id)
        {
            await _unitOfWork.UserRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
