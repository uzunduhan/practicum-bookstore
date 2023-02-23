using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.WebApi.AuthorOperations.Queries.GetAuthorDetail;
using WebApi.DBOperations;
using WebApi.Entities;


namespace Application.AuthorOperations.Query.GetAuthorDetail
{
    public class GetAuthorDetailQueryTests: IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetAuthorDetailQueryTests(CommonTestFixture commonTestFixture)
        {
            _context = commonTestFixture.context;
            _mapper = commonTestFixture.Mapper;
        }


        [Fact]
        public void WhenNotExistAuthorTitleIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            //arrange (hazırlık)
            GetAuthorDetailQuery command = new GetAuthorDetailQuery(_context, _mapper);
            command.AuthorId = 99;
            //act & assert (çalıştırma, doğrulama)

            FluentActions
            .Invoking(()=> command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("yazar bulunamadı");

          
        }

        // [Fact]

        // public void WhenValidInputsAreGiven_Book_ShouldBeGot()
        // {
        //     //arrange (hazırlık)
        //     GetAuthorDetailQuery command = new GetAuthorDetailQuery(_context, _mapper);
        //     command.AuthorId = 2;
        //     //act & assert (çalıştırma, doğrulama)

        //     FluentActions
        //     .Invoking(()=> command.Handle())
        //     .Should().NotThrow();
        // }
    }
}