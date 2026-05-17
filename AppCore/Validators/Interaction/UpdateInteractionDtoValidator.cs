using AppCore.Dto;
using FluentValidation;

namespace AppCore.Validators.Interaction;

public class UpdateInteractionDtoValidator : AbstractValidator<UpdateInteractionDto>
{
    public  UpdateInteractionDtoValidator()
    {
        RuleFor(x => x.Content)
            .MaximumLength(5000).WithMessage("Treść nie może przekraczać 5000 znaków")
            .When(x => x.Content != null);
        RuleFor(x => x.PhoneNumber)
            .MaximumLength(20).WithMessage("Numer telefonu nie może przekraczać 20 znaków")
            .When(x => x.PhoneNumber != null);
        RuleFor(x => x.EmailAddress)
            .EmailAddress().WithMessage("Nieprawidłowy adres email")
            .MaximumLength(200).WithMessage("Adres email nie może przekraczać 200 znaków")
            .When(x => x.EmailAddress != null);
        RuleFor(x => x.Subject)
            .MaximumLength(120).WithMessage("Temat nie może przekraczać 120 znaków")
            .When(x => x.Subject != null);
        RuleFor(x => x.Location)
            .MaximumLength(200).WithMessage("Lokalizacja nie może przekraczać 200 znaków")
            .When(x => x.Location != null);
        RuleFor(x => x.DurationMinutes)
            .GreaterThan(0).WithMessage("Czas spotkania musi być większy od 0")
            .When(x => x.DurationMinutes.HasValue);
        
    }
}