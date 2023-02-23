using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.GenreOperations.Commands.UpdateGenre;
using WebApi.DBOperations;
using WebApi.Entities;


namespace Application.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommandValidatorTests: IClassFixture<CommonTestFixture>
    {

        [Theory]
        [InlineData("lor")]
        [InlineData(" ")]
         [InlineData("l r")]
        public void WhenInvalidInputsAreGıven_Validator_ShoulBeReturnErrors(string name)
        {
            //arrange
            UpdateGenreCommand command = new UpdateGenreCommand(null);
            command.Model = new UpdateGenreModel()
            {
                Name = name
            };

            command.GenreId = 2;

            //act
            UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGıven_Validator_ShoulNotBeReturnError()
        {

           UpdateGenreCommand command = new UpdateGenreCommand(null);

            command.Model = new UpdateGenreModel()
            {
                Name = "lord of the rings 7"
            };

            command.GenreId = 2;

            UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeLessThanOrEqualTo(0);
        }

        
    }
}