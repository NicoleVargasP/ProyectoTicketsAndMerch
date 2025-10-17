using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TicketsAndMerch.Api.Responses;
using TicketsAndMerch.Core.Entities;
using TicketsAndMerch.Core.CustomEntities;
using TicketsAndMerch.Core.Interfaces;
using TicketsAndMerch.Infrastructure.DTOs;
using TicketsAndMerch.Infrastructure.Validators;

namespace TicketsAndMerch.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConcertController : ControllerBase
    {
        private readonly IConcertService _concertService;
        private readonly IMapper _mapper;
        private readonly IValidationService _validationService;

        public ConcertController(
            IConcertService concertService,
            IMapper mapper,
            IValidationService validationService)
        {
            _concertService = concertService;
            _mapper = mapper;
            _validationService = validationService;
        }

       
        [HttpGet("dto/mapper")]
        public async Task<IActionResult> GetAllConcerts()
        {
            var concerts = await _concertService.GetAllConcertsAsync();
            var concertsDto = _mapper.Map<IEnumerable<ConcertDto>>(concerts);
            var response = new ApiResponse<IEnumerable<ConcertDto>>(concertsDto);

            return Ok(response);
        }

        
        [HttpGet("dto/mapper/{id}")]
        public async Task<IActionResult> GetConcertById(int id)
        {
          
            var validation = await _validationService.ValidateAsync(new GetByIdRequest { Id = id });
            if (!validation.IsValid)
                return BadRequest(new { Errors = validation.Errors });

            var concert = await _concertService.GetConcertByIdAsync(id);
            if (concert == null)
                return NotFound("Concierto no encontrado.");

            var concertDto = _mapper.Map<ConcertDto>(concert);
            var response = new ApiResponse<ConcertDto>(concertDto);

            return Ok(response);
        }

      
        [HttpPost("dto/mapper")]
        public async Task<IActionResult> CreateConcert([FromBody] ConcertDto concertDto)
        {
           
            var validation = await _validationService.ValidateAsync(concertDto);
            if (!validation.IsValid)
                return BadRequest(new { Errors = validation.Errors });

            var concert = _mapper.Map<Concert>(concertDto);
            await _concertService.AddConcertAsync(concert);

            var response = new ApiResponse<Concert>(concert);
            return Ok(response);
        }

        
        [HttpPut("dto/mapper/{id}")]
        public async Task<IActionResult> UpdateConcert(int id, [FromBody] ConcertDto concertDto)
        {
            if (id != concertDto.ConcertId)
                return BadRequest("El ID no coincide.");

            var existingConcert = await _concertService.GetConcertByIdAsync(id);
            if (existingConcert == null)
                return NotFound("Concierto no encontrado.");

            _mapper.Map(concertDto, existingConcert);
            await _concertService.UpdateConcertAsync(existingConcert);

            var response = new ApiResponse<Concert>(existingConcert);
            return Ok(response);
        }

    
        [HttpDelete("dto/mapper/{id}")]
        public async Task<IActionResult> DeleteConcert(int id)
        {
            var concert = await _concertService.GetConcertByIdAsync(id);
            if (concert == null)
                return NotFound("Concierto no encontrado.");

            await _concertService.DeleteConcertAsync(concert);
            return NoContent();
        }
    }
}
