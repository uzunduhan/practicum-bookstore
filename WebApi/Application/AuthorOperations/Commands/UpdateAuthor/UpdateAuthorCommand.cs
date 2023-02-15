using WebApi.DBOperations;

namespace WebApi.Application.WebApi.AuthorOperations.Commands.UpdateAuthor
{
    public class  UpdateAuthorCommand
    {
        private readonly BookStoreDbContext _context;
        public int AuthorId { get; set; }
        public UpdateAuthorModel Model {get; set;}
        public UpdateAuthorCommand(BookStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var author = _context.Authors.SingleOrDefault(x=>x.Id == AuthorId);
            if(author is null)
            {
                throw new InvalidOperationException("kitap bulunamadı");
            }

            author.Name = Model.Name != default ? Model.Name: author.Name;
            author.SurName = Model.SurName != default ? Model.SurName: author.SurName;

            _context.SaveChanges();
        }

   
    }

        public class UpdateAuthorModel
        {
            public string Name { get; set; }
            public string SurName { get; set; }
        }
}