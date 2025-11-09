using System;

namespace TicketsAndMerch.Infrastructure.Queries
{
    public static class BuyTicketQueries
    {
        public static string BuyTicketQuerySqlServer = @"
            SELECT Id, UserId, TicketId, Quantity, TotalAmount, PurchaseDate, PaymentState
            FROM [BuyTickets]
            ORDER BY PurchaseDate DESC;";

        public static string BuyTicketByIdQuerySQLServer = @"
            SELECT *
            FROM [BuyTickets]
            WHERE Id=@id
            ORDER BY Id DESC;";

        public static string BuyTicketQueryMySql = @"
            SELECT Id, UserId, TicketId, Quantity, TotalAmount, PurchaseDate, PaymentState
            FROM BuyTickets
            ORDER BY PurchaseDate DESC;";

        public static string BuyTicketsWithUserAndConcert = @"
            SELECT bt.Id, u.UserName, c.Title, bt.Quantity, bt.TotalAmount, bt.PurchaseDate, bt.PaymentState
            FROM BuyTickets bt
            INNER JOIN Users u ON bt.UserId = u.Id
            INNER JOIN Tickets t ON bt.TicketId = t.Id
            INNER JOIN Concerts c ON t.ConcertId = c.Id
            ORDER BY bt.PurchaseDate DESC;";
    }
}
