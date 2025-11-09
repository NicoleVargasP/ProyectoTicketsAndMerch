using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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
    public class ConcertController : ControllerBase
    {
        private readonly IConcertService _concertService;
        private readonly IMapper _mapper;
        private readonly IValidationService _validationService;

        public ConcertController(
            IConcertService concertService,
            IMapper mapper,
            IValidationService validationService)
        {
            _concertService = concertService;
            _mapper = mapper;
            _validationService = validationService;
        }

        #region Dto Mapper (con QueryFilter y Paginación)

        /// <summary>
        /// Recupera una lista paginada de conciertos como DTOs según filtros
        /// </summary>
        /// <remarks>
        /// Convierte las entidades Concert en ConcertDto y devuelve la respuesta paginada.
        /// </remarks>
        /// <param name="concertQueryFilter">Filtros de consulta y paginación</param>
        /// <param name="idAux">Identificador auxiliar (opcional, se mantiene por compatibilidad con el ejemplo)</param>
        /// <returns>Lista paginada de ConcertDto</returns>
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<ConcertDto>>))]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpGet("dto/mapper")]
        public async Task<IActionResult> GetConcertsDtoMapper(
            [FromQuery] ConcertQueryFilter concertQueryFilter, int idAux)
        {
            try
            {
                // Nota: el servicio debe devolver un ResponseData con Pagination (igual que el ejemplo Post)
                var result = await _concertService.GetAllConcertsAsync(concertQueryFilter);

                // Mapeo de la página actual a DTOs
                var concertsDto = _mapper.Map<IEnumerable<ConcertDto>>(result.Pagination);

                var pagination = new Pagination
                {
                    TotalCount = result.Pagination.TotalCount,
                    PageSize = result.Pagination.PageSize,
                    CurrentPage = result.Pagination.CurrentPage,
                    TotalPages = result.Pagination.TotalPages,
                    HasNextPage = result.Pagination.HasNextPage,
                    HasPreviousPage = result.Pagination.HasPreviousPage
                };

                var response = new ApiResponse<IEnumerable<ConcertDto>>(concertsDto)
                {
                    Pagination = pagination,
                    Messages = result.Messages
                };

                return StatusCode((int)result.StatusCode, response);
            }
            catch (Exception err)
            {
                var responsePost = new ResponseData()
                {
                    Messages = new Message[] { new() { Type = "Error", Description = err.Message } },
                };
                return StatusCode(500, responsePost);
            }
        }

        /// <summary>
        /// Recupera conciertos usando Dapper (lectura optimizada).
        /// </summary>
        [HttpGet("dto/dapper")]
        public async Task<IActionResult> GetConcertsDtoDapper()
        {
            var concerts = await _concertService.GetAllConcertsDapperAsync();
            var concertsDto = _mapper.Map<IEnumerable<ConcertDto>>(concerts);
            var response = new ApiResponse<IEnumerable<ConcertDto>>(concertsDto);
            return Ok(response);
        }

        [HttpGet("dto/mapper/{id}")]
        public async Task<IActionResult> GetConcertDtoById(int id)
        {
            #region Validaciones
            var validationRequest = new GetByIdRequest { Id = id };
            var validationResult = await _validationService.ValidateAsync(validationRequest);

            if (!validationResult.IsValid)
            {
                return BadRequest(new
                {
                    Message = "Error de validación del ID",
                    Errors = validationResult.Errors
                });
            }
            #endregion

            var concert = await _concertService.GetConcertByIdAsync(id);
            if (concert == null)
                return NotFound("Concierto no encontrado.");

            var concertDto = _mapper.Map<ConcertDto>(concert);
            var response = new ApiResponse<ConcertDto>(concertDto);
            return Ok(response);
        }

        [HttpPost("dto/mapper")]
        public async Task<IActionResult> InsertConcertDtoMapper([FromBody] ConcertDto concertDto)
        {
            #region Validaciones
            var validationResult = await _validationService.ValidateAsync(concertDto);
            if (!validationResult.IsValid)
                return BadRequest(new { Errors = validationResult.Errors });
            #endregion

            try
            {
                var concert = _mapper.Map<Concert>(concertDto);
                await _concertService.AddConcertAsync(concert);

                var response = new ApiResponse<Concert>(concert);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { Message = ex.Message });
            }
        }

        [HttpPut("dto/mapper/{id}")]
        public async Task<IActionResult> UpdateConcertDtoMapper(int id, [FromBody] ConcertDto concertDto)
        {
            if (id != concertDto.ConcertId)
                return BadRequest("El Id del concierto no coincide");

            var concert = await _concertService.GetConcertByIdAsync(id);
            if (concert == null)
                return NotFound("Concierto no encontrado");

            _mapper.Map(concertDto, concert);
            await _concertService.UpdateConcertAsync(concert);

            var response = new ApiResponse<Concert>(concert);
            return Ok(response);
        }

        [HttpDelete("dto/mapper/{id}")]
        public async Task<IActionResult> DeleteConcertDtoMapper(int id)
        {
            var concert = await _concertService.GetConcertByIdAsync(id);
            if (concert == null)
                return NotFound("Concierto no encontrado");

            await _concertService.DeleteConcertAsync(id);
            return NoContent();
        }

        #endregion
    }
}
