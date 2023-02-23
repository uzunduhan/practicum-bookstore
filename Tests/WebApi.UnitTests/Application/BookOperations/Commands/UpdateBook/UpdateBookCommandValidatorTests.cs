using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.WebApi.BookOperations.Commands.UpdateBook;
using WebApi.DBOperations;
using WebApi.Entities;


namespace Application.BookOperations.Commands.UpdateBook
{
    public class UpdateBookCommandValidatorTests: IClassFixture<CommonTestFixture>
    {

        [Theory]
        [InlineData("lord of the rings", 0)]
        [InlineData("lord of the rings", 1)]        
        [InlineData("lord of the rings", 0)]
        [InlineData("", 0)]
        [InlineData("", 1)]
        [InlineData("", 1)]
        [InlineData("lor", 1)]
        [InlineData("lord", 0)]
        [InlineData("lord", 1)]
        [InlineData(" ", 1)]
        //[InlineData("lord of the", 100, 1)]
        public void WhenInvalidInputsAreGıven_Validator_ShoulBeReturnErrors(string title, int genreId)
        {
            //arrange
            UpdateBookCommand command = new UpdateBookCommand(null);
            command.Model = new UpdateBookModel()
            {
                Title = title, 
                GenreId = genreId
            };

            command.BookId = 2;

            //act
            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGıven_Validator_ShoulNotBeReturnError()
        {

           UpdateBookCommand command = new UpdateBookCommand(null);

            command.Model = new UpdateBookModel()
            {
                Title = "lord of the rings 7",
                GenreId = 2
            };

            command.BookId = 2;

            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeLessThanOrEqualTo(0);
        }

        
    }
}