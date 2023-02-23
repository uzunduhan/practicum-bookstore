using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.GenreOperations.Commands.CreateGenre;
using WebApi.DBOperations;
using WebApi.Entities;

namespace Application.GenreOperations.Commands.CreateGenre
{
    public class CreateGenreCommandValidatorTests: IClassFixture<CommonTestFixture>
    {

        [Theory]
        [InlineData("")]
        [InlineData("lor")]
        [InlineData(" l")]
        public void WhenInvalidInputsAreGıven_Validator_ShoulBeReturnErrors(string name)
        {
            //arrange
            CreateGenreCommand command = new CreateGenreCommand(null);
            command.Model = new CreateGenreModel()
            {
                Name = name
            };

            //act
            CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }


        [Fact]
        public void WhenValidInputsAreGıven_Validator_ShoulNotBeReturnError()
        {

           CreateGenreCommand command = new CreateGenreCommand(null);

            command.Model = new CreateGenreModel()
            {
                Name = "lord of the rings 2"
            };

            CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeLessThanOrEqualTo(0);
        }

        
    }
}