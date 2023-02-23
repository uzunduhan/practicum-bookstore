using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.WebApi.AuthorOperations.Commands.UpdateAuthor;
using WebApi.DBOperations;
using WebApi.Entities;


namespace Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandValidatorTests: IClassFixture<CommonTestFixture>
    {

        [Theory]
        [InlineData("lor", "of the rings")]
        [InlineData(" ", "of the")]
         [InlineData("lord of", "th")]
        public void WhenInvalidInputsAreGıven_Validator_ShoulBeReturnErrors(string name, string surname)
        {
            //arrange
            UpdateAuthorCommand command = new UpdateAuthorCommand(null);
            command.Model = new UpdateAuthorModel()
            {
                Name = name,
                SurName = surname
            };

            command.AuthorId = 2;

            //act
            UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGıven_Validator_ShoulNotBeReturnError()
        {

           UpdateAuthorCommand command = new UpdateAuthorCommand(null);

            command.Model = new UpdateAuthorModel()
            {
                Name = "lord of the rings 7",
                SurName = "hobbit"
            };

            command.AuthorId = 2;

            UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeLessThanOrEqualTo(0);
        }

        
    }
}