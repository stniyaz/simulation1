using AutoMapper;
using Hackathon.Business.Dtos.Category;
using Hackathon.Business.Helpers.Exceptions;
using Hackathon.Business.Services.Interfaces;
using Hackathon.Core.Entities;
using Hackathon.Core.Interfaces;

namespace Hackathon.Business.Services.Implementations;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _repository;
    private readonly IMapper _mapper;

    public CategoryService(ICategoryRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public IEnumerable<CategoryGetDto> GetAll()
    {
        var categories = _repository.GetAll();
        return _mapper.Map<IEnumerable<CategoryGetDto>>(categories);
    }

    public async Task<CategoryGetDto> GetByIdAsync(Guid id)
    {
        var category = await _repository.GetByIdAsync(id)
            ?? throw new EntityNotFoundException($"ID = {id} olan kateqoriya tapılmadı");

        return _mapper.Map<CategoryGetDto>(category);
    }

    public async Task CreateAsync(CategoryCreateDto dto)
    {
        if (await _repository.IsExistAsync(c => c.Name == dto.Name))
            throw new EntityExistException($"{dto.Name} artıq mövcuddur");

        var category = _mapper.Map<Category>(dto);
        await _repository.CreateAsync(category);
        await _repository.SaveChangesAsync();
    }

    public async Task UpdateAsync(CategoryUpdateDto dto)
    {
        var category = await _repository.GetByIdAsync(dto.Id)
            ?? throw new EntityNotFoundException($"{dto.Id} ID-li kateqoriya tapılmadı");

        if (await _repository.IsExistAsync(c => c.Name == dto.Name && c.Id != dto.Id))
            throw new EntityExistException($"{dto.Name} artıq mövcuddur");

        category.Name = dto.Name;
        await _repository.UpdateAsync(category);
        await _repository.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var category = await _repository.GetByIdAsync(id)
            ?? throw new EntityNotFoundException($"{id} ID-li kateqoriya tapılmadı");

        _repository.Delete(category);
        await _repository.SaveChangesAsync();
    }
}