using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TicketsAndMerch.Api.Responses;
using TicketsAndMerch.Core.Entities;
using TicketsAndMerch.Core.Interfaces;
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

        public TicketController(ITicketService ticketService, IMapper mapper, IValidationService validationService)
        {
            _ticketService = ticketService;
            _mapper = mapper;
            _validationService = validationService;
        }

        #region Dto Mapper
        [HttpGet("dto/mapper")]
        public async Task<IActionResult> GetTicketsDtoMapper()
        {
            var tickets = await _ticketService.GetAllTicketsAsync();
            var ticketsDto = _mapper.Map<IEnumerable<TicketDto>>(tickets);
            var response = new ApiResponse<IEnumerable<TicketDto>>(ticketsDto);
            return Ok(response);
        }

        [HttpGet("dto/mapper/{id}")]
        public async Task<IActionResult> GetTicketDtoMapperId(int id)
        {
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

            var ticket = _mapper.Map<Ticket>(ticketDto);
            await _ticketService.AddTicketAsync(ticket);
            var response = new ApiResponse<Ticket>(ticket);
            return Ok(response);
        }

        [HttpPut("dto/mapper/{id}")]
        public async Task<IActionResult> UpdateTicketDtoMapper(int id, [FromBody] TicketDto ticketDto)
        {
            if (id != ticketDto.TicketId)
                return BadRequest("El Id del ticket no coincide.");

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

            await _ticketService.DeleteTicketAsync(ticket);
            return NoContent();
        }
        #endregion
    }
}
