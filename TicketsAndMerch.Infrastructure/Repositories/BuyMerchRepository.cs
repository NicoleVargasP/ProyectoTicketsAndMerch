// TicketsAndMerch.Infrastructure.Repositories/BuyMerchRepository.cs
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TicketsAndMerch.Core.CustomEntities;
using TicketsAndMerch.Core.Entities;
using TicketsAndMerch.Core.Enum;
using TicketsAndMerch.Core.Interfaces;
using TicketsAndMerch.Infrastructure.Data;
using TicketsAndMerch.Infrastructure.Queries;

namespace TicketsAndMerch.Infrastructure.Repositories
{
    public class BuyMerchRepository : BaseRepository<BuyMerch>, IBuyMerchRepository
    {
        private readonly IDapperContext _dapper;

        public BuyMerchRepository(TicketsAndMerchContext context, IDapperContext dapper)
            : base(context)
        {
            _dapper = dapper;
        }

        public async Task<IEnumerable<BuyMerch>> GetAllBuyMerchAsync()
        {
            try
            {
                var sql = _dapper.Provider switch
                {
                    DatabaseProvider.SqlServer => BuyMerchQueries.BuyMerchQuerySqlServer,
                    DatabaseProvider.MySql => @"", // completar si usas MySQL
                    _ => throw new NotSupportedException("Provider no soportado")
                };
                return await _dapper.QueryAsync<BuyMerch>(sql);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener las compras de merchandising: {ex.Message}");
            }
        }

        public async Task<BuyMerch?> GetBuyMerchByIdAsync(int id)
        {
            try
            {
                var sql = _dapper.Provider switch
                {
                    DatabaseProvider.SqlServer => BuyMerchQueries.BuyMerchByIdQuerySQLServer,
                    DatabaseProvider.MySql => @"",
                    _ => throw new NotSupportedException("Provider no soportado")
                };

                return await _dapper.QueryFirstOrDefaultAsync<BuyMerch>(sql, new { id });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
