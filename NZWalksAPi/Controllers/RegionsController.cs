using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalksAPi.Data;
using NZWalksAPi.Models.Domain;
using NZWalksAPi.Models.DTO;
using NZWalksAPi.Repositories;

namespace NZWalksAPi.Controllers
{
    //https://localhost:1234/api/regions
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext dbContext;
        private readonly IMapper mapper;
        public IRegionRepository regionRepository;

        public RegionsController(NZWalksDbContext dbContext,IRegionRepository regionRepository,IMapper mapper)
        {
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            //Get data from database -Domain models
            var regionsDomain =await regionRepository.GetAllAsync();

            //Maps Domain Models to DTO's
            //var regionDto = new List<RegionDto>();
            //foreach (var regionDomain in regionsDomain)
            //{
            //    regionDto.Add(new RegionDto()
            //    {
            //        Id = regionDomain.Id,
            //        Name = regionDomain.Name,
            //        Code = regionDomain.Code,
            //        RegionImageUrl = regionDomain.RegionImageUrl,

            //    });
            //}

            //Maps Domain Models to DTO's using automapper
           var regionsDto= mapper.Map<List<RegionDto>>(regionsDomain);
            
            //Return DTOs
            return Ok(regionsDto);
        }

        //Get Single Region (get region by id)
        //Get:https://localhost:1234/api/regions/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            //First way
            //var region =dbContext.Regions.Find(id);

            //Second way
            var regionDomain =await regionRepository.GetByIdAsync(id);

            if (regionDomain == null)
            {
                return NotFound();
            }
            //Map or convert Domain Model Region to DTO Region
            //var regionDto = new RegionDto
            //{
            //    Id = regionDomain.Id,
            //    Name = regionDomain.Name,
            //    Code = regionDomain.Code,
            //    RegionImageUrl = regionDomain.RegionImageUrl,

            //};

            //using automapper for maping
            var regionsDto=mapper.Map<RegionDto>(regionDomain);
            //Return DTO back to client
            return Ok(regionsDto);
        }


        //Post to create new region
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            if(ModelState.IsValid)
            {

                //Map DTO to domain model
                //var regionDomainModel = new Region
                //{
                //    Code = addRegionRequestDto.Code,
                //    Name = addRegionRequestDto.Name,
                //    RegionImageUrl = addRegionRequestDto.RegionImageUrl,
                //};

                var regionDomainModel = mapper.Map<Region>(addRegionRequestDto);
                //Use Domain Model to create Region
                regionDomainModel = await regionRepository.CreateAsync(regionDomainModel);

                //Map Domain model back to dto
                //var regionDto = new RegionDto
                //{
                //    Id = regionDomainModel.Id,
                //    Name = regionDomainModel.Name,
                //    Code = regionDomainModel.Code,
                //    RegionImageUrl = regionDomainModel.RegionImageUrl,
                //};


                var regionDto = mapper.Map<RegionDto>(regionDomainModel);
                return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        //Update REgion put request
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id,[FromBody]UpdateRegionRequestDto updateRegionRequestDto)
        {
          if (ModelState.IsValid)
            {
                //map dto to domain model
                //var regionDomainModel = new Region
                //{
                //    Code = updateRegionRequestDto.Code,
                //    Name = updateRegionRequestDto.Name,
                //    RegionImageUrl = updateRegionRequestDto.RegionImageUrl,

                //};


                var regionDomainModel = mapper.Map<Region>(updateRegionRequestDto);

                //Check if region exists
                regionDomainModel = await regionRepository.UpdateAsync(id, regionDomainModel);
                if (regionDomainModel == null)
                {
                    return NotFound();
                }
                //Map Dto to domain model

                // regionDomainModel.Code = updateRegionRequestDto.Code;
                // regionDomainModel.Name = updateRegionRequestDto.Name;
                // regionDomainModel.RegionImageUrl = updateRegionRequestDto.RegionImageUrl;
                //await dbContext.SaveChangesAsync();

                //Convert Domain model to dto
                //var regionDto = new RegionDto
                //{
                //    Id= regionDomainModel.Id,
                //    Code = regionDomainModel.Code,
                //    Name = regionDomainModel.Name,
                //    RegionImageUrl = regionDomainModel.RegionImageUrl,
                //};
                var regionDto = mapper.Map<RegionDto>(regionDomainModel);
                return Ok(regionDto);
            }
          else { return BadRequest(ModelState); }


        }


        //Delete region
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
           var regionDomainModel=await regionRepository.DeleteAsync(id);

            if (regionDomainModel == null)
            {
                return NotFound();
            }

            //Delete Region
            // dbContext.Regions.Remove(regionDomainModel);
            //await dbContext.SaveChangesAsync();

            //Optional REturn the deleted region back
            //Map domain model to dto
            //var regionDto = new RegionDto
            //{
            //    Id = regionDomainModel.Id,
            //    Name = regionDomainModel.Name,
            //    Code = regionDomainModel.Code,
            //    RegionImageUrl = regionDomainModel.RegionImageUrl,
            //};

            var regionDto = mapper.Map<Region>(regionDomainModel);
            return Ok(regionDto);
        }
    }
}