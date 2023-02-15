using FluentValidation;

namespace WebApi.Application.WebApi.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommandValidator : AbstractValidator<CreateAuthorCommand> 
    {
        public CreateAuthorCommandValidator()
        {
            RuleFor(command => command.Model.Birthday.Date).NotEmpty().LessThan(DateTime.Now.Date);
            RuleFor(command => command.Model.Name).NotEmpty().MinimumLength(4);
            RuleFor(command => command.Model.SurName).NotEmpty().MinimumLength(4);
        }
    }

}