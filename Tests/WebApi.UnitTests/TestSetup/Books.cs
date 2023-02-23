
using WebApi.DBOperations;
using WebApi.Entities;

namespace TestSetup
{
    public static class Books
    {
        public static void AddBooks(this BookStoreDbContext context)
        {
             context.Books.AddRange(
                       //ID = 1,
                    new Book{
                     Title = "Lean Startup",
                     GenreId = 1, // personel growth
                     AuthorId = 1,
                     PageCount = 200,
                     PublishDate = new DateTime(2001,06,12)
                },
                     new Book{
                      //ID = 2,
                      Title = "Herland",
                      GenreId = 2, //science fiction
                      AuthorId = 2,
                      PageCount = 250,
                      PublishDate = new DateTime(2010,05,12)
                  },
                  new Book{
                      //ID = 3,
                      Title = "Dune",
                      GenreId = 2, //science fiction
                      AuthorId = 2,
                      PageCount = 540,
                      PublishDate = new DateTime(2010,05,12)
                  }
                );

        }
    }
}