using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.GenreOperations.Commands.CreateGenre;
using WebApi.DBOperations;
using WebApi.Entities;

namespace Application.GenreOperations.Commands.CreateGenre
{
    public class CreateGenreCommandTests: IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateGenreCommandTests(CommonTestFixture commonTestFixture)
        {
            _context = commonTestFixture.context;
            _mapper = commonTestFixture.Mapper;
        }


        [Fact]
        public void WhenAlreadyExistGenreNameIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            //arrange (hazırlık)
            var genre = new Genre() {Name = "WhenAlreadyExistGenreNameIsGiven_InvalidOperationException_ShouldBeReturn", IsActive = true};
            _context.Genres.Add(genre);
            _context.SaveChanges();

            CreateGenreCommand command = new CreateGenreCommand(_context);
            command.Model = new CreateGenreModel(){Name = genre.Name};
            //act & assert (çalıştırma, doğrulama)

            FluentActions
            .Invoking(()=> command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("kitap türü zaten var");

          
        }

        [Fact]

        public void WhenValidInputsAreGiven_Book_ShouldBeCreated()
        {
            //arrange
            CreateGenreCommand command = new CreateGenreCommand(_context);
            CreateGenreModel model = new CreateGenreModel()
            {
                Name = "belgesel"
            };

            command.Model = model;

            //act
            FluentActions.Invoking(()=>command.Handle()).Invoke();

            //assert
            var genre = _context.Genres.SingleOrDefault(x=>x.Name == model.Name);
            genre.Should().NotBeNull();
            genre.Name.Should().Be(model.Name);

        }
    }
}