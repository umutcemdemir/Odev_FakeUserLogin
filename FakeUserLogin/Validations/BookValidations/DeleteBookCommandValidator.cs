using FakeUserLogin.Applications.BookOperations.Commands;
using FluentValidation;

namespace FakeUserLogin.Validations.BookValidations
{
    public class DeleteBookCommandValidator : AbstractValidator<DeleteBookCommand>
    {
        public DeleteBookCommandValidator()
        {
            RuleFor(command => command.BookId).GreaterThan(0);
        }
    }
}
