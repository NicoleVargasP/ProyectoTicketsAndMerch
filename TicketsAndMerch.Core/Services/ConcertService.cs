using TicketsAndMerch.Core.Entities;
using TicketsAndMerch.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TicketsAndMerch.Core.Services
{
    public class ConcertService : IConcertService
    {
        private readonly IConcertRepository _concertRepository;

        public ConcertService(IConcertRepository concertRepository)
        {
            _concertRepository = concertRepository;
        }


        public async Task<IEnumerable<Concert>> GetAllConcertsAsync()
        {
            return await _concertRepository.GetAllConcertsAsync();
        }

        public async Task<Concert> GetConcertByIdAsync(int id)
        {
            return await _concertRepository.GetConcertByIdAsync(id);
        }

        public async Task AddConcertAsync(Concert concert)
        {
            if (string.IsNullOrWhiteSpace(concert.Title))
                throw new Exception("El título del concierto es obligatorio.");

            if (string.IsNullOrWhiteSpace(concert.Location))
                throw new Exception("La ubicación del concierto es obligatoria.");

            if (concert.Date < DateOnly.FromDateTime(DateTime.Today))
                throw new Exception("La fecha del concierto no puede ser anterior a hoy.");

            if (concert.AvailableTickets < 0)
                throw new Exception("El número de entradas disponibles no puede ser negativo.");

            await _concertRepository.AddConcertAsync(concert);
        }

        public async Task UpdateConcertAsync(Concert concert)
        {
            await _concertRepository.UpdateConcertAsync(concert);
        }

        public async Task DeleteConcertAsync(Concert concert)
        {
            await _concertRepository.DeleteConcertAsync(concert);
        }
    }
}
