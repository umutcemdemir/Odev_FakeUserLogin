using FakeUserLogin.Applications.BookOperations.Queries;
using FluentValidation;

namespace FakeUserLogin.Validations.BookValidations
{
    public class GetBookByIdQueryValidator : AbstractValidator<GetBookByIdQuery>
    {
        public GetBookByIdQueryValidator()
        {
            RuleFor(query => query.BookId).GreaterThan(0);
        }
    }
}
