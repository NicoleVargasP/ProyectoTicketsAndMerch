using System.Net;
using TicketsAndMerch.Core.CustomEntities;
using TicketsAndMerch.Core.Entities;
using TicketsAndMerch.Core.Exceptions;
using TicketsAndMerch.Core.Interfaces;
using TicketsAndMerch.Core.QueryFilters;

namespace TicketsAndMerch.Core.Services
{
    public class TicketService : ITicketService
    {
        private readonly IUnitOfWork _unitOfWork;
        public TicketService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseData> GetAllTicketsAsync(TicketQueryFilter filters)
        {
            var tickets = await _unitOfWork.TicketRepository.GetAll();

            if (filters.ConcertId != null)
                tickets = tickets.Where(x => x.ConcertId == filters.ConcertId);

            if (!string.IsNullOrEmpty(filters.TicketType))
                tickets = tickets.Where(x => x.TicketType.ToLower().Contains(filters.TicketType.ToLower()));

            var pagedTickets = PagedList<object>.Create(tickets, filters.PageNumber, filters.PageSize);

            return new ResponseData()
            {
                Messages = new[] { new Message { Type = "Information", Description = "Tickets recuperados correctamente." } },
                Pagination = pagedTickets,
                StatusCode = HttpStatusCode.OK
            };
        }

        public async Task<IEnumerable<Ticket>> GetAllTicketsDapperAsync()
        {
            var tickets = await _unitOfWork.TicketRepository.GetAll();
            return tickets;
        }

        public async Task<Ticket> GetTicketByIdAsync(int id)
        {
            return await _unitOfWork.TicketRepository.GetById(id);
        }

        public async Task AddTicketAsync(Ticket ticket)
        {
            if (ticket.ConcertId <= 0)
                throw new BussinessException("El Id del concierto es obligatorio.");

            if (ticket.Price <= 0)
                throw new BussinessException("El precio debe ser mayor que 0.");

            await _unitOfWork.TicketRepository.Add(ticket);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateTicketAsync(Ticket ticket)
        {
            await _unitOfWork.TicketRepository.Update(ticket);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteTicketAsync(int id)
        {
            await _unitOfWork.TicketRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
