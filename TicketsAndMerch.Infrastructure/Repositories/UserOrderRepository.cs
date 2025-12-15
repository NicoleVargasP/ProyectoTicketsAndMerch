using TicketsAndMerch.Core.CustomEntities;
using TicketsAndMerch.Core.Entities;
using TicketsAndMerch.Core.Interfaces;
using TicketsAndMerch.Core.QueryFilters;
using TicketsAndMerch.Infrastructure.Data;
using TicketsAndMerch.Infrastructure.Queries;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TicketsAndMerch.Infrastructure.Repositories
{
    public class UserOrderRepository : IUserOrderRepository
    {
        private readonly IDapperContext _dapper;

        public UserOrderRepository(IDapperContext dapper)
        {
            _dapper = dapper;
        }

        public async Task<IEnumerable<UserOrder>> GetUserOrdersByLoginAsync(string login, UserOrderQueryFilter filters)
        {
            var sql = @"
                SELECT 
                    o.Id AS OrderId,
                    CASE 
                        WHEN o.TicketId IS NOT NULL THEN 'Ticket'
                        ELSE 'Merch'
                    END AS OrderType,
                    COALESCE(t.Title, m.MerchName) AS ItemName,
                    CASE 
                        WHEN o.TicketId IS NOT NULL THEN 1
                        ELSE ISNULL(o.MerchId, 1)
                    END AS Quantity,
                    o.OrderAmount AS TotalAmount,
                    o.State AS PaymentState,
                    o.DateOrder AS PurchaseDate
                FROM Orders o
                INNER JOIN Users u ON o.UserId = u.Id
                INNER JOIN Security s ON u.Login = s.Login
                LEFT JOIN Tickets t ON o.TicketId = t.Id
                LEFT JOIN Merch m ON o.MerchId = m.Id
                WHERE s.Login = @Login
                  AND (@OrderType IS NULL OR (CASE WHEN o.TicketId IS NOT NULL THEN 'Ticket' ELSE 'Merch' END) = @OrderType)
                  AND (@Status IS NULL OR o.State = @Status)
                ORDER BY o.DateOrder DESC;
            ";

            return await _dapper.QueryAsync<UserOrder>(sql, new
            {
                Login = login,
                OrderType = filters.OrderType,
                Status = filters.Status
            });
        }
    }
}
