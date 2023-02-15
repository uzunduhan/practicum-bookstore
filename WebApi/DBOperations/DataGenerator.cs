using Microsoft.EntityFrameworkCore;
using WebApi.Entities;

namespace WebApi.DBOperations
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using(var context = new BookStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
            {
                if(context.Books.Any())
                {
                    return;
                }

                context.Genres.AddRange(
                    new Genre{
                        Name = "Personel Growth"
                    },
                    new Genre{
                        Name = "Science Fiction"
                    },
                    new Genre{
                        Name = "Romance"
                    }
                );

                context.Authors.AddRange(
                    new Author{
                        Name = "Ahmet",
                        SurName = "Uzun",
                        Birthday = new DateTime(1955, 03, 11)
                    },

                      new Author{
                        Name = "Naci",
                        SurName = "Seri",
                        Birthday = new DateTime(1980,09,02)
                    }

                );

                context.Books.AddRange(
                       new Book{
                        //ID = 1,
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

                context.SaveChanges();
            }
        }
    }
}