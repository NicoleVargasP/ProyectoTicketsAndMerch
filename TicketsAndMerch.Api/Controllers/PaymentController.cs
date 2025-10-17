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
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        private readonly IMapper _mapper;
        private readonly IValidationService _validationService;

        public PaymentController(IPaymentService paymentService, IMapper mapper, IValidationService validationService)
        {
            _paymentService = paymentService;
            _mapper = mapper;
            _validationService = validationService;
        }

        #region Dto Mapper
        [HttpGet("dto/mapper")]
        public async Task<IActionResult> GetPaymentsDtoMapper()
        {
            var payments = await _paymentService.GetAllPaymentsAsync();
            var paymentsDto = _mapper.Map<IEnumerable<PaymentDto>>(payments);
            var response = new ApiResponse<IEnumerable<PaymentDto>>(paymentsDto);
            return Ok(response);
        }

        [HttpGet("dto/mapper/{id}")]
        public async Task<IActionResult> GetPaymentDtoMapperId(int id)
        {
            var payment = await _paymentService.GetPaymentByIdAsync(id);
            if (payment == null)
                return NotFound("Pago no encontrado.");

            var paymentDto = _mapper.Map<PaymentDto>(payment);
            var response = new ApiResponse<PaymentDto>(paymentDto);
            return Ok(response);
        }

        [HttpPost("dto/mapper")]
        public async Task<IActionResult> AddPaymentDtoMapper([FromBody] PaymentDto paymentDto)
        {
            var validation = await _validationService.ValidateAsync(paymentDto);
            if (!validation.IsValid)
                return BadRequest(new { Errors = validation.Errors });

            var payment = _mapper.Map<Payment>(paymentDto);
            await _paymentService.AddPaymentAsync(payment);
            var response = new ApiResponse<Payment>(payment);
            return Ok(response);
        }

        [HttpPut("dto/mapper/{id}")]
        public async Task<IActionResult> UpdatePaymentDtoMapper(int id, [FromBody] PaymentDto paymentDto)
        {
            if (id != paymentDto.PaymentId)
                return BadRequest("El Id del pago no coincide.");

            var payment = await _paymentService.GetPaymentByIdAsync(id);
            if (payment == null)
                return NotFound("Pago no encontrado.");

            _mapper.Map(paymentDto, payment);
            await _paymentService.UpdatePaymentAsync(payment);
            var response = new ApiResponse<Payment>(payment);
            return Ok(response);
        }

        [HttpDelete("dto/mapper/{id}")]
        public async Task<IActionResult> DeletePaymentDtoMapper(int id)
        {
            var payment = await _paymentService.GetPaymentByIdAsync(id);
            if (payment == null)
                return NotFound("Pago no encontrado.");

            await _paymentService.DeletePaymentAsync(payment);
            return NoContent();
        }
        #endregion
    }
}
