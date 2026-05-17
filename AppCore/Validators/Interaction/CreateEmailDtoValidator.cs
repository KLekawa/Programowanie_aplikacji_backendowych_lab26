using AppCore.Dto;
using FluentValidation;

namespace AppCore.Validators.Interaction;

public class CreateEmailDtoValidator : AbstractValidator<CreateEmailDto>
{
    public CreateEmailDtoValidator()
    {
        RuleFor(x => x.Content)
            .NotEmpty().WithMessage("Treść jest wymagana")
            .MaximumLength(5000).WithMessage("Treść nie może przekraczać 5000 znaków");
        RuleFor(x => x.EmailAddress)
            .NotEmpty().WithMessage("Adres email jest wymagany")
            .EmailAddress().WithMessage("Nieprawidłowy adres email")
            .MaximumLength(200).WithMessage("Adres email nie może przekraczać 200 znaków");
        RuleFor(x => x.Subject)
            .NotEmpty().WithMessage("Temat jest wymagany")
            .MaximumLength(120).WithMessage("Temat nie może przekraczać 120 znaków");
    }
}