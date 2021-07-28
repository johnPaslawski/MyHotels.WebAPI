using AutoMapper;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyHotels.WebAPI.Controllers;
using MyHotels.WebAPI.Infrastructure;
using MyHotels.WebAPI.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyHotels.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<CountriesController> _logger;

        public HotelsController(IUnitOfWork uow, IMapper mapper, ILogger<CountriesController> logger)
        {
            _uow = uow;
            _mapper = mapper;
            _logger = logger;
        }

        // GET ... api/hotels
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IList<HotelDto>>> GetHotels()
        {
            _logger.LogInformation($"{nameof(GetHotels)} called...");

            try
            {
                var hotels = await _uow.Hotels.GetAll();
                var results = _mapper.Map<IList<HotelDto>>(hotels);

                return Ok(results);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"Coś poszło nie tak w: {nameof(GetHotels)}");

                //return StatusCode(StatusCodes.Status500InternalServerError,
                //    "Internal server error, please try again later...");

                return Problem("Błąd, internal server 500 error.");
            }
        }

        // GET ... api/hotels/1
        [HttpGet("{id:int}", Name = "GetHotel")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CountryDto>> GetHotel(int id)
        {
            _logger.LogInformation($"{nameof(GetHotel)} called...");

            try
            {
                var hotel = await _uow.Hotels.Get(c => c.Id == id, new List<string> { "Country" });

                if (hotel == null)
                {
                    return NotFound($" Nie znaleziono hotelu id = {id}");
                }

                var result = _mapper.Map<HotelDto>(hotel);

                return Ok(result);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"Coś poszło nie tak w: {nameof(GetHotel)}");

                return Problem("Błąd, internal server 500 error.");
            }
        }


        // POST ... api/hotels
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateHotel([FromBody] CreateHotelDto hotelDto)
        {
            _logger.LogInformation($"{nameof(CreateHotel)} called...");

            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid POST attempt in {nameof(CreateHotel)}");

                return BadRequest(ModelState);
            }

            try
            {
                var hotel = _mapper.Map<Hotel>(hotelDto);
                await _uow.Hotels.Add(hotel);
                await _uow.Save();

                return CreatedAtRoute("GetCountry", new { id = hotel.Id }, hotel);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"Something went wrong in {nameof(CreateHotel)}");

                return Problem("Internal server error, please try again later...");
            }
        }
    }
}
