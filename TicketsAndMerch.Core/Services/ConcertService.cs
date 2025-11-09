using System.Net;
using TicketsAndMerch.Core.CustomEntities;
using TicketsAndMerch.Core.Entities;
using TicketsAndMerch.Core.Exceptions;
using TicketsAndMerch.Core.Interfaces;
using TicketsAndMerch.Core.QueryFilters;

namespace TicketsAndMerch.Core.Services
{
    public class ConcertService : IConcertService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ConcertService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseData> GetAllConcertsAsync(ConcertQueryFilter filters)
        {
            var concerts = await _unitOfWork.ConcertRepositoryExtra.GetAll();

            if (!string.IsNullOrEmpty(filters.Title))
            {
                concerts = concerts.Where(x => x.Title.ToLower().Contains(filters.Title.ToLower()));
            }

            if (filters.Date != null)
            {
                concerts = concerts.Where(x => x.Date == filters.Date);
            }

            var pagedConcerts = PagedList<object>.Create(concerts, filters.PageNumber, filters.PageSize);

            if (pagedConcerts.Any())
            {
                return new ResponseData()
                {
                    Messages = new[] { new Message { Type = "Information", Description = "Conciertos recuperados correctamente." } },
                    Pagination = pagedConcerts,
                    StatusCode = HttpStatusCode.OK
                };
            }
            else
            {
                return new ResponseData()
                {
                    Messages = new[] { new Message { Type = "Warning", Description = "No se encontraron registros de conciertos." } },
                    Pagination = pagedConcerts,
                    StatusCode = HttpStatusCode.NotFound
                };
            }
        }

        public async Task<IEnumerable<Concert>> GetAllConcertsDapperAsync()
        {
            var concerts = await _unitOfWork.ConcertRepositoryExtra.GetAll();
            return concerts;
        }

        public async Task<Concert> GetConcertByIdAsync(int id)
        {
            return await _unitOfWork.ConcertRepositoryExtra.GetById(id);
        }

        public async Task AddConcertAsync(Concert concert)
        {
            if (string.IsNullOrWhiteSpace(concert.Title))
                throw new BussinessException("El título del concierto es obligatorio.");

            if (string.IsNullOrWhiteSpace(concert.Location))
                throw new BussinessException("La ubicación del concierto es obligatoria.");

            if (concert.Date < DateOnly.FromDateTime(DateTime.Today))
                throw new BussinessException("La fecha del concierto no puede ser anterior a hoy.");

            if (concert.AvailableTickets < 0)
                throw new BussinessException("El número de entradas disponibles no puede ser negativo.");

            await _unitOfWork.ConcertRepository.Add(concert);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateConcertAsync(Concert concert)
        {
            await _unitOfWork.ConcertRepository.Update(concert);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteConcertAsync(int id)
        {
            await _unitOfWork.ConcertRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
