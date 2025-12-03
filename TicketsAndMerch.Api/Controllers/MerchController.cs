using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class MerchController : ControllerBase
    {
        private readonly IMerchService _merchService;
        private readonly IMapper _mapper;
        private readonly IValidationService _validationService;

        public MerchController(IMerchService merchService, IMapper mapper, IValidationService validationService)
        {
            _merchService = merchService;
            _mapper = mapper;
            _validationService = validationService;
        }

        #region Dto Mapper con QueryFilter y paginación

        /// <summary>
        /// Recupera una lista paginada de productos de merchandising según filtros.
        /// </summary>
        /// <remarks>
        /// Este método convierte las entidades <see cref="Merch"/> en <see cref="MerchDto"/> 
        /// y devuelve una respuesta con metadatos de paginación.
        /// </remarks>
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<MerchDto>>))]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpGet("dto/mapper")]
        public async Task<IActionResult> GetMerchDtoMapper(
            [FromQuery] MerchQueryFilter merchQueryFilter, int idAux)
        {
            try
            {
                var result = await _merchService.GetAllMerchAsync(merchQueryFilter);

                var merchesDto = _mapper.Map<IEnumerable<MerchDto>>(result.Pagination);

                var pagination = new Pagination
                {
                    TotalCount = result.Pagination.TotalCount,
                    PageSize = result.Pagination.PageSize,
                    CurrentPage = result.Pagination.CurrentPage,
                    TotalPages = result.Pagination.TotalPages,
                    HasNextPage = result.Pagination.HasNextPage,
                    HasPreviousPage = result.Pagination.HasPreviousPage
                };

                var response = new ApiResponse<IEnumerable<MerchDto>>(merchesDto)
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
        /// Recupera productos de merchandising utilizando Dapper (consulta optimizada).
        /// </summary>
        [HttpGet("dto/dapper")]
        public async Task<IActionResult> GetMerchDtoDapper()
        {
            var merches = await _merchService.GetAllMerchDapperAsync();
            var merchesDto = _mapper.Map<IEnumerable<MerchDto>>(merches);
            var response = new ApiResponse<IEnumerable<MerchDto>>(merchesDto);
            return Ok(response);
        }

        #endregion

        #region CRUD estándar con DTO Mapper

        [HttpGet("dto/mapper/{id}")]
        public async Task<IActionResult> GetMerchDtoById(int id)
        {
            var validation = await _validationService.ValidateAsync(new GetByIdRequest { Id = id });
            if (!validation.IsValid)
                return BadRequest(new { Errors = validation.Errors });

            var merch = await _merchService.GetMerchByIdAsync(id);
            if (merch == null)
                return NotFound("Producto no encontrado.");

            var merchDto = _mapper.Map<MerchDto>(merch);
            var response = new ApiResponse<MerchDto>(merchDto);
            return Ok(response);
        }

        [HttpPost("dto/mapper")]
        public async Task<IActionResult> AddMerchDtoMapper([FromBody] MerchDto merchDto)
        {
            var validation = await _validationService.ValidateAsync(merchDto);
            if (!validation.IsValid)
                return BadRequest(new { Errors = validation.Errors });

            try
            {
                var merch = _mapper.Map<Merch>(merchDto);
                await _merchService.AddMerchAsync(merch);
                var response = new ApiResponse<Merch>(merch);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Error al crear el producto", Error = ex.Message });
            }
        }

        [HttpPut("dto/mapper/{id}")]
        public async Task<IActionResult> UpdateMerchDtoMapper(int id, [FromBody] MerchDto merchDto)
        {
            if (id != merchDto.Id)
                return BadRequest("El Id del producto no coincide.");

            var merch = await _merchService.GetMerchByIdAsync(id);
            if (merch == null)
                return NotFound("Producto no encontrado.");

            _mapper.Map(merchDto, merch);
            await _merchService.UpdateMerchAsync(merch);

            var response = new ApiResponse<Merch>(merch);
            return Ok(response);
        }

        [HttpDelete("dto/mapper/{id}")]
        public async Task<IActionResult> DeleteMerchDtoMapper(int id)
        {
            var merch = await _merchService.GetMerchByIdAsync(id);
            if (merch == null)
                return NotFound("Producto no encontrado.");

            await _merchService.DeleteMerchAsync(id);
            return NoContent();
        }

        #endregion
    }
}
