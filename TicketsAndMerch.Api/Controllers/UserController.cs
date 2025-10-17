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
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IValidationService _validationService;

        public UserController(IUserService userService, IMapper mapper, IValidationService validationService)
        {
            _userService = userService;
            _mapper = mapper;
            _validationService = validationService;
        }

        #region Dto Mapper
        [HttpGet("dto/mapper")]
        public async Task<IActionResult> GetUsersDtoMapper()
        {
            var users = await _userService.GetAllUsersAsync();
            var usersDto = _mapper.Map<IEnumerable<UserDto>>(users);
            var response = new ApiResponse<IEnumerable<UserDto>>(usersDto);
            return Ok(response);
        }

        [HttpGet("dto/mapper/{id}")]
        public async Task<IActionResult> GetUserDtoMapperId(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
                return NotFound("Usuario no encontrado.");

            var userDto = _mapper.Map<UserDto>(user);
            var response = new ApiResponse<UserDto>(userDto);
            return Ok(response);
        }

        [HttpPost("dto/mapper")]
        public async Task<IActionResult> AddUserDtoMapper([FromBody] UserDto userDto)
        {
            var validation = await _validationService.ValidateAsync(userDto);
            if (!validation.IsValid)
                return BadRequest(new { Errors = validation.Errors });

            var user = _mapper.Map<User>(userDto);
            await _userService.AddUserAsync(user);
            var response = new ApiResponse<User>(user);
            return Ok(response);
        }

        [HttpPut("dto/mapper/{id}")]
        public async Task<IActionResult> UpdateUserDtoMapper(int id, [FromBody] UserDto userDto)
        {
            if (id != userDto.UserId)
                return BadRequest("El Id del usuario no coincide.");

            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
                return NotFound("Usuario no encontrado.");

            _mapper.Map(userDto, user);
            await _userService.UpdateUserAsync(user);
            var response = new ApiResponse<User>(user);
            return Ok(response);
        }

        [HttpDelete("dto/mapper/{id}")]
        public async Task<IActionResult> DeleteUserDtoMapper(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
                return NotFound("Usuario no encontrado.");

            await _userService.DeleteUserAsync(user);
            return NoContent();
        }
        #endregion
        // REGISTRO DE USUARIO (caso de uso)
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            try
            {
                var newUser = await _userService.AddUserAsync(user);
                var response = new ApiResponse<User>(newUser);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }
    }

}
