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
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IValidationService _validationService;

        public UserController(
            IUserService userService,
            IMapper mapper,
            IValidationService validationService)
        {
            _userService = userService;
            _mapper = mapper;
            _validationService = validationService;
        }

        #region Dto Mapper con QueryFilter y Paginación

        /// <summary>
        /// Recupera una lista paginada de usuarios aplicando filtros.
        /// </summary>
        /// <remarks>
        /// Permite filtrar por nombre, rol, email o rango de fecha de registro.
        /// Devuelve los resultados paginados junto con mensajes de estado.
        /// </remarks>
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<UserDto>>))]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpGet("dto/mapper")]
        public async Task<IActionResult> GetUsersDtoMapper([FromQuery] UserQueryFilter userQueryFilter, int idAux)
        {
            try
            {
                var result = await _userService.GetAllUsersAsync(userQueryFilter);

                var usersDto = _mapper.Map<IEnumerable<UserDto>>(result.Pagination);

                var pagination = new Pagination
                {
                    TotalCount = result.Pagination.TotalCount,
                    PageSize = result.Pagination.PageSize,
                    CurrentPage = result.Pagination.CurrentPage,
                    TotalPages = result.Pagination.TotalPages,
                    HasNextPage = result.Pagination.HasNextPage,
                    HasPreviousPage = result.Pagination.HasPreviousPage
                };

                var response = new ApiResponse<IEnumerable<UserDto>>(usersDto)
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
        /// Recupera todos los usuarios mediante Dapper (versión optimizada de lectura).
        /// </summary>
        [HttpGet("dto/dapper")]
        public async Task<IActionResult> GetUsersDtoDapper()
        {
            var users = await _userService.GetAllUsersDapperAsync();
            var usersDto = _mapper.Map<IEnumerable<UserDto>>(users);
            var response = new ApiResponse<IEnumerable<UserDto>>(usersDto);
            return Ok(response);
        }

        #endregion

        #region CRUD estándar con DTO Mapper

        [HttpGet("dto/mapper/{id}")]
        public async Task<IActionResult> GetUserDtoMapperId(int id)
        {
            var validation = await _validationService.ValidateAsync(new GetByIdRequest { Id = id });
            if (!validation.IsValid)
                return BadRequest(new { Errors = validation.Errors });

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

            try
            {
                var user = _mapper.Map<User>(userDto);
                await _userService.AddUserAsync(user);
                var response = new ApiResponse<User>(user);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Error al crear el usuario", Error = ex.Message });
            }
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

            await _userService.DeleteUserAsync(id);
            return NoContent();
        }

        #endregion

        #region Registro principal

        /// <summary>
        /// Registra un nuevo usuario en el sistema validando correo y contraseña.
        /// </summary>
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<User>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserDto userDto)
        {
            var validation = await _validationService.ValidateAsync(userDto);
            if (!validation.IsValid)
                return BadRequest(new { Errors = validation.Errors });

            try
            {
                var user = _mapper.Map<User>(userDto);
                var result = await _userService.AddUserAsync(user);
                return Ok(new ApiResponse<User>(result));
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        #endregion
    }
}
