using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalksAPi.CustomActionFilters;
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
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddWalkRequestDto addWalkRequestDto)
        {
              //Map AddWalkRequestDto to Walk model
                var walkDomainModel = mapper.Map<Walk>(addWalkRequestDto);
                await walkRepository.CreateAsync(walkDomainModel);

                //Map domain model to DTO
                return Ok(mapper.Map<WalkDto>(walkDomainModel));

           

        }
       
        //Get walks
        //GET: /api/walks?filterOn=name&filterQuery=Track&sortBy=Name&isAscending=true&pageNo=1&pageSize=1000
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery,
            [FromQuery] string? sortBy, [FromQuery] bool? isAscending,
            [FromQuery] int PageNo = 1, [FromQuery] int PageSize=1000
            )
        {
            var walksDomainModel = await walkRepository.GetAllAsync(filterOn, filterQuery, sortBy, isAscending ?? true, PageNo ,PageSize);
            //Map Domain model to DTO

            return Ok(mapper.Map<List<WalkDto>>(walksDomainModel));
        }


        //Get Walk by Id
        //GET: /api/walks/id
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var walkDomainModel = await walkRepository.GetByIdAsync(id);
            if (walkDomainModel == null)
            {
                return NotFound();
            }
            //Map domain model to dto
            return Ok(mapper.Map<WalkDto>(walkDomainModel));
        }

        //Update walk by id
        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]

        public async Task<IActionResult> Update([FromRoute] Guid id,UpdateWalkRequestDto updateWalkRequestDto )
        {
      
                //Map DTO to Domain Model
                var walkDomainModel = mapper.Map<Walk>(updateWalkRequestDto);

                walkDomainModel = await walkRepository.UpdateAsync(id, walkDomainModel);

                if (walkDomainModel == null)
                {
                    return NotFound();
                }
                //Map domain model to dto
                return Ok(mapper.Map<WalkDto>(walkDomainModel));
           
        }
        //Delete walk by id
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var deletedWalkDomainModel=await walkRepository.DeleteAsync(id);
            if(deletedWalkDomainModel == null)
            { 
                return NotFound(); 
            }
            //map domain model to dto
            return Ok(mapper.Map<WalkDto>(deletedWalkDomainModel));
        }
    }
}
