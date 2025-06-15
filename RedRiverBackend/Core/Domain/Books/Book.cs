using System.Reflection.Metadata;

namespace RedRiverApp.Core.Domain.Books
{
    //public record Book(Guid Id, string Title, string Author, int Year);
    public class Book(Guid Id, string Title, string Author, int Year)
    {
        private readonly Guid Id = Id;
        private string Title = Title;
        private string Author = Author;
        private int Year = Year;

        public void Update(UpdateBookRequest updateBook)
        {
            Title = updateBook.Title;
            Author = updateBook.Author;
            Year = updateBook.Year;
        }

        public Guid GetId()
        {
            return Id;
        }

        public string GetTitle()
        {
            return Title;
        }

        public string GetAuthor()
        {
            return Author;
        }
        public int GetYear()
        {
            return Year;
        }
        public virtual bool Equals(Book? other)
        {
            if (other is null) return false;

            return Id == other.Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }

}
