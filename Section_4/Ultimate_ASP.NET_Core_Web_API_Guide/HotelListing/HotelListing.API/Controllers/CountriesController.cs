using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelListing.API.Data;
using HotelListing.API.DTOs;
using AutoMapper;
using HotelListing.API.Contracts;

namespace HotelListing.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly ICountriesRepository _repository;
        private readonly IMapper _mapper;
        public CountriesController(ICountriesRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // GET: api/Countries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CountryGetSimpleDto>>> GetCountries()
        {
            var countries = await _repository.GetAllAsync();
            var records = _mapper.Map<List<CountryGetSimpleDto>>(countries.ToList());
            return records;
        }

        // GET: api/Countries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CountryGetDetailedDto>> GetCountry(int id)
        {
            var country = await _repository.GetDetailsAsync(id);

            if (country == null)
            {
                return NotFound();
            }

            var countryDto = _mapper.Map<CountryGetDetailedDto>(country);

            return countryDto;
        }

        // PUT: api/Countries/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCountry(int id, CountryPutDto countryDto)
        {
            if (id != countryDto.Id)
            {
                return BadRequest();
            }

            //_context.Entry(country).State = EntityState.Modified;

            var country = await _repository.GetAsync(id);

            if (country == null)
            {
                return NotFound();
            }

            // other way to use mapper; updates existing entity
            //_mapper.Map(countryDto, country);

            var countryToUpdate = _mapper.Map<Country>(countryDto);

            //??
            try
            {
                await _repository.UpdateAsync(countryToUpdate);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await CountryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Countries
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Country>> PostCountry(CountryPostDto countryDto)
        {
            var country = _mapper.Map<Country>(countryDto);

            await _repository.AddAsync(country);

            return CreatedAtAction("GetCountry", new { id = country.Id }, country);
        }

        // DELETE: api/Countries/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCountry(int id)
        {
            await _repository.DeleteAsync(id);

            return NoContent();
        }

        private async Task<bool> CountryExists(int id)
        {
            return await _repository.Exists(id);
        }
    }
}
