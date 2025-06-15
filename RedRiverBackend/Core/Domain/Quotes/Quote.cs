namespace RedRiverApp.Core.Domain.Quotes
{
    public class Quote(Guid Id, string Text, string Author)
    {
        private readonly Guid Id = Id;
        private string Text = Text;
        private string Author = Author;

        public void Update(UpdateQuoteRequest updateQuote)
        {
            Text = updateQuote.Text;
            Author = updateQuote.Author;
        }

        public Guid GetId()
        {
            return Id;
        }

        public string GetText()
        {
            return Text;
        }

        public string GetAuthor()
        {
            return Author;
        }

        public virtual bool Equals(Quote? other)
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
