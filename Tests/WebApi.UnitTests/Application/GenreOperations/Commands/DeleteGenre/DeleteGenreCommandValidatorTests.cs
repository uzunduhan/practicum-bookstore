using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.GenreOperations.Commands.DeleteGenre;
using WebApi.DBOperations;
using WebApi.Entities;

namespace Application.GenreOperations.Commands.DeleteGenre
{

    public class DeleteGenreCommandValidatorTests: IClassFixture<CommonTestFixture>
    {



        private readonly BookStoreDbContext _context;

        public DeleteGenreCommandValidatorTests(CommonTestFixture commonTestFixture)
        {
            _context = commonTestFixture.context;
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-10)]
        [InlineData(-987)]
        [InlineData(-19827)]
        public void WhenInvalidInputsAreGıven_Validator_ShouldBeReturnErrors(int genreId)
        {
            //arrange
            DeleteGenreCommand command = new DeleteGenreCommand(_context);
            command.GenreId = genreId;

            //act
            DeleteGenreCommandValidator validator = new DeleteGenreCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }


        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        [InlineData(987)]
        [InlineData(19827)]
        public void WhenInvalidInputsAreGıven_Validator_ShouldNotBeReturnErrors(int genreId)
        {
            //arrange
            DeleteGenreCommand command = new DeleteGenreCommand(_context);
            command.GenreId = genreId;

            //act
            DeleteGenreCommandValidator validator = new DeleteGenreCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeLessThanOrEqualTo(0);
        }

        
    }
}