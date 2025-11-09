using System;

namespace TicketsAndMerch.Infrastructure.Queries
{
    public static class MerchQueries
    {
        public static string MerchQuerySqlServer = @"
            SELECT Id, MerchName, Description, Price, TypeMerch, Stock
            FROM [Merch]
            ORDER BY Price DESC;";

        public static string MerchByIdQuerySQLServer = @"
            SELECT *
            FROM [Merch]
            WHERE Id=@id
            ORDER BY Id DESC;";

        public static string MerchLowStock = @"
            SELECT Id, MerchName, Stock, Price
            FROM Merch
            WHERE Stock < @StockLimit
            ORDER BY Stock ASC;";
    }
}
