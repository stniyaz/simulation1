using FluentValidation;
using Hackathon.Business.Dtos.AuthDtos;

namespace Hackathon.Business.Validations.AuthValidations;

public class RegisterDtoValidator : AbstractValidator<RegisterDto>
{
    public RegisterDtoValidator()
    {
        RuleFor(x => x.FullName)
            .NotEmpty().WithMessage("Ad və Soyad boş ola bilməz.")
            .MaximumLength(100).WithMessage("Ad çox uzundur.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email boş ola bilməz.")
            .EmailAddress().WithMessage("Düzgün email formatı daxil edin.");

        RuleFor(x => x.UserName)
            .NotEmpty().WithMessage("İstifadəçi adı mütləqdir.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Şifrə mütləqdir.")
            .MinimumLength(6).WithMessage("Şifrə ən azı 6 simvol olmalıdır.");

        RuleFor(x => x.ConfirmPassword)
            .Equal(x => x.Password).WithMessage("Şifrələr uyğun gəlmir.");

        RuleFor(x => x.Photo)
            .Must(x => x == null || x.Length <= 5 * 1024 * 1024)
            .WithMessage("Şəkil ölçüsü 5MB-dan çox ola bilməz.")
            .Must(x => x == null || new[] { "image/jpeg", "image/png", "image/jpg" }.Contains(x.ContentType))
            .WithMessage("Yalnız .jpg və .png formatında şəkillər qəbul edilir.");
    }
}
