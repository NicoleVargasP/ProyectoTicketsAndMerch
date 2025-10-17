using Microsoft.EntityFrameworkCore;
using TicketsAndMerch.Core.Entities;
using TicketsAndMerch.Core.Interfaces;
using TicketsAndMerch.Infrastructure.Data;

namespace TicketsAndMerch.Infrastructure.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        private readonly TicketsAndMerchContext _context;

        public TicketRepository(TicketsAndMerchContext context)
        {
            _context = context;
        }

        // Obtener todos los tickets
        public async Task<IEnumerable<Ticket>> GetAllTicketsAsync()
        {
            var tickets = await _context.Tickets
                .Include(t => t.Concert) // Incluye el concierto al que pertenece
                .ToListAsync();

            return tickets;
        }

        // Obtener un ticket por ID
        public async Task<Ticket> GetTicketByIdAsync(int id)
        {
            var ticket = await _context.Tickets
                .Include(t => t.Concert)
                .FirstOrDefaultAsync(t => t.TicketId == id);

            return ticket;
        }

        // Crear un nuevo ticket
        public async Task AddTicketAsync(Ticket ticket)
        {
            _context.Tickets.Add(ticket);
            await _context.SaveChangesAsync();
        }

        // Actualizar un ticket existente
        public async Task UpdateTicketAsync(Ticket ticket)
        {
            _context.Tickets.Update(ticket);
            await _context.SaveChangesAsync();
        }

        // Eliminar un ticket
        public async Task DeleteTicketAsync(Ticket ticket)
        {
            _context.Tickets.Remove(ticket);
            await _context.SaveChangesAsync();
        }
    }
}
