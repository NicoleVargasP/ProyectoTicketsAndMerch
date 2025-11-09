using Microsoft.EntityFrameworkCore;
using TicketsAndMerch.Core.Entities;
using TicketsAndMerch.Core.Enum;
using TicketsAndMerch.Core.Interfaces;
using TicketsAndMerch.Infrastructure.Data;
using TicketsAndMerch.Infrastructure.Queries;

namespace TicketsAndMerch.Infrastructure.Repositories
{
    public class PaymentRepository : BaseRepository<Payment>, IPaymentRepository
    {
        private readonly IDapperContext _dapper;
        public PaymentRepository(TicketsAndMerchContext context, IDapperContext dapper) : base(context)
        {
            _dapper = dapper;
        }

        public async Task<IEnumerable<Payment>> GetAllPaymentsAsync()
        {
            try
            {
                var sql = _dapper.Provider switch
                {
                    DatabaseProvider.SqlServer => PaymentQueries.PaymentQuerySqlServer,

                    DatabaseProvider.MySql => @"",
                    _ => throw new NotSupportedException("Provider no soportado")
                };

                return await _dapper.QueryAsync<Payment>(sql);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Payment> GetPaymentByIdAsync(int ide)
        {
            try
            {
                var sql = _dapper.Provider switch
                {
                    DatabaseProvider.SqlServer => PaymentQueries.PaymentByIdQuerySQLServer,

                    DatabaseProvider.MySql => @"",
                    _ => throw new NotSupportedException("Provider no soportado")
                };

                return await _dapper.QueryFirstOrDefaultAsync<Payment>(sql, new { id = ide });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<Payment>> GetPaymentsByOrderAsync(int orderId)
        {
            return await _entities
                .Include(p => p.Order)
                .Where(p => p.OrderId == orderId)
                .ToListAsync();
        }
    }
}
