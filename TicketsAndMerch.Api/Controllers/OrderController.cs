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
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;
        private readonly IValidationService _validationService;

        public OrderController(IOrderService orderService, IMapper mapper, IValidationService validationService)
        {
            _orderService = orderService;
            _mapper = mapper;
            _validationService = validationService;
        }

        #region Dto Mapper con QueryFilter y paginación

        /// <summary>
        /// Recupera una lista paginada de órdenes registradas según filtros.
        /// </summary>
        /// <remarks>
        /// Este método convierte las entidades <see cref="Order"/> en <see cref="OrderDto"/> 
        /// y devuelve la información paginada con metadatos.
        /// </remarks>
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<OrderDto>>))]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpGet("dto/mapper")]
        public async Task<IActionResult> GetOrdersDtoMapper([FromQuery] OrderQueryFilter orderQueryFilter, int idAux)
        {
            try
            {
                var result = await _orderService.GetAllOrdersAsync(orderQueryFilter);

                var ordersDto = _mapper.Map<IEnumerable<OrderDto>>(result.Pagination);

                var pagination = new Pagination
                {
                    TotalCount = result.Pagination.TotalCount,
                    PageSize = result.Pagination.PageSize,
                    CurrentPage = result.Pagination.CurrentPage,
                    TotalPages = result.Pagination.TotalPages,
                    HasNextPage = result.Pagination.HasNextPage,
                    HasPreviousPage = result.Pagination.HasPreviousPage
                };

                var response = new ApiResponse<IEnumerable<OrderDto>>(ordersDto)
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
        /// Recupera las órdenes usando Dapper (consulta optimizada).
        /// </summary>
        [HttpGet("dto/dapper")]
        public async Task<IActionResult> GetOrdersDtoDapper()
        {
            var orders = await _orderService.GetAllOrdersDapperAsync();
            var ordersDto = _mapper.Map<IEnumerable<OrderDto>>(orders);
            var response = new ApiResponse<IEnumerable<OrderDto>>(ordersDto);
            return Ok(response);
        }

        #endregion

        #region CRUD estándar con DTO Mapper

        [HttpGet("dto/mapper/{id}")]
        public async Task<IActionResult> GetOrderDtoById(int id)
        {
            var validation = await _validationService.ValidateAsync(new GetByIdRequest { Id = id });
            if (!validation.IsValid)
                return BadRequest(new { Errors = validation.Errors });

            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null)
                return NotFound("Orden no encontrada.");

            var orderDto = _mapper.Map<OrderDto>(order);
            var response = new ApiResponse<OrderDto>(orderDto);
            return Ok(response);
        }

        [HttpPost("dto/mapper")]
        public async Task<IActionResult> AddOrderDtoMapper([FromBody] OrderDto orderDto)
        {
            var validation = await _validationService.ValidateAsync(orderDto);
            if (!validation.IsValid)
                return BadRequest(new { Errors = validation.Errors });

            try
            {
                var order = _mapper.Map<Order>(orderDto);
                await _orderService.AddOrderAsync(order);
                var response = new ApiResponse<Order>(order);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Error al crear la orden", Error = ex.Message });
            }
        }

        [HttpPut("dto/mapper/{id}")]
        public async Task<IActionResult> UpdateOrderDtoMapper(int id, [FromBody] OrderDto orderDto)
        {
            if (id != orderDto.Id)
                return BadRequest("El Id de la orden no coincide.");

            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null)
                return NotFound("Orden no encontrada.");

            _mapper.Map(orderDto, order);
            await _orderService.UpdateOrderAsync(order);

            var response = new ApiResponse<Order>(order);
            return Ok(response);
        }

        [HttpDelete("dto/mapper/{id}")]
        public async Task<IActionResult> DeleteOrderDtoMapper(int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null)
                return NotFound("Orden no encontrada.");

            await _orderService.DeleteOrderAsync(id);
            return NoContent();
        }

        #endregion
    }
}
