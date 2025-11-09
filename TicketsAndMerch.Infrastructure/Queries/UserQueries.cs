using System;

namespace TicketsAndMerch.Infrastructure.Queries
{
    public static class UserQueries
    {
        public static string UserQuerySqlServer = @"
            SELECT *
            FROM [Users]
            ORDER BY Id DESC";
        public static string UserByIdQuerySQLServer= @"
            SELECT *
            FROM [Users]
            WHERE Id=@id
            ORDER BY Id DESC;";

        public static string UserQueryMySql = @"
            SELECT *
            FROM `Users`
            ORDER BY Id DESC;";

        public static string UsersWithOrders = @"
            SELECT u.Id, u.UserName, u.Email, COUNT(o.OrderId) AS TotalOrders
            FROM [Users] u
            LEFT JOIN [Orders] o ON u.Id = o.Id
            GROUP BY u.Id, u.UserName, u.Email
            HAVING COUNT(o.OrderId) > 0
            ORDER BY TotalOrders DESC;";
    }
}
