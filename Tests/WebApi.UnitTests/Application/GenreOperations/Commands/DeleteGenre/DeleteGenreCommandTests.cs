using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.GenreOperations.Commands.DeleteGenre;
using WebApi.DBOperations;
using WebApi.Entities;


namespace Application.GenreOperations.Commands.DeleteGenre
{
    public class DeleteGenreCommandTests: IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public DeleteGenreCommandTests(CommonTestFixture commonTestFixture)
        {
            _context = commonTestFixture.context;
            _mapper = commonTestFixture.Mapper;
        }


        [Fact]
        public void WhenNotExistGenreTitleIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            //arrange (hazırlık)
            DeleteGenreCommand command = new DeleteGenreCommand(_context);
            command.GenreId = 99;
            //act & assert (çalıştırma, doğrulama)

            FluentActions
            .Invoking(()=> command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("kitap türü bulunamadı");

          
        }

        [Fact]

        public void WhenValidInputsAreGiven_Genre_ShouldBeDeleted()
        {
            //arrange (hazırlık)
            DeleteGenreCommand command = new DeleteGenreCommand(_context);
            command.GenreId = 1;
            //act & assert (çalıştırma, doğrulama)

            FluentActions
            .Invoking(()=> command.Handle())
            .Should().NotThrow();
        }
    }
}