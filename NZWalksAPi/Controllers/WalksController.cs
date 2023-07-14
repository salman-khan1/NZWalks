using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalksAPi.Models.Domain;
using NZWalksAPi.Models.DTO;
using NZWalksAPi.Repositories;

namespace NZWalksAPi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IWalkRepository walkRepository;

        public WalksController(IMapper mapper,IWalkRepository walkRepository)
        {
            this.mapper=mapper;
            this.walkRepository = walkRepository;
        }
        //CREATE WALK
        //POST://API/WALKS
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddWalkRequestDto addWalkRequestDto)
        {
            //Map AddWalkRequestDto to Walk model
            var walkDomainModel = mapper.Map<Walk>(addWalkRequestDto);
            await walkRepository.CreateAsync(walkDomainModel);

            //Map domain model to DTO
            return Ok(mapper.Map<WalkDto>(walkDomainModel));

        }
    }
}
