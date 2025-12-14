using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TicketsAndMerch.Core.CustomEntities;
using TicketsAndMerch.Core.Interfaces;
using TicketsAndMerch.Infrastructure.DTOs;
using TicketsAndMerch.Core.QueryFilters;
using TicketsAndMerch.Api.Responses;

namespace TicketsAndMerch.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserOrderController : ControllerBase
    {
        private readonly IUserOrderService _userOrderService;
        private readonly IMapper _mapper;

        public UserOrderController(IUserOrderService userOrderService, IMapper mapper)
        {
            _userOrderService = userOrderService;
            _mapper = mapper;
        }

        /// <summary>
        /// Obtiene todas las órdenes de compras del usuario autenticado
        /// </summary>
      //  [Authorize]
        [HttpGet("user-orders")]
        public async Task<IActionResult> GetUserOrders([FromQuery] UserOrderQueryFilter filters)
        {
            // Obtener id del usuario autenticado
            var userId = int.Parse(User.Identity!.Name!); // asumir que Name tiene UserId

            var orders = await _userOrderService.GetUserOrdersAsync(userId, filters);

            var ordersDto = _mapper.Map<IEnumerable<UserOrderDto>>(orders);

            var response = new ApiResponse<IEnumerable<UserOrderDto>>(ordersDto);

            return Ok(response);
        }
    }
}
