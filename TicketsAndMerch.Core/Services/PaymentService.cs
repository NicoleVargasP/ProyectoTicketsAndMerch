using TicketsAndMerch.Core.Entities;
using TicketsAndMerch.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TicketsAndMerch.Core.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;

        public PaymentService(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task<IEnumerable<Payment>> GetAllPaymentsAsync()
        {
            return await _paymentRepository.GetAllPaymentsAsync();
        }

        public async Task<Payment> GetPaymentByIdAsync(int id)
        {
            return await _paymentRepository.GetPaymentByIdAsync(id);
        }

        public async Task AddPaymentAsync(Payment payment)
        {
            if (string.IsNullOrWhiteSpace(payment.Method))
                throw new Exception("El método de pago es obligatorio.");

            if (payment.OrderAmount <= 0)
                throw new Exception("El monto debe ser mayor que 0.");

            if (string.IsNullOrWhiteSpace(payment.PaymentState))
                throw new Exception("El estado del pago es obligatorio.");

            payment.PaymentDate = payment.PaymentDate == default ? DateTime.Now : payment.PaymentDate;

            await _paymentRepository.AddPaymentAsync(payment);
        }

        public async Task UpdatePaymentAsync(Payment payment)
        {
            var existing = await _paymentRepository.GetPaymentByIdAsync(payment.PaymentId);
            if (existing == null)
                throw new Exception("El pago no existe.");

            existing.Method = payment.Method;
            existing.PaymentDate = payment.PaymentDate;
            existing.OrderAmount = payment.OrderAmount;
            existing.PaymentState = payment.PaymentState;

            await _paymentRepository.UpdatePaymentAsync(existing);
        }

        public async Task DeletePaymentAsync(Payment payment)
        {
            await _paymentRepository.DeletePaymentAsync(payment);
        }
    }
}
