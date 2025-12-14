using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Net;
using TicketsAndMerch.Api.Responses;
using TicketsAndMerch.Core.CustomEntities;
using TicketsAndMerch.Core.Interfaces;
using TicketsAndMerch.Infrastructure.DTOs;
using TicketsAndMerch.Infrastructure.Validators;

namespace TicketsAndMerch.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BuyMerchController : ControllerBase
    {
        private readonly IBuyMerchService _buyMerchService;
        private readonly IMapper _mapper;
        private readonly IValidationService _validationService;

        public BuyMerchController(
            IBuyMerchService buyMerchService,
            IMapper mapper,
            IValidationService validationService)
        {
            _buyMerchService = buyMerchService;
            _mapper = mapper;
            _validationService = validationService;
        }

        /// <summary>
        /// Permite registrar una compra de merchandising.
        /// </summary>
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<BuyMerchDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpPost("dto/mapper")]
        public async Task<IActionResult> BuyMerchDtoMapper([FromBody] BuyMerchDto buyMerchDto)
        {
            var validationResult = await _validationService.ValidateAsync(buyMerchDto);
            if (!validationResult.IsValid)
                return BadRequest(new { Errors = validationResult.Errors });

            try
            {
                var buyMerch = _mapper.Map<BuyMerch>(buyMerchDto);
                var result = await _buyMerchService.BuyMerchAsync(buyMerch);
                var resultDto = _mapper.Map<BuyMerchDto>(result);

                var response = new ApiResponse<BuyMerchDto>(resultDto)
                {
                    Messages = new[]
                    {
                        new Message
                        {
                            Type = "Information",
                            Description = "Compra de merchandising registrada correctamente. Se generó una orden con estado 'Pendiente'."
                        }
                    }
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                var response = new ResponseData
                {
                    Messages = new[] { new Message { Type = "Error", Description = ex.Message } }
                };
                return StatusCode(500, response);
            }
        }

       // [Authorize]
        [HttpGet("dto/dapper")]
        public async Task<IActionResult> GetAllBuyMerchDapper()
        {
            var buyMerch = await _buyMerchService.GetAllBuyMerchDapperAsync();
            var buyMerchDto = _mapper.Map<IEnumerable<BuyMerchDto>>(buyMerch);
            var response = new ApiResponse<IEnumerable<BuyMerchDto>>(buyMerchDto);
            return Ok(response);
        }

       // [Authorize]
        [HttpGet("dto/mapper/{id}")]
        public async Task<IActionResult> GetBuyMerchById(int id)
        {
            // Validación básica
            if (id <= 0) return BadRequest(new { Errors = "Id inválido" });

            var buyMerch = await _buyMerchService.GetBuyMerchByIdAsync(id);
            if (buyMerch == null) return NotFound("Compra no encontrada.");

            var buyMerchDto = _mapper.Map<BuyMerchDto>(buyMerch);
            var response = new ApiResponse<BuyMerchDto>(buyMerchDto);
            return Ok(response);
        }

        [HttpDelete("dto/mapper/{id}")]
        public async Task<IActionResult> DeleteBuyMerch(int id)
        {
            var buyMerch = await _buyMerchService.GetBuyMerchByIdAsync(id);
            if (buyMerch == null) return NotFound("Compra no encontrada.");

            await _buyMerchService.DeleteBuyMerchAsync(id);
            return NoContent();
        }
    }
}
