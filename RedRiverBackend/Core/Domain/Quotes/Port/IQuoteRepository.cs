namespace RedRiverApp.Core.Domain.Quotes.Port
{
    public interface IQuoteRepository
    {
        List<Quote> GetAll();

        Quote Get(Guid id);

        Quote Save(Quote quote);

        Quote Update(Quote quote);

        void Delete(Guid id);
    }
}
