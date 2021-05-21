using FluentValidation;

namespace DotNet.EventSourcing.Service.Application.Persons.Commands.CreatePerson
{
    public class CreatePersonCommandValidator : AbstractValidator<CreatePersonCommand>
    {
        public CreatePersonCommandValidator()
        {
            RuleFor(v => v.Name)
                .MaximumLength(100)
                .NotEmpty();

            RuleFor(v => v.Age)
                .GreaterThan(0);

            RuleFor(v => v.Gender)
                .IsInEnum();
        }
    }
}
