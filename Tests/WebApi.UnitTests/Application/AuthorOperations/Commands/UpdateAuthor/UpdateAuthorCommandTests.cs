using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.WebApi.AuthorOperations.Commands.UpdateAuthor;
using WebApi.DBOperations;
using WebApi.Entities;


namespace Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandTests: IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public UpdateAuthorCommandTests(CommonTestFixture commonTestFixture)
        {
            _context = commonTestFixture.context;
            _mapper = commonTestFixture.Mapper;
        }


        [Fact]
        public void WhenNotExistGenreIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            //arrange (hazırlık)
            var author = new UpdateAuthorModel() {Name = "Personel Growth", SurName="bilim"};
            UpdateAuthorCommand command = new UpdateAuthorCommand(_context);
            command.Model = author;
            command.AuthorId = 88;
            //act & assert (çalıştırma, doğrulama)

            FluentActions
            .Invoking(()=> command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("yazar bulunamadı");

          
        }

        // [Fact]
        // public void WhenExistGenreIsGiven_InvalidOperationException_ShouldBeUpdated()
        // {
        //     //arrange (hazırlık)
        //     var author = new UpdateAuthorModel() {Name = "uzay çağı", SurName="uzay"};
        //     UpdateAuthorCommand command = new UpdateAuthorCommand(_context);
        //     command.Model = author;
        //     command.AuthorId = 2;
        //     //act & assert (çalıştırma, doğrulama)

        //     FluentActions
        //     .Invoking(()=> command.Handle())
        //     .Should().NotThrow();

          
        // }
    }
}