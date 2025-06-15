
using System.Collections.Concurrent;
using RedRiverApp.Core.Domain.Books;
using RedRiverApp.Core.Domain.Books.Port;

namespace RedRiverApp.Infrastructure.Persistence.Books
{
    public class BookInMemoryRepository : IBookRepository
    {
        private readonly ConcurrentDictionary<Guid, Book> repository;

        public BookInMemoryRepository()
        {
            repository = new ConcurrentDictionary<Guid, Book>();
        }


        public Book Save(Book book)
        {
            repository.TryAdd(book.GetId(), book);
            return book;
        }

        public List<Book> GetAll()
        {
            return repository.Values.ToList();
        }
        public Book Get(Guid id)
        {
            if (repository.TryGetValue(id, out Book? value))
            {
                return value;
            }

            throw new KeyNotFoundException($"Book med id '{id}' finns inte");
        }
        public Book Update(Book book)
        {
            // Säkerställer att book med id finns innan uppdatering.
            Get(book.GetId());
            repository[book.GetId()] = book;
            return book;
        }

        public void Delete(Guid id)
        {
            repository.Remove(id, out _);
        }
    }
}