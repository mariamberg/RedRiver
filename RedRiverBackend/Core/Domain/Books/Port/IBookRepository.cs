namespace RedRiverApp.Core.Domain.Books.Port
{
    public interface IBookRepository
    {
        List<Book> GetAll();
        Book Save(Book book);

        Book Get(Guid id);

        Book Update(Book book);

        void Delete(Guid id);
    }
}