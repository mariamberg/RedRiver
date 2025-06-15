using RedRiverApp.Core.Domain.Books;

namespace RedRiverApp.WebApi.Converter
{
    public class BookConverter
    {
        public BookResponse ConvertToResponse(Book book)
        {
            return new BookResponse(book.GetId(), book.GetTitle(), book.GetAuthor(), book.GetYear());
        }
    }
}