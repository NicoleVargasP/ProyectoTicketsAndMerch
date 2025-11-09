using System;

namespace TicketsAndMerch.Infrastructure.Queries
{
    public static class PaymentQueries
    {
        public static string PaymentQuerySqlServer = @"
            SELECT Id, OrderId, Method, PaymentDate, OrderAmount, PaymentState
            FROM Payment
            ORDER BY PaymentDate DESC
            OFFSET 0 ROWS FETCH NEXT @Limit ROWS ONLY;";

        public static string PaymentQueryMySql = @"
            SELECT Id, OrderId, Method, PaymentDate, OrderAmount, PaymentState
            FROM Payment
            ORDER BY PaymentDate DESC
            LIMIT @Limit;";

        public static string PaymentsByMethod = @"
            SELECT Method, COUNT(*) AS TotalPagos, SUM(OrderAmount) AS MontoTotal
            FROM Payment
            WHERE PaymentState = 'Completado'
            GROUP BY Method;";
    }
}
