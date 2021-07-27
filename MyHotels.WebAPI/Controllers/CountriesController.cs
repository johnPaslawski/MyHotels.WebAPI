using AutoMapper;
using Domain;
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
            _logger.LogInformation($"janek: {nameof(GetCountries)} called...");

            try
            {
                var countries = await _uow.Countries.GetAll();
                var results = _mapper.Map<IList<CountryDto>>(countries);

                return Ok(results);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"janek: Something went wrong in {nameof(GetCountries)}");

                // jeżeli problem nie jest dostępny w postaci standardowej metody,
                // sami dodajemy info użytkownika:
                //return StatusCode(StatusCodes.Status500InternalServerError,
                //    "Internal server error, please try again later...");

                return Problem("janek: Diffrent server error, please try again later OR FIX IT...");
            }

        }

        // GET ... api/countries/1
        [HttpGet("{id:int}", Name = "GetCountry")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CountryDto>> GetCountry(int id)
        {
            _logger.LogInformation($"janek: {nameof(GetCountry)} called...");

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
                _logger.LogError(exception, $"janek: Something went wrong in {nameof(GetCountry)}");

                //return StatusCode(StatusCodes.Status500InternalServerError,
                //    "Internal server error, please try again later...");

                return Problem("janek: Internal server error, please try again later...");
            }
        }

        // POST ... api/countries
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateCountry([FromBody] CreateCountryDto countryDto)
        {
            _logger.LogInformation($"janek:  {nameof(CreateCountry)} called...");

            if (!ModelState.IsValid)
            {
                _logger.LogError($"janek: Invalid POST attempt in {nameof(CreateCountry)}");

                return BadRequest(ModelState);
            }

            try
            {
                var country = _mapper.Map<Country>(countryDto);
                await _uow.Countries.Add(country);
                await _uow.Save();

                return CreatedAtRoute("GetCountry", new { id = country.Id }, country);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"janek: Something went wrong in {nameof(CreateCountry)}");

                return Problem("janek: Internal server error, please try again later...");
            }
        }

        // PUT ... api/countries/1
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateCountry(int id, [FromBody] UpdateCountryDto countryDto)
        {
            _logger.LogInformation($"janek: {nameof(UpdateCountry)} called...");

            if (!ModelState.IsValid)
            {
                _logger.LogError($"janek: ech.. Invalid PUT attempt in {nameof(UpdateCountry)}");

                return BadRequest(ModelState);
            }

            try
            {
                //najpier trzeba miec obiekt ktory bedziemy aktualizowali, z bazy:
                var country = await _uow.Countries.Get(x => x.Id == id);
                //jeśli go nie ma:
                if (country == null)
                {
                    return BadRequest("Z jakiegoś powodu nie znaleziono obiektu o żądanym id");
                }

                //jeżeli mamy nasz obiekt pobrany z bazy i nasze Dto które zostało przkazane,
                //możemy te dwie rzeczy połączyć w całość, trochę inaczej niż ostatnio:

                _mapper.Map(countryDto, country);
                //musimy przekazać nowy stan tego obiektu: 
                _uow.Countries.Modify(country);
                await _uow.Save();

                return NoContent();
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"janek: Something went wrong in {nameof(UpdateCountry)}");

                return Problem("janek: Internal server error, please try again later...");
            }
        }

        
    }
}
