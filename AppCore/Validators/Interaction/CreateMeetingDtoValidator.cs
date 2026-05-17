using AppCore.Dto;
using FluentValidation;

namespace AppCore.Validators.Interaction;

public class CreateMeetingDtoValidator : AbstractValidator<CreateMeetingDto>
{
    public CreateMeetingDtoValidator()
    {
        RuleFor(x => x.Content)
            .NotEmpty().WithMessage("Treść jest wymagana")
            .MaximumLength(5000).WithMessage("Treść nie może przekraczać 5000 znaków");
        RuleFor(x => x.Location)
            .NotEmpty().WithMessage("Lokalizacja jest wymagana")
            .MaximumLength(200).WithMessage("Lokalizacja nie może przekraczać 200 znaków");
        RuleFor(x => x.DurationMinutes)
            .NotEmpty().WithMessage("Czas trwania spotkania jest wymagany")
            .GreaterThan(0).WithMessage("Czas spotkania musi być większy od 0");
    }
   
}