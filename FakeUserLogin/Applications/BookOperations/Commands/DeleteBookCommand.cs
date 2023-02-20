using FakeUserLogin.DbOperations;
using FakeUserLogin.Models;

namespace FakeUserLogin.Applications.BookOperations.Commands
{
    public class DeleteBookCommand
    {
        public int BookId { get; set; }


        private readonly BookStoreDbContext _dbContext;

        public DeleteBookCommand(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            Book? book = _dbContext.Books.SingleOrDefault(x => x.Id == BookId);

            if (book is null)
                throw new InvalidOperationException("Kitap bulunamadı");

            _dbContext.Books.Remove(book);
            _dbContext.SaveChanges();

        }
    }
}
