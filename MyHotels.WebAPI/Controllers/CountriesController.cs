using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyHotels.WebAPI.Infrastructure;
using MyHotels.WebAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyHotels.WebAPI.Controllers
{
    [ApiController]
    // teraz ścieżkja zostanie DOGENEROWANA na podstawie pierwszej części nazwy kontrolera
    [Route("api/[controller]")]
    public class CountriesController : Controller
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<CountriesController> _logger;

        public CountriesController(IUnitOfWork uow, IMapper mapper, ILogger<CountriesController> logger)
        {
            this._uow = uow;
            this._mapper = mapper;
            this._logger = logger;
        }

        [HttpGet]
        //jakie statusy chcemy zwrócić
        // musimy zdefiniować czego klient może sie spodziewać w odpowiedzi 
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IList<CountryDto>>> GetCountries()
        {
            _logger.LogInformation($"{nameof(GetCountries)} called...");

            try
            {
                var countries = await _uow.Countries.GetAll();
                var results = _mapper.Map<IList<CountryDto>>(countries);

                return Ok(results);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"Something went wrong in {nameof(GetCountries)}");

                // jeżeli problem nie jest dostępny w postaci standardowej metody,
                // sami dodajemy info użytkownika:
                //return StatusCode(StatusCodes.Status500InternalServerError,
                //    "Internal server error, please try again later...");

                return Problem("Diffrent server error, please try again later OR FIX IT...");
            }

        }

        // GET ... api/countries/1
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CountryDto>> GetCountry(int id)
        {
            _logger.LogInformation($"{nameof(GetCountry)} called...");

            try
            {
                var country = await _uow.Countries.Get(c => c.Id == id, new List<string> { "Hotels" });

                if (country == null)
                {
                    return NotFound($"Ech... Not found country with id = {id}");
                }

                var result = _mapper.Map<CountryDto>(country);

                return Ok(result);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"Something went wrong in {nameof(GetCountry)}");

                //return StatusCode(StatusCodes.Status500InternalServerError,
                //    "Internal server error, please try again later...");

                return Problem("Internal server error, please try again later...");
            }
        }
    }
}
