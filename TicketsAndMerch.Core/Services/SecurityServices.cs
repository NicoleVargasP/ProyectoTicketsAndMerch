using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsAndMerch.Core.Entities;
using TicketsAndMerch.Core.Interfaces;


namespace TicketsAndMerch.Core.Services
{
    public class SecurityServices : ISecurityServices
    {
        private readonly IUnitOfWork _unitOfWork;
        public SecurityServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Security> GetLoginByCredentials(UserLogin login)
        {
            return await _unitOfWork.SecurityRepositoryExtra.GetLoginByCredentials(login);
        }

        public async Task RegisterUser(Security security)
        {
            
            await _unitOfWork.SecurityRepositoryExtra.Add(security);

            
            var user = new User
            {
                UserName = security.Name,
                Email = security.Login,         
                Contrasenia = security.Password,
                Rol = security.Role.ToString()
            };

            await _unitOfWork.UserRepository.Add(user);

           
            await _unitOfWork.SaveChangesAsync();
        }

    }
}

