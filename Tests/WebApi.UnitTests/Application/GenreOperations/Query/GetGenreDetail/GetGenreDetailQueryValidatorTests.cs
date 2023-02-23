using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.GenreOperations.Queries.GetGenreDetail;
using WebApi.DBOperations;
using WebApi.Entities;

namespace Application.GenreOperations.Query.GetGenreDetail
{

    public class GetGenreDetailQueryValidatorTests: IClassFixture<CommonTestFixture>
    {



        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetGenreDetailQueryValidatorTests(CommonTestFixture commonTestFixture)
        {
            _context = commonTestFixture.context;
            _mapper = commonTestFixture.Mapper;
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-10)]
        [InlineData(-987)]
        [InlineData(-19827)]
        public void WhenInvalidInputsAreGıven_Validator_ShouldBeReturnErrors(int genreId)
        {
            //arrange
            GetGenreDetailQuery command = new GetGenreDetailQuery(_mapper, _context);
            command.GenreId = genreId;

            //act
            GetGenreDetailQueryValidator validator = new GetGenreDetailQueryValidator();
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
            GetGenreDetailQuery command = new GetGenreDetailQuery(_mapper, _context);
            command.GenreId = genreId;

            //act
            GetGenreDetailQueryValidator validator = new GetGenreDetailQueryValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeLessThanOrEqualTo(0);
        }

        
    }
}