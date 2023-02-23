using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.GenreOperations.Commands.UpdateGenre;
using WebApi.Application.WebApi.BookOperations.Commands.UpdateBook;
using WebApi.DBOperations;
using WebApi.Entities;


namespace Application.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommandTests: IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public UpdateGenreCommandTests(CommonTestFixture commonTestFixture)
        {
            _context = commonTestFixture.context;
            _mapper = commonTestFixture.Mapper;
        }


        [Fact]
        public void WhenNotExistGenreIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            //arrange (hazırlık)
            var genre = new UpdateGenreModel() {Name = "Personel Growth", IsActive=true};
            UpdateGenreCommand command = new UpdateGenreCommand(_context);
            command.Model = genre;
            command.GenreId = 2;
            //act & assert (çalıştırma, doğrulama)

            FluentActions
            .Invoking(()=> command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("aynı isimli bir kitap türü zaten mevcut");

          
        }

        [Fact]
        public void WhenExistGenreIsGiven_InvalidOperationException_ShouldBeUpdated()
        {
            //arrange (hazırlık)
            var genre = new UpdateGenreModel() {Name = "uzay çağı", IsActive=true};
            UpdateGenreCommand command = new UpdateGenreCommand(_context);
            command.Model = genre;
            command.GenreId = 3;
            //act & assert (çalıştırma, doğrulama)

            FluentActions
            .Invoking(()=> command.Handle())
            .Should().NotThrow();

          
        }
    }
}