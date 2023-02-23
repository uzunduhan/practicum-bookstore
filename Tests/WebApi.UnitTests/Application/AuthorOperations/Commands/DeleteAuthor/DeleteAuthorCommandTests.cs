using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.WebApi.AuthorOperations.Commands.DeleteAuthor;
using WebApi.DBOperations;
using WebApi.Entities;


namespace Application.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommandTests: IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public DeleteAuthorCommandTests(CommonTestFixture commonTestFixture)
        {
            _context = commonTestFixture.context;
            _mapper = commonTestFixture.Mapper;
        }


        [Fact]
        public void WhenNotExistAuthorTitleIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            //arrange (hazırlık)
            DeleteAuthorCommand command = new DeleteAuthorCommand(_context);
            command.AuthorId = 99;
            //act & assert (çalıştırma, doğrulama)

            FluentActions
            .Invoking(()=> command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("yazar bulunamadı");

          
        }

        // [Fact]
        // public void WhenExistBookToAuthorIdIsGiven_InvalidOperationException_ShouldBeReturn()
        // {
        //     //arrange (hazırlık)
        //     DeleteAuthorCommand command = new DeleteAuthorCommand(_context);
        //     command.AuthorId = 1;
        //     //act & assert (çalıştırma, doğrulama)

        //     FluentActions
        //     .Invoking(()=> command.Handle())
        //     .Should().Throw<InvalidOperationException>().And.Message.Should().Be("hata, silinecek yazara ait kitap mevcut");

          
        // }
    }
}