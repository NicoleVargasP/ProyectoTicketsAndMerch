using TicketsAndMerch.Core.Entities;
using TicketsAndMerch.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TicketsAndMerch.Core.Services
{
    public class MerchService : IMerchService
    {
        private readonly IMerchRepository _merchRepository;

        public MerchService(IMerchRepository merchRepository)
        {
            _merchRepository = merchRepository;
        }

        public async Task<IEnumerable<Merch>> GetAllMerchAsync()
        {
            return await _merchRepository.GetAllMerchAsync();
        }

        public async Task<Merch> GetMerchByIdAsync(int id)
        {
            return await _merchRepository.GetMerchByIdAsync(id);
        }

        public async Task AddMerchAsync(Merch merch)
        {
            if (string.IsNullOrWhiteSpace(merch.MerchName))
            {
                throw new Exception("El nombre del producto es obligatorio.");
            }

            if (merch.Price <= 0)
            {
                throw new Exception("El precio debe ser mayor que 0.");
            }

            if (merch.Stock < 0)
            {
                throw new Exception("El stock no puede ser negativo.");
            }

            await _merchRepository.AddMerchAsync(merch);
        }

        public async Task UpdateMerchAsync(Merch merch)
        {
            var existing = await _merchRepository.GetMerchByIdAsync(merch.MerchId);
            if (existing == null)
            {
                throw new Exception("El producto no existe.");
            }

            existing.MerchName = merch.MerchName;
            existing.Description = merch.Description;
            existing.Price = merch.Price;
            existing.TypeMerch = merch.TypeMerch;
            existing.Stock = merch.Stock;

            await _merchRepository.UpdateMerchAsync(existing);
        }

        public async Task DeleteMerchAsync(Merch merch)
        {
            await _merchRepository.DeleteMerchAsync(merch);
        }
    }
}
