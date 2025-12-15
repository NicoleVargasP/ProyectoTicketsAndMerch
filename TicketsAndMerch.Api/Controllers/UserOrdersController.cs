using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketsAndMerch.Core.Entities;
using TicketsAndMerch.Core.Interfaces;
using TicketsAndMerch.Infrastructure.DTOs;
using TicketsAndMerch.Core.QueryFilters;
using TicketsAndMerch.Api.Responses; // asegúrate de tener esta clase

namespace TicketsAndMerch.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserOrderController : ControllerBase
    {
        private readonly IUserOrderService _userOrderService;
        private readonly ISecurityRepository _securityRepository;
        private readonly IMapper _mapper;

        public UserOrderController(
            IUserOrderService userOrderService,
            ISecurityRepository securityRepository,
            IMapper mapper)
        {
            _userOrderService = userOrderService;
            _securityRepository = securityRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Obtiene todas las órdenes del usuario autenticado por login
        /// </summary>
        [Authorize]
        [HttpGet("user-orders")]
        public async Task<IActionResult> GetUserOrders([FromQuery] UserOrderQueryFilter filters)
        {
            var login = User.Claims.FirstOrDefault(c => c.Type == "Login")?.Value;

            if (string.IsNullOrEmpty(login))
                return Unauthorized("Token inválido");

           
            var orders = await _userOrderService.GetUserOrdersByLoginAsync(login, filters);

            var ordersDto = _mapper.Map<IEnumerable<UserOrderDto>>(orders);
            return Ok(new ApiResponse<IEnumerable<UserOrderDto>>(ordersDto));
        }
    }
}
