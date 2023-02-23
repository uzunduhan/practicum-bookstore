using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.WebApi.AuthorOperations.Queries.GetAuthorDetail;
using WebApi.DBOperations;
using WebApi.Entities;

namespace Application.AuthorOperations.Query.GetAuthorDetail
{

    public class GetAuthorDetailQueryValidatorTests: IClassFixture<CommonTestFixture>
    {



        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetAuthorDetailQueryValidatorTests(CommonTestFixture commonTestFixture)
        {
            _context = commonTestFixture.context;
            _mapper = commonTestFixture.Mapper;
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-10)]
        [InlineData(-987)]
        [InlineData(-19827)]
        public void WhenInvalidInputsAreGıven_Validator_ShouldBeReturnErrors(int authorId)
        {
            //arrange
            GetAuthorDetailQuery command = new GetAuthorDetailQuery(_context, _mapper);
            command.AuthorId = authorId;

            //act
            GetAuthorDetailQueryValidator validator = new GetAuthorDetailQueryValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }


        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        [InlineData(987)]
        [InlineData(19827)]
        public void WhenInvalidInputsAreGıven_Validator_ShouldNotBeReturnErrors(int authorId)
        {
            //arrange
            GetAuthorDetailQuery command = new GetAuthorDetailQuery(_context, _mapper);
            command.AuthorId = authorId;

            //act
            GetAuthorDetailQueryValidator validator = new GetAuthorDetailQueryValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeLessThanOrEqualTo(0);
        }

        
    }
}