using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.GenreOperations.Queries.GetGenreDetail;
using WebApi.DBOperations;
using WebApi.Entities;


namespace Application.GenreOperations.Query.GetGenreDetail
{
    public class GetGenreDetailQueryTests: IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetGenreDetailQueryTests(CommonTestFixture commonTestFixture)
        {
            _context = commonTestFixture.context;
            _mapper = commonTestFixture.Mapper;
        }


        [Fact]
        public void WhenNotExistGenreTitleIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            //arrange (hazırlık)
            GetGenreDetailQuery command = new GetGenreDetailQuery(_mapper,_context);
            command.GenreId = 99;
            //act & assert (çalıştırma, doğrulama)

            FluentActions
            .Invoking(()=> command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("kitap türü bulunamadı");

          
        }

        [Fact]

        public void WhenValidInputsAreGiven_Book_ShouldBeGot()
        {
            //arrange (hazırlık)
            GetGenreDetailQuery command = new GetGenreDetailQuery(_mapper, _context);
            command.GenreId = 2;
            //act & assert (çalıştırma, doğrulama)

            FluentActions
            .Invoking(()=> command.Handle())
            .Should().NotThrow();
        }
    }
}