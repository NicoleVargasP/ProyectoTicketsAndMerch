using System.Net;
using TicketsAndMerch.Core.CustomEntities;
using TicketsAndMerch.Core.Entities;
using TicketsAndMerch.Core.Exceptions;
using TicketsAndMerch.Core.Interfaces;
using TicketsAndMerch.Core.QueryFilters;

namespace TicketsAndMerch.Core.Services
{
    public class BuyTicketService : IBuyTicketService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BuyTicketService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseData> GetAllBuyTicketsAsync(BuyTicketQueryFilter filters)
        {
            var buyTickets = await _unitOfWork.BuyTicketRepositoryExtra.GetAll();

            if (filters.UserId != null)
                buyTickets = buyTickets.Where(x => x.UserId == filters.UserId);

            if (filters.ConcertId != null)
                buyTickets = buyTickets.Where(x => x.ConcertId == filters.ConcertId);

            var pagedBuyTickets = PagedList<object>.Create(buyTickets, filters.PageNumber, filters.PageSize);

            if (pagedBuyTickets.Any())
            {
                return new ResponseData
                {
                    Messages = new[] { new Message { Type = "Information", Description = "Compras de tickets recuperadas correctamente." } },
                    Pagination = pagedBuyTickets,
                    StatusCode = HttpStatusCode.OK
                };
            }

            return new ResponseData
            {
                Messages = new[] { new Message { Type = "Warning", Description = "No se encontraron registros de compras de tickets." } },
                Pagination = pagedBuyTickets,
                StatusCode = HttpStatusCode.NotFound
            };
        }

        public async Task<IEnumerable<BuyTicket>> GetAllBuyTicketsDapperAsync()
        {
            return await _unitOfWork.BuyTicketRepositoryExtra.GetAll();
        }

        public async Task<BuyTicket> GetBuyTicketByIdAsync(int id)
        {
            return await _unitOfWork.BuyTicketRepositoryExtra.GetById(id);
        }

        public async Task<BuyTicket> BuyTicketAsync(BuyTicket buyTicket)
        {
            // Validación de usuario
            var user = await _unitOfWork.UserRepositoryExtra.GetById(buyTicket.UserId);
            if (user == null)
                throw new BussinessException("El usuario no está registrado.");

            // Validación de concierto
            var concert = await _unitOfWork.ConcertRepositoryExtra.GetById(buyTicket.ConcertId);
            if (concert == null)
                throw new BussinessException("El concierto seleccionado no existe.");

            if (concert.AvailableTickets < buyTicket.Quantity)
                throw new BussinessException("No hay suficientes tickets disponibles.");

            // Calcular el total (precio unitario ejemplo = 100)
            buyTicket.TotalAmount = buyTicket.Quantity * 100;
            buyTicket.PaymentState = "Pendiente";

            // Guardar la compra
            await _unitOfWork.BuyTicketRepository.Add(buyTicket);

            // Actualizar tickets disponibles en el concierto
            concert.AvailableTickets -= buyTicket.Quantity;
            if (concert.AvailableTickets <= 0)
                concert.Description += " (AGOTADO)";

            await _unitOfWork.ConcertRepository.Update(concert);
            await _unitOfWork.SaveChangesAsync();

            return buyTicket;
        }

        public async Task DeleteBuyTicketAsync(int id)
        {
            await _unitOfWork.BuyTicketRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
