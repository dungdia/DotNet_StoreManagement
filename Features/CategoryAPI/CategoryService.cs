using AutoMapper;
using DotNet_StoreManagement.Domain.entities;
using DotNet_StoreManagement.Domain.entities.@base;
using DotNet_StoreManagement.Domain.enums;
using DotNet_StoreManagement.Features.CategoryAPI.dtos;
using DotNet_StoreManagement.Features.CategoryAPI.impl;
using DotNet_StoreManagement.SharedKernel.configuration;
using DotNet_StoreManagement.SharedKernel.exception;
using DotNet_StoreManagement.SharedKernel.persistence;

namespace DotNet_StoreManagement.Features.CategoryAPI;

[Service]
public class CategoryService
{
    private readonly ICategoryRepository _repo;
    private readonly IMapper _mapper;
    public CategoryService(ICategoryRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<Page<Category>> GetPageableCategoryAsync(CategoryFilterDTO? dtoFilter, PageRequest pageRequest)
    {
        IQueryable<Category> query = _repo.GetQueryable();

        query = query
            .Filter("CategoryId", dtoFilter?.CategoryId.ToString(), FilterType.CONTAINS)
            .Filter("CategoryName", dtoFilter?.CategoryName, FilterType.CONTAINS);

        return await _repo.FindAllPageAsync(
            query,
            pageRequest.PageNumber,
            pageRequest.PageSize
        );
    }

    public async Task<ICollection<Category>> GetAllCategoriesAsync()
    {
        return await _repo.GetAllAsync();
    }

    public async Task<Category> CreateCategoryAsync(CategoryDTO dto)
    {
        var category = _mapper.Map<Category>(dto);

        await _repo.AddAsync(category);
        var affectedRows = await _repo.SaveChangesAsync();

        if (affectedRows == 0) throw APIException.InternalServerError("Create customer failed!");

        return category;
    }

    public async Task<Category> EditCategory(int id, CategoryDTO dto)
    {
        var category = await _repo.GetByIdAsync(id);

        if(category == null) throw APIException.BadRequest("Category not found!");
        await _repo.UpdateAsync(_mapper.Map(dto, category));

        var affectedRows = await _repo.SaveChangesAsync();
        if (affectedRows == 0) throw APIException.InternalServerError("Update category failed!");

        return category;
    }

    public async Task<Category> DeleteCategory(int id)
    {
        var category = await _repo.GetByIdAsync(id);

        if (category == null) throw APIException.BadRequest("Category not found!");
        await _repo.DeleteAsync(category);

        var affectedRows = await _repo.SaveChangesAsync();
        if (affectedRows == 0) throw APIException.InternalServerError("Delete category failed!");

        return category;
    }
}
