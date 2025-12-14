using System;

namespace TicketsAndMerch.Infrastructure.Queries
{
    public static class BuyMerchQueries
    {

        public static string BuyMerchQuerySqlServer = @"
            SELECT Id, UserId, MerchId, Quantity, TotalAmount, PaymentMethod, PaymentState, PurchaseDate
            FROM [BuyMerch]
            ORDER BY PurchaseDate DESC;";

        public static string BuyMerchByIdQuerySQLServer = @"
            SELECT *
            FROM [BuyMerch]
            WHERE Id = @id
            ORDER BY Id DESC;";

        public static string BuyMerchWithUserAndMerch = @"
            SELECT 
                bm.Id,
                u.UserName,
                m.MerchName,
                m.Price,
                bm.Quantity,
                bm.TotalAmount,
                bm.PaymentMethod,
                bm.PaymentState,
                bm.PurchaseDate
            FROM BuyMerch bm
            INNER JOIN Users u ON bm.UserId = u.Id
            INNER JOIN Merch m ON bm.MerchId = m.Id
            ORDER BY bm.PurchaseDate DESC;";
    }
}
