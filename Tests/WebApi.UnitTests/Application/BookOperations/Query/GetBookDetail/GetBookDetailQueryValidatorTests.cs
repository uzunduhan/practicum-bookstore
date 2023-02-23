using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.WebApi.BookOperations.Queries.GetBookDetail;
using WebApi.DBOperations;
using WebApi.Entities;

namespace Application.BookOperations.Query.GetBookDetail
{

    public class GetBookDetailQueryValidatorTests: IClassFixture<CommonTestFixture>
    {



        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetBookDetailQueryValidatorTests(CommonTestFixture commonTestFixture)
        {
            _context = commonTestFixture.context;
            _mapper = commonTestFixture.Mapper;
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-10)]
        [InlineData(-987)]
        [InlineData(-19827)]
        public void WhenInvalidInputsAreGıven_Validator_ShouldBeReturnErrors(int bookId)
        {
            //arrange
            GetBookDetailQuery command = new GetBookDetailQuery(_context, _mapper);
            command.BookId = bookId;

            //act
            GetBookDetailQueryValidator validator = new GetBookDetailQueryValidator();
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
            GetBookDetailQuery command = new GetBookDetailQuery(_context, _mapper);
            command.BookId = bookId;

            //act
            GetBookDetailQueryValidator validator = new GetBookDetailQueryValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeLessThanOrEqualTo(0);
        }

        
    }
}