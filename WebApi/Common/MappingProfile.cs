using AutoMapper;
using WebApi.Application.GenreOperations.Queries.GetGenreDetail;
using WebApi.Application.GenreOperations.Queries.GetGenres;
using WebApi.Application.WebApi.AuthorOperations.Queries.GetAuthorDetail;
using WebApi.Application.WebApi.AuthorOperations.Queries.GetAuthors;
using WebApi.Application.WebApi.BookOperations.Queries.GetBookDetail;
using WebApi.Application.WebApi.BookOperations.Queries.GetBooks;
using WebApi.Entities;

namespace WebApi.Common
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<Application.WebApi.BookOperations.Commands.CreateBook.CreateBookCommand.CreateBookModel, Book>();
            CreateMap<Book, BookDetailViewModel>().ForMember(destination => destination.Genre, opt=>opt.MapFrom(src=> src.Genre.Name)).
            ForMember(destination=>destination.Author, opt=>opt.MapFrom(src=>src.Author.Name + " " + src.Author.SurName));
           
            CreateMap<Book, BooksViewModel>().ForMember(destination => destination.Genre, opt=>opt.MapFrom(src=> src.Genre.Name)).
            ForMember(destination=>destination.Author, opt=>opt.MapFrom(src=>src.Author.Name + " " + src.Author.SurName));

            CreateMap<Genre, GenresViewModel>();
            CreateMap<Genre, GenreDetailViewModel>();

            CreateMap<Application.WebApi.AuthorOperations.Commands.CreateAuthor.CreateAuthorCommand.CreateAuthorModel, Author>();
            CreateMap<Author, AuthorsViewModel>().ForMember(destination=>destination.Books, opt=>opt.MapFrom(src=>src.Books));
            CreateMap<Author, AuthorDetailViewModel>().ForMember(destination=>destination.Books, opt=>opt.MapFrom(src=>src.Books));
        }
    }
}