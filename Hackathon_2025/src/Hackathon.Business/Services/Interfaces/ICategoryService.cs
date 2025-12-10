using Hackathon.Business.Dtos.Category;

namespace Hackathon.Business.Services.Interfaces;

public interface ICategoryService
{
    IEnumerable<CategoryGetDto> GetAll();
    Task<CategoryGetDto> GetByIdAsync(Guid id);
    Task CreateAsync(CategoryCreateDto dto);
    Task UpdateAsync(CategoryUpdateDto dto);
    Task DeleteAsync(Guid id);
}
