using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using RedRiverApp.Core.Domain.Quotes;
using RedRiverApp.WebApi.Converter;

namespace RedRiverApp.WebApi.Controller
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class QuoteController : ControllerBase
    {
        private readonly QuoteService service;
        private readonly QuoteConverter converter;

        public QuoteController(QuoteService service, QuoteConverter converter)
        {
            this.service = service;
            this.converter = converter;
        }

        [HttpGet("{id:guid}")]
        public ActionResult<QuoteResponse> Get(Guid id)
        {
            var quote = service.GetQuote(id);
            QuoteResponse response = converter.ConvertToResponse(quote);
            return Ok(response);
        }

        [HttpGet("all")]
        public ActionResult<List<QuoteResponse>> All()
        {
            return service.GetAll().Select(converter.ConvertToResponse).ToList();
        }

        [HttpPost]
        public ActionResult<QuoteResponse> Save([FromBody] NewQuoteRequest newQuote)
        {
            var quote = service.Save(newQuote);
            var response = converter.ConvertToResponse(quote);
            return Ok(response);
        }

        [HttpPut("{id:guid}")]
        public ActionResult<QuoteResponse> Update(Guid id, [FromBody] UpdateQuoteRequest updateQuote)
        {
            var quote = service.Update(id, updateQuote);
            QuoteResponse response = converter.ConvertToResponse(quote);
            return Ok(response);
        }

        [HttpDelete("{id:guid}")]
        public ActionResult<List<QuoteResponse>> Delete(Guid id)
        {
            service.Delete(id);
            return All();
        }

    }
}