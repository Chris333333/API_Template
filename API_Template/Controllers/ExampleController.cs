using API_Template.Controllers.Base;
using App.Spec;
using AutoMapper;
using Data.DTO;
using Data.Entities.DatabaseDB;
using Data.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API_Template.Controllers
{
    public class ExampleController(IMapper mapper, IGenericRepository<MockEntity> mockEntityGenericRepository) : BaseMappingApiController(mapper)
    {
        private readonly IGenericRepository<MockEntity> _mockEntityGenericRepository = mockEntityGenericRepository;

        /// <summary>
        /// Get an example entity by ID.
        /// This endpoint retrieves a single example entity based on the provided ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("exampleID/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MockEntityDTO>> GetExampleByID(int id)
        {
            var spec = new MockEntitySpec(id);
            return await GetMappedEntityWithSpec<MockEntity, MockEntityDTO>(_mockEntityGenericRepository, spec);
        }

        /// <summary>
        /// Get a paginated list of example entities.
        /// This endpoint retrieves a list of example entities based on the provided pagination parameters.
        /// </summary>
        /// <param name="specParams"></param>
        /// <returns></returns>
        [HttpGet("examplePaged")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetExamplePaged([FromQuery] BaseSpecParams specParams)
        {
            var spec = new MockEntitySpec(specParams);
            return await CreateMappedPagedResult<MockEntity, MockEntityDTO>(_mockEntityGenericRepository, spec, specParams);
        }
    }
}