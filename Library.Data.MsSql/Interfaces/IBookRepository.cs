namespace Library.Data.MsSql.Interfaces
{
    using Library.Domain;
    using System.Collections.Generic;

    public interface IBookRepository
    {
        void Add(Book newBook);

        void Update(Book updatedBook);

        void Remove(int id);

        Book GetBookById(int id);

        List<Book> GetAllBooks();

        List<Author> GetAuthorsByBookId(int id);

        void AddAuthorToBook(int id, int authorId);

        void TakeBookByReader(int id, int ReaderId);

        List<string> GetBookHistory(int bookId);
    }
}
