using Microsoft.EntityFrameworkCore;
using TicketsAndMerch.Core.Entities;
using TicketsAndMerch.Core.Enum;
using TicketsAndMerch.Core.Interfaces;
using TicketsAndMerch.Infrastructure.Data;
using TicketsAndMerch.Infrastructure.Queries;


namespace TicketsAndMerch.Infrastructure.Repositories
{
    public class ConcertRepository : BaseRepository<Concert>, IConcertRepository
    {
        private readonly IDapperContext _dapper;
        public ConcertRepository(TicketsAndMerchContext context, IDapperContext dapper) : base(context)
        {
            _dapper = dapper;
        }

        public async Task<IEnumerable<Concert>> GetAllConcertsAsync()
        {
            try
            {
                var sql = _dapper.Provider switch
                {
                    DatabaseProvider.SqlServer => ConcertQueries.ConcertQuerySqlServer,

                    DatabaseProvider.MySql => @"",
                    _ => throw new NotSupportedException("Provider no soportado")
                };

                return await _dapper.QueryAsync<Concert>(sql);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Concert> GetConcertByIdAsync(int ide)
        {
            try
            {
                var sql = _dapper.Provider switch
                {
                    DatabaseProvider.SqlServer => ConcertQueries.ConcertByIdQuerySQLServer,

                    DatabaseProvider.MySql => @"",
                    _ => throw new NotSupportedException("Provider no soportado")
                };

                return await _dapper.QueryFirstOrDefaultAsync<Concert>(sql, new { id = ide });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<IEnumerable<Concert>> GetAvailableConcertsAsync()
        {
            try
            {
                var sql = _dapper.Provider switch
                {
                    DatabaseProvider.SqlServer => ConcertQueries.GetAvailableConcertsQuery,

                    DatabaseProvider.MySql => @"",
                    _ => throw new NotSupportedException("Provider no soportado")
                };

                return await _dapper.QueryAsync<Concert>(sql);
            }
            catch (Exception ex)
            {
throw new Exception(ex.Message);
            }
        }

    }
}
