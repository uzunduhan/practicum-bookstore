using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.Application.WebApi.BookOperations.Queries.GetBooks;
using WebApi.DBOperations;

namespace WebApi.Application.WebApi.AuthorOperations.Queries.GetAuthorDetail
{
    public class  GetAuthorDetailQuery
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public int AuthorId{get; set;}
        public GetAuthorDetailQuery(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public AuthorDetailViewModel Handle()
        {
            var author = _context.Authors.Include(x=>x.Books).ThenInclude(a=>a.Genre).Where(author=>author.Id == AuthorId).SingleOrDefault();
            
            if(author is null)
            {
                throw new InvalidOperationException("yazar bulunamadı");
            }

            AuthorDetailViewModel vm = _mapper.Map<AuthorDetailViewModel>(author);

            return vm;
        }

    }

    public class AuthorDetailViewModel
    {
        public string Name { get; set; }
        public string SurName { get; set; }
        public string BirthDay { get; set; }
        public List<BooksViewModel> Books {get; set; }


    }
    
}