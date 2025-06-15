using RedRiverApp.Core.Domain.Quotes;

namespace RedRiverApp.WebApi.Converter
{
    public class QuoteConverter
    {
        public QuoteResponse ConvertToResponse(Quote quote)
        {
            return new QuoteResponse(quote.GetId(), quote.GetText(), quote.GetAuthor());
        }
    }
}