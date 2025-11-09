using System;

namespace TicketsAndMerch.Infrastructure.Queries
{
    public static class OrderQueries
    {
        public static string OrderQuerySqlServer = @"
            SELECT Id, UserId, DateOrder, State, MerchId, TicketId, OrderAmount, UnitPrice, OrderDetail
            FROM [Order]
            ORDER BY DateOrder DESC
            OFFSET 0 ROWS FETCH NEXT @Limit ROWS ONLY;";

        public static string OrderQueryMySql = @"
            SELECT Id, UserId, DateOrder, State, MerchId, TicketId, OrderAmount, UnitPrice, OrderDetail
            FROM `Order`
            ORDER BY DateOrder DESC
            LIMIT @Limit;";

        public static string OrdersByUser = @"
            SELECT o.Id, o.UserId, o.DateOrder, SUM(o.OrderAmount) AS Total
            FROM [Order] o
            WHERE o.UserId = @UserId
            GROUP BY o.Id, o.UserId, o.DateOrder
            ORDER BY o.DateOrder DESC;";
    }
}
