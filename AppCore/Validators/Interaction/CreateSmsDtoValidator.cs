using AppCore.Dto;
using FluentValidation;

namespace AppCore.Validators.Interaction;

public class CreateSmsDtoValidator : AbstractValidator<CreateSmsDto>
{
    public CreateSmsDtoValidator()
    {

        RuleFor(x => x.Content)
            .NotEmpty().WithMessage("Treść jest wymagana")
            .MaximumLength(5000).WithMessage("Treść nie może przekraczać 5000 znaków");
        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("Numer telefonu jest wymagany")
            .MaximumLength(20).WithMessage("Numer telefonu nie może przekraczać 20 znaków");
    }
}