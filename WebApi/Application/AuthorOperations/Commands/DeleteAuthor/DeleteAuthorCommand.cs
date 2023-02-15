using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;

namespace WebApi.Application.WebApi.AuthorOperations.Commands.DeleteAuthor{
    public class  DeleteAuthorCommand
    {
        private readonly BookStoreDbContext _context;
        public int AuthorId { get; set; }
        public DeleteAuthorCommand(BookStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var author = _context.Authors.Include(x=>x.Books).SingleOrDefault(x=>x.Id == AuthorId);
            if(author is null)
            {
                throw new InvalidOperationException("kitap bulunamadı");

            }

            _context.Authors.Remove(author);
            _context.SaveChanges();
        }
    }
}