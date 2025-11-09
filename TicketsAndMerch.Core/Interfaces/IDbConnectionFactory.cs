using TicketsAndMerch.Core.Enum;
using System.Data;

namespace TicketsAndMerch.Core.Interfaces
{
    public interface IDbConnectionFactory
    {
        DatabaseProvider Provider { get; }
        IDbConnection CreateConnection();
    }
}
