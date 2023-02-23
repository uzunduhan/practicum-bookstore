using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.WebApi.BookOperations.Commands.DeleteBook;
using WebApi.DBOperations;
using WebApi.Entities;


namespace Application.BookOperations.Commands.DeleteBook
{
    public class DeleteBookCommandTests: IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public DeleteBookCommandTests(CommonTestFixture commonTestFixture)
        {
            _context = commonTestFixture.context;
            _mapper = commonTestFixture.Mapper;
        }


        [Fact]
        public void WhenNotExistBookTitleIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            //arrange (hazırlık)
            DeleteBookCommand command = new DeleteBookCommand(_context);
            command.BookId = 99;
            //act & assert (çalıştırma, doğrulama)

            FluentActions
            .Invoking(()=> command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("kitap bulunamadı");

          
        }

        [Fact]

        public void WhenValidInputsAreGiven_Book_ShouldBeDeleted()
        {
            //arrange (hazırlık)
            DeleteBookCommand command = new DeleteBookCommand(_context);
            command.BookId = 1;
            //act & assert (çalıştırma, doğrulama)

            FluentActions
            .Invoking(()=> command.Handle())
            .Should().NotThrow();
        }
    }
}