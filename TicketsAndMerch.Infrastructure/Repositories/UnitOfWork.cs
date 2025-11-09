using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;
using System.Threading.Tasks;
using TicketsAndMerch.Core.CustomEntities;
using TicketsAndMerch.Core.Entities;
using TicketsAndMerch.Core.Interfaces;
using TicketsAndMerch.Infrastructure.Data;

namespace TicketsAndMerch.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TicketsAndMerchContext _context;
        private readonly IDapperContext _dapper; // opcional, si usas Dapper
        private IDbContextTransaction? _efTransaction;

        // campos privados para inicialización lazy
        private IBaseRepository<Concert>? _concertRepository;
        private IBaseRepository<Merch>? _merchRepository;
        private IBaseRepository<Order>? _orderRepository;
        private IBaseRepository<Payment>? _paymentRepository;
        private IBaseRepository<Ticket>? _ticketRepository;
        private IBaseRepository<User>? _userRepository;
        private IBaseRepository<BuyTicket>? _buyTicketRepository;

        private IUserRepository _userRepositoryExtra;
        private IConcertRepository _concertRepositoryExtra;
        private ITicketRepository _ticketRepositoryExtra;
        private IPaymentRepository _paymentRepositoryExtra;
        private IOrderRepository _orderRepositoryExtra;
        private IMerchRepository _merchRepositoryExtra;
        private IBuyTicketRepository _buyTicketRepositoryExtra;

        public UnitOfWork(TicketsAndMerchContext context, IDapperContext dapper)
        {
            _context = context;
            _dapper = dapper;
        }

        // Propiedades exigidas por la interfaz IUnitOfWork
        public IBaseRepository<Concert> ConcertRepository =>
           _concertRepository ??= new BaseRepository<Concert>(_context);
        public IBaseRepository<Merch> MerchRepository =>
            _merchRepository ??= new BaseRepository<Merch>(_context);

        // <= Asegúrate de que esta propiedad exista (Order)
        public IBaseRepository<Order> OrderRepository =>
            _orderRepository ??= new BaseRepository<Order>(_context);
        public IBaseRepository<Payment> PaymentRepository =>
            _paymentRepository ??= new BaseRepository<Payment>(_context);

        public IBaseRepository<Ticket> TicketRepository =>
            _ticketRepository ??= new BaseRepository<Ticket>(_context);
        public IBaseRepository<User> UserRepository =>
            _userRepository ??= new BaseRepository<User>(_context);
        public IBaseRepository<BuyTicket> BuyTicketRepository =>
            _buyTicketRepository ??= new BaseRepository<BuyTicket>(_context);


        public IUserRepository UserRepositoryExtra =>
            _userRepositoryExtra ??= new UserRepository(_context, _dapper);
        public IConcertRepository ConcertRepositoryExtra =>
            _concertRepositoryExtra ??= new ConcertRepository(_context, _dapper);
        public ITicketRepository TicketRepositoryExtra =>
            _ticketRepositoryExtra ??= new TicketRepository(_context, _dapper);
        public IPaymentRepository PaymentRepositoryExtra =>
            _paymentRepositoryExtra ??= new PaymentRepository(_context, _dapper);
        public IOrderRepository OrderRepositoryExtra =>
            _orderRepositoryExtra ??= new OrderRepository(_context, _dapper);
        public IMerchRepository MerchRepositoryExtra =>
            _merchRepositoryExtra ??= new MerchRepository(_context, _dapper);
        public IBuyTicketRepository BuyTicketRepositoryExtra =>
            _buyTicketRepositoryExtra ??= new BuyTicketRepository(_context, _dapper);



        // Sincrónico / asincrónico
        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        // Transacción EF + ambient Dapper (si usas Dapper)
        public async Task BeginTransactionAsync()
        {
            if (_efTransaction == null)
            {
                _efTransaction = await _context.Database.BeginTransactionAsync();

                // Registrar la conexión/tx en DapperContext para que Dapper use la misma transacción
                var conn = _context.Database.GetDbConnection();
                var tx = _efTransaction.GetDbTransaction();
                _dapper.SetAmbientConnection(conn, tx);
            }
        }

        public async Task CommitAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
                if (_efTransaction != null)
                {
                    await _efTransaction.CommitAsync();
                    await _efTransaction.DisposeAsync();
                    _efTransaction = null;
                }
            }
            finally
            {
                // limpiar la conexión ambient de Dapper aunque falle el commit
                _dapper.ClearAmbientConnection();
            }
        }

        public async Task RollbackAsync()
        {
            if (_efTransaction != null)
            {
                await _efTransaction.RollbackAsync();
                await _efTransaction.DisposeAsync();
                _efTransaction = null;
            }

            _dapper.ClearAmbientConnection();
        }

        // Métodos opcionales para Dapper
        public IDbConnection? GetDbConnection()
        {
            return _context.Database.GetDbConnection();
        }

        public IDbTransaction? GetDbTransaction()
        {
            return _efTransaction?.GetDbTransaction();
        }

        // Dispose
        public void Dispose()
        {
            _efTransaction?.Dispose();
            _context.Dispose();
        }
    }
}
