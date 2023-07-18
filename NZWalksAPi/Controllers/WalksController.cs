﻿using AutoMapper;
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
            if (ModelState.IsValid)
            {     //Map AddWalkRequestDto to Walk model
                var walkDomainModel = mapper.Map<Walk>(addWalkRequestDto);
                await walkRepository.CreateAsync(walkDomainModel);

                //Map domain model to DTO
                return Ok(mapper.Map<WalkDto>(walkDomainModel));

            }
            else
            {
                return BadRequest(ModelState);
            }

        }
       
        //Get walks
        //GET: /api/walks
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var walksDomainModel = await walkRepository.GetAllAsync();
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

        public async Task<IActionResult> Update([FromRoute] Guid id,UpdateWalkRequestDto updateWalkRequestDto )
        {
            if (ModelState.IsValid)
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
            else
            {
                return BadRequest(ModelState);
            }
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
