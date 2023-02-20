using FakeUserLogin.DbOperations;
using FakeUserLogin.Enums;
using FakeUserLogin.Models;
using FakeUserLogin.ViewModels.BookViewModels;
using Microsoft.EntityFrameworkCore;

namespace FakeUserLogin.Applications.BookOperations.Queries
{
    public class GetBooksByFilterQuery
    {
        public string? BookTitle { get; set; }
        public string? BookGenre { get; set; }
        public string? BookPageCount { get; set; }
        public string? BookPublishYear { get; set; }

        private readonly BookStoreDbContext _dbContext;

        public GetBooksByFilterQuery(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<GetBooksByFilterViewModel> Handle()
        {
            List<Book> books = _dbContext.Books.OrderBy(x => x.Id).ToList();

            List<GetBooksByFilterViewModel> getBooksByFilterViewModel = new List<GetBooksByFilterViewModel>();

            foreach (Book book in books)
            {
                getBooksByFilterViewModel.Add(new GetBooksByFilterViewModel
                {
                    Title = book.Title,
                    Genre = ((GenreEnum)book.GenreId).ToString(),
                    PageCount = book.PageCount,
                    PublishDate = book.PublishDate.Date.ToString("yyyy")
                });
            }

            if (BookTitle is not null)
            {
                getBooksByFilterViewModel = getBooksByFilterViewModel.
                    Where(x => x.Title.ToLower().Contains(BookTitle.ToLower())).ToList();
            }
            if (BookGenre is not null)
            {
                getBooksByFilterViewModel = getBooksByFilterViewModel.
                    Where(x => x.Genre.ToLower().Contains(BookGenre.ToLower())).ToList();
            }
            if (BookPageCount is not null)
            {
                getBooksByFilterViewModel = getBooksByFilterViewModel.
                    Where(x => x.PageCount.ToString().ToLower().Contains(BookPageCount.ToLower())).ToList();
            }
            if (BookPublishYear is not null)
            {
                getBooksByFilterViewModel = getBooksByFilterViewModel.
                    Where(x => x.PublishDate.ToLower().Equals(BookPublishYear.ToLower())).ToList();
            }


            return getBooksByFilterViewModel;
        }
    }
}
