namespace Library.Service.Interfaces
{
    using Library.Domain;
    using System.Collections.Generic;

    public interface IBookService
    {
        void Create(Book newBook);

        void Edit(Book updatedBook);

        void Delete(int id);

        Book GetBookById(int id);

        List<Book> GetAllBooks();

        List<Author> GetAuthorsByBookId(int id);

        void AddAuthorToBook(int id, int authorId);

        void TakeBookByReader(int id, int readerId);

        List<string> GetBookHistory(int bookId);
    }
}
