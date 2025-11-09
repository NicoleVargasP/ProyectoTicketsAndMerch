using System;

namespace TicketsAndMerch.Infrastructure.Queries
{
    public static class TicketQueries
    {
        public static string TicketQuerySqlServer = @"
            SELECT Id, ConcertId, Price, TicketType, Stock
            FROM [Tickets]
            ORDER BY Price DESC;";

        public static string TicketByIdQuerySQLServer = @"
            SELECT *
            FROM [Tickets]
            WHERE Id=@id
            ORDER BY Id DESC;";


        public static string TicketQueryMySql = @"
            SELECT Id, ConcertId, Price, TicketType, Stock
            FROM Tickets
            ORDER BY Price DESC";

        public static string TicketsByConcert = @"
            SELECT t.ConcertId, COUNT(t.Id) AS TotalTickets, SUM(t.Stock) AS StockTotal
            FROM Tickets t
            GROUP BY t.ConcertId
            ORDER BY TotalTickets DESC;";
    }
}
