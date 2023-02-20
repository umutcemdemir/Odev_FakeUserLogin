
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
    public class BookQueryController : Controller
    {

        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public BookQueryController(BookStoreDbContext context, IMapper mapper)
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

        [HttpGet]
        [Route("GetById")]
        public IActionResult GetBook([FromQuery] string id)
        {
            BookDetailViewModel result;

            try
            {
                GetBookByIdQuery query = new(_context, _mapper);
                query.BookId = Convert.ToInt16(id);
                GetBookByIdQueryValidator validator = new();
                validator.ValidateAndThrow(query);

                result = query.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(result);

        }

        [HttpPut]
        public IActionResult UpdateUserWithPut([FromBody] UpdateBookWithPutViewModel updateBook,
            [FromQuery] string id)
        {
            try
            {
                UpdateBookWithPutCommand command = new(_context);
                command.BookId = Convert.ToInt16(id);
                command.Model = updateBook;
                UpdateBookWithPutCommandValidator validator = new();
                validator.ValidateAndThrow(command);

                command.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok("Kitap güncellenmiştir");
        }

        [HttpPatch]
        public IActionResult UpdateUserWithPatch([FromBody] UpdateBookWithPatchViewModel updateBook,
            [FromQuery] string id)
        {
            try
            {
                UpdateBookWithPatchCommand command = new(_context);
                command.BookId = Convert.ToInt16(id);
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

        [HttpDelete]
        public IActionResult Delete([FromQuery] string id)
        {
            try
            {
                DeleteBookCommand command = new(_context);
                command.BookId = Convert.ToInt16(id);
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
