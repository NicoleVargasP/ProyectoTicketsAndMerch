using System;

namespace TicketsAndMerch.Infrastructure.Queries
{
    public static class MerchQueries
    {
        public static string MerchQuerySqlServer = @"
            SELECT Id, MerchName, Description, Price, TypeMerch, Stock
            FROM Merch
            ORDER BY Price DESC
            OFFSET 0 ROWS FETCH NEXT @Limit ROWS ONLY;";

        public static string MerchQueryMySql = @"
            SELECT Id, MerchName, Description, Price, TypeMerch, Stock
            FROM Merch
            ORDER BY Price DESC
            LIMIT @Limit;";

        public static string MerchLowStock = @"
            SELECT Id, MerchName, Stock, Price
            FROM Merch
            WHERE Stock < @StockLimit
            ORDER BY Stock ASC;";
    }
}
