using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.WebApi.BookOperations.Commands.UpdateBook;
using WebApi.DBOperations;
using WebApi.Entities;


namespace Application.BookOperations.Commands.UpdateBook
{
    public class UpdateBookCommandTests: IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public UpdateBookCommandTests(CommonTestFixture commonTestFixture)
        {
            _context = commonTestFixture.context;
            _mapper = commonTestFixture.Mapper;
        }


        [Fact]
        public void WhenNotExistBookIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            //arrange (hazırlık)
            var book = new UpdateBookModel() {Title = "WhenNotExistBookIsGiven_InvalidOperationException_ShouldBeReturn", GenreId = 2};
            UpdateBookCommand command = new UpdateBookCommand(_context);
            command.Model = book;
            command.BookId = 2;
            //act & assert (çalıştırma, doğrulama)

            FluentActions
            .Invoking(()=> command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("kitap bulunamadı");

          
        }

        [Fact]
        public void WhenExistBookIsGiven_InvalidOperationException_ShouldBeUpdated()
        {
            //arrange (hazırlık)
            var book = new UpdateBookModel() {Title = "WhenExistBookIsGiven_InvalidOperationException_ShouldBeUpdated", GenreId = 2};
            UpdateBookCommand command = new UpdateBookCommand(_context);
            command.Model = book;
            command.BookId = 3;
            //act & assert (çalıştırma, doğrulama)

            FluentActions
            .Invoking(()=> command.Handle())
            .Should().NotThrow();

          
        }
    }
}