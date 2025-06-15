using System.Collections.Concurrent;

using RedRiverApp.Core.Domain.Quotes;

using RedRiverApp.Core.Domain.Quotes.Port;

namespace RedRiverApp.Infrastructure.Persistence.Quotes
{
    public class QuoteInMemoryRepository : IQuoteRepository
    {
        private readonly ConcurrentDictionary<Guid, Quote> repository;

        public QuoteInMemoryRepository()
        {
            repository = new ConcurrentDictionary<Guid, Quote>();
        }

        public List<Quote> GetAll()
        {
            return repository.Values.ToList();
        }

        public Quote Get(Guid id)
        {
            if (repository.TryGetValue(id, out Quote? value))
            {
                return value;
            }

            throw new KeyNotFoundException($"Citat med id '{id}' finns inte");
        }


        public Quote Save(Quote quote)
        {
            repository.TryAdd(quote.GetId(), quote);
            return quote;
        }

        public void Delete(Guid id)
        {
            repository.Remove(id, out _);
        }

        public Quote Update(Quote quote)
        {
            // Säkerställer att quote med id finns innan uppdatering.
            Get(quote.GetId());
            repository[quote.GetId()] = quote;
            return quote;
        }
    }
}