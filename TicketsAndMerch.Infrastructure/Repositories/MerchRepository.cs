using Microsoft.EntityFrameworkCore;
using TicketsAndMerch.Core.Entities;
using TicketsAndMerch.Core.Interfaces;
using TicketsAndMerch.Infrastructure.Data;

namespace TicketsAndMerch.Infrastructure.Repositories
{
    public class MerchRepository : IMerchRepository
    {
        private readonly TicketsAndMerchContext _context;

        public MerchRepository(TicketsAndMerchContext context)
        {
            _context = context;
        }

        // Obtener todos los productos de merch
        public async Task<IEnumerable<Merch>> GetAllMerchAsync()
        {
            var merches = await _context.Merches.ToListAsync();
            return merches;
        }

        // Obtener un producto de merch por ID
        public async Task<Merch> GetMerchByIdAsync(int id)
        {
            var merch = await _context.Merches
                .FirstOrDefaultAsync(m => m.MerchId == id);
            return merch;
        }

        // Insertar un nuevo producto de merch
        public async Task AddMerchAsync(Merch merch)
        {
            _context.Merches.Add(merch);
            await _context.SaveChangesAsync();
        }

        // Actualizar un producto de merch existente
        public async Task UpdateMerchAsync(Merch merch)
        {
            _context.Merches.Update(merch);
            await _context.SaveChangesAsync();
        }

        // Eliminar un producto de merch
        public async Task DeleteMerchAsync(Merch merch)
        {
            _context.Merches.Remove(merch);
            await _context.SaveChangesAsync();
        }
    }
}
