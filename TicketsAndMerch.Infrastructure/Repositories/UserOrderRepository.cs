using TicketsAndMerch.Core.CustomEntities;
using TicketsAndMerch.Core.Entities;
using TicketsAndMerch.Core.Interfaces;
using TicketsAndMerch.Core.QueryFilters;
using TicketsAndMerch.Infrastructure.Data;
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
                    COALESCE(t.TicketType, m.MerchName) AS ItemName,
                    1 AS Quantity, -- Ajusta si Merch puede tener varias unidades
                    o.OrderAmount AS TotalAmount,
                    o.State AS PaymentState,
                    o.DateOrder AS PurchaseDate
                FROM Orders o
                INNER JOIN Users u ON o.UserId = u.Id
                LEFT JOIN Tickets t ON o.TicketId = t.Id
                LEFT JOIN Merch m ON o.MerchId = m.Id
                WHERE u.Email = @Login -- Usamos el email del token
                  AND o.State <> 'Cancelada' -- Excluir órdenes canceladas
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

