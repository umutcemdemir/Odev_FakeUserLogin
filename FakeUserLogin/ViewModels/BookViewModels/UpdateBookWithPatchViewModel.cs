namespace FakeUserLogin.ViewModels.BookViewModels
{
    public class UpdateBookWithPatchViewModel
    {
        public string Title { get; set; }
        public int GenreId { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }
    }
}
