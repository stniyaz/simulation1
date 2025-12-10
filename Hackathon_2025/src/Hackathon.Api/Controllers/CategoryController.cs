using Hackathon.Business.Dtos.Category;
using Hackathon.Business.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Hackathon.Api.Controllers;

/// <summary>
/// Manages API endpoints for category operations.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    /// <summary>
    /// Initializes a new instance of the <see cref="CategoryController"/> class.
    /// </summary>
    /// <param name="categoryService">Service providing business logic for categories.</param>
    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    /// <summary>
    /// Retrieves all categories.
    /// </summary>
    /// <returns>A list of categories.</returns>
    /// <response code="200">Successfully retrieved categories.</response>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<CategoryGetDto>), 200)]
    public IActionResult GetAll()
    {
        var categories = _categoryService.GetAll();
        return Ok(categories);
    }

    /// <summary>
    /// Retrieves a category by its ID.
    /// </summary>
    /// <param name="id">The category ID.</param>
    /// <returns>The category details.</returns>
    /// <response code="200">Successfully retrieved the category.</response>
    /// <response code="404">Category not found.</response>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(CategoryGetDto), 200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var category = await _categoryService.GetByIdAsync(id);
        return Ok(category);
    }

    /// <summary>
    /// Creates a new category.
    /// </summary>
    /// <param name="dto">Category creation data transfer object.</param>
    /// <returns>No content.</returns>
    /// <response code="200">Category successfully created.</response>
    /// <response code="409">Category with the same name already exists.</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Create([FromBody] CategoryCreateDto dto)
    {
        await _categoryService.CreateAsync(dto);
        return Ok();
    }

    /// <summary>
    /// Updates an existing category.
    /// </summary>
    /// <param name="dto">Category update data transfer object.</param>
    /// <returns>No content.</returns>
    /// <response code="204">Category successfully updated.</response>
    /// <response code="404">Category not found.</response>
    /// <response code="409">Category name conflicts with another existing category.</response>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Update([FromBody] CategoryUpdateDto dto)
    {
        await _categoryService.UpdateAsync(dto);
        return NoContent();
    }

    /// <summary>
    /// Deletes a category by its ID.
    /// </summary>
    /// <param name="id">The category ID.</param>
    /// <returns>No content.</returns>
    /// <response code="204">Category successfully deleted.</response>
    /// <response code="404">Category not found.</response>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _categoryService.DeleteAsync(id);
        return NoContent();
    }
}
