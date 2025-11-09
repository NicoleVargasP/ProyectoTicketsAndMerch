using TicketsAndMerch.Core.Entities;
using System;
using System.Data;
using System.Threading.Tasks;

namespace TicketsAndMerch.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IBaseRepository<Concert> ConcertRepository { get; }
        IBaseRepository<Merch> MerchRepository { get; }
        IBaseRepository<Order> OrderRepository { get; }
        IBaseRepository<Payment> PaymentRepository { get; }
        IBaseRepository<Ticket> TicketRepository { get; }
        IBaseRepository<User> UserRepository { get; }

        void SaveChanges();
        Task SaveChangesAsync();

        Task BeginTransactionAsync();
        Task CommitAsync();
        Task RollbackAsync();

        // Métodos opcionales para integración con Dapper
        IDbConnection? GetDbConnection();
        IDbTransaction? GetDbTransaction();
    }
}
