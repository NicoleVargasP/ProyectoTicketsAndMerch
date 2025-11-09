using System;

namespace TicketsAndMerch.Infrastructure.Queries
{
    public static class OrderQueries
    {
        public static string OrderQuerySqlServer = @"
            SELECT Id, UserId, DateOrder, State, MerchId, TicketId, OrderAmount, UnitPrice, OrderDetail
            FROM [Orders]
            ORDER BY DateOrder DESC;";

        public static string OrderByIdQuerySQLServer = @"
            SELECT *
            FROM [Orders]
            WHERE Id=@id
            ORDER BY Id DESC;";

        public static string OrderQueryMySql = @"
            SELECT Id, UserId, DateOrder, State, MerchId, TicketId, OrderAmount, UnitPrice, OrderDetail
            FROM `Order`
            ORDER BY DateOrder DESC";

        public static string OrdersByUser = @"
            SELECT o.Id, o.UserId, o.DateOrder, SUM(o.OrderAmount) AS Total
            FROM [Orders] o
            WHERE o.UserId = @UserId
            GROUP BY o.Id, o.UserId, o.DateOrder
            ORDER BY o.DateOrder DESC;";
    }
}
