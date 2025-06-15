using RedRiverApp.Core.Domain.Books;
using Microsoft.AspNetCore.Mvc;
using RedRiverApp.WebApi.Converter;
using Microsoft.AspNetCore.Authorization;

namespace RedRiverApp.WebApi.Controller
{
    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly BookService service;
        private readonly BookConverter converter;

        public BookController(BookService service, BookConverter converter)
        {
            this.service = service;
            this.converter = converter;
        }

        [HttpGet("{id:guid}")]
        public ActionResult<BookResponse> Get(Guid id)
        {
            var book = service.Get(id);
            BookResponse response = converter.ConvertToResponse(book);
            return Ok(response);
        }

        [HttpGet("all")]
        public ActionResult<List<BookResponse>> All()
        {
            return service.GetAll().Select(converter.ConvertToResponse).ToList();
        }

        [HttpPost]
        public ActionResult<BookResponse> Save([FromBody] NewBookRequest newBook)
        {
            var book = service.Save(newBook);
            var response = converter.ConvertToResponse(book);
            return Ok(response);
        }

        [HttpPut("{id:guid}")]
        public ActionResult<BookResponse> Update(Guid id, [FromBody] UpdateBookRequest updateBook)
        {
            var book = service.Update(id, updateBook);
            BookResponse response = converter.ConvertToResponse(book);
            return Ok(response);
        }

        [HttpDelete("{id:guid}")]
        public ActionResult<List<BookResponse>> Delete(Guid id)
        {
            service.Delete(id);
            return All();
        }
    }
}