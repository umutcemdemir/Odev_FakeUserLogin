using FakeUserLogin.DbOperations;
using FakeUserLogin.Models;
using FakeUserLogin.ViewModels.BookViewModels;

namespace FakeUserLogin.Applications.BookOperations.Commands.UpdateBook
{
    public class UpdateBookWithPutCommand
    {
        public UpdateBookWithPutViewModel Model { get; set; }
        public int BookId { get; set; }


        private readonly BookStoreDbContext _dbCcontext;

        public UpdateBookWithPutCommand(BookStoreDbContext dbContext)
        {
            _dbCcontext = dbContext;
        }

        public void Handle()
        {
            Book? book = _dbCcontext.Books.SingleOrDefault(x => x.Id == BookId);


            if (book is null)
                throw new InvalidOperationException("Kitap bulunamadı");

            book.Title = Model.Title != default ? Model.Title : book.Title;
            book.GenreId = Model.GenreId != default ? Model.GenreId : book.GenreId;
            book.PageCount = Model.PageCount != default ? Model.PageCount : book.PageCount;
            book.PublishDate = Model.PublishDate != default ? Model.PublishDate.Date : book.PublishDate.Date;

            _dbCcontext.SaveChanges();

        }
    }
}
