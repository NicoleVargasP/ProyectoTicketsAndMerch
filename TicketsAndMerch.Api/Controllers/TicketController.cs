using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Net;
using TicketsAndMerch.Api.Responses;
using TicketsAndMerch.Core.CustomEntities;
using TicketsAndMerch.Core.Entities;
using TicketsAndMerch.Core.Interfaces;
using TicketsAndMerch.Core.QueryFilters;
using TicketsAndMerch.Infrastructure.DTOs;
using TicketsAndMerch.Infrastructure.Validators;

namespace TicketsAndMerch.Api.Controllers
{
   
    [ApiController]
    [Route("api/[controller]")]
    public class TicketController : ControllerBase
    {
        private readonly ITicketService _ticketService;
        private readonly IMapper _mapper;
        private readonly IValidationService _validationService;

        public TicketController(
            ITicketService ticketService,
            IMapper mapper,
            IValidationService validationService)
        {
            _ticketService = ticketService;
            _mapper = mapper;
            _validationService = validationService;
        }

        #region Dto Mapper con QueryFilter y paginación

        /// <summary>
        /// Recupera una lista paginada de tickets según filtros aplicados.
        /// </summary>
        /// <remarks>
        /// Este método permite aplicar filtros (por concierto, tipo, precio o stock) y devuelve los resultados paginados.
        /// </remarks>
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<TicketDto>>))]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpGet("dto/mapper")]
        public async Task<IActionResult> GetTicketsDtoMapper([FromQuery] TicketQueryFilter ticketQueryFilter, int idAux)
        {
            try
            {
                var result = await _ticketService.GetAllTicketsAsync(ticketQueryFilter);

                var ticketsDto = _mapper.Map<IEnumerable<TicketDto>>(result.Pagination);

                var pagination = new Pagination
                {
                    TotalCount = result.Pagination.TotalCount,
                    PageSize = result.Pagination.PageSize,
                    CurrentPage = result.Pagination.CurrentPage,
                    TotalPages = result.Pagination.TotalPages,
                    HasNextPage = result.Pagination.HasNextPage,
                    HasPreviousPage = result.Pagination.HasPreviousPage
                };

                var response = new ApiResponse<IEnumerable<TicketDto>>(ticketsDto)
                {
                    Pagination = pagination,
                    Messages = result.Messages
                };

                return StatusCode((int)result.StatusCode, response);
            }
            catch (Exception ex)
            {
                var responseError = new ResponseData
                {
                    Messages = new[] { new Message { Type = "Error", Description = ex.Message } }
                };
                return StatusCode(500, responseError);
            }
        }

        /// <summary>
        /// Recupera todos los tickets usando Dapper (consulta optimizada).
        /// </summary>
        [HttpGet("dto/dapper")]
        public async Task<IActionResult> GetTicketsDtoDapper()
        {
            var tickets = await _ticketService.GetAllTicketsDapperAsync();
            var ticketsDto = _mapper.Map<IEnumerable<TicketDto>>(tickets);
            var response = new ApiResponse<IEnumerable<TicketDto>>(ticketsDto);
            return Ok(response);
        }

        #endregion

        #region CRUD estándar con DTO Mapper

        [HttpGet("dto/mapper/{id}")]
        public async Task<IActionResult> GetTicketDtoMapperById(int id)
        {
            var validation = await _validationService.ValidateAsync(new GetByIdRequest { Id = id });
            if (!validation.IsValid)
                return BadRequest(new { Errors = validation.Errors });

            var ticket = await _ticketService.GetTicketByIdAsync(id);
            if (ticket == null)
                return NotFound("Ticket no encontrado.");

            var ticketDto = _mapper.Map<TicketDto>(ticket);
            var response = new ApiResponse<TicketDto>(ticketDto);
            return Ok(response);
        }

        [HttpPost("dto/mapper")]
        public async Task<IActionResult> AddTicketDtoMapper([FromBody] TicketDto ticketDto)
        {
            var validation = await _validationService.ValidateAsync(ticketDto);
            if (!validation.IsValid)
                return BadRequest(new { Errors = validation.Errors });

            try
            {
                var ticket = _mapper.Map<Ticket>(ticketDto);
                await _ticketService.AddTicketAsync(ticket);

                var response = new ApiResponse<Ticket>(ticket);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Error al crear el ticket", Error = ex.Message });
            }
        }

        [HttpPut("dto/mapper/{id}")]
        public async Task<IActionResult> UpdateTicketDtoMapper(int id, [FromBody] TicketDto ticketDto)
        {
            if (id != ticketDto.Id)
                return BadRequest("El Id del ticket no coincide con el cuerpo de la solicitud.");

            var ticket = await _ticketService.GetTicketByIdAsync(id);
            if (ticket == null)
                return NotFound("Ticket no encontrado.");

            _mapper.Map(ticketDto, ticket);
            await _ticketService.UpdateTicketAsync(ticket);

            var response = new ApiResponse<Ticket>(ticket);
            return Ok(response);
        }

        [HttpDelete("dto/mapper/{id}")]
        public async Task<IActionResult> DeleteTicketDtoMapper(int id)
        {
            var ticket = await _ticketService.GetTicketByIdAsync(id);
            if (ticket == null)
                return NotFound("Ticket no encontrado.");

            await _ticketService.DeleteTicketAsync(id);
            return NoContent();
        }

        #endregion
    }
}
