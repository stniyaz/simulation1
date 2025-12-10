using FluentValidation;
using Hackathon.Business.Dtos.Category;

namespace Hackathon.Business.Validations.Category;

public class CategoryUpdateDtoValidator : AbstractValidator<CategoryUpdateDto>
{
    public CategoryUpdateDtoValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id boş ola bilməz.");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Kateqoriya adı boş ola bilməz.")
            .MaximumLength(50).WithMessage("Kateqoriya adı maksimum 50 simvol ola bilər.");
    }
}