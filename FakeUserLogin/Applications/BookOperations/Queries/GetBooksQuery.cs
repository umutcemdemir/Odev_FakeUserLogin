using AutoMapper;
using FakeUserLogin.DbOperations;
using FakeUserLogin.Models;
using FakeUserLogin.ViewModels.BookViewModels;

namespace FakeUserLogin.Applications.BookOperations.Queries
{
    public class GetBooksQuery
    {
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetBooksQuery(BookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public List<BooksViewModel> Handle()
        {
            List<Book> books = _dbContext.Books.OrderBy(x => x.Id).ToList();

            List<BooksViewModel> booksViewModel = _mapper.Map<List<BooksViewModel>>(books);


            return booksViewModel;
        }
    }
}
