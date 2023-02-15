using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.WebApi.AuthorOperations.Commands.CreateAuthor;
using WebApi.Application.WebApi.AuthorOperations.Commands.DeleteAuthor;
using WebApi.Application.WebApi.AuthorOperations.Commands.UpdateAuthor;
using WebApi.Application.WebApi.AuthorOperations.Queries.GetAuthorDetail;
using WebApi.Application.WebApi.AuthorOperations.Queries.GetAuthors;
using WebApi.DBOperations;
using static WebApi.Application.WebApi.AuthorOperations.Commands.CreateAuthor.CreateAuthorCommand;

namespace WebApi.Controllers
{

    [ApiController]
    [Route("[controller]s")]
    public class AuthorController: ControllerBase
    {
        private readonly BookStoreDbContext _context;  
        private readonly IMapper _mapper;

        public AuthorController(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult GetAuthors()
        {
            GetAuthorsQuery query = new GetAuthorsQuery(_context, _mapper);
            var obj = query.Handle();
            return Ok(obj);
        }

        [HttpGet("{id}")]
        public IActionResult GetByID(int id)
        {
             AuthorDetailViewModel result;
     
           
              GetAuthorDetailQuery query = new GetAuthorDetailQuery(_context, _mapper);
              query.AuthorId = id;

              GetAuthorDetailQueryValidator validator = new GetAuthorDetailQueryValidator();
              //ValidationResult validateResult = validator.Validate(query);
              validator.ValidateAndThrow(query);

              result = query.Handle();
            
     

            return Ok(result);
          
        }

        [HttpPost]
        public IActionResult AddAuthor([FromBody] CreateAuthorModel newAuthor)
        {
            CreateAuthorCommand command = new CreateAuthorCommand(_context, _mapper); 

            command.Model =  newAuthor;
            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator(); 
            validator.ValidateAndThrow(command);
            command.Handle();

        

          return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateAuthor(int id, [FromBody] UpdateAuthorModel updatedAuthor)
        {

            UpdateAuthorCommand command = new UpdateAuthorCommand(_context);
            command.AuthorId = id;
            command.Model = updatedAuthor;

            UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
            //ValidationResult result = validator.Validate(command);
            validator.ValidateAndThrow(command);

            
            command.Handle();


            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
         
            DeleteAuthorCommand command = new DeleteAuthorCommand(_context);
            command.AuthorId = id;
            DeleteAuthorCommandValidator validator = new DeleteAuthorCommandValidator();
            validator.ValidateAndThrow(command);  
            command.Handle();
                     
            return Ok();
        }
    }
}