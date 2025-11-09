using System;

namespace TicketsAndMerch.Infrastructure.Queries
{
    public static class ConcertQueries
    {
        public static string ConcertQuerySqlServer = @"
            SELECT Id, Title, Description, Location, Date, AvailableTickets
            FROM [Concerts]
            ORDER BY Date DESC;";

        public static string ConcertByIdQuerySQLServer = @"
            SELECT *
            FROM [Concerts]
            WHERE Id=@id
            ORDER BY Id DESC;";

        public static string ConcertQueryMySql = @"
            SELECT Id, Title, Description, Location, Date, AvailableTickets
            FROM Concerts
            ORDER BY Date DESC";

        public static string ConcertsWithTickets = @"
            SELECT c.Id, c.Title, c.Location, c.Date, COUNT(t.Id) AS TotalTickets
            FROM Concerts c
            LEFT JOIN Tickets t ON c.Id = t.ConcertId
            GROUP BY c.Id, c.Title, c.Location, c.Date
            HAVING COUNT(t.Id) > 0
            ORDER BY c.Date DESC;";
    }
}
