using System;

namespace TicketsAndMerch.Infrastructure.Queries
{
    public static class UserQueries
    {
        public static string UserQuerySqlServer = @"
            SELECT Id, UserName, Email, Contrasenia, Rol
            FROM [User]
            ORDER BY Id DESC
            OFFSET 0 ROWS FETCH NEXT @Limit ROWS ONLY;";

        public static string UserQueryMySql = @"
            SELECT Id, UserName, Email, Contrasenia, Rol
            FROM `User`
            ORDER BY Id DESC
            LIMIT @Limit;";

        public static string UsersWithOrders = @"
            SELECT u.Id, u.UserName, u.Email, COUNT(o.Id) AS TotalOrders
            FROM [User] u
            LEFT JOIN [Order] o ON u.Id = o.UserId
            GROUP BY u.Id, u.UserName, u.Email
            HAVING COUNT(o.Id) > 0
            ORDER BY TotalOrders DESC;";
    }
}
