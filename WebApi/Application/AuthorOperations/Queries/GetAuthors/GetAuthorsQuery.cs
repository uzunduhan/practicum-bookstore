using AutoMapper;
using WebApi.Entities;
using WebApi.DBOperations;
using WebApi.Application.WebApi.BookOperations.Queries.GetBooks;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Application.WebApi.AuthorOperations.Queries.GetAuthors
{
    public class  GetAuthorsQuery{

        private readonly BookStoreDbContext _dbcontext;
        private readonly IMapper _mapper;
        public GetAuthorsQuery(BookStoreDbContext dbContext, IMapper mapper)
        {
            _dbcontext = dbContext;
            _mapper = mapper;
        }

        public List<AuthorsViewModel> Handle()
        {
            var authorList = _dbcontext.Authors.Include(x=>x.Books).ThenInclude(a=>a.Genre).OrderBy(x=>x.Id).ToList<Author>();
            List<AuthorsViewModel> vm = _mapper.Map<List<AuthorsViewModel>>(authorList);//new List<BooksViewModel>();

            return vm;
        }
    }

    public class AuthorsViewModel
    {
        public string Name { get; set; }
        public string SurName { get; set; }
        public string BirthDay{get; set;}
        public List<BooksViewModel> Books {get; set; }
    }
}