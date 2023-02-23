using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.WebApi.BookOperations.Commands.DeleteBook;
using WebApi.DBOperations;
using WebApi.Entities;

namespace Application.BookOperations.Commands.DeleteBook
{

    public class DeleteBookCommandValidatorTests: IClassFixture<CommonTestFixture>
    {



        private readonly BookStoreDbContext _context;

        public DeleteBookCommandValidatorTests(CommonTestFixture commonTestFixture)
        {
            _context = commonTestFixture.context;
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-10)]
        [InlineData(-987)]
        [InlineData(-19827)]
        public void WhenInvalidInputsAreGıven_Validator_ShouldBeReturnErrors(int bookId)
        {
            //arrange
            DeleteBookCommand command = new DeleteBookCommand(_context);
            command.BookId = bookId;

            //act
            DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }


        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        [InlineData(987)]
        [InlineData(19827)]
        public void WhenInvalidInputsAreGıven_Validator_ShouldNotBeReturnErrors(int bookId)
        {
            //arrange
            DeleteBookCommand command = new DeleteBookCommand(_context);
            command.BookId = bookId;

            //act
            DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeLessThanOrEqualTo(0);
        }

        
    }
}