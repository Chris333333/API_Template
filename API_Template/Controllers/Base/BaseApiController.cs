using App.Helpers;
using App.Spec;
using Data.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API_Template.Controllers.Base
{
    /// <summary>
    /// Base controller not to repeat in each controller just to derive from Controller class
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class BaseApiController : ControllerBase
    {

        protected async Task<ActionResult> CreatePagedResult<T>(IGenericRepository<T> repository, ISpecification<T> spec, BaseSpecParams pagination) where T : class
        {
            var totalItems = await repository.CountAsync(spec);
            var data = await repository.ListAllWithSpecAsync(spec);
            var result = new Pagination<T>(pagination.PageIndex, pagination.PageSize, totalItems, data);
            return Ok(result);
        }

        protected async Task<ActionResult<T>> GetEntity<T>(IGenericRepository<T> repository, int id) where T : class
        {
            var entity = await repository.GetByIdAsync(id);
            if (entity == null) return NotFound();
            return entity;
        }

        protected async Task<ActionResult<IReadOnlyList<T>>> GetAllEntities<T>(IGenericRepository<T> repository) where T : class
        {
            var entities = await repository.ListAllAsync();
            return Ok(entities);
        }

        protected async Task<ActionResult<T>> DeleteEntity<T>(IGenericRepository<T> repository, int id) where T : class
        {
            var entity = await repository.GetByIdAsync(id);
            if (entity == null) return NotFound();
            repository.Delete(entity);
            await repository.SaveAllAsync();
            return entity;
        }
    }
}