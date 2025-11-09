using Microsoft.EntityFrameworkCore;
using TicketsAndMerch.Core.Entities;
using TicketsAndMerch.Core.Enum;
using TicketsAndMerch.Core.Interfaces;
using TicketsAndMerch.Infrastructure.Data;
using TicketsAndMerch.Infrastructure.Queries;

namespace TicketsAndMerch.Infrastructure.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {

        private readonly IDapperContext _dapper;
        public UserRepository(TicketsAndMerchContext context, IDapperContext dapper ) : base(context)
        {
            _dapper = dapper;
        }

        // Obtener todos los usuarios
        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            try
            {
                var sql = _dapper.Provider switch
                {
                    DatabaseProvider.SqlServer => UserQueries.UserQuerySqlServer,

                    DatabaseProvider.MySql => @"",
                    _ => throw new NotSupportedException("Provider no soportado")
                };

                return await _dapper.QueryAsync<User>(sql);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Obtener un usuario por ID
        public async Task<User?> GetUserByIdAsync(int ide)
        {
            try
            {
                var sql = _dapper.Provider switch
                {
                    DatabaseProvider.SqlServer => UserQueries.UserByIdQuerySQLServer,

                    DatabaseProvider.MySql => @"",
                    _ => throw new NotSupportedException("Provider no soportado")
                };

                return await _dapper.QueryFirstOrDefaultAsync<User>(sql, new {id=ide});
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
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

