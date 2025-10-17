using TicketsAndMerch.Core.Entities;
using TicketsAndMerch.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TicketsAndMerch.Core.Services
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _ticketRepository;

        public TicketService(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        public async Task<IEnumerable<Ticket>> GetAllTicketsAsync()
        {
            return await _ticketRepository.GetAllTicketsAsync();
        }

        public async Task<Ticket> GetTicketByIdAsync(int id)
        {
            return await _ticketRepository.GetTicketByIdAsync(id);
        }

        public async Task AddTicketAsync(Ticket ticket)
        {
            if (ticket.ConcertId <= 0)
                throw new Exception("El Id del concierto es obligatorio.");

            if (ticket.Price <= 0)
                throw new Exception("El precio del ticket debe ser mayor que 0.");

            if (string.IsNullOrWhiteSpace(ticket.TicketType))
                throw new Exception("El tipo de ticket es obligatorio.");

            if (ticket.Stock < 0)
                throw new Exception("El stock no puede ser negativo.");

            await _ticketRepository.AddTicketAsync(ticket);
        }

        public async Task UpdateTicketAsync(Ticket ticket)
        {
            var existing = await _ticketRepository.GetTicketByIdAsync(ticket.TicketId);
            if (existing == null)
                throw new Exception("El ticket no existe.");

            existing.ConcertId = ticket.ConcertId;
            existing.Price = ticket.Price;
            existing.TicketType = ticket.TicketType;
            existing.Stock = ticket.Stock;

            await _ticketRepository.UpdateTicketAsync(existing);
        }

        public async Task DeleteTicketAsync(Ticket ticket)
        {
            await _ticketRepository.DeleteTicketAsync(ticket);
        }
    }
}
