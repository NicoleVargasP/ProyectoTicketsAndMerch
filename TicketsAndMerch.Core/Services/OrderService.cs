using TicketsAndMerch.Core.Entities;
using TicketsAndMerch.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TicketsAndMerch.Core.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await _orderRepository.GetAllOrdersAsync();
        }

        public async Task<Order> GetOrderByIdAsync(int id)
        {
            return await _orderRepository.GetOrderByIdAsync(id);
        }

        public async Task AddOrderAsync(Order order)
        {
            if (order.UserId <= 0)
                throw new Exception("El usuario es obligatorio.");

            if (order.OrderAmount <= 0)
                throw new Exception("La cantidad debe ser mayor que 0.");

            if (order.UnitPrice <= 0)
                throw new Exception("El precio unitario debe ser mayor que 0.");

            order.DateOrder = order.DateOrder == default ? DateTime.Now : order.DateOrder;
            await _orderRepository.AddOrderAsync(order);
        }

        public async Task UpdateOrderAsync(Order order)
        {
            var existing = await _orderRepository.GetOrderByIdAsync(order.OrderId);
            if (existing == null)
                throw new Exception("La orden no existe.");

            existing.State = order.State;
            existing.MerchId = order.MerchId;
            existing.TicketId = order.TicketId;
            existing.OrderAmount = order.OrderAmount;
            existing.UnitPrice = order.UnitPrice;
            existing.OrderDetail = order.OrderDetail;

            await _orderRepository.UpdateOrderAsync(existing);
        }

        public async Task DeleteOrderAsync(Order order)
        {
            await _orderRepository.DeleteOrderAsync(order);
        }
    }
}
