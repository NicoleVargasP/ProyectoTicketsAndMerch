using TicketsAndMerch.Core.Entities;

namespace TicketsAndMerch.Core.Interfaces
{
    public interface IPaymentRepository : IBaseRepository<Payment>
    {
        Task<IEnumerable<Payment>> GetAllPaymentsAsync();
        Task<Payment> GetPaymentByIdAsync(int id);
        Task<IEnumerable<Payment>> GetPaymentsByOrderAsync(int orderId);
    }
}

