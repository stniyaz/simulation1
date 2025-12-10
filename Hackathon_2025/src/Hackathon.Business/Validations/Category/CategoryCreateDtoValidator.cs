using FluentValidation;
using Hackathon.Business.Dtos.Category;

namespace Hackathon.Business.Validations.Category;

public class CategoryCreateDtoValidator : AbstractValidator<CategoryCreateDto>
{
    public CategoryCreateDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Kateqoriya adı boş ola bilməz.")
            .MaximumLength(50).WithMessage("Kateqoriya adı maksimum 50 simvol ola bilər.");
    }
}
