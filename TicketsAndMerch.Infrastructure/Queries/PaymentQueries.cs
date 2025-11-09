using System;

namespace TicketsAndMerch.Infrastructure.Queries
{
    public static class PaymentQueries
    {
        public static string PaymentQuerySqlServer = @"
            SELECT Id, OrderId, Method, PaymentDate, OrderAmount, PaymentState
            FROM [Payments]
            ORDER BY PaymentDate DESC;";

        public static string PaymentByIdQuerySQLServer = @"
            SELECT *
            FROM [Payments]
            WHERE Id=@id
            ORDER BY Id DESC;";

        public static string PaymentQueryMySql = @"
            SELECT Id, OrderId, Method, PaymentDate, OrderAmount, PaymentState
            FROM Payments
            ORDER BY PaymentDate DESC";

        public static string PaymentsByMethod = @"
            SELECT Method, COUNT(*) AS TotalPagos, SUM(OrderAmount) AS MontoTotal
            FROM Payments
            WHERE PaymentState = 'Completado'
            GROUP BY Method;";
    }
}
