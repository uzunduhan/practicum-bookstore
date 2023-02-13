using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using WebApi.BookOperations.CreateBook;
using WebApi.BookOperations.DeleteBook;
using WebApi.BookOperations.GetBookDetail;
using WebApi.BookOperations.GetBooks;
using WebApi.BookOperations.UpdateBook;
using WebApi.DBOperations;
using static WebApi.BookOperations.CreateBook.CreateBookCommand;
using static WebApi.BookOperations.UpdateBook.UpdateBookCommand;

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
            try
            {
           
              GetBookDetailQuery query = new GetBookDetailQuery(_context, _mapper);
              query.BookId = id;

              GetBookDetailQueryValidator validator = new GetBookDetailQueryValidator();
              //ValidationResult validateResult = validator.Validate(query);
              validator.ValidateAndThrow(query);

              result = query.Handle();
            }
            catch (Exception ex)
            {
                
                return BadRequest(ex.Message);
            }

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
        public IActionResult AddBook([FromBody] CreateBookModel newBook)
        {
            CreateBookCommand command = new CreateBookCommand(_context, _mapper); 

            try
            {
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
            }
            catch (Exception ex)
            {
                
                return BadRequest(ex.Message);
            }
         
        

          return Ok();
        }


        //put
        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] UpdateBookModel updatedBook)
        {
         try
         {
            UpdateBookCommand command = new UpdateBookCommand(_context);
            command.BookId = id;
            command.Model = updatedBook;

            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            //ValidationResult result = validator.Validate(command);
            validator.ValidateAndThrow(command);

            
            command.Handle();
         }
         catch (Exception ex)
         {
            
            return BadRequest(ex.Message);
         }

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            try
            {
                 DeleteBookCommand command = new DeleteBookCommand(_context);
                 command.BookId = id;
                 DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
                 validator.ValidateAndThrow(command);

                 command.Handle();
            }
            catch (Exception ex)
            {
                
                return BadRequest(ex.Message);
            }
  
          
            return Ok();
        }

    }
}