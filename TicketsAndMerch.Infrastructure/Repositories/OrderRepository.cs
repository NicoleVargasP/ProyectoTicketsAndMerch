using Microsoft.EntityFrameworkCore;
using TicketsAndMerch.Core.Entities;
using TicketsAndMerch.Core.Interfaces;
using TicketsAndMerch.Infrastructure.Data;

namespace TicketsAndMerch.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly TicketsAndMerchContext _context;

        public OrderRepository(TicketsAndMerchContext context)
        {
            _context = context;
        }

        // 🟢 Obtener todas las órdenes
        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            var orders = await _context.Orders
                .Include(o => o.User)    
                .Include(o => o.Merch)   
                .Include(o => o.Ticket)  
                .ToListAsync();

            return orders;
        }

        // 🟡 Obtener una orden por ID
        public async Task<Order> GetOrderByIdAsync(int id)
        {
            var order = await _context.Orders
                .Include(o => o.User)
                .Include(o => o.Merch)
                .Include(o => o.Ticket)
                .FirstOrDefaultAsync(o => o.OrderId == id);

            return order;
        }

        // Agregar una nueva orden
        public async Task AddOrderAsync(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
        }

        // Actualizar una orden existente
        public async Task UpdateOrderAsync(Order order)
        {
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
        }

        // Eliminar una orden
        public async Task DeleteOrderAsync(Order order)
        {
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
        }
    }
}
