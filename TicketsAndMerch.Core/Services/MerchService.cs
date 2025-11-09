using System.Net;
using TicketsAndMerch.Core.CustomEntities;
using TicketsAndMerch.Core.Entities;
using TicketsAndMerch.Core.Exceptions;
using TicketsAndMerch.Core.Interfaces;
using TicketsAndMerch.Core.QueryFilters;

namespace TicketsAndMerch.Core.Services
{
    public class MerchService : IMerchService
    {
        private readonly IUnitOfWork _unitOfWork;

        public MerchService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseData> GetAllMerchAsync(MerchQueryFilter filters)
        {
            var merch = await _unitOfWork.MerchRepositoryExtra.GetAll();

            if (!string.IsNullOrEmpty(filters.MerchName))
                merch = merch.Where(x => x.MerchName.ToLower().Contains(filters.MerchName.ToLower()));

            if (filters.MinPrice != null)
                merch = merch.Where(x => x.Price >= filters.MinPrice);

            if (filters.MaxPrice != null)
                merch = merch.Where(x => x.Price <= filters.MaxPrice);

            var pagedMerch = PagedList<object>.Create(merch, filters.PageNumber, filters.PageSize);

            return new ResponseData()
            {
                Messages = new[] { new Message { Type = "Information", Description = "Productos recuperados correctamente." } },
                Pagination = pagedMerch,
                StatusCode = HttpStatusCode.OK
            };
        }

        public async Task<IEnumerable<Merch>> GetAllMerchDapperAsync()
        {
            var merch = await _unitOfWork.MerchRepositoryExtra.GetAll();
            return merch;
        }

        public async Task<Merch> GetMerchByIdAsync(int id)
        {
            return await _unitOfWork.MerchRepositoryExtra.GetById(id);
        }

        public async Task AddMerchAsync(Merch merch)
        {
            if (string.IsNullOrWhiteSpace(merch.MerchName))
                throw new BussinessException("El nombre del producto es obligatorio.");

            if (merch.Price <= 0)
                throw new BussinessException("El precio debe ser mayor que 0.");

            if (merch.Stock < 0)
                throw new BussinessException("El stock no puede ser negativo.");

            await _unitOfWork.MerchRepository.Add(merch);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateMerchAsync(Merch merch)
        {
            await _unitOfWork.MerchRepository.Update(merch);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteMerchAsync(int id)
        {
            await _unitOfWork.MerchRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
