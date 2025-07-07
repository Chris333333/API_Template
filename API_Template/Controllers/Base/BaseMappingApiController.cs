using App.Helpers;
using App.Spec;
using AutoMapper;
using Data.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API_Template.Controllers.Base;

public class BaseMappingApiController(IMapper mapper) : BaseApiController
{
    protected readonly IMapper _mapper = mapper;

    /// <summary>
    /// Add an entity with mapping
    /// Can be created while calling await AddEntity<MainSignal, ScadaSignalsDTO>(_signalsRepo, dto);
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TDto"></typeparam>
    /// <param name="repository"></param>
    /// <param name="dto"></param>
    /// <returns></returns>
    protected async Task<ActionResult<T>> AddEntity<T, TDto>(IGenericRepository<T> repository, TDto dto) where T : class
    {
        var entity = _mapper.Map<T>(dto);
        repository.Add(entity);
        await repository.SaveAllAsync();
        return Ok(entity);
    }
    /// <summary>
    /// Create a paged result with mapping
    /// 
    /// Can be created while calling await CreateMappedPagedResult<MainSignal, ScadaSignalsDTO>(_signalsRepo, spec, specParams);
    /// Where MainSignal is the entity, ScadaSignalsDTO is the DTO, _signalsRepo is the repository, spec is the specification, specParams is the pagination parameters.
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TDto"></typeparam>
    /// <param name="repository"></param>
    /// <param name="spec"></param>
    /// <param name="pagination"></param>
    /// <returns></returns>
    protected async Task<ActionResult> CreateMappedPagedResult<T, TDto>(IGenericRepository<T> repository, ISpecification<T> spec, BaseSpecParams pagination) where T : class
    {
        var totalItems = await repository.CountAsync(spec);
        var data = await repository.ListAllWithSpecAsync(spec);
        var mappedData = _mapper.Map<IReadOnlyList<TDto>>(data);
        var result = new Pagination<TDto>(pagination.PageIndex, pagination.PageSize, totalItems, mappedData);
        return Ok(result);
    }

    /// <summary>
    /// Get an entity with specification and map it to a DTO.
    /// This method is used to retrieve a single entity based on the provided specification and map it to a DTO.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TDto"></typeparam>
    /// <param name="repository"></param>
    /// <param name="spec"></param>
    /// <returns></returns>
    protected async Task<ActionResult<TDto>> GetMappedEntityWithSpec<T, TDto>(IGenericRepository<T> repository, ISpecification<T> spec) where T : class
    {
        var entity = await repository.GetEntityWithSpec(spec);
        if (entity == null) return NotFound();
        return _mapper.Map<TDto>(entity);
    }
}
