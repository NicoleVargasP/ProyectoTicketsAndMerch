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
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        private readonly IMapper _mapper;
        private readonly IValidationService _validationService;

        public PaymentController(
            IPaymentService paymentService,
            IMapper mapper,
            IValidationService validationService)
        {
            _paymentService = paymentService;
            _mapper = mapper;
            _validationService = validationService;
        }

        #region Dto Mapper con QueryFilter y paginación

        /// <summary>
        /// Recupera una lista paginada de pagos según filtros aplicados.
        /// </summary>
        /// <remarks>
        /// Convierte las entidades <see cref="Payment"/> en <see cref="PaymentDto"/> 
        /// y devuelve los resultados en formato paginado.
        /// </remarks>
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<PaymentDto>>))]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpGet("dto/mapper")]
        public async Task<IActionResult> GetPaymentsDtoMapper([FromQuery] PaymentQueryFilter paymentQueryFilter, int idAux)
        {
            try
            {
                var result = await _paymentService.GetAllPaymentsAsync(paymentQueryFilter);

                var paymentsDto = _mapper.Map<IEnumerable<PaymentDto>>(result.Pagination);

                var pagination = new Pagination
                {
                    TotalCount = result.Pagination.TotalCount,
                    PageSize = result.Pagination.PageSize,
                    CurrentPage = result.Pagination.CurrentPage,
                    TotalPages = result.Pagination.TotalPages,
                    HasNextPage = result.Pagination.HasNextPage,
                    HasPreviousPage = result.Pagination.HasPreviousPage
                };

                var response = new ApiResponse<IEnumerable<PaymentDto>>(paymentsDto)
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
        /// Recupera todos los pagos utilizando Dapper (consulta optimizada).
        /// </summary>
        [HttpGet("dto/dapper")]
        public async Task<IActionResult> GetPaymentsDtoDapper()
        {
            var payments = await _paymentService.GetAllPaymentsDapperAsync();
            var paymentsDto = _mapper.Map<IEnumerable<PaymentDto>>(payments);
            var response = new ApiResponse<IEnumerable<PaymentDto>>(paymentsDto);
            return Ok(response);
        }

        #endregion

        #region CRUD estándar con DTO Mapper

        [HttpGet("dto/mapper/{id}")]
        public async Task<IActionResult> GetPaymentDtoMapperId(int id)
        {
            var validation = await _validationService.ValidateAsync(new GetByIdRequest { Id = id });
            if (!validation.IsValid)
                return BadRequest(new { Errors = validation.Errors });

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

            try
            {
                var payment = _mapper.Map<Payment>(paymentDto);
                await _paymentService.AddPaymentAsync(payment);

                var response = new ApiResponse<Payment>(payment);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Error al registrar el pago", Error = ex.Message });
            }
        }

        [HttpPut("dto/mapper/{id}")]
        public async Task<IActionResult> UpdatePaymentDtoMapper(int id, [FromBody] PaymentDto paymentDto)
        {
            if (id != paymentDto.Id)
                return BadRequest("El Id del pago no coincide con el cuerpo de la solicitud.");

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

            await _paymentService.DeletePaymentAsync(id);
            return NoContent();
        }

        #endregion
    }
}
