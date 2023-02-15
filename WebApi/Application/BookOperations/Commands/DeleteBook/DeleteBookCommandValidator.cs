using FluentValidation;

namespace WebApi.Application.WebApi.BookOperations.Commands.DeleteBook
{
    public class DeleteBookCommandValidator: AbstractValidator<DeleteBookCommand>
    {
        public DeleteBookCommandValidator()
        {
            RuleFor(command => command.BookId).GreaterThan(0);
        }
    }
}