using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.WebApi.BookOperations.Queries.GetBookDetail;
using WebApi.DBOperations;
using WebApi.Entities;


namespace Application.BookOperations.Query.GetBookDetail
{
    public class GetBookDetailQueryTests: IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetBookDetailQueryTests(CommonTestFixture commonTestFixture)
        {
            _context = commonTestFixture.context;
            _mapper = commonTestFixture.Mapper;
        }


        [Fact]
        public void WhenNotExistBookTitleIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            //arrange (hazırlık)
            GetBookDetailQuery command = new GetBookDetailQuery(_context, _mapper);
            command.BookId = 99;
            //act & assert (çalıştırma, doğrulama)

            FluentActions
            .Invoking(()=> command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("kitap bulunamadı");

          
        }

        [Fact]

        public void WhenValidInputsAreGiven_Book_ShouldBeGot()
        {
            //arrange (hazırlık)
            GetBookDetailQuery command = new GetBookDetailQuery(_context,_mapper);
            command.BookId = 2;
            //act & assert (çalıştırma, doğrulama)

            FluentActions
            .Invoking(()=> command.Handle())
            .Should().NotThrow();
        }
    }
}