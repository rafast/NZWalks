using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.Models.Domain;
using NZWalks.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NZWalks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IWalkRepository _walkRepository;
        private readonly IMapper mapper;

        public WalksController(IWalkRepository walkRepository, IMapper mapper)
        {
            _walkRepository = walkRepository;
            this.mapper = mapper;
        }

        // GET: api/<WalksController>
        [HttpGet]
        public async Task<IActionResult> GetAllWalksAsync()
        {
            var walksDomain = await _walkRepository.GetAllAsync();

            var walksDto = mapper.Map<List<Models.DTOs.Walk>>(walksDomain);

            return Ok(walksDto);
        }

        // GET api/<WalksController>/5
        [HttpGet("{id:guid}")]
        [ActionName("GetWalkAsync")]
        public async Task<IActionResult> GetWalkAsync(Guid id)
        {
            var walkDomain = await _walkRepository.GetAsync(id);

            if (walkDomain == null)
            {
                return NotFound();
            }

            var walkDto = mapper.Map<Models.DTOs.Walk>(walkDomain);
            return Ok(walkDto);
        }

        // POST api/<WalksController>
        [HttpPost]
        public async Task<IActionResult> AddWalkAsync([FromBody] Models.DTOs.AddWalkRequest addWalkRequest)
        {
            var walkDomain = mapper.Map<Walk>(addWalkRequest);

            walkDomain = await _walkRepository.AddAsync(walkDomain);

            var walkDto = mapper.Map<Models.DTOs.Walk>(walkDomain);

            return CreatedAtAction(nameof(GetWalkAsync), new { id = walkDto.Id }, walkDto);
        }

        // PUT api/<WalksController>/5
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateWalkAsync(Guid id, [FromBody] Models.DTOs.UpdateWalkRequest updateWalkRequest)
        {
            var walkDomain = mapper.Map<Walk>(updateWalkRequest);
            walkDomain = await _walkRepository.UpdateAsync(id, walkDomain);

            if (walkDomain == null)
            {
                return NotFound();
            }

            var walkDto = mapper.Map<Models.DTOs.Walk>(walkDomain);
            return Ok(walkDto);
        }

        // DELETE api/<WalksController>/5
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteWalkAsync(Guid id)
        {
            var walkDomain = await _walkRepository.DeleteAsync(id);

            if (walkDomain == null)
            {
                return NotFound();
            }

            var walkDto = mapper.Map<Models.DTOs.Walk>(walkDomain);
            return Ok(walkDto);
        }
    }
}
