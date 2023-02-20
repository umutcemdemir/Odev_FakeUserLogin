using System.ComponentModel.DataAnnotations;

namespace FakeUserLogin.ViewModels.BookViewModels
{
    public class CreateBookViewModel
    {
        public string Title { get; set; }
        public int GenreId { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }
    }
}
