using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TicketsAndMerch.Api.Responses;
using TicketsAndMerch.Core.CustomEntities;
using TicketsAndMerch.Core.Entities;
using TicketsAndMerch.Core.Interfaces;
using TicketsAndMerch.Core.Services;
using TicketsAndMerch.Infrastructure.DTOs;
using TicketsAndMerch.Infrastructure.Validators;

namespace TicketsAndMerch.Api.Controllers
{
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

        #region Dto Mapper

        // --------------------------------------------------------------
        // Obtener todos los merch (usando DTO + Mapper)
        // --------------------------------------------------------------
        [HttpGet("dto/mapper")]
        public async Task<IActionResult> GetAllMerchDtoMapper()
        {
            var merches = await _merchService.GetAllMerchAsync();
            var merchesDto = _mapper.Map<IEnumerable<MerchDto>>(merches);

            var response = new ApiResponse<IEnumerable<MerchDto>>(merchesDto);
            return Ok(response);
        }

        // --------------------------------------------------------------
        // Obtener un merch por ID (con validación)
        // --------------------------------------------------------------
        [HttpGet("dto/mapper/{id}")]
        public async Task<IActionResult> GetMerchDtoMapperById(int id)
        {
            #region Validación
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

            var merch = await _merchService.GetMerchByIdAsync(id);
            if (merch == null)
                return NotFound(new { Error = "Producto no encontrado." });

            var merchDto = _mapper.Map<MerchDto>(merch);
            var response = new ApiResponse<MerchDto>(merchDto);

            return Ok(response);
        }

        // --------------------------------------------------------------
        // Crear nuevo merch (DTO + validación)
        // --------------------------------------------------------------
        [HttpPost("dto/mapper")]
        public async Task<IActionResult> InsertMerchDtoMapper([FromBody] MerchDto merchDto)
        {
            #region Validación con FluentValidation
            var validationResult = await _validationService.ValidateAsync(merchDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(new { Errors = validationResult.Errors });
            }
            #endregion

            var merch = _mapper.Map<Merch>(merchDto);
            await _merchService.AddMerchAsync(merch);

            var response = new ApiResponse<Merch>(merch);
            return Ok(response);
        }

        // --------------------------------------------------------------
        // Actualizar merch existente (DTO + Mapper)
        // --------------------------------------------------------------
        [HttpPut("dto/mapper/{id}")]
        public async Task<IActionResult> UpdateMerchDtoMapper(int id, [FromBody] MerchDto merchDto)
        {
            if (id != merchDto.MerchId)
                return BadRequest("El Id del producto no coincide con el del cuerpo.");

            var merch = await _merchService.GetMerchByIdAsync(id);
            if (merch == null)
                return NotFound("Producto no encontrado.");

            _mapper.Map(merchDto, merch);
            await _merchService.UpdateMerchAsync(merch);

            var response = new ApiResponse<Merch>(merch);
            return Ok(response);
        }

        // --------------------------------------------------------------
        // Eliminar merch existente
        // --------------------------------------------------------------
        [HttpDelete("dto/mapper/{id}")]
        public async Task<IActionResult> DeleteMerchDtoMapper(int id)
        {
            var merch = await _merchService.GetMerchByIdAsync(id);
            if (merch == null)
                return NotFound("Producto no encontrado.");

            await _merchService.DeleteMerchAsync(merch);
            return NoContent();
        }

        #endregion
    }
}
