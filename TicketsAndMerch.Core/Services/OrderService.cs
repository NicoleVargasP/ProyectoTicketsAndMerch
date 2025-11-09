using System.Net;
using TicketsAndMerch.Core.CustomEntities;
using TicketsAndMerch.Core.Entities;
using TicketsAndMerch.Core.Exceptions;
using TicketsAndMerch.Core.Interfaces;
using TicketsAndMerch.Core.QueryFilters;

namespace TicketsAndMerch.Core.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        public OrderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseData> GetAllOrdersAsync(OrderQueryFilter filters)
        {
            var orders = await _unitOfWork.OrderRepository.GetAll();

            if (filters.UserId != null)
                orders = orders.Where(x => x.UserId == filters.UserId);

            if (!string.IsNullOrWhiteSpace(filters.State))
                orders = orders.Where(x => x.State != null &&
                                           x.State.ToLower().Contains(filters.State.ToLower()));

            if (filters.StartDate != null)
                orders = orders.Where(x => x.DateOrder.HasValue &&
                                           x.DateOrder.Value.Date >= filters.StartDate.Value.Date);

            if (filters.EndDate != null)
                orders = orders.Where(x => x.DateOrder.HasValue &&
                                           x.DateOrder.Value.Date <= filters.EndDate.Value.Date);

            var pagedOrders = PagedList<object>.Create(orders, filters.PageNumber, filters.PageSize);

            return new ResponseData()
            {
                Messages = new[] { new Message { Type = "Information", Description = "Órdenes recuperadas correctamente." } },
                Pagination = pagedOrders,
                StatusCode = HttpStatusCode.OK
            };
        }

        public async Task<IEnumerable<Order>> GetAllOrdersDapperAsync()
        {
            var orders = await _unitOfWork.OrderRepository.GetAll();
            return orders;
        }

        public async Task<Order> GetOrderByIdAsync(int id)
        {
            return await _unitOfWork.OrderRepository.GetById(id);
        }

        public async Task AddOrderAsync(Order order)
        {
            if (order.UserId <= 0)
                throw new BussinessException("El usuario es obligatorio.");

            if (order.OrderAmount <= 0)
                throw new BussinessException("La cantidad debe ser mayor que 0.");

            order.DateOrder ??= DateTime.Now;

            await _unitOfWork.OrderRepository.Add(order);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateOrderAsync(Order order)
        {
            await _unitOfWork.OrderRepository.Update(order);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteOrderAsync(int id)
        {
            await _unitOfWork.OrderRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
