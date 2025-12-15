namespace TicketsAndMerch.Infrastructure.Queries
{
    public static class UserOrderQueries
    {
        public static string GetUserOrdersByLoginSqlServer = @"
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
            LEFT JOIN Tickets t ON o.TicketId = t.Id
            LEFT JOIN Merch m ON o.MerchId = m.Id
            INNER JOIN Security s ON o.UserId = s.Id
            WHERE s.Login = @Login
              AND (@OrderType IS NULL OR 
                  (CASE WHEN o.TicketId IS NOT NULL THEN 'Ticket' ELSE 'Merch' END) = @OrderType)
              AND (@Status IS NULL OR o.State = @Status)
            ORDER BY o.DateOrder DESC;
        ";
    }
}
