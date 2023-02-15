using FluentValidation;

namespace WebApi.Application.WebApi.AuthorOperations.Commands.UpdateAuthor{
    public class UpdateAuthorCommandValidator: AbstractValidator<UpdateAuthorCommand>{
        public UpdateAuthorCommandValidator()
        {
            RuleFor(command=>command.Model.Name).NotEmpty().MinimumLength(4);
            RuleFor(command=>command.Model.SurName).NotEmpty().MinimumLength(4); 
            RuleFor(command=>command.AuthorId).GreaterThan(0);
        }
    }
}