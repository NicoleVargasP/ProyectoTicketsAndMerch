using Microsoft.EntityFrameworkCore;
using TicketsAndMerch.Core.Entities;
using TicketsAndMerch.Core.Interfaces;
using TicketsAndMerch.Infrastructure.Data;

namespace TicketsAndMerch.Infrastructure.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly TicketsAndMerchContext _context;

        public PaymentRepository(TicketsAndMerchContext context)
        {
            _context = context;
        }

        // Obtener todos los pagos
        public async Task<IEnumerable<Payment>> GetAllPaymentsAsync()
        {
            var payments = await _context.Payments
                .Include(p => p.Order) 
                .ToListAsync();

            return payments;
        }

        // Obtener un pago por ID
        public async Task<Payment> GetPaymentByIdAsync(int id)
        {
            var payment = await _context.Payments
                .Include(p => p.Order)
                .FirstOrDefaultAsync(p => p.PaymentId == id);

            return payment;
        }

        // Crear un nuevo pago
        public async Task AddPaymentAsync(Payment payment)
        {
            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();
        }

        // Actualizar un pago existente
        public async Task UpdatePaymentAsync(Payment payment)
        {
            _context.Payments.Update(payment);
            await _context.SaveChangesAsync();
        }

        // Eliminar un pago
        public async Task DeletePaymentAsync(Payment payment)
        {
            _context.Payments.Remove(payment);
            await _context.SaveChangesAsync();
        }
    }
}
