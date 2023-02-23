using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.WebApi.AuthorOperations.Commands.CreateAuthor;
using WebApi.DBOperations;
using WebApi.Entities;
using static WebApi.Application.WebApi.AuthorOperations.Commands.CreateAuthor.CreateAuthorCommand;

namespace Application.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommandTests: IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateAuthorCommandTests(CommonTestFixture commonTestFixture)
        {
            _context = commonTestFixture.context;
            _mapper = commonTestFixture.Mapper;
        }


        [Fact]
        public void WhenAlreadyExistAuthorNameIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            //arrange (hazırlık)
            var author = new Author() {Name = "WhenAlreadyExistAuthorNameIsGiven_InvalidOperationException_ShouldBeReturn",SurName = "param", Birthday = new DateTime(1990,01,10)};
            _context.Authors.Add(author);
            _context.SaveChanges();

            CreateAuthorCommand command = new CreateAuthorCommand(_context, _mapper);
            command.Model = new CreateAuthorModel(){Name = author.Name, SurName = author.SurName};
            //act & assert (çalıştırma, doğrulama)

            FluentActions
            .Invoking(()=> command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("yazar zaten mevcut");

          
        }

        [Fact]

        public void WhenValidInputsAreGiven_Author_ShouldBeCreated()
        {
            //arrange
            CreateAuthorCommand command = new CreateAuthorCommand(_context, _mapper);
            CreateAuthorModel model = new CreateAuthorModel()
            {
                Name = "belgesel",
                SurName = "aksiyon",
                Birthday = new DateTime(1960,05,05)
            };

            command.Model = model;

            //act
            FluentActions.Invoking(()=>command.Handle()).Invoke();

            //assert
            var author = _context.Authors.SingleOrDefault(x=>x.Name == model.Name);
            author.Should().NotBeNull();
            author.Name.Should().Be(model.Name);
            author.SurName.Should().Be(model.SurName);
            author.Birthday.Should().Be(model.Birthday);

        }
    }
}