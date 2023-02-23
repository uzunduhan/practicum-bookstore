using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.WebApi.BookOperations.Commands.CreateBook;
using WebApi.DBOperations;
using WebApi.Entities;
using static WebApi.Application.WebApi.BookOperations.Commands.CreateBook.CreateBookCommand;

namespace Application.BookOperations.Commands.CreateBook
{
    public class CreateBookCommandValidatorTests: IClassFixture<CommonTestFixture>
    {

        [Theory]
        [InlineData("lord of the rings", 0, 0)]
        [InlineData("lord of the rings", 0, 1)]        
        [InlineData("lord of the rings", 100, 0)]
        [InlineData("", 0, 0)]
        [InlineData("", 100, 1)]
        [InlineData("", 0, 1)]
        [InlineData("lor", 100, 1)]
        [InlineData("lord", 100, 0)]
        [InlineData("lord", 0, 1)]
        [InlineData(" ", 100, 1)]
        //[InlineData("lord of the", 100, 1)]
        public void WhenInvalidInputsAreGıven_Validator_ShoulBeReturnErrors(string title, int pageCount, int genreId)
        {
            //arrange
            CreateBookCommand command = new CreateBookCommand(null, null);
            command.Model = new CreateBookModel()
            {
                Title = title, PageCount = pageCount, PublishDate=DateTime.Now.Date.AddYears(-1),
                GenreId = genreId
            };

            //act
            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenDateTimeEqualNowIsGiven_Validator_ShouldBeReturnError()
        {

             CreateBookCommand command = new CreateBookCommand(null, null);

            command.Model = new CreateBookModel()
            {
                Title = "lord of the rings", PageCount = 100, PublishDate=DateTime.Now.Date,
                GenreId = 2
            };

            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGıven_Validator_ShoulNotBeReturnError()
        {

           CreateBookCommand command = new CreateBookCommand(null, null);

            command.Model = new CreateBookModel()
            {
                Title = "lord of the rings 2", PageCount = 100, PublishDate=DateTime.Now.Date.AddYears(-1),
                GenreId = 2
            };

            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeLessThanOrEqualTo(0);
        }

        
    }
}