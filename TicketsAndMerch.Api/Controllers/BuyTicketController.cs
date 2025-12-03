using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Net;
using TicketsAndMerch.Api.Responses;
using TicketsAndMerch.Core.CustomEntities;
using TicketsAndMerch.Core.Entities;
using TicketsAndMerch.Core.Interfaces;
using TicketsAndMerch.Infrastructure.DTOs;
using TicketsAndMerch.Infrastructure.Validators;

namespace TicketsAndMerch.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class BuyTicketController : ControllerBase
    {
        private readonly IBuyTicketService _buyTicketService;
        private readonly IMapper _mapper;
        private readonly IValidationService _validationService;

        public BuyTicketController(
            IBuyTicketService buyTicketService,
            IMapper mapper,
            IValidationService validationService)
        {
            _buyTicketService = buyTicketService;
            _mapper = mapper;
            _validationService = validationService;
        }

        #region DTO Mapper (Compra de tickets)

        /// <summary>
        /// Permite registrar una compra de ticket(s) para un concierto disponible.
        /// </summary>
        /// <remarks>
        /// Valida usuario, concierto y disponibilidad de entradas.  
        /// Genera la orden y registra un pago con estado “Pendiente”.
        /// </remarks>
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<BuyTicketDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpPost("dto/mapper")]
        public async Task<IActionResult> BuyTicketDtoMapper([FromBody] BuyTicketDto buyTicketDto)
        {
            #region Validaciones
            var validationResult = await _validationService.ValidateAsync(buyTicketDto);
            if (!validationResult.IsValid)
                return BadRequest(new { Errors = validationResult.Errors });
            #endregion

            try
            {
                // Mapeo del DTO a la entidad
                var buyTicket = _mapper.Map<BuyTicket>(buyTicketDto);

                // Ejecución del caso de uso principal
                var result = await _buyTicketService.BuyTicketAsync(buyTicket);

                // Mapeo de vuelta a DTO para respuesta
                var resultDto = _mapper.Map<BuyTicketDto>(result);

                var response = new ApiResponse<BuyTicketDto>(resultDto)
                {
                    Messages = new[]
                    {
                        new Message
                        {
                            Type = "Information",
                            Description = "Compra registrada correctamente. Se generó una orden con estado 'Pendiente'."
                        }
                    }
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                var response = new ResponseData
                {
                    Messages = new[]
                    {
                        new Message { Type = "Error", Description = ex.Message }
                    }
                };
                return StatusCode(500, response);
            }
        }

        /// <summary>
        /// Recupera todas las compras de tickets mediante Dapper (lectura optimizada).
        /// </summary>
        [HttpGet("dto/dapper")]
        public async Task<IActionResult> GetAllBuyTicketsDapper()
        {
            var buyTickets = await _buyTicketService.GetAllBuyTicketsDapperAsync();
            var buyTicketsDto = _mapper.Map<IEnumerable<BuyTicketDto>>(buyTickets);
            var response = new ApiResponse<IEnumerable<BuyTicketDto>>(buyTicketsDto);
            return Ok(response);
        }

        /// <summary>
        /// Recupera los detalles de una compra específica.
        /// </summary>
        [HttpGet("dto/mapper/{id}")]
        public async Task<IActionResult> GetBuyTicketById(int id)
        {
            var validation = await _validationService.ValidateAsync(new GetByIdRequest { Id = id });
            if (!validation.IsValid)
                return BadRequest(new { Errors = validation.Errors });

            var buyTicket = await _buyTicketService.GetBuyTicketByIdAsync(id);
            if (buyTicket == null)
                return NotFound("Compra no encontrada.");

            var buyTicketDto = _mapper.Map<BuyTicketDto>(buyTicket);
            var response = new ApiResponse<BuyTicketDto>(buyTicketDto);
            return Ok(response);
        }

        /// <summary>
        /// Elimina una compra de ticket (solo para pruebas o mantenimiento).
        /// </summary>
        [HttpDelete("dto/mapper/{id}")]
        public async Task<IActionResult> DeleteBuyTicket(int id)
        {
            var buyTicket = await _buyTicketService.GetBuyTicketByIdAsync(id);
            if (buyTicket == null)
                return NotFound("Compra no encontrada.");

            await _buyTicketService.DeleteBuyTicketAsync(id);
            return NoContent();
        }

        #endregion
    }
}
