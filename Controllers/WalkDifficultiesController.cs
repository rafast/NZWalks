using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.Models.Domain;
using NZWalks.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NZWalks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class WalkDifficultiesController : ControllerBase
    {
        private readonly IWalkDifficultyRepository _walkDifficultyRepository;
        private readonly IMapper mapper;

        public WalkDifficultiesController(IWalkDifficultyRepository walkDifficultyRepository, IMapper mapper)
        {
            this._walkDifficultyRepository = walkDifficultyRepository;
            this.mapper = mapper;
        }

        // GET: api/<WalkDifficultiesController>
        [HttpGet]
        public async Task<IActionResult> GetAllWalkDifficultiesAsync()
        {
            var walkDifficultiesDomain = await _walkDifficultyRepository.GetAllAsync();

            var walkDifficultiesDto = mapper.Map<List<Models.DTOs.WalkDifficulty>>(walkDifficultiesDomain);

            return Ok(walkDifficultiesDto);
        }

        // GET api/<WalkDifficultiesController>/5
        [HttpGet("{id:guid}")]
        [ActionName("GetWalkDifficultyAsync")]
        public async Task<IActionResult> GetWalkDifficultyAsync(Guid id)
        {
            var walkDifficultyDomain = await _walkDifficultyRepository.GetAsync(id);

            if (walkDifficultyDomain == null)
            {
                return NotFound();
            }

            var walkDifficultyDto = mapper.Map<Models.DTOs.WalkDifficulty>(walkDifficultyDomain);
            return Ok(walkDifficultyDto);
        }

        // POST api/<WalkDifficultiesController>
        [HttpPost]
        public async Task<IActionResult> AddWalkDifficultyAsync([FromBody] Models.DTOs.AddWalkDifficultyRequest addWalkDifficultyRequest)
        {
            // validate incoming request
            if (!ValidateAddWalkDifficultyAsync(addWalkDifficultyRequest))
            {
                return BadRequest(ModelState);
            }
            var walkDifficultyDomain = mapper.Map<WalkDifficulty>(addWalkDifficultyRequest);

            walkDifficultyDomain = await _walkDifficultyRepository.AddAsync(walkDifficultyDomain);

            var walkDifficultyDto = mapper.Map<Models.DTOs.WalkDifficulty>(walkDifficultyDomain);

            return CreatedAtAction(nameof(GetWalkDifficultyAsync), new { id = walkDifficultyDto.Id }, walkDifficultyDto);
        }

        // PUT api/<WalkDifficultiesController>/5
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateWalkDifficultyAsync(Guid id, [FromBody] Models.DTOs.UpdateWalkDifficultyRequest updateWalkDifficultyRequest)
        {
            // Validate the incoming request
            if (!ValidateUpdateWalkDifficultyAsync(updateWalkDifficultyRequest))
            {
                return BadRequest(ModelState);
            }

            var walkDifficultyDomain = mapper.Map<WalkDifficulty>(updateWalkDifficultyRequest);
            walkDifficultyDomain = await _walkDifficultyRepository.UpdateAsync(id, walkDifficultyDomain);

            if (walkDifficultyDomain == null)
            {
                return NotFound();
            }

            var walkDifficultyDto = mapper.Map<Models.DTOs.WalkDifficulty>(walkDifficultyDomain);
            return Ok(walkDifficultyDto);
        }

        // DELETE api/<WalkDifficultiesController>/5
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteWalkDifficultyAsync(Guid id)
        {
            var walkDifficultyDomain = await _walkDifficultyRepository.DeleteAsync(id);

            if (walkDifficultyDomain == null)
            {
                return NotFound();
            }

            var walkDifficultyDto = mapper.Map<Models.DTOs.WalkDifficulty>(walkDifficultyDomain);
            return Ok(walkDifficultyDto);
        }

        #region Private methods

        private bool ValidateAddWalkDifficultyAsync(Models.DTOs.AddWalkDifficultyRequest addWalkDifficultyRequest)
        {
            if (addWalkDifficultyRequest == null)
            {
                ModelState.AddModelError(nameof(addWalkDifficultyRequest),
                    $"{nameof(addWalkDifficultyRequest)} is required.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(addWalkDifficultyRequest.Code))
            {
                ModelState.AddModelError(nameof(addWalkDifficultyRequest.Code),
                    $"{nameof(addWalkDifficultyRequest.Code)} is required.");
            }

            if (ModelState.ErrorCount > 0)
            {
                return false;
            }

            return true;
        }


        private bool ValidateUpdateWalkDifficultyAsync(Models.DTOs.UpdateWalkDifficultyRequest updateWalkDifficultyRequest)
        {
            if (updateWalkDifficultyRequest == null)
            {
                ModelState.AddModelError(nameof(updateWalkDifficultyRequest),
                    $"{nameof(updateWalkDifficultyRequest)} is required.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(updateWalkDifficultyRequest.Code))
            {
                ModelState.AddModelError(nameof(updateWalkDifficultyRequest.Code),
                    $"{nameof(updateWalkDifficultyRequest.Code)} is required.");
            }

            if (ModelState.ErrorCount > 0)
            {
                return false;
            }

            return true;
        }
        #endregion
    }
}
