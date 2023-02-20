using AutoMapper;
using FakeUserLogin.DbOperations;
using FakeUserLogin.Models;
using FakeUserLogin.ViewModels.BookViewModels;

namespace FakeUserLogin.Applications.BookOperations.Queries
{
    public class GetBookByIdQuery
    {
        public int BookId { get; set; }


        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetBookByIdQuery(BookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public BookDetailViewModel Handle()
        {
            Book? book = _dbContext.Books.SingleOrDefault(x => x.Id == BookId);

            if (book is null)
                throw new InvalidOperationException("Kitap bulunamadı");



            BookDetailViewModel bookDetailViewModel = _mapper.Map<BookDetailViewModel>(book);


            return bookDetailViewModel;
        }
    }
}
