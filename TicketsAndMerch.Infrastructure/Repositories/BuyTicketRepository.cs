using Microsoft.EntityFrameworkCore;
using TicketsAndMerch.Core.CustomEntities;
using TicketsAndMerch.Core.Entities;
using TicketsAndMerch.Core.Enum;
using TicketsAndMerch.Core.Interfaces;
using TicketsAndMerch.Infrastructure.Data;
using TicketsAndMerch.Infrastructure.Queries;

namespace TicketsAndMerch.Infrastructure.Repositories
{
    public class BuyTicketRepository : BaseRepository<BuyTicket>, IBuyTicketRepository
    {
        private readonly IDapperContext _dapper;

        public BuyTicketRepository(TicketsAndMerchContext context, IDapperContext dapper)
            : base(context)
        {
            _dapper = dapper;
        }

        public async Task<IEnumerable<BuyTicket>> GetAllBuyTicketsAsync()
        {
            try
            {
                var sql = _dapper.Provider switch
                {
                    DatabaseProvider.SqlServer => BuyTicketQueries.BuyTicketQuerySqlServer,
                    DatabaseProvider.MySql => @"",
                    _ => throw new NotSupportedException("Provider no soportado")
                };
                return await _dapper.QueryAsync<BuyTicket>(sql);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener las compras de tickets: {ex.Message}");
            }
        }

        public async Task<BuyTicket?> GetBuyTicketByIdAsync(int ide)
        {
              try
            {
                var sql = _dapper.Provider switch
                {
                    DatabaseProvider.SqlServer =>BuyTicketQueries.BuyTicketByIdQuerySQLServer,

                    DatabaseProvider.MySql => @"",
                    _ => throw new NotSupportedException("Provider no soportado")
                };

                return await _dapper.QueryFirstOrDefaultAsync<BuyTicket>(sql, new { id = ide });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
