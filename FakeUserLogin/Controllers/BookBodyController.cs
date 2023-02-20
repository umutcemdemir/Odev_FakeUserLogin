using AutoMapper;
using FakeUserLogin.Applications.BookOperations.Commands;
using FakeUserLogin.Applications.BookOperations.Commands.UpdateBook;
using FakeUserLogin.Applications.BookOperations.Queries;
using FakeUserLogin.DbOperations;
using FakeUserLogin.Validations.BookValidations;
using FakeUserLogin.ViewModels.BookViewModels;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FakeUserLogin.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[Controller]")]
    public class BookBodyController : Controller
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public BookBodyController(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetBooks()
        {
            GetBooksQuery query = new(_context, _mapper);
            List<BooksViewModel> result = query.Handle();

            if (result is null)
                return NotFound("Liste bulunamadı");

            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetBook(int id)
        {
            BookDetailViewModel result;

            try
            {
                GetBookByIdQuery query = new(_context, _mapper);
                query.BookId = id;
                GetBookByIdQueryValidator valiator = new();
                valiator.ValidateAndThrow(query);

                result = query.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(result);
        }


        [HttpGet]
        [Route("GetByFilter")]
        public IActionResult GetByFilter([FromQuery] string? title = null, [FromQuery] string? genre = null,
            [FromQuery] string? pageCount = null, [FromQuery] string? publishYear = null)
        {
            List<GetBooksByFilterViewModel> result;

            try
            {
                GetBooksByFilterQuery query = new(_context);
                query.BookTitle = title;
                query.BookGenre = genre;
                query.BookPageCount = pageCount;
                query.BookPublishYear = publishYear;
                result = query.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


            return Ok(result);
        }

        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookViewModel newBook)
        {
            CreateBookCommand command = new(_context, _mapper);

            try
            {
                command.Model = newBook;

                CreateBookCommandValidator validator = new();
                validator.ValidateAndThrow(command);

                command.Handle();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Created("~api/Book/GetBooks", newBook);

        }

        [HttpPut("{id}")]
        public IActionResult UpdateUserWithPut([FromBody] UpdateBookWithPutViewModel updateBook, int id)
        {
            try
            {
                UpdateBookWithPutCommand command = new(_context);
                command.BookId = id;
                command.Model = updateBook;
                UpdateBookWithPutCommandValidator validator = new();
                validator.ValidateAndThrow(command);

                command.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok("Kitap Güncellenmiştir");
        }

        [HttpPatch("{id}")]
        public IActionResult UpdateUserWithPatch([FromBody] UpdateBookWithPatchViewModel updateBook, int id)
        {
            try
            {
                UpdateBookWithPatchCommand command = new(_context);
                command.BookId = id;
                command.Model = updateBook;
                UpdateBookWithPatchCommandValidator validator = new();
                validator.ValidateAndThrow(command);

                command.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok("Kitap güncellenmiştir");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                DeleteBookCommand command = new(_context);
                command.BookId = id;
                DeleteBookCommandValidator validator = new();
                validator.ValidateAndThrow(command);

                command.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok("Kitap silinmiştir");
        }
    }
}
