using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.Common;
using WebApi.DBOperations;

namespace TestSetup
{
    public class CommonTestFixture
    {
        public BookStoreDbContext context {get; set;}
        public IMapper Mapper {get; set;}
        public CommonTestFixture()
        {
            var options = new DbContextOptionsBuilder<BookStoreDbContext>().UseInMemoryDatabase(databaseName: "BookStoreTestDb").Options;
            context = new BookStoreDbContext(options);
            context.Database.EnsureCreated();
            context.AddBooks();
            context.AddGenres();
            context.SaveChanges();

            Mapper = new MapperConfiguration(cfg=>{cfg.AddProfile<MappingProfile>(); }).CreateMapper();
        }
    }

}