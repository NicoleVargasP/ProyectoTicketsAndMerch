using System;

namespace TicketsAndMerch.Infrastructure.Queries
{
    public static class ConcertQueries
    {
        public static string ConcertQuerySqlServer = @"
            SELECT Id, Title, Description, Location, Date, AvailableTickets
            FROM Concert
            ORDER BY Date DESC
            OFFSET 0 ROWS FETCH NEXT @Limit ROWS ONLY;";

        public static string ConcertQueryMySql = @"
            SELECT Id, Title, Description, Location, Date, AvailableTickets
            FROM Concert
            ORDER BY Date DESC
            LIMIT @Limit;";

        public static string ConcertsWithTickets = @"
            SELECT c.Id, c.Title, c.Location, c.Date, COUNT(t.Id) AS TotalTickets
            FROM Concert c
            LEFT JOIN Ticket t ON c.Id = t.ConcertId
            GROUP BY c.Id, c.Title, c.Location, c.Date
            HAVING COUNT(t.Id) > 0
            ORDER BY c.Date DESC;";
    }
}
