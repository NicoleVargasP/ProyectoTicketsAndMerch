using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TicketsAndMerch.Core.Entities;
using TicketsAndMerch.Core.CustomEntities;
using TicketsAndMerch.Core.Interfaces;
using TicketsAndMerch.Infrastructure.Data;

namespace TicketsAndMerch.Infrastructure.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected readonly TicketsAndMerchContext _context;
        protected readonly DbSet<T> _entities;

        public BaseRepository(TicketsAndMerchContext context)
        {
            _context = context;
            _entities = context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _entities.ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            return await _entities.FindAsync(id);
        }

        public async Task Add(T entity)
        {
            await _entities.AddAsync(entity);
        }

        public async Task Update(T entity)
        {
             _entities.Update(entity);
        }

        public async Task Delete(int id)
        {
            var entity = await GetById(id);
            if (entity != null)
                _entities.Remove(entity);
        }
    }
}
