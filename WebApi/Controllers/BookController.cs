using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.WebApi.BookOperations.Commands.CreateBook;
using WebApi.Application.WebApi.BookOperations.Commands.DeleteBook;
using WebApi.Application.WebApi.BookOperations.Queries.GetBookDetail;
using WebApi.Application.WebApi.BookOperations.Queries.GetBooks;
using WebApi.Application.WebApi.BookOperations.Commands.UpdateBook;
using WebApi.DBOperations;



namespace WebApi.Controllers{

    [ApiController]
    [Route("[controller]s")]
    public class BookController: ControllerBase
    {


        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public BookController(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetBooks()
        {
          GetBooksQuery query = new GetBooksQuery(_context, _mapper);
          var result = query.Handle();
          return Ok(result);
          
        }

        
        [HttpGet("{id}")]
        public IActionResult GetByID(int id)
        {
            BookDetailViewModel result;
     
           
              GetBookDetailQuery query = new GetBookDetailQuery(_context, _mapper);
              query.BookId = id;

              GetBookDetailQueryValidator validator = new GetBookDetailQueryValidator();
              //ValidationResult validateResult = validator.Validate(query);
              validator.ValidateAndThrow(query);

              result = query.Handle();
            
     

            return Ok(result);
          
        }


        // [HttpGet]
        // public Book Get([FromQuery] string id)
        // {
        //     var book = _context.Books.Where(book=>book.ID == Convert.ToInt32(id)).SingleOrDefault();
        //     return book;
        // }

        //post
        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookCommand.CreateBookModel newBook)
        {
            CreateBookCommand command = new CreateBookCommand(_context, _mapper); 

     
               command.Model =  newBook;

               CreateBookCommandValidator validator = new CreateBookCommandValidator();
               //ValidationResult result = validator.Validate(command);
               validator.ValidateAndThrow(command);
               command.Handle();

            //    if(!result.IsValid)
            //    {
            //         foreach(var item in result.Errors)
            //         {
            //             Console.WriteLine("ozellik "+ item.PropertyName + "- error nessage: " + item.ErrorMessage);
            //         }
            //    }
            //    else
            //    {
            //         command.Handle();
            //    }
            

        

          return Ok();
        }


        //put
        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] UpdateBookModel updatedBook)
        {

            UpdateBookCommand command = new UpdateBookCommand(_context);
            command.BookId = id;
            command.Model = updatedBook;

            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            //ValidationResult result = validator.Validate(command);
            validator.ValidateAndThrow(command);

            
            command.Handle();


            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
         
                 DeleteBookCommand command = new DeleteBookCommand(_context);
                 command.BookId = id;
                 DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
                 validator.ValidateAndThrow(command);

                 command.Handle();
            
     
  
          
            return Ok();
        }

    }
}