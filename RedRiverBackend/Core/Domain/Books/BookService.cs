using RedRiverApp.Core.Domain.Books.Port;

namespace RedRiverApp.Core.Domain.Books
{
    public class BookService
    {
        private readonly IBookRepository bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            this.bookRepository = bookRepository;
        }

        public List<Book> GetAll()
        {
            return [.. bookRepository.GetAll().OrderBy(book => book.GetTitle())];
        }
        
        public Book Save(NewBookRequest newBook)
        {
            Guid id = Guid.NewGuid();
            Book book = new(id, newBook.Title, newBook.Author, newBook.Year);
            return bookRepository.Save(book);
        }

        public Book Get(Guid id)
        {
            return bookRepository.Get(id);
        }

        public Book Update(Guid id, UpdateBookRequest updatedBook)
        {
            Book book = bookRepository.Get(id);
            book.Update(updatedBook);
            return bookRepository.Update(book);
        }

        public void Delete(Guid id)
        {
            bookRepository.Delete(id);
        }
    }
}
