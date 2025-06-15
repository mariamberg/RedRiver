using RedRiverApp.Core.Domain.Quotes.Port;

namespace RedRiverApp.Core.Domain.Quotes
{
    public class QuoteService
    {
        private readonly IQuoteRepository quoteRepository;

        public QuoteService(IQuoteRepository quoteRepository)
        {
            this.quoteRepository = quoteRepository;
        }

        public List<Quote> GetAll()
        {
            return [.. quoteRepository.GetAll().OrderBy(quote => quote.GetText())];
        }

        public Quote Save(NewQuoteRequest newQuote)
        {
            Guid id = Guid.NewGuid();
            Quote quote = new(id, newQuote.Text, newQuote.Author);
            return quoteRepository.Save(quote);
        }

        public Quote GetQuote(Guid id)
        {
            return quoteRepository.Get(id);
        }

        public Quote Update(Guid id, UpdateQuoteRequest updatedQuote)
        {
            Quote quote = quoteRepository.Get(id);
            quote.Update(updatedQuote);
            return quoteRepository.Update(quote);
        }

        public void Delete(Guid id)
        {
            quoteRepository.Delete(id);
        }

    }
}