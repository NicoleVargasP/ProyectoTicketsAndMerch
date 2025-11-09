using System.Net;
using TicketsAndMerch.Core.CustomEntities;
using TicketsAndMerch.Core.Entities;
using TicketsAndMerch.Core.Exceptions;
using TicketsAndMerch.Core.Interfaces;
using TicketsAndMerch.Core.QueryFilters;

namespace TicketsAndMerch.Core.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IUnitOfWork _unitOfWork;
        public PaymentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseData> GetAllPaymentsAsync(PaymentQueryFilter filters)
        {
            var payments = await _unitOfWork.PaymentRepository.GetAll();

            if (!string.IsNullOrEmpty(filters.Method))
                payments = payments.Where(x => x.Method.ToLower().Contains(filters.Method.ToLower()));

            var pagedPayments = PagedList<object>.Create(payments, filters.PageNumber, filters.PageSize);

            return new ResponseData()
            {
                Messages = new[] { new Message { Type = "Information", Description = "Pagos recuperados correctamente." } },
                Pagination = pagedPayments,
                StatusCode = HttpStatusCode.OK
            };
        }

        public async Task<IEnumerable<Payment>> GetAllPaymentsDapperAsync()
        {
            var payments = await _unitOfWork.PaymentRepository.GetAll();
            return payments;
        }

        public async Task<Payment> GetPaymentByIdAsync(int id)
        {
            return await _unitOfWork.PaymentRepository.GetById(id);
        }

        public async Task AddPaymentAsync(Payment payment)
        {
            if (string.IsNullOrWhiteSpace(payment.Method))
                throw new BussinessException("El método de pago es obligatorio.");

            if (payment.OrderAmount <= 0)
                throw new BussinessException("El monto debe ser mayor que 0.");

            await _unitOfWork.PaymentRepository.Add(payment);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdatePaymentAsync(Payment payment)
        {
            await _unitOfWork.PaymentRepository.Update(payment);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeletePaymentAsync(int id)
        {
            await _unitOfWork.PaymentRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
