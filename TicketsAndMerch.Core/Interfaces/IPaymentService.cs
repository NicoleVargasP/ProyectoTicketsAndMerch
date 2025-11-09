using TicketsAndMerch.Core.CustomEntities;
using TicketsAndMerch.Core.Entities;
using TicketsAndMerch.Core.QueryFilters;

namespace TicketsAndMerch.Core.Interfaces
{
    public interface IPaymentService
    {
        Task<ResponseData> GetAllPaymentsAsync(PaymentQueryFilter filters);
        Task<IEnumerable<Payment>> GetAllPaymentsDapperAsync();
        Task<Payment> GetPaymentByIdAsync(int id);
        Task AddPaymentAsync(Payment payment);
        Task UpdatePaymentAsync(Payment payment);
        Task DeletePaymentAsync(int id);
    }
}
