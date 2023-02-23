using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.WebApi.AuthorOperations.Commands.CreateAuthor;
using WebApi.DBOperations;
using WebApi.Entities;
using static WebApi.Application.WebApi.AuthorOperations.Commands.CreateAuthor.CreateAuthorCommand;

namespace Application.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommandValidatorTests: IClassFixture<CommonTestFixture>
    {

        public static readonly object[][] CorrectData =
        {
          new object[] { "ti", "testing 1", 
                          new DateTime(2017,3,1)},
          new object[] { "title 2", "",
                          new DateTime(2019, 2, 1)},
           new object[] { "title 2", "title 3",
                          new DateTime(2030, 2, 1)}
        };

        [Theory, MemberData(nameof(CorrectData))]
        public void WhenInvalidInputsAreGıven_Validator_ShoulBeReturnErrors(string name, string surname, DateTime birthday)
        {
            //arrange
            CreateAuthorCommand command = new CreateAuthorCommand(null, null);
            command.Model = new CreateAuthorModel()
            {
                Name = name, SurName = surname, Birthday = birthday
            };

            //act
            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }


        [Fact]
        public void WhenValidInputsAreGıven_Validator_ShoulNotBeReturnError()
        {

           CreateAuthorCommand command = new CreateAuthorCommand(null, null);

            command.Model = new CreateAuthorModel()
            {
                Name = "lord of the rings 2",
                SurName = "new author",
                Birthday = new DateTime(1960,03,10)
            };

            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeLessThanOrEqualTo(0);
        }

        
    }
}