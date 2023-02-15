using FluentValidation;

namespace WebApi.Application.WebApi.BookOperations.Queries.GetBookDetail
{
    public class GetBookDetailQueryValidator: AbstractValidator<GetBookDetailQuery>
    {
        public GetBookDetailQueryValidator()
        {
            RuleFor(command=>command.BookId).GreaterThan(0);
        }
    }
}