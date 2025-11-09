using System;

namespace TicketsAndMerch.Infrastructure.Queries
{
    public static class TicketQueries
    {
        public static string TicketQuerySqlServer = @"
            SELECT Id, ConcertId, Price, TicketType, Stock
            FROM Ticket
            ORDER BY Price DESC
            OFFSET 0 ROWS FETCH NEXT @Limit ROWS ONLY;";

        public static string TicketQueryMySql = @"
            SELECT Id, ConcertId, Price, TicketType, Stock
            FROM Ticket
            ORDER BY Price DESC
            LIMIT @Limit;";

        public static string TicketsByConcert = @"
            SELECT t.ConcertId, COUNT(t.Id) AS TotalTickets, SUM(t.Stock) AS StockTotal
            FROM Ticket t
            GROUP BY t.ConcertId
            ORDER BY TotalTickets DESC;";
    }
}
