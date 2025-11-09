using Microsoft.EntityFrameworkCore;
using TicketsAndMerch.Core.Entities;
using TicketsAndMerch.Core.Enum;
using TicketsAndMerch.Core.Interfaces;
using TicketsAndMerch.Infrastructure.Data;
using TicketsAndMerch.Infrastructure.Queries;

namespace TicketsAndMerch.Infrastructure.Repositories
{
    public class TicketRepository : BaseRepository<Ticket>, ITicketRepository
    {
        private readonly IDapperContext _dapper;
        public TicketRepository(TicketsAndMerchContext context, IDapperContext dapper) : base(context)
        {
            _dapper = dapper;
        }

        public async Task<IEnumerable<Ticket>> GetAllTicketsAsync()
        {
            try
            {
                var sql = _dapper.Provider switch
                {
                    DatabaseProvider.SqlServer => TicketQueries.TicketQuerySqlServer,

                    DatabaseProvider.MySql => @"",
                    _ => throw new NotSupportedException("Provider no soportado")
                };

                return await _dapper.QueryAsync<Ticket>(sql);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Ticket?> GetTicketByIdAsync(int ide)
        {
            try
            {
                var sql = _dapper.Provider switch
                {
                    DatabaseProvider.SqlServer => TicketQueries.TicketByIdQuerySQLServer,

                    DatabaseProvider.MySql => @"",
                    _ => throw new NotSupportedException("Provider no soportado")
                };

                return await _dapper.QueryFirstOrDefaultAsync<Ticket>(sql, new { id = ide });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<Ticket>> GetTicketsByConcertAsync(int concertId)
        {
            return await _entities
                .Include(t => t.Concert)
                .Where(t => t.ConcertId == concertId)
                .ToListAsync();
        }
    }
}
