using Microsoft.EntityFrameworkCore;
using TicketsAndMerch.Core.Entities;
using TicketsAndMerch.Core.Interfaces;
using TicketsAndMerch.Infrastructure.Data;

namespace TicketsAndMerch.Infrastructure.Repositories
{
    public class ConcertRepository : IConcertRepository
    {
        private readonly TicketsAndMerchContext _context;

        public ConcertRepository(TicketsAndMerchContext context)
        {
            _context = context;
        }

        // Obtener todos los conciertos
        public async Task<IEnumerable<Concert>> GetAllConcertsAsync()
        {
            var concerts = await _context.Concerts.ToListAsync();
            return concerts;
        }

        // Obtener un concierto por ID
        public async Task<Concert> GetConcertByIdAsync(int id)
        {
            var concert = await _context.Concerts
                .FirstOrDefaultAsync(c => c.ConcertId == id);
            return concert;
        }

        // Insertar un nuevo concierto
        public async Task AddConcertAsync(Concert concert)
        {
            _context.Concerts.Add(concert);
            await _context.SaveChangesAsync();
        }

        // Actualizar un concierto existente
        public async Task UpdateConcertAsync(Concert concert)
        {
            _context.Concerts.Update(concert);
            await _context.SaveChangesAsync();
        }

        // Eliminar un concierto
        public async Task DeleteConcertAsync(Concert concert)
        {
            _context.Concerts.Remove(concert);
            await _context.SaveChangesAsync();
        }
    }
}
