using Microsoft.EntityFrameworkCore;
using TicketsAndMerch.Core.Entities;
using TicketsAndMerch.Core.Enum;
using TicketsAndMerch.Core.Interfaces;
using TicketsAndMerch.Infrastructure.Data;
using TicketsAndMerch.Infrastructure.Queries;

namespace TicketsAndMerch.Infrastructure.Repositories
{
    public class MerchRepository : BaseRepository<Merch>, IMerchRepository
    {
        private readonly IDapperContext _dapper;
        public MerchRepository(TicketsAndMerchContext context, IDapperContext dapper) : base(context)
        {
            _dapper = dapper;
        }

        public async Task<IEnumerable<Merch>> GetAllMerchAsync()
        {
            try
            {
                var sql = _dapper.Provider switch
                {
                    DatabaseProvider.SqlServer => MerchQueries.MerchQuerySqlServer,

                    DatabaseProvider.MySql => @"",
                    _ => throw new NotSupportedException("Provider no soportado")
                };

                return await _dapper.QueryAsync<Merch>(sql);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Merch> GetMerchByIdAsync(int ide)
        {
            try
            {
                var sql = _dapper.Provider switch
                {
                    DatabaseProvider.SqlServer => MerchQueries.MerchByIdQuerySQLServer,

                    DatabaseProvider.MySql => @"",
                    _ => throw new NotSupportedException("Provider no soportado")
                };

                return await _dapper.QueryFirstOrDefaultAsync<Merch>(sql, new { id = ide });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<Merch>> GetMerchInStockAsync()
        {
            return await _entities
                .Where(m => m.Stock > 0)
                .OrderByDescending(m => m.Stock)
                .ToListAsync();
        }
    }
}
