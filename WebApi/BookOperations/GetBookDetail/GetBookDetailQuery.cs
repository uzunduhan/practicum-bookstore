using AutoMapper;
using WebApi.Common;
using WebApi.DBOperations;

namespace WebApi.BookOperations.GetBookDetail
{
    public class  GetBookDetailQuery
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public int BookId{get; set;}
        public GetBookDetailQuery(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public BookDetailViewModel Handle()
        {
            var book = _context.Books.Where(book=>book.ID == BookId).SingleOrDefault();
            
            if(book is null)
            {
                throw new InvalidOperationException("kitap bulunamadı");
            }

            BookDetailViewModel vm = _mapper.Map<BookDetailViewModel>(book); //new BookDetailViewModel();
            // vm.Title = book.Title;
            // vm.PageCount = book.PageCount;
            // vm.PublishDate = book.PublishDate.Date.ToString("dd/MM/yyyy");
            // vm.Genre = ((GenreEnum)book.GenreId).ToString();

            return vm;
        }

    }

    public class BookDetailViewModel
    {
        public string Title { get; set; }
        public string Genre { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }
    }
    
}