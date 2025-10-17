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

        #region Dto Mapper
        [HttpGet("dto/mapper")]
        public async Task<IActionResult> GetAllOrdersDtoMapper()
        {
            var orders = await _orderService.GetAllOrdersAsync();
            var ordersDto = _mapper.Map<IEnumerable<OrderDto>>(orders);
            var response = new ApiResponse<IEnumerable<OrderDto>>(ordersDto);
            return Ok(response);
        }

        [HttpGet("dto/mapper/{id}")]
        public async Task<IActionResult> GetOrderDtoMapperId(int id)
        {
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

            var order = _mapper.Map<Order>(orderDto);
            await _orderService.AddOrderAsync(order);
            var response = new ApiResponse<Order>(order);
            return Ok(response);
        }

        [HttpPut("dto/mapper/{id}")]
        public async Task<IActionResult> UpdateOrderDtoMapper(int id, [FromBody] OrderDto orderDto)
        {
            if (id != orderDto.OrderId)
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

            await _orderService.DeleteOrderAsync(order);
            return NoContent();
        }
        #endregion
    }
}
