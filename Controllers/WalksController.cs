using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.Models.Domain;
using NZWalks.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NZWalks.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class WalksController : ControllerBase
    {
        private readonly IWalkRepository _walkRepository;
        private readonly IRegionRepository _regionRepository;
        private readonly IWalkDifficultyRepository _walkDifficultyRepository;
        private readonly IMapper mapper;

        public WalksController(IWalkRepository walkRepository, IMapper mapper, IRegionRepository regionRepository, IWalkDifficultyRepository walkDifficultyRepository)
        {
            _walkRepository = walkRepository;
            _regionRepository = regionRepository;
            _walkDifficultyRepository = walkDifficultyRepository;
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
            // Validate the incoming request
            if (!(await ValidateAddWalkAsync(addWalkRequest)))
            {
                return BadRequest(ModelState);
            }
            var walkDomain = mapper.Map<Walk>(addWalkRequest);

            walkDomain = await _walkRepository.AddAsync(walkDomain);

            var walkDto = mapper.Map<Models.DTOs.Walk>(walkDomain);

            return CreatedAtAction(nameof(GetWalkAsync), new { id = walkDto.Id }, walkDto);
        }

        // PUT api/<WalksController>/5
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateWalkAsync(Guid id, [FromBody] Models.DTOs.UpdateWalkRequest updateWalkRequest)
        {
            // Validate the incoming request
            if (!(await ValidateUpdateWalkAsync(updateWalkRequest)))
            {
                return BadRequest(ModelState);
            }
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

        #region Private methods

        private async Task<bool> ValidateAddWalkAsync(Models.DTOs.AddWalkRequest addWalkRequest)
        {
            //if (addWalkRequest == null)
            //{
            //    ModelState.AddModelError(nameof(addWalkRequest),
            //        $"{nameof(addWalkRequest)} cannot be empty.");
            //    return false;
            //}

            //if (string.IsNullOrWhiteSpace(addWalkRequest.Name))
            //{
            //    ModelState.AddModelError(nameof(addWalkRequest.Name),
            //        $"{nameof(addWalkRequest.Name)} is required.");
            //}

            //if (addWalkRequest.Length <= 0)
            //{
            //    ModelState.AddModelError(nameof(addWalkRequest.Length),
            //        $"{nameof(addWalkRequest.Length)} should be greater than zero.");
            //}

            var region = await _regionRepository.GetAsync(addWalkRequest.RegionId);
            if (region == null)
            {
                ModelState.AddModelError(nameof(addWalkRequest.RegionId),
                    $"{nameof(addWalkRequest.RegionId)} is invalid.");
            }

            var walkDifficulty = await _walkDifficultyRepository.GetAsync(addWalkRequest.WalkDifficultyId);
            if (walkDifficulty == null)
            {
                ModelState.AddModelError(nameof(addWalkRequest.WalkDifficultyId),
                       $"{nameof(addWalkRequest.WalkDifficultyId)} is invalid.");

            }

            if (ModelState.ErrorCount > 0)
            {
                return false;
            }

            return true;
        }

        private async Task<bool> ValidateUpdateWalkAsync(Models.DTOs.UpdateWalkRequest updateWalkRequest)
        {
            //if (updateWalkRequest == null)
            //{
            //    ModelState.AddModelError(nameof(updateWalkRequest),
            //        $"{nameof(updateWalkRequest)} cannot be empty.");
            //    return false;
            //}

            //if (string.IsNullOrWhiteSpace(updateWalkRequest.Name))
            //{
            //    ModelState.AddModelError(nameof(updateWalkRequest.Name),
            //        $"{nameof(updateWalkRequest.Name)} is required.");
            //}

            //if (updateWalkRequest.Length <= 0)
            //{
            //    ModelState.AddModelError(nameof(updateWalkRequest.Length),
            //        $"{nameof(updateWalkRequest.Length)} should be greater than zero.");
            //}

            var region = await _regionRepository.GetAsync(updateWalkRequest.RegionId);
            if (region == null)
            {
                ModelState.AddModelError(nameof(updateWalkRequest.RegionId),
                    $"{nameof(updateWalkRequest.RegionId)} is invalid.");
            }

            var walkDifficulty = await _walkDifficultyRepository.GetAsync(updateWalkRequest.WalkDifficultyId);
            if (walkDifficulty == null)
            {
                ModelState.AddModelError(nameof(updateWalkRequest.WalkDifficultyId),
                       $"{nameof(updateWalkRequest.WalkDifficultyId)} is invalid.");

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
